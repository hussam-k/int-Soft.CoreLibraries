using intSoft.MVC.Core.Common;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Extensions;

namespace intSoft.MVC.Core.HTMLHelperSettings.Lising
{
    public class UiGridSetting : SettingsBase
    {
        public string UiGridDefinitionController { get; set; }
        public string UiGridDefinitionAction { get; set; }
        public UiGridSetting(string uiGridDefinitionController, string uiGridDefinitionAction)
        {
            UiGridDefinitionController = uiGridDefinitionController;
            UiGridDefinitionAction = uiGridDefinitionAction;

            var controllerNameCamelCase = UiGridDefinitionController.RemoveSubString(DefaultValuesBase.ControllerNameSuffix).ToCamelCase();
            Class = DefaultCssClasses.UiGrid;
            Name = string.Format(DefaultJavaScriptNames.UiGridName, controllerNameCamelCase);
        }
    }
}
