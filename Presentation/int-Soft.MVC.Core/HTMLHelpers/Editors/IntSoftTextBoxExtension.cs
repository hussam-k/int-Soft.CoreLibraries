using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.HTMLHelpers.Editors
{
    public static class IntSoftTextBoxExtension
    {
        public static MvcHtmlString TextBox(this IntSoftExtension helper, TextBoxSetting setting)
        {
            return TextBox("text", setting);
        }
        public static MvcHtmlString Email(this IntSoftExtension helper, TextBoxSetting setting)
        {
            return TextBox("email", setting);
        }
        public static MvcHtmlString Number(this IntSoftExtension helper, TextBoxSetting setting)
        {
            return TextBox("number", setting);
        }
        public static MvcHtmlString Url(this IntSoftExtension helper, TextBoxSetting setting)
        {
            return TextBox("url", setting);
        }
        public static MvcHtmlString Password(this IntSoftExtension helper, TextBoxSetting setting)
        {
            return TextBox("password", setting);
        }
        private static MvcHtmlString TextBox(string type, TextBoxSetting setting)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.TextBox),
                setting.Name.ToCamelCase(),
                type,
                setting.Validations,
                setting.Class,
                setting.Attributes));
        }
        public static MvcHtmlString MultilineTextBox(this IntSoftExtension helper, MultilineTextBoxSetting setting)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.Multiline),
                setting.Name.ToCamelCase(),
                setting.Columns,
                setting.Rows,
                setting.Class,
                setting.Validations,
                setting.Attributes));
        }
        public static MvcHtmlString Label(this IntSoftExtension helper, string propertyName, string text)
        {
            return new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.Label),
                propertyName.ToCamelCase(),
                text,
                "control-label"
                ));
        }

    }
}
