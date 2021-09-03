using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using IntSoft.DAL.Models;
using IntSoft.DAL.RepositoriesBase;
using int_Soft.NetCore.Communication;
using Microsoft.AspNet.Identity;
using StructureMap.Attributes;

namespace intSoft.MVC.Core.Identity
{
    public abstract class UserManagerBase<TUser, TRole, TUserRole, TUserClaim, TUserLogin> : UserManager<TUser, Guid>
        where TUser : class, IUserBase<TUserRole, TUserClaim, TUserLogin>, IUser<Guid>
        where TUserLogin : class, IUserLoginBase
        where TUserClaim : class, IUserClaimBase
        where TRole : class, IRoleBase
        where TUserRole : class, IUserRoleBase
    {
        #region Constructors

        protected UserManagerBase(UserStoreBase<TUser, TRole, TUserRole, TUserClaim, TUserLogin> store)
            : base(store)
        {
        }

        #endregion

        #region Properties

        public ApplicationAccountConfiguration AccountConfiguration { get; set; }
        public IMessageService EmailMessageService { get; set; }
        public IMessageService SmsMessageService { get; set; }

        [SetterProperty]
        public IUserRepository<TUser, TUserRole, TUserClaim, TUserLogin> UserRepository { get; set; }

        #endregion

        #region UserManager Overrides

        public override async Task<IList<string>> GetRolesAsync(Guid userId)
        {
            var result = new List<string>(await UserRepository.GetRolesAsync(userId));
            return await Task.FromResult(result);
        }

        #endregion

        #region Opertions

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(TUser user)
        {
            var userIdentity = await CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public async Task<IList<string>> GetOperationsAsync(Guid userId)
        {
            var result = new List<string>(await UserRepository.GetOperationsAsync(userId));
            return await Task.FromResult(result);
        }

        public async Task<MessageServiceResult> SendEmailAsync(Message message)
        {
            return await EmailMessageService.SendAsync(message);
        }

        public async Task<MessageServiceResult> SendSmsAsync(Message message)
        {
            return await SmsMessageService.SendAsync(message);
        }

        public virtual async Task<IEnumerable<TUser>> GetAll(int pageNumber, int pageSize, Expression<Func<TUser, bool>> predicate = null)
        {
            return await Task.FromResult(UserRepository.GetAll(pageNumber, pageNumber, predicate));
        }

        public virtual async Task<TUser> Get(Guid id)
        {
            return await Task.FromResult(UserRepository.Get(id));
        }

        public virtual async Task<IEnumerable<TUser>> GetAll()
        {
            return await Task.FromResult(UserRepository.GetAll());
        }

        public virtual async Task<IdentityResult> ConfirmPhoneNumberAsync(TUser user)
        {
            try
            {
                var store = Store as IUserPhoneNumberStore<TUser, Guid>;
                if (store != null)
                    await store.SetPhoneNumberConfirmedAsync(user, true);
                
                await Store.UpdateAsync(user);
                return await Task.FromResult(IdentityResult.Success);
            }
            catch (Exception)
            {
                return IdentityResult.Failed();
            }
        }

        public virtual async Task<IdentityResult> SetEmailAndUsernameAsync(TUser user, string email)
        {
            try
            {
                var userEmailStore = Store as IUserEmailStore<TUser, Guid>;
                if (userEmailStore != null)
                    await userEmailStore.SetEmailAsync(user, email);
                user.UserName = email;
                user.EmailConfirmed = false;
                await Store.UpdateAsync(user);
                return await Task.FromResult(IdentityResult.Success);
            }
            catch (Exception)
            {
                return IdentityResult.Failed();
            }
        }

        #endregion
    }
}