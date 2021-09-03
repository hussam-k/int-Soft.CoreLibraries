using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using intSoft.MVC.Core.Common;
using intSoft.Res.Messages;

namespace intSoft.MVC.Core.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomValidateAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    ValidateRequestHeader(filterContext.HttpContext.Request);
                }
                else
                {
                    AntiForgery.Validate();
                }
            }
            catch (HttpAntiForgeryException ex)
            {
                throw new HttpAntiForgeryException(Messages.AntiForgeryTokenCookieNotFound, ex);
            }
        }

        private void ValidateRequestHeader(HttpRequestBase request)
        {
            var cookieToken = string.Empty;
            var formToken = string.Empty;
            var tokenValue = "";
            if (request.Headers[DefaultValuesBase.AntiForgeryValidationKey] != null)
                tokenValue = request.Headers[DefaultValuesBase.AntiForgeryValidationKey];
            if (!string.IsNullOrEmpty(tokenValue))
            {
                var tokens = tokenValue.Split(':');
                if (tokens.Length == 2)
                {
                    cookieToken = tokens[0].Trim();
                    formToken = tokens[1].Trim();
                }
            }
            AntiForgery.Validate(cookieToken, formToken);
        }
    }
}