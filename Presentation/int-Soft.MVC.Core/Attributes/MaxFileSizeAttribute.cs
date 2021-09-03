using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web;
using intSoft.MVC.Core.Common;
using intSoft.Res.DisplayNames;

namespace intSoft.MVC.Core.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        /// <summary>
        /// The maximum allowed file size in MB.
        /// </summary>
        public int MaxFileSize { get; private set; }

        public MaxFileSizeAttribute(int maxFileSize = DefaultValuesBase.MaxFileSizeNumber)
        {
            MaxFileSize = maxFileSize;
            ErrorMessageResourceType = typeof (DisplayNames);
        }
        
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null) return false;

            return file.ContentLength <= MaxFileSize * 1024 * 1024;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var propertyType = context.ObjectInstance.GetType().GetProperty(context.MemberName).GetType();

            if (propertyType.GetCustomAttribute<RequiredAttribute>() == null && value == null)
                return ValidationResult.Success;

            return base.IsValid(value, context);
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(MaxFileSize.ToString());
        }
    }
}