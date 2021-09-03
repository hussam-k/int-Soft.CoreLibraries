using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using intSoft.MVC.Core.Common;
using MvcInfrastructure.Base;

namespace intSoft.MVC.Core.Security
{
    public class IntSoftAuthorizeAttribute : AuthorizeAttribute
    {
        private bool _isAlwaysAllowed;
        private string _operationControllerName;
        private string _operationActionName;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var customActionAuthorizationAttr =
                filterContext.ActionDescriptor.GetCustomAttributes(typeof (CustomActionAuthorizationAttribute), false)
                    .FirstOrDefault();

            var attri = (CustomActionAuthorizationAttribute) customActionAuthorizationAttr;

            _isAlwaysAllowed = attri != null && attri.IsAlwaysAllowed;
            _operationControllerName = attri != null ? attri.OperationControllerName : null;
            _operationActionName = attri != null ? attri.OperationActionName : null;

            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorizationHelper = DependencyResolver.Current.GetService<IAuthorizationHelper>(); 
            var rd = httpContext.Request.RequestContext.RouteData;

            if (!string.IsNullOrEmpty(Users) && !string.IsNullOrWhiteSpace(Users) &&
                Users.Contains(httpContext.User.Identity.Name) || httpContext.User.Identity.Name == DefaultValuesBase.Admin || _isAlwaysAllowed) 
                return true;

            if (!_isAlwaysAllowed && !httpContext.User.Identity.IsAuthenticated)
                return false;
            
            if (rd.Route == null)
                return true;

            string currentAction, currentController;

            if (_operationControllerName != null && _operationActionName != null)
            {
                currentController = _operationControllerName;
                currentAction = _operationActionName;
            }
            else
            {
                currentAction = rd.GetRequiredString("action");
                currentController = rd.GetRequiredString("controller");
            }
            
            return authorizationHelper.CheckAuthorization(currentController, currentAction,
                Roles.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries).ToList());
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                    //new RedirectToRouteResult(new
                    //RouteValueDictionary(new {controller = "Account", action = "Login"}));
            }
        }
    }
}