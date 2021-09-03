using System;
using System.Drawing;
using System.Web.Mvc;
using intSoft.MVC.Core.Common;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.Helpers.ButtonListProviders;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public class FormSetting : SettingsBase
    {
        public FormSetting(Type controllerType)
            : this(controllerType.Name)
        {
        }

        private FormSetting(string controllerName)
        {
            Controller = controllerName.RemoveSubString(DefaultValuesBase.ControllerNameSuffix);
            Legend = string.Format(DefaultValuesBase.LegendPattern, Controller);
            Title = string.Format(DefaultValuesBase.TitlePattern, Controller);
            TitleColor = Color.White;
            LegendColor = Color.White;
            SaveServerAction = DefaultValuesBase.SaveAction;
            GetModelServerAction = DefaultValuesBase.GetModelAction;
            SubmitClientAction = DefaultJavaScriptNames.Save;
            WithContainer = true;
            var controllerNameCamelCase = Controller.ToCamelCase();
            RedirectState = string.Format(DefaultJavaScriptNames.RedirectState, controllerNameCamelCase);
            Name = string.Format(DefaultJavaScriptNames.FormName, controllerNameCamelCase);
            ButtonListProvider = typeof (DefaultFormButtonListProvider);
        }

        public string Controller { get; set; }
        public string SaveServerAction { get; set; }
        public string GetModelServerAction { get; set; }
        public string SubmitClientAction { get; set; }
        public string RedirectState { get; set; }
        public string RedirectURL { get; set; }
        public string Title { get; set; }
        public Color TitleColor { get; set; }
        public Color LegendColor { get; set; }
        public string Legend { get; set; }
        public bool RequireCaptcha { get; set; }
        public bool WithContainer { get; set; }
        public Type ButtonListProvider { get; set; }
        public MvcHtmlString Content { get; set; }
        public MvcHtmlString ButtonsPanelContent { get; set; }
    }
}