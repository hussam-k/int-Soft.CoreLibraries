using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using intSoft.MVC.Core.Common;
using Microsoft.AspNet.Identity;
using MvcInfrastructure.Base;

namespace intSoft.MVC.Core.Extensions
{
    public static class IdentityExtensions
    {
        public static Guid GetUserGuid(this IIdentity identity)
        {
            return new Guid(identity.GetUserId());
        }

        public static bool HasAccessToOperation(this IPrincipal principal, string operation)
        {
            var session = DependencyResolver.Current.GetService<HttpSessionStateBase>();
            return principal.Identity.Name == "admin" ||
                   (session[DefaultValuesBase.SessionUserOperationsKey] != null
                    && ((IList<string>) session[DefaultValuesBase.SessionUserOperationsKey]).Contains(operation));
        }

        public static bool HasAccessToOperation(this IPrincipal principal, string controller, string action)
        {
            var session = DependencyResolver.Current.GetService<HttpSessionStateBase>();
            return principal.Identity.Name == "admin" ||
                   (session[DefaultValuesBase.SessionUserOperationsKey] != null
                    &&
                    ((IList<string>)session[DefaultValuesBase.SessionUserOperationsKey]).Contains(
                        string.Format("{0}_{1}", controller, action)));
        }
    }
}