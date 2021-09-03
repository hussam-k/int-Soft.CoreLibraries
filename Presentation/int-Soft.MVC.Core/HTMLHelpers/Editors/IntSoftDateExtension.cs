using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelperSettings.Editors;

namespace intSoft.MVC.Core.HTMLHelpers.Editors
{
    public static class IntSoftDateExtension
    {
        public static MvcHtmlString Date(this IntSoftExtension helper, DateTimePickerSetting setting)
        {
            if (setting.UseNestedComboBoxes) return helper.NestedComboBoxesDate(setting);
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.Date),
                setting.Name.ToCamelCase(),
                setting.Class,
                setting.Validations,
                setting.Format,
                setting.MinDateTime.ToJavascriptDate(),
                setting.MaxDateTime.ToJavascriptDate(),
                setting.CloseLabel,
                setting.TodayLabel,
                setting.Attributes,
                setting.PopupColorClass
                ));
        }
        internal static MvcHtmlString NestedComboBoxesDate(this IntSoftExtension helper, DateTimePickerSetting setting)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.NestedComboBoxesDate),
                setting.Name.ToCamelCase(),
                setting.Class,
                setting.MinYear,
                setting.MinMonth,
                setting.MinDay,
                setting.MaxYear,
                setting.MaxMonth,
                setting.MaxDay,
                setting.Validations
                ));
        }
    }
}
