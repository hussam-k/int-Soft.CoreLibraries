using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using IntSoft.DAL.Common;
using IntSoft.DAL.Models;
using IntSoft.DAL.RepositoriesBase;
using intSoft.MVC.Core.Common;
using intSoft.MVC.Core.Filters;
using intSoft.MVC.Core.Helpers;
using intSoft.MVC.Core.HTTPApplication;
using intSoft.MVC.Core.Identity;
using Microsoft.AspNet.Identity;

namespace intSoft.MVC.Core.Controllers
{
    public class LocalizationControllerBase<TClientSideResources, TUser, TUserRole, TUserClaim, TUserLogin> : ControllerBase
        where TUser : class, IUserBase<TUserRole, TUserClaim, TUserLogin>, IUser<Guid>, IEntity, new() 
        where TUserRole : IUserRoleBase 
        where TUserClaim : IUserClaimBase 
        where TUserLogin : IUserLoginBase
    {
        [HttpPost]
        [AllowAnonymous]
        [CustomValidateAntiForgeryToken]
        public ActionResult ChangeLanguage(string language)
        {
            var appConfig = DependencyResolver.Current.GetService<ApplicationConfiguration>();
            var currentLanguage = appConfig == null ? DefaultValuesBase.DefaultLanguage : (appConfig.Localization.AcceptedLanguages.Contains(language) ? language : appConfig.Localization.PreferredLanguage);

            var currentUser = DependencyResolver.Current.GetService<ICurrentUser<TUser>>();
            var userRepository = DependencyResolver.Current.GetService<Repository<TUser>>();
            var isLogedIn = currentUser != null && currentUser.User != null;

            //the user is loged in
            if (isLogedIn && userRepository != null)
            {
                currentUser.User.PreferredLanguage = currentLanguage;
                try
                {
                    userRepository.Save(currentUser.User);
                }
                catch
                {
                    
                }
            }

            Session[DefaultValuesBase.HttpRequestCurrentLanguage] = currentLanguage;

            if (Request.Cookies[DefaultValuesBase.HttpRequestCurrentLanguage] != null)
                Response.SetCookie(new HttpCookie(DefaultValuesBase.HttpRequestCurrentLanguage, currentLanguage));

            return JsonSuccess(true);
        }

        [HttpGet]
        public ActionResult GetCurrentLanguage()
        {
            return JsonSuccess(Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName);
        }
        
        [HttpGet]
        public ActionResult Get(string collation = "en-US")
        {
            switch (collation)
            {
                case "en-US":
                    return JsonSuccess(ResourceHelper.ResourceToDictionary(typeof(TClientSideResources), new CultureInfo(collation)), false);
                case "ar-SY":
                    return JsonSuccess(ResourceHelper.ResourceToDictionary(typeof(TClientSideResources), new CultureInfo(collation)), false);
                default:
                    return JsonError("Unknown collation");
            }
        }
    }
}