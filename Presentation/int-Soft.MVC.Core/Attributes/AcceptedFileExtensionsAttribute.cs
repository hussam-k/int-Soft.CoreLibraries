using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using intSoft.MVC.Core.Common;
using intSoft.Res.DisplayNames;

namespace intSoft.MVC.Core.Attributes
{
    public class AcceptedFileExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _acceptedExtension;
        private readonly string _acceptedExtensions;

        public AcceptedFileExtensionsAttribute(string acceptedExtensions = DefaultValuesBase.AcceptedImageExtensions)
        {
            _acceptedExtensions = acceptedExtensions;
            _acceptedExtension = acceptedExtensions.Split(',');
            ErrorMessageResourceType = typeof (DisplayNames);
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            if (file == null) return false;

            string extension = Path.GetExtension(file.FileName);
            return _acceptedExtension.FirstOrDefault(ext => ext == extension) != null;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            Type propertyType = context.ObjectInstance.GetType().GetProperty(context.MemberName).GetType();

            if (propertyType.GetCustomAttribute<RequiredAttribute>() == null && value == null)
                return ValidationResult.Success;

            return base.IsValid(value, context);
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(_acceptedExtensions);
        }
    }
}