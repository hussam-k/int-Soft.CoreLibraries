using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using intSoft.MVC.Core.Attributes;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.Helpers;
using intSoft.MVC.Core.HTMLHelpers.Base;

namespace intSoft.MVC.Core.HTMLHelpers.Editors
{
    public static class IntSoftValidationMessageExtension
    {
        public static MvcHtmlString ValidationMessage(this IntSoftExtension helper,
            string propertyName,
            string formName,
            string validationAttibute,
            string validationMessage)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.ValidationMessage),
                formName,
                propertyName.ToCamelCase(),
                validationAttibute,
                validationMessage
                ));
        }
        public static MvcHtmlString RequiredValidationMessage(this IntSoftExtension helper, string propertyName,
            string formName)
        {
            return helper.ValidationMessage(propertyName, formName, "required", string.Format("{0} is requierd", propertyName));
        }
        public static MvcHtmlString RequiredValidationMessage(this IntSoftExtension helper, string propertyName, string formName, ValidationAttribute validationAttribute)
        {
            return helper.ValidationMessage(propertyName, formName, "required", ResourceHelper.GetStringFromValidationAttribute(validationAttribute));
        }
        public static MvcHtmlString EmailValidationMessage(this IntSoftExtension helper, string propertyName, string formName)
        {
            return helper.ValidationMessage(propertyName, formName, "email", string.Format("{0} is invalid email", propertyName));
        }
        public static MvcHtmlString EmailValidationMessage(this IntSoftExtension helper, string propertyName, string formName, ValidationAttribute validationAttribute)
        {
            return helper.ValidationMessage(propertyName, formName, "email", ResourceHelper.GetStringFromValidationAttribute(validationAttribute));
        }
        public static MvcHtmlString UrlValidationMessage(this IntSoftExtension helper, string propertyName, string formName)
        {
            return helper.ValidationMessage(propertyName, formName, "url", string.Format("{0} is invalid url", propertyName));
        }
        public static MvcHtmlString UrlValidationMessage(this IntSoftExtension helper, string propertyName, string formName, ValidationAttribute validationAttribute)
        {
            return helper.ValidationMessage(propertyName, formName, "url", ResourceHelper.GetStringFromValidationAttribute(validationAttribute));
        }
        public static MvcHtmlString MinLengthValidationMessage(this IntSoftExtension helper, string propertyName, string formName)
        {
            return helper.ValidationMessage(propertyName, formName, "minlength", string.Format("{0} should be greater than", propertyName));
        }
        public static MvcHtmlString MinLengthValidationMessage(this IntSoftExtension helper, string propertyName, string formName, ValidationAttribute validationAttribute)
        {
            return helper.ValidationMessage(propertyName, formName, "minlength", ResourceHelper.GetStringFromValidationAttribute(validationAttribute));
        }
        public static MvcHtmlString CompareValidationMessage(this IntSoftExtension helper, string propertyName, string formName, ValidationAttribute validationAttribute)
        {
            return helper.ValidationMessage(propertyName, formName, "match", ResourceHelper.GetStringFromValidationAttribute(validationAttribute));
        }
        public static MvcHtmlString MaxLengthValidationMessage(this IntSoftExtension helper, string propertyName, string formName)
        {
            return helper.ValidationMessage(propertyName, formName, "maxlength", string.Format("{0} should be smaller than", propertyName));
        }
        public static MvcHtmlString MaxLengthValidationMessage(this IntSoftExtension helper, string propertyName, string formName, ValidationAttribute validationAttribute)
        {
            return helper.ValidationMessage(propertyName, formName, "maxlength", ResourceHelper.GetStringFromValidationAttribute(validationAttribute));
        }
        public static MvcHtmlString MaxFileSizeValidationMessage(this IntSoftExtension helper, string propertyName, string formName, ValidationAttribute validationAttribute)
        {
            return helper.ValidationMessage(propertyName, formName, "maxSize", validationAttribute.FormatErrorMessage(""));
        }
        public static MvcHtmlString FileExtensionValidationMessage(this IntSoftExtension helper, string propertyName, string formName, ValidationAttribute validationAttribute)
        {
            return helper.ValidationMessage(propertyName, formName, "pattern", validationAttribute.FormatErrorMessage(""));
        }
        public static MvcHtmlString RegularExpressionValidationMessage(this IntSoftExtension helper, string propertyName, string formName, ValidationAttribute validationAttribute)
        {
            return helper.ValidationMessage(propertyName, formName, "pattern", validationAttribute.FormatErrorMessage(""));
        }
        public static MvcHtmlString CustomValidationMessage(this IntSoftExtension helper, string propertyName, string formName, IntSoftCustomValidationAttribute validationAttribute)
        {
            return helper.ValidationMessage(propertyName, formName, validationAttribute.ValidationKey,
                ResourceHelper.GetStringFromResourceType(validationAttribute.ErrorMessageResourceType,
                    validationAttribute.ErrorMessageResourceName));
        }
    }
}