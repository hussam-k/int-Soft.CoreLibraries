using System;
using System.Data.Entity.Utilities;
using System.Security.Claims;
using System.Threading.Tasks;
using IntSoft.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace intSoft.MVC.Core.Identity
{
    public abstract class UserSignInManagerBase<TUser, TRole, TUserRole, TUserClaim, TUserLogin> :
        SignInManager<TUser, Guid>
        where TUser : class, IUser<Guid>, IUserBase<TUserRole, TUserClaim, TUserLogin>
        where TUserLogin : class, IUserLoginBase
        where TUserClaim : class, IUserClaimBase
        where TRole : class, IRoleBase
        where TUserRole : class, IUserRoleBase
    {
        protected UserSignInManagerBase(UserManagerBase<TUser, TRole, TUserRole, TUserClaim, TUserLogin> userManager,
            IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public UserManagerBase<TUser, TRole, TUserRole, TUserClaim, TUserLogin> Manager
        {
            get { return UserManager as UserManagerBase<TUser, TRole, TUserRole, TUserClaim, TUserLogin>; }
        }

        public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent,
            bool shouldLockout)
        {
            if (UserManager == null)
            {
                return SignInStatus.Failure;
            }
            var user = await UserManager.FindByNameAsync(userName).WithCurrentCulture();
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
            {
                return SignInStatus.LockedOut;
            }
            if (Manager.AccountConfiguration.RequireEmailVerificationForLogin && ! await UserManager.IsEmailConfirmedAsync(user.Id).WithCurrentCulture())
            {
                return SignInStatus.RequiresVerification;
            }
            if (await UserManager.CheckPasswordAsync(user, password).WithCurrentCulture())
            {
                return await SignInOrTwoFactor(user, isPersistent).WithCurrentCulture();
            }
            if (shouldLockout)
            {
                // If lockout is requested, increment access failed count which might lock out the user
                await UserManager.AccessFailedAsync(user.Id).WithCurrentCulture();
                if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
                {
                    return SignInStatus.LockedOut;
                }
            }
            return SignInStatus.Failure;
        }

        private async Task<SignInStatus> SignInOrTwoFactor(TUser user, bool isPersistent)
        {
            var id = Convert.ToString(user.Id);
            if (await UserManager.GetTwoFactorEnabledAsync(user.Id).WithCurrentCulture()
                && (await UserManager.GetValidTwoFactorProvidersAsync(user.Id).WithCurrentCulture()).Count > 0
                && !await AuthenticationManager.TwoFactorBrowserRememberedAsync(id).WithCurrentCulture())
            {
                var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id));
                AuthenticationManager.SignIn(identity);
                return SignInStatus.RequiresVerification;
            }
            await SignInAsync(user, isPersistent, false).WithCurrentCulture();
            return SignInStatus.Success;
        }
    }
}