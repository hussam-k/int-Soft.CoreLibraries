using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using intSoft.MVC.Core.Common;
using IntSoft.DAL.Common;
using IntSoft.DAL.Models;
using IntSoft.DAL.RepositoriesBase;
using MvcInfrastructure.Base;
using StructureMap.Attributes;

namespace intSoft.MVC.Core.Security
{
    public class AuthorizationHelper<TOperation> : IAuthorizationHelper 
        where TOperation : class, IEntity, IOperationBase
    {
        [SetterProperty]
        public IRepository<TOperation> Repository { get; set; }


        [SetterProperty]
        public HttpContextBase HttpContext { get; set; }

        public bool CheckAuthorization(string controllerName, string actionName,
            IEnumerable<string> allowedRoleNames = null)
        {
            if (HttpContext.User.Identity.Name.ToLower() == DefaultValuesBase.Admin)
                return true;

            var userRoles = HttpContext.Session[DefaultValuesBase.SessionUserRoleKey] as List<string>;
            var userOperations = HttpContext.Session[DefaultValuesBase.SessionUserOperationsKey] as List<string>;

            if (userRoles == null || userOperations == null)
            {
                if (HttpContext.Request.IsAjaxRequest())
                {
                    HttpContext.Response.SetStatus(401);
                }
                return false;
            }

            if (allowedRoleNames != null && allowedRoleNames.Any())
            {
                var roles = allowedRoleNames.Select(x => Repository.FirstOrDefault(y => y.Name.Equals(x)));
                return roles.Any(x => x != null) &&
                       userRoles.Any(userRoleName => roles.Any(y => y.Name.Equals(userRoleName)));
            }

            return userOperations.Any(x => x == string.Format("{0}_{1}", controllerName, actionName));
        }

        public bool CheckAuthorization(string controllerName)
        {
            if (HttpContext.User.Identity.Name.ToLower() == DefaultValuesBase.Admin)
                return true;

            var userRoles = HttpContext.Session[DefaultValuesBase.SessionUserRoleKey] as List<string>;
            var userOperations = HttpContext.Session[DefaultValuesBase.SessionUserOperationsKey] as List<string>;

            if (userRoles == null || userOperations == null)
            {
                if (HttpContext.Request.IsAjaxRequest())
                {
                    HttpContext.Response.SetStatus(401);
                }
                return false;
            }

            return userOperations.Any(x => x.Contains(string.Format("{0}_", controllerName)));
        }
    }
}