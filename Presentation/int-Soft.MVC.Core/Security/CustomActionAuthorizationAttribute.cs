using System;

namespace intSoft.MVC.Core.Security
{
    public class CustomActionAuthorizationAttribute : Attribute
    {
        public bool IsAlwaysAllowed { get; set; }

        public string OperationControllerName { get; set; }

        public string OperationActionName { get; set; }
    }
}
