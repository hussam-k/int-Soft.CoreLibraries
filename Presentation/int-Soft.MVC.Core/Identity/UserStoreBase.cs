using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using IntSoft.DAL.Models;
using IntSoft.DAL.RepositoriesBase;
using Microsoft.AspNet.Identity;
using StructureMap.Attributes;

namespace intSoft.MVC.Core.Identity
{
    public abstract class UserStoreBase<TUser, TRole, TUserRole, TUserClaim, TUserLogin> :
        IUserPasswordStore<TUser, Guid>,
        IUserEmailStore<TUser, Guid>,
        IUserPhoneNumberStore<TUser, Guid>,
        IUserClaimStore<TUser, Guid>,
        IUserLoginStore<TUser, Guid>,
        IUserRoleStore<TUser, Guid>,
        IUserSecurityStampStore<TUser, Guid>,
        IUserLockoutStore<TUser, Guid>,
        IUserTwoFactorStore<TUser, Guid>
        where TUserClaim : class, IUserClaimBase
        where TUserLogin : class, IUserLoginBase
        where TRole : class, IRoleBase
        where TUserRole : class, IUserRoleBase
        where TUser : class, IUserBase<TUserRole, TUserClaim, TUserLogin>, IUser<Guid>
    {
        #region Abstract

        public abstract void Dispose();

        #endregion

        #region Properties

        [SetterProperty]
        public IUserRepository<TUser, TUserRole, TUserClaim, TUserLogin> UserRepository { get; set; }

        [SetterProperty]
        public IRepository<TUserClaim> UserClaimsRepository { get; set; }

        [SetterProperty]
        public IRepository<TRole> RoleRepository { get; set; }

        #endregion

        #region IUserStore

        public virtual async Task CreateAsync(TUser user)
        {
            await Task.Run(() => UserRepository.Save(user));
        }
        
        public virtual async Task UpdateAsync(TUser user)
        {
            await Task.Run(() => UserRepository.Save(user));
        }

        public async Task DeleteAsync(TUser user)
        {
            await Task.Run(() => UserRepository.Remove(user));
        }

        public async Task<TUser> FindByIdAsync(Guid userId)
        {
            return await Task.FromResult(UserRepository.Get(userId));
        }

        public async Task<TUser> FindByNameAsync(string userName)
        {
            UserRepository =
                DependencyResolver.Current.GetService<IUserRepository<TUser, TUserRole, TUserClaim, TUserLogin>>();
            return await Task.FromResult(
                UserRepository.FirstOrDefault(
                    u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)));
        }

        #endregion

        #region IUserClaimStore

