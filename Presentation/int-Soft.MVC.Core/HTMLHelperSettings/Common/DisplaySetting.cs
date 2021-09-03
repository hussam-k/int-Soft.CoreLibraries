using System;
using System.Web.Mvc;
using intSoft.MVC.Core.Common;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Extensions;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public class DisplaySetting : SettingsBase
    {
        public DisplaySetting(Type controllerType)
            : this(controllerType.Name)
        {
        }

        private DisplaySetting(string controllerName)
        {
            Controller = controllerName.RemoveSubString(DefaultValuesBase.ControllerNameSuffix);
            var controllerNameCamelCase = Controller.ToCamelCase();
            Name = string.Format(DefaultJavaScriptNames.FormName, controllerNameCamelCase);
            CreateState = string.Format(DefaultJavaScriptNames.CreateState, controllerNameCamelCase);
            UpdateState = string.Format(DefaultJavaScriptNames.UpdateState, controllerNameCamelCase);
            ListState = controllerNameCamelCase;
        }

        public string Controller { get; set; }
        public string CreateState { get; set; }
        public string UpdateState { get; set; }
        public string ListState { get; set; }
        public string TitleExpression { get; set; }
        public MvcHtmlString BodyContent { get; set; }
        public Type ActionListProvider { get; set; }
        public MvcHtmlString ActionContent { get; set; }
        public bool ShowLabelInHeader { get; set; }
    }
}