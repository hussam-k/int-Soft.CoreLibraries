using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Enumerations;
using intSoft.MVC.Core.Helpers;
using intSoft.Res.DisplayNames;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public class ButtonSetting : SettingsBase
    {
        public ButtonSetting()
        {
            Type = ButtonType.Button;
        }
        public string InnerHtml { get; set; }
        public bool IsAutoFocused { get; set; }
        public bool HasIcon { get; set; }
        public bool ShowContentString { get; set; }
        public string IconClass { get; set; }
        public ButtonType Type { get; set; }
        public string ClickAction { get; set; }

        #region Factory Methods
        public static ButtonSetting CancelButtonSetting()
        {
            return new ButtonSetting
            {
                Class = DefaultCssClasses.CancelButton,
                ClickAction = DefaultJavaScriptNames.Cancel,
                InnerHtml = ResourceHelper.GetStringFromResourceType(typeof(DisplayNames), "Cancel")
            };
        }
        public static ButtonSetting SaveButtonSetting()
        {
            return new ButtonSetting
            {
                Class = DefaultCssClasses.SaveButton,
                ClickAction = DefaultJavaScriptNames.Save,
                InnerHtml = ResourceHelper.GetStringFromResourceType(typeof(DisplayNames), "Save")
            };
        }
        public static ButtonSetting SubmitButtonSetting()
        {
            return new ButtonSetting
            {
                Class = DefaultCssClasses.SaveButton,
                InnerHtml = ResourceHelper.GetStringFromResourceType(typeof(DisplayNames), "Save"),
                Type = ButtonType.Submit
            };
        }
        public static ButtonSetting CreateButtonSetting()
        {
            return new ButtonSetting
            {
                Class = DefaultCssClasses.CreateButton,
                ClickAction = DefaultJavaScriptNames.Create,
                InnerHtml = "<i class=\"zmdi zmdi-plus\"></i>"
            };
        }
        public static ButtonSetting DraftButtonSetting()
        {
            return new ButtonSetting
            {
                Class = DefaultCssClasses.DraftsButton,
                ClickAction = DefaultJavaScriptNames.Drafts,
                InnerHtml = "<i class=\"zmdi zmdi-refresh\"></i>"
            };
        }
        public static ButtonSetting ListButtonSetting()
        {
            return new ButtonSetting
            {
                Class = DefaultCssClasses.ListButton,
                ClickAction = DefaultJavaScriptNames.List,
                InnerHtml = "<i class=\"zmdi zmdi-format-list-bulleted\"></i>"
            };
        } 
        #endregion

    }
}