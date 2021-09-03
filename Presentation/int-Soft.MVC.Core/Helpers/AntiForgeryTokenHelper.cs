using System.Web;
using System.Web.Helpers;
using intSoft.MVC.Core.Common;

namespace intSoft.MVC.Core.Helpers
{
    public static class AntiForgeryTokenHelper
    {
        public static string GetAntiForgeryToken()
        {
            string cookieToken, formToken;
            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            return cookieToken + ":" + formToken;
        }

        public static void SetAntiForgeryToken()
        {
            if (HttpContext.Current.Response.Cookies[DefaultValuesBase.AntiForgeryValidationKey] == null ||
                string.IsNullOrEmpty(HttpContext.Current.Response.Cookies[DefaultValuesBase.AntiForgeryValidationKey].Value))
            {
                HttpContext.Current.Response.Cookies.Add(
                    new HttpCookie(DefaultValuesBase.AntiForgeryValidationKey, GetAntiForgeryToken()));
            }
        }   
    }
}