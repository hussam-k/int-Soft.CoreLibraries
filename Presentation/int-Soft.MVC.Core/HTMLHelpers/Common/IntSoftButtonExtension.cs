using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Enumerations;
using intSoft.MVC.Core.Helpers;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelperSettings;
using intSoft.Res.DisplayNames;

namespace intSoft.MVC.Core.HTMLHelpers.Common
{
    public static class IntSoftButtonExtension
    {
        public static MvcHtmlString Button(this IntSoftExtension helper, ButtonSetting settings)
        {
            if (settings.Type == ButtonType.Submit) return helper.SubmitButton(settings.InnerHtml, settings.Class);
            if (settings.Type == ButtonType.StateButton) return helper.StateButton(settings as StateButtonSetting);
            return new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.Button),
                settings.Class,
                settings.ClickAction,
                settings.InnerHtml,
                settings.Type));
        }
        public static MvcHtmlString StateButton(this IntSoftExtension helper, StateButtonSetting settings)
        {
            return new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.StateButton),
                settings.Class,
                settings.InnerHtml,
                settings.StateName));
        }
        public static MvcHtmlString SubmitButton(this IntSoftExtension helper, string value, string cssClass)
        {
            return new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.FormSubmitButton),
                cssClass,
                value));
        }
        public static MvcHtmlString SubmitButton(this IntSoftExtension helper)
        {
            return helper.SubmitButton(ResourceHelper.GetStringFromResourceType(typeof(DisplayNames), "Save"),
                DefaultCssClasses.SaveButton);
        }
    }
}