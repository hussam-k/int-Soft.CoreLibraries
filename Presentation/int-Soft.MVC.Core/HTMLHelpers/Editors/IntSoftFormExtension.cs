using System.Threading;
using System.Web;
using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelperSettings;
using intSoft.MVC.Core.HTMLHelperSettings.Common;
using intSoft.MVC.Core.HTTPApplication;

namespace intSoft.MVC.Core.HTMLHelpers.Editors
{
    public static class IntSoftFormExtension
    {
        public static MvcHtmlString FormGroup(this IntSoftExtension helper, FormGroupSetting setting, SettingsBase propertySettings)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.FormGroup),
                setting.FormName,
                setting.PropertyName.ToCamelCase(),
                setting.LabelContent,
                setting.InputContent,
                setting.ValidationMessageContent,
                propertySettings.ContainerAttributes
                ));
        }
        public static MvcHtmlString ModelHeader(this IntSoftExtension helper, FormSetting setting = null)
        {
            setting = setting ?? helper.HtmlHelper.ViewData.ModelMetadata.GetCustomAttribute<FormSetting>();
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.ModelHeader),
                            setting.GetStringFromResourceType(setting.Title),
                            setting.GetStringFromResourceType(setting.Legend),
                            setting.TitleColor.Name,
                            setting.LegendColor.Name));
        }
        public static MvcHtmlString Form(this IntSoftExtension helper, FormSetting setting)
        {
            var form = MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.Form),
                setting.Controller,
                setting.Class,
                setting.Name,
                setting.RedirectState,
                setting.Content,
                setting.ButtonsPanelContent,
                setting.SaveServerAction,
                setting.GetModelServerAction,
                setting.SubmitClientAction,
                setting.RedirectURL
                ));
            return setting.WithContainer
                ? MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.FormContainer),
                    helper.ModelHeader(setting),
                    form
                    ))
                : form;
        }
        public static MvcHtmlString Modal(this IntSoftExtension helper, ModalSetting setting)
        {
            return
                MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.Modal),
                    setting.HeaderContent, setting.BodyContent, setting.FooterContent));
        }
        public static MvcHtmlString Div(this IntSoftExtension helper, string cssClass, MvcHtmlString content)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.Div), cssClass, content));
        }
        public static MvcHtmlString Captcha(this IntSoftExtension helper)
        {
            if (HttpContext.Current.IsDebuggingEnabled) return MvcHtmlString.Empty;
            var lang = Thread.CurrentThread.CurrentCulture.Name.Contains("ar") ? "ar" : "en";
            var key = DependencyResolver.Current.GetService<ApplicationConfiguration>().PublicCaptchaKey;
            return
                MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.Captcha), lang, key));
        }
    }
}
