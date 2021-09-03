using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.HTMLHelpers.Editors
{
    public static class IntSoftCheckBoxExtension
    {
        public static MvcHtmlString CheckBox(this IntSoftExtension helper, CheckBoxSettings checkBoxSettings)
        {
            return CheckBox(checkBoxSettings.Name, checkBoxSettings.Label, checkBoxSettings.Attributes, checkBoxSettings.ContainerAttributes);
        }
        public static MvcHtmlString ToggleCheckBox(this IntSoftExtension helper, CheckBoxSettings checkBoxSettings)
        {
            return ToggleCheckBox(checkBoxSettings);
        }
        private static MvcHtmlString ToggleCheckBox(CheckBoxSettings checkBoxSettings)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.ToggleCheckBox),
                checkBoxSettings.Name.ToCamelCase(),
                checkBoxSettings.Label,
                checkBoxSettings.Attributes,
                checkBoxSettings.ContainerAttributes
                ));
        }
        private static MvcHtmlString CheckBox(string propertyName, string checkBoxlabel, string attributes, string containerAttributes)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.CheckBox),
                propertyName.ToCamelCase(),
                checkBoxlabel,
                attributes,
                containerAttributes
                ));
        }
    }
}