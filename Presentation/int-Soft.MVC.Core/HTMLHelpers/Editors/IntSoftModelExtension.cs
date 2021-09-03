using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using intSoft.MVC.Core.Attributes;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Enumerations;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.Helpers;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelpers.Common;
using intSoft.MVC.Core.HTMLHelperSettings;
using intSoft.MVC.Core.HTMLHelperSettings.Common;
using intSoft.MVC.Core.HTMLHelperSettings.Editors;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace intSoft.MVC.Core.HTMLHelpers.Editors
{
    public static class IntSoftModelExtension
    {
        public static MvcHtmlString EditorForModel(this IntSoftExtension helper, FormSetting formSetting = null)
        {
            formSetting = formSetting ?? helper.HtmlHelper.ViewData.ModelMetadata.GetCustomAttribute<FormSetting>();

            if (formSetting == null) return null;

            formSetting.Content = formSetting.Content == null || string.IsNullOrEmpty(formSetting.Content.ToString())
                ? helper.EditorForProperties(formSetting)
                : formSetting.Content;
            formSetting.ButtonsPanelContent = formSetting.ButtonsPanelContent == null ||
                                              string.IsNullOrEmpty(formSetting.ButtonsPanelContent.ToString())
                ? helper.GetFormButtonPanelContent(formSetting.ButtonListProvider)
                : formSetting.ButtonsPanelContent;

            return helper.Form(formSetting);
        }
        public static MvcHtmlString ModalEditorForModel(this IntSoftExtension helper)
        {
            var modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            var formSetting = modelMetadata.GetCustomAttribute<FormSetting>();
            if (formSetting == null) return null;

            formSetting.WithContainer = false;
            formSetting.Content = helper.EditorForProperties(formSetting);

            var modalSetting = modelMetadata.GetCustomAttribute<ModalSetting>() ?? new ModalSetting();
            
            modalSetting.HeaderContent = helper.HeaderForModel(formSetting);
            modalSetting.BodyContent = helper.Form(formSetting);
            modalSetting.FooterContent = helper.GetFormButtonPanelContent(modalSetting.ButtonListProvider);

            return helper.Modal(modalSetting);
        }
        public static MvcHtmlString HeaderForModel(this IntSoftExtension helper)
        {
            var modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            var formSetting = modelMetadata.GetCustomAttribute<FormSetting>();

            if (formSetting == null) return null;

            return helper.HeaderForModel(formSetting);
        }
        public static MvcHtmlString HeaderForModel(this IntSoftExtension helper, FormSetting formSetting)
        {
            return helper.ModelHeader(formSetting);
        }
        public static MvcHtmlString EditorForProperties(this IntSoftExtension helper)
        {
            var modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            var formSetting = modelMetadata.GetCustomAttribute<FormSetting>();

            return formSetting == null ? null : helper.EditorForProperties(formSetting);
        }
        public static MvcHtmlString EditorForProperties(this IntSoftExtension helper, FormSetting formSetting)
        {
            var modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            var propertiesModelMetaData = modelMetadata.Properties;

            var propertyEditors = "";
            propertyEditors += helper.AntiForgeryToken();
            foreach (var property in propertiesModelMetaData)
            {
                var setting = property.GetCustomAttribute<EditorSettingBase>();
                var displayAtAttribute = property.GetCustomAttribute<DisplayAtAttribute>() ?? new DisplayAtAttribute();

                if (setting == null ||
                    (!displayAtAttribute.DisplayAt.HasFlag(DisplayAt.Editor) &&
                     !displayAtAttribute.DisplayAt.HasFlag(DisplayAt.All)))
                {
                    continue;
                }

                if (displayAtAttribute.IsHiddenInput)
                {
                    propertyEditors += helper.HtmlHelper.Hidden(property.PropertyName);
                }
                else
                {
                    setting.Name = property.PropertyName;
                    var checkBoxSetting = setting as CheckBoxSettings;
                    if (checkBoxSetting != null)
                    {
                        checkBoxSetting.Label = ResourceHelper.GetStringFromResourceType(checkBoxSetting.ResourceType,
                            checkBoxSetting.Label);
                        propertyEditors += checkBoxSetting.DataType == DataDisplayType.CheckBox
                            ? helper.CheckBox(checkBoxSetting)
                            : helper.ToggleCheckBox(checkBoxSetting);
                    }
                    else
                    {
                        var labelText = property.GetPropertyTitleFromDisplayAttribute(true);
                        var formGroup = new FormGroupSetting
                        {
                            InputContent = MvcHtmlString.Create(GetEditorForProperty(helper, property, setting)),
                            LabelContent = helper.Label(property.PropertyName, labelText),
                            FormName = formSetting.Name,
                            PropertyName = property.PropertyName,
                            ValidationMessageContent =
                                MvcHtmlString.Create(GetValidationMessages(helper, property, formSetting.Name))
                        };

                        propertyEditors += helper.FormGroup(formGroup, setting);
                    }
                }
            }
            if (formSetting.RequireCaptcha)
                propertyEditors += helper.Captcha();

            return MvcHtmlString.Create(propertyEditors);
        }
        public static MvcHtmlString GetFormButtonPanelContent(this IntSoftExtension helper, Type buttonListProvideType)
        {
            var buttonPanelContent = "";
            var buttonListProvider = DependencyResolver.Current.GetService(buttonListProvideType) as IFormButtonListProvider;

            if (buttonListProvider != null)
            {
                var buttonList = buttonListProvider.GetButtonList();
                buttonPanelContent = buttonList.Aggregate(buttonPanelContent, (current, button) => current + (helper.Button(button) + " "));
            }
            
            return MvcHtmlString.Create(buttonPanelContent);
        }
        private static string GetEditorForProperty(IntSoftExtension helper, ModelMetadata property, EditorSettingBase settings)
        {
            settings.Validations = GetValidationAttributes(property);
            switch (settings.DataType)
            {
                case DataDisplayType.Date:
                    return helper.Date(settings as DateTimePickerSetting).ToString();
                case DataDisplayType.Text:
                    return helper.TextBox(settings as TextBoxSetting).ToString();
                case DataDisplayType.MultilineText:
                    return helper.MultilineTextBox(settings as MultilineTextBoxSetting).ToString();
                case DataDisplayType.EmailAddress:
                    return helper.Email(settings as TextBoxSetting).ToString();
                case DataDisplayType.Password:
                    return helper.Password(settings as TextBoxSetting).ToString();
                case DataDisplayType.Url:
                    return helper.Url(settings as TextBoxSetting).ToString();
                case DataDisplayType.Number:
                    return helper.Number(settings as TextBoxSetting).ToString();
                case DataDisplayType.ComboBox:
                    return helper.ComboBox(settings as ComboBoxSettings).ToString();
                case DataDisplayType.ComboBoxRemote:
                    return helper.RemoteComboBox(settings as ComboBoxSettings).ToString();
                case DataDisplayType.ComboBoxCascade:
                    return helper.CascadeComboBox(settings as ComboBoxSettings).ToString();
                case DataDisplayType.Photo:
                    return helper.Photo(settings as FileSetting).ToString();
                default:
                    return null;
            }
        }
        private static string GetValidationMessages(IntSoftExtension helper, ModelMetadata modelMetadata, string formName)
        {
            var result = new StringBuilder();

            var requierdAttribute = modelMetadata.GetCustomAttribute<RequiredAttribute>();
            if (requierdAttribute != null)
                result.Append(helper.RequiredValidationMessage(modelMetadata.PropertyName, formName, requierdAttribute));

            var emailAttribute = modelMetadata.GetCustomAttribute<EmailAddressAttribute>();
            if (emailAttribute != null)
                result.Append(helper.EmailValidationMessage(modelMetadata.PropertyName, formName, emailAttribute));

            var urlAttribute = modelMetadata.GetCustomAttribute<UrlAttribute>();
            if (urlAttribute != null)
                result.Append(helper.UrlValidationMessage(modelMetadata.PropertyName, formName, emailAttribute));

            var maxLengthAttribute = modelMetadata.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLengthAttribute != null)
                result.Append(helper.MaxLengthValidationMessage(modelMetadata.PropertyName, formName, maxLengthAttribute));

            var minLengthAttribute = modelMetadata.GetCustomAttribute<MinLengthAttribute>();
            if (minLengthAttribute != null)
                result.Append(helper.MinLengthValidationMessage(modelMetadata.PropertyName, formName, minLengthAttribute));

            var compareAttribute = modelMetadata.GetCustomAttribute<CompareAttribute>();
            if (compareAttribute != null)
                result.Append(helper.CompareValidationMessage(modelMetadata.PropertyName, formName, compareAttribute));

            var maxFileSizeAttribute = modelMetadata.GetCustomAttribute<MaxFileSizeAttribute>();
            if (maxFileSizeAttribute != null)
                result.Append(helper.MaxFileSizeValidationMessage(modelMetadata.PropertyName, formName,
                    maxFileSizeAttribute));

            var acceptedFileExtensionsAttribute = modelMetadata.GetCustomAttribute<AcceptedFileExtensionsAttribute>();
            if (acceptedFileExtensionsAttribute != null)
                result.Append(helper.FileExtensionValidationMessage(modelMetadata.PropertyName, formName,
                    acceptedFileExtensionsAttribute));

            var regularExpression = modelMetadata.GetCustomAttribute<RegularExpressionAttribute>();
            if (regularExpression != null)
                result.Append(helper.FileExtensionValidationMessage(modelMetadata.PropertyName, formName,
                    regularExpression));

            var customValidationAttributes = modelMetadata.GetCustomAttributes<IntSoftCustomValidationAttribute>();
            foreach (var customValidationAttribute in customValidationAttributes)
            {
                result.Append(helper.CustomValidationMessage(modelMetadata.PropertyName, formName,
                    customValidationAttribute));
            }
            return result.ToString();
        }
        private static string GetValidationAttributes(ModelMetadata modelMetadata)
        {
            var result = new StringBuilder();

            var minLengthAttribute = modelMetadata.GetCustomAttribute<MinLengthAttribute>();
            if (minLengthAttribute != null)
                result.Append(string.Format(AngularAttributeTemplates.MinLength, minLengthAttribute.Length));

            var maxLengthAttribute = modelMetadata.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLengthAttribute != null)
                result.Append(string.Format(AngularAttributeTemplates.MaxLength, maxLengthAttribute.Length));

            var regularExpressionAttribute = modelMetadata.GetCustomAttribute<RegularExpressionAttribute>();
            if (regularExpressionAttribute != null)
                result.Append(string.Format(AngularAttributeTemplates.Pattern, regularExpressionAttribute.Pattern.ToAngularPattern()));

            var requiredAttribute = modelMetadata.GetCustomAttribute<RequiredAttribute>();
            if (requiredAttribute != null)
                result.Append(string.Format(AngularAttributeTemplates.Required, true.ToString().ToCamelCase()));

            var compareAttribute = modelMetadata.GetCustomAttribute<CompareAttribute>();
            if (compareAttribute != null)
                result.Append(string.Format(AngularAttributeTemplates.Compare,
                    compareAttribute.OtherProperty.ToCamelCase()));

            return result.ToString();
        }
        public static MvcHtmlString AntiForgeryToken(this IntSoftExtension helper)
        {
            return
                new MvcHtmlString(
                    string.Format("<input id='antiForgeryToken' data-ng-model='antiForgeryToken' type='hidden'" +
                                  "data-ng-init=\"antiForgeryToken='{0}'\" />",
                        AntiForgeryTokenHelper.GetAntiForgeryToken()));
        }
    }
}