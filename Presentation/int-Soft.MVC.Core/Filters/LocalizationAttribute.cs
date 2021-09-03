using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using intSoft.MVC.Core.Common;
using intSoft.MVC.Core.HTTPApplication;

namespace intSoft.MVC.Core.Filters
{
    public class LocalizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            string acceptLanguage;
            
            var appConfiguration = DependencyResolver.Current.GetService<ApplicationConfiguration>();
            var defaultLanguage = appConfiguration != null ? appConfiguration.Localization.PreferredLanguage : DefaultValuesBase.DefaultLanguage;
            
            if (filterContext.HttpContext.Session[DefaultValuesBase.HttpRequestCurrentLanguage] != null)
                acceptLanguage = filterContext.HttpContext.Session[DefaultValuesBase.HttpRequestCurrentLanguage].ToString();
            else if (filterContext.HttpContext.Request.Cookies[DefaultValuesBase.HttpRequestCurrentLanguage] != null)
                acceptLanguage = filterContext.HttpContext.Request.Cookies[DefaultValuesBase.HttpRequestCurrentLanguage].Value;
            else
            {
                var headerAcceptLanguage = filterContext.RequestContext.HttpContext.Request.Headers[DefaultValuesBase.HttpRequestHeaderKeysAcceptLanguage];
                acceptLanguage = (string.IsNullOrWhiteSpace(headerAcceptLanguage) || headerAcceptLanguage.Length < 2) ? defaultLanguage : headerAcceptLanguage.Substring(0, 2);
                acceptLanguage = appConfiguration != null && appConfiguration.Localization.AcceptedLanguages.Contains(acceptLanguage) ? acceptLanguage : defaultLanguage;
                filterContext.HttpContext.Response.SetCookie(new HttpCookie(DefaultValuesBase.HttpRequestCurrentLanguage, acceptLanguage));
            }

            CultureInfo currentCulture;
            try
            {
                currentCulture = CultureInfo.CreateSpecificCulture(acceptLanguage);
            }
            catch (CultureNotFoundException)
            {
                currentCulture = new CultureInfo(DefaultValuesBase.DefaultCultureName);
            }

            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = currentCulture;
        }
    }
}