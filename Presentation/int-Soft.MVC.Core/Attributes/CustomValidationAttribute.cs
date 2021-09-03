using System;
using intSoft.Res.DisplayNames;

namespace intSoft.MVC.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IntSoftCustomValidationAttribute : Attribute
    {
        public IntSoftCustomValidationAttribute(string validationKey)
        {
            ValidationKey = validationKey;
            ErrorMessageResourceType = typeof (DisplayNames);
        }

        public string ValidationKey { get; set; }

        public Type ErrorMessageResourceType { get; set; }

        public string ErrorMessageResourceName { get; set; }
    }
}