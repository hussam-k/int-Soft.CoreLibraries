using System.Collections.Generic;

namespace MvcInfrastructure.Base
{
    public interface IAuthorizationHelper
    {
        bool CheckAuthorization(string controllerName, string actionName, IEnumerable<string> allowedRoleNames = null);
        bool CheckAuthorization(string controllerName);
    }
}