        public virtual async Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            return await Task.FromResult(
                UserClaimsRepository.GetAll(uc => uc.UserId == user.Id)
                    .Select(c => new Claim(c.ClaimType, c.ClaimValue))
                    .ToList());
        }

        public virtual async Task AddClaimAsync(TUser user, Claim claim)
        {
            var model = DependencyResolver.Current.GetService<TUserClaim>();
            model.UserId = user.Id;
            model.ClaimType = claim.Type;
            model.ClaimValue = claim.Value;

            await Task.Run(() => UserClaimsRepository.Save(model));
        }

        public virtual async Task RemoveClaimAsync(TUser user, Claim claim)
        {
            var model = UserClaimsRepository.FirstOrDefault(
                uc => uc.UserId == user.Id && uc.ClaimType == claim.Type && uc.ClaimValue == claim.Value);

            if (model != null)
            {
                await Task.Run(() => UserClaimsRepository.Remove(model));
            }
        }

        #endregion

        #region IUserEmailStore

        public virtual async Task SetEmailAsync(TUser user, string email)
        {
            await Task.FromResult(user.Email = email);
        }

        public virtual async Task<string> GetEmailAsync(TUser user)
        {
            return await Task.FromResult(user.Email);
        }

        public virtual async Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            return await Task.FromResult(user.EmailConfirmed);
        }

        public virtual async Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            await Task.FromResult(user.EmailConfirmed = confirmed);
        }

        public virtual async Task<TUser> FindByEmailAsync(string email)
        {
            return await Task.FromResult(UserRepository.FirstOrDefault(u => u.Email.ToLower() == email.ToLower()));
        }

        #endregion

        #region IUserPhoneNumberStore
        
        public async Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            await Task.FromResult(user.PhoneNumber = phoneNumber);
        }

        public virtual async Task<string> GetPhoneNumberAsync(TUser user)
        {
            return await Task.FromResult(user.PhoneNumber);
        }

        public virtual async Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            return await Task.FromResult(user.PhoneNumberConfirmed);
        }

        public virtual async Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            await Task.FromResult(user.PhoneNumberConfirmed = confirmed);
        }

        #endregion

        #region IUserLoginStore

        public virtual async Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            await Task.Run(() =>
            {
                var model = UserRepository.Get(user.Id);
                var userLogin = DependencyResolver.Current.GetService<TUserLogin>();
                userLogin.LoginProvider = login.LoginProvider;
                userLogin.ProviderKey = login.ProviderKey;
                userLogin.UserId = model.Id;
                model.UserLogins.Add(userLogin);
            });
        }

        public virtual async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            await Task.Run(() =>
            {
                var userModel = UserRepository.Get(user.Id);
                var model =
                    user.UserLogins.FirstOrDefault(
                        x =>
                            x.UserId == user.Id && x.LoginProvider == login.LoginProvider &&
                            x.ProviderKey == login.ProviderKey);
                userModel.UserLogins.Add(model);
            });
        }

        public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            var userModel = UserRepository.Get(user.Id);
            return
                await
                    Task.FromResult(
                        userModel.UserLogins.Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey)).ToList());
        }

        public virtual async Task<TUser> FindAsync(UserLoginInfo login)
        {
            return await Task.FromResult(
                UserRepository.FirstOrDefault(
                    x =>
                        x.UserLogins.Any(
                            l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey)));
        }

        #endregion

        #region IUserPasswordStore

        public virtual async Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            await Task.FromResult(user.PasswordHash = passwordHash);
        }

        public virtual async Task<string> GetPasswordHashAsync(TUser user)
        {
            return await Task.FromResult(user.PasswordHash);
        }

        public virtual async Task<bool> HasPasswordAsync(TUser user)
        {
            return await Task.FromResult(string.IsNullOrEmpty(user.PasswordHash));
        }

        #endregion

        #region IUserRoleStore

        public Task AddToRoleAsync(TUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(TUser user)
        {
            return (await UserRepository.GetRolesAsync(user.Id)).ToList();
        }

        public async Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            return (await GetRolesAsync(user)).Contains(roleName);
        }

        #endregion

        #region IUserLockedOut

        public async Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            return  await 
                Task.FromResult(user.LockoutEndDateUtc.HasValue
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                    : new DateTimeOffset());
        }

        public async Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            await Task.FromResult(user.LockoutEndDateUtc = lockoutEnd.UtcDateTime);
        }

        public async Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            user.AccessFailedCount++;
            return await Task.FromResult(user.AccessFailedCount);
        }

        public async Task ResetAccessFailedCountAsync(TUser user)
        {
            await Task.FromResult(user.AccessFailedCount = 0);
        }

        public async Task<int> GetAccessFailedCountAsync(TUser user)
        {
            return await Task.FromResult(user.AccessFailedCount);
        }

        public async Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            return await Task.FromResult(user.LockoutEnabled);
        }

        public async Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            await Task.FromResult(user.LockoutEnabled = enabled);
        }

        #endregion

        #region IUserSecurityStampStore

        public async Task SetSecurityStampAsync(TUser user, string stamp)
        {
            await Task.FromResult(user.SecurityStamp = stamp);
        }

        public async Task<string> GetSecurityStampAsync(TUser user)
        {
            return await Task.FromResult(user.SecurityStamp);
        }

        #endregion

        #region IUserTwoFactorStore
        
        public virtual async Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            await Task.FromResult(user.TwoFactorEnabled = enabled);
        }

        public virtual async Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            return await Task.FromResult(user.TwoFactorEnabled);
        }
        
        #endregion

    }
}