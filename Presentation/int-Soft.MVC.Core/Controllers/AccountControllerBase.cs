using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using intSoft.MVC.Core.Common;
using intSoft.MVC.Core.Filters;
using intSoft.MVC.Core.Identity;
using intSoft.MVC.Core.Identity.Models;
using intSoft.MVC.Core.ModelWrappersBase;
using intSoft.MVC.Core.Security;
using intSoft.MVC.Core.Utilities;
using intSoft.Res.DisplayNames;
using intSoft.Res.Messages;
using IntSoft.DAL.Common;
using IntSoft.DAL.Models;
using IntSoft.DAL.RepositoriesBase;
using int_Soft.NetCore.Communication;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StructureMap.Attributes;
using Sets = MlkPwgen.Sets;

namespace intSoft.MVC.Core.Controllers
{
    public abstract class AccountControllerBase<TUser, TUserManager, TUserSignInManager, TRole, TUserRole, TUserClaim,
        TUserLogin> : ControllerBase
        where TUser : class, IUserBase<TUserRole, TUserClaim, TUserLogin>, IUser<Guid>, new()
        where TUserManager : UserManagerBase<TUser, TRole, TUserRole, TUserClaim, TUserLogin>
        where TUserSignInManager : UserSignInManagerBase<TUser, TRole, TUserRole, TUserClaim, TUserLogin>
        where TUserLogin : class, IUserLoginBase
        where TUserClaim : class, IUserClaimBase
        where TRole : class, IRoleBase
        where TUserRole : class, IUserRoleBase, IEntity
    {
        #region Helpers

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        protected virtual async Task SendActivationEmailAsync(TUser user)
        {
            var code = UserManager.GenerateEmailConfirmationToken(user.Id);
            var callbackUrl = Url.Action("ConfirmEmail", "Accounts", new { userId = user.Id, code },
                Request.Url.Scheme);
            await UserManager.SendEmailAsync(new Message
            {
                Subject = Messages.ActivationEmailTitle,
                Destination = user.Email,
                Body = string.Format(Messages.ActivationEmailBody, callbackUrl),
                From = "Account@Mentoria.me",
                FromDisplayText = "Mentoria"
            });
        }

        #endregion

        #region Properties

        private TUserManager _userManager;
        private TUserSignInManager _signInManager;

        [SetterProperty]
        public ICurrentUser<TUser> CurrentUser { get; set; }

        public virtual TUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<TUserManager>(); }
            private set { _userManager = value; }
        }


        public virtual TUserSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<TUserSignInManager>(); }
            set { _signInManager = value; }
        }

        public virtual IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        #endregion

        #region Actions

        #region Login

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        [AllowAnonymous]
        [CustomValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Login(UserLoginModel userLogin)
        {
            try
            {
                if (!ModelState.IsValid) 
                    return JsonValidationError();

                var signInResult = await SignInManager.PasswordSignInAsync(userLogin.Username, userLogin.Password, false, true);

                switch (signInResult)
                {
                    case SignInStatus.Success:
                        var user = await UserManager.FindByNameAsync(userLogin.Username);
                        Session.Add(DefaultValuesBase.SessionUserKey, user);
                        Session.Add(DefaultValuesBase.HttpRequestCurrentLanguage, user.PreferredLanguage);
                        Session.Add(DefaultValuesBase.SessionUserRoleKey, await UserManager.GetRolesAsync(user.Id));
                        Session.Add(DefaultValuesBase.SessionUserOperationsKey, await UserManager.GetOperationsAsync(user.Id));
                        return JsonSuccess("Loged in :) ");
                    case SignInStatus.LockedOut:
                        return JsonError("Locked account");
                    case SignInStatus.RequiresVerification:
                        return JsonError("Needs verification");
                    case SignInStatus.Failure:
                        return JsonError(Messages.InvalidUserNameOrPassword);
                    default:
                        return JsonValidationError();
                }
            }
            catch (Exception)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return JsonError(Messages.GeneralError);
            }
        }

        [HttpGet]
        [CustomActionAuthorization(IsAlwaysAllowed = true)]
        public virtual ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }

        #endregion

        #region Register

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Register()
        {
            return PartialView();
        }

        [HttpPost]
        [AllowAnonymous]
        [CustomValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Register(UserWrapperBase<TUser> userLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = new IdentityResult();
                    var user = RegisterAccount(userLogin, result);
                    return result.Succeeded
                        ? await Task.FromResult(JsonSuccess(user))
                        : await Task.FromResult(JsonError(result.Errors.Aggregate("", (current, error) => current + error + ", ")));
                }

                return JsonValidationError();
            }
            catch (Exception)
            {
                return JsonError(Messages.GeneralError);
            }
        }

        protected virtual async Task<TUser> RegisterAccount(UserWrapperBase<TUser> userLogin, IdentityResult result)
        {
            var user = userLogin.GetModel();
            user.Email = userLogin.Email;
            user.UserName = userLogin.Email;
            result = await UserManager.CreateAsync(user, userLogin.Password);
            if (result.Succeeded)
            {
                if (UserManager.AccountConfiguration.SendActivationMailUponRegister)
                    await SendActivationEmailAsync(user);
                if (!UserManager.AccountConfiguration.RequireEmailVerificationForLogin)
                    await SignInManager.PasswordSignInAsync(user.UserName, userLogin.Password, true, true);
            }
            return user;
        }

        #endregion

        #region Reactivate

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Reactivate()
        {
            return PartialView();
        }

        [HttpPost]
        [CustomValidateAntiForgeryToken]
        [AllowAnonymous]
        public virtual async Task<ActionResult> Reactivate(string email)
        {
            if (string.IsNullOrEmpty(email)) return JsonValidationError();
            var user = await UserManager.FindByEmailAsync(email);
            if (user == null || user.EmailConfirmed)
                return JsonSuccess(Messages.ConfirmResetMessage);
            await SendActivationEmailAsync(user);
            return JsonSuccess(Messages.ConfirmResetMessage);
        }

        #endregion

        #region ConfirmEmail

        [AllowAnonymous]
        public virtual async Task<ActionResult> ConfirmEmail()
        {
            return await Task.FromResult(PartialView());
        }

        [AllowAnonymous]
        [HttpPost]
        [CustomValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ConfirmEmail(Guid userId, string code)
        {
            if (code == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return result.Succeeded
                ? JsonSuccess(Messages.EmailConfirmedSuccessfully)
                : JsonError(result.Errors.Aggregate("", (current, resultError) => current + resultError));
        }

        #endregion

        #region ChangeEmail
        
        [Authorize]
        public virtual ActionResult ChangeEmail()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize]
        [CustomValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ChangeEmail(string password, string email)
        {
            if (!ModelState.IsValid) return JsonValidationError();
            if (CurrentUser.User == null)
                return JsonError(Messages.GeneralError);
            if (!await UserManager.CheckPasswordAsync(CurrentUser.User, password))
                return JsonError(Messages.GeneralError);
            var result = await UserManager.SetEmailAndUsernameAsync(CurrentUser.User, email);
            if (!result.Succeeded) return JsonError(Messages.GeneralError);
            var user = await UserManager.FindByEmailAsync(email);
            await SendActivationEmailAsync(user);
            return JsonSuccess(Messages.EmailChangedSuccessfully);
        }
        
        #endregion

        #region ForgotPassword

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult ForgotPassword()
        {
            return PartialView();
        }

        [HttpPost]
        [CustomValidateAntiForgeryToken]
        [AllowAnonymous]
        public virtual async Task<ActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email)) return JsonValidationError();
            var user = await UserManager.FindByEmailAsync(email);
            if (user == null)
                return JsonSuccess(Messages.ConfirmResetMessage);
            var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            if (Request.Url == null) return JsonSuccess(Messages.ConfirmResetMessage);
            var callbackUrl = Url.Action("ResetPassword", "Accounts", new { userId = user.Id, code },
                Request.Url.Scheme);
            //await UserManager.SendEmailAsync(user.Id, "Reset Password",
            //    "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a><br/>" +
            //    " Username:" + user.UserName);
            var result = await UserManager.SendEmailAsync(new Message
            {
                Subject = "Password Reset",
                From = "Accounts@Mentoria.me",
                FromDisplayText = "Mentoria",
                Destination = user.Email,
                Body =
                    string.Format("Please reset your password by clicking <a href=\"{0}\">here</a><br/> Username: {1}",
                        callbackUrl, user.UserName)
            });
            return result.Succeeded
                ? JsonSuccess(Messages.ConfirmResetMessage)
                : JsonError(result.ErrorMessage);
        }

        #endregion

        #region ResetPassword

        [AllowAnonymous]
        public virtual ActionResult ResetPassword()
        {
            return PartialView();
        }

        [HttpPost]
        [AllowAnonymous]
        [CustomValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ResetPassword(Guid id, string code, string password)
        {
            if (!ModelState.IsValid) return JsonValidationError();
            var user = await UserManager.FindByIdAsync(id);
            if (user == null) return JsonError(Messages.GeneralError);
            if (!(await UserManager.PasswordValidator.ValidateAsync(password)).Succeeded)
                return JsonError(DisplayNames.InvalidPassword);
            var result = await UserManager.ResetPasswordAsync(user.Id, code, password);
            return result.Succeeded
                ? JsonSuccess(Messages.PasswordChangedSuccessfully)
                : JsonError(Messages.GeneralError);
        }

        #endregion

        #region ChangePassword

        [Authorize]
        public virtual ActionResult ChangePassword()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize]
        [CustomValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ChangePassword(string currentPassword, string password)
        {
            if (!ModelState.IsValid) return JsonValidationError();
            if (CurrentUser.User == null)
                return JsonError(Messages.GeneralError);
            var result = await UserManager.ChangePasswordAsync(CurrentUser.User.Id, currentPassword, password);
            return result.Succeeded
                ? JsonSuccess(Messages.PasswordChangedSuccessfully)
                : JsonError(Messages.GeneralError);
        }

        #endregion

        #region PhoneNumberVerification

        public virtual ActionResult PhoneVerification()
        {
            return PartialView();
        }

        [HttpPost]
        [AllowAnonymous]
        [CustomValidateAntiForgeryToken]
        public virtual async Task<ActionResult> SendPhoneVerification(string phoneNumber)
        {
            try
            {
                if (!ModelState.IsValid) return JsonValidationError();
                if (!Regex.IsMatch(phoneNumber, CommonRegularExpression.InternationalEgyptianMobileNumber))
                    return JsonError(DisplayNames.InvalidPhoneNumber);
                var code = await SendPhoneVerificationCode(phoneNumber);
                return HttpContext.IsDebuggingEnabled
                    ? (ActionResult) JsonSuccess(code)
                    : new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return JsonError(Messages.GeneralError);
            }
        }

        protected virtual async Task<string> SendPhoneVerificationCode(string phoneNumber)
        {
            if (Session[DefaultValuesBase.SessionPhoneNumber] != null && Session[DefaultValuesBase.SessionPhoneVerificationCode] != null
                && Session[DefaultValuesBase.SessionPhoneNumber].ToString() == phoneNumber)
            {
                var lastSendDate = new DateTime((long)Session[DefaultValuesBase.SessionPhoneVerificationDate]);
                if (DateTime.Now < lastSendDate.AddMinutes(5))
                    return Session[DefaultValuesBase.SessionPhoneVerificationCode].ToString();
            }
            var code = InvitationCodeGenerator.GenerateCode(6, Sets.Digits);
            Session[DefaultValuesBase.SessionPhoneNumber] = phoneNumber;
            Session[DefaultValuesBase.SessionPhoneVerificationCode] = code;
            Session[DefaultValuesBase.SessionPhoneVerificationDate] = DateTime.Now.Ticks;
            if (HttpContext.IsDebuggingEnabled) return code;
            var result = await UserManager.SendSmsAsync(new Message
            {
                Body = code,
                Destination = phoneNumber,
                Subject = "Mentoria"
            });
            if (result.Succeeded)
                return code;
            throw new Exception(result.ErrorMessage);
            //await smsService.SendAsync(new IdentityMessage
            //{
            //    Destination = phoneNumber,
            //    Body = code,
            //    Subject = "Mentoria"
            //});
        }

        #endregion

        #region ChangePhoneNumber

        [Authorize]
        public virtual ActionResult ChangePhoneNumber()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize]
        [CustomValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ChangePhoneNumber(string password, string phoneNumber, string verificationCode)
        {
            if (!ModelState.IsValid) return JsonValidationError();
            if (CurrentUser.User == null)
                return JsonError(Messages.GeneralError);
            if (!await UserManager.CheckPasswordAsync(CurrentUser.User, password))
                return JsonError(Messages.GeneralError);
            var valid = CheckVerificationCode(verificationCode, phoneNumber);
            if (!valid) return JsonError(DisplayNames.InvalidPhoneVerificationCode);
            var updatePhoneNumberResult = await UserManager.SetPhoneNumberAsync(CurrentUser.User.Id, phoneNumber);
            if (!updatePhoneNumberResult.Succeeded) return JsonError(Messages.GeneralError);
            var confirmPhoneNumberResult = await UserManager.ConfirmPhoneNumberAsync(CurrentUser.User);
            if (!confirmPhoneNumberResult.Succeeded) return JsonError(Messages.GeneralError);
            return JsonSuccess(Messages.PhoneNumberChangedSuccessfully);
        }

        [HttpPost]
        [Authorize]
        [CustomValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ConfirmPhoneNumber(string code, string phoneNumber)
        {
            if (!ModelState.IsValid) return JsonValidationError();
            if (CurrentUser.User == null)
                return JsonError(Messages.GeneralError);
            var valid = CheckVerificationCode(code, phoneNumber);
            if (!valid) return JsonError(DisplayNames.InvalidPhoneVerificationCode);
            var result = await UserManager.ConfirmPhoneNumberAsync(CurrentUser.User);
            if (!result.Succeeded) return JsonError(Messages.GeneralError);
            return JsonSuccess(Messages.PhoneNumberConfirmedSuccessfully);
        }

        #endregion

        [HttpPost]
        [CustomValidateAntiForgeryToken]
        [CustomActionAuthorization]
        public virtual async Task<ActionResult> List(int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                var result = pageNumber.HasValue && pageSize.HasValue
                    ? await UserManager.GetAll(pageNumber.Value, pageSize.Value)
                    : await UserManager.GetAll();
                return JsonSuccess(result);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        
        #endregion

        #region Availability Checks

        [HttpPost]
        [AllowAnonymous]
        [CustomValidateAntiForgeryToken]
        public virtual async Task<ActionResult> CheckEmail(string email)
        {
            return await UserManager.FindByEmailAsync(email) == null
                ? JsonSuccess(true)
                : JsonError(DisplayNames.DuplicatedEmail);
        }

        [HttpPost]
        [AllowAnonymous]
        [CustomValidateAntiForgeryToken]
        public virtual async Task<ActionResult> CheckPassword(string password)
        {
            var succeeded = (await UserManager.PasswordValidator.ValidateAsync(password)).Succeeded;
            return succeeded ? JsonSuccess(true) : JsonError(DisplayNames.InvalidPassword);
        }
        
        [HttpPost]
        [AllowAnonymous]
        [CustomValidateAntiForgeryToken]
        public virtual ActionResult CheckPhoneVerificationCodeValidity(string verificationCode, string phoneNumber)
        {
            return JsonSuccess(CheckVerificationCode(verificationCode, phoneNumber));
        }

        protected virtual bool CheckVerificationCode(string code, string phoneNumber)
        {
            var valid = Session[DefaultValuesBase.SessionPhoneNumber] != null
                        && Session[DefaultValuesBase.SessionPhoneNumber].ToString() == phoneNumber
                        && Session[DefaultValuesBase.SessionPhoneVerificationCode] != null
                        && Session[DefaultValuesBase.SessionPhoneVerificationCode].ToString() == code;
            //&& Session[DefaultValuesBase.SessionPhoneVerificationDate] != null
            //&& DateTime.Now < new DateTime((long)Session[DefaultValuesBase.SessionPhoneVerificationDate]).AddMinutes(5);

            return valid;
        }
        #endregion
    }
}