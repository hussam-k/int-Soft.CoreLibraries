
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Enumerations;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public class TextBoxSetting : EditorSettingBase
    {
        public TextBoxSetting()
        {
            Class = DefaultCssClasses.FormControl;
            DataType = DataDisplayType.Text;
        }
        public bool IsAutoFocused { get; set; }

        public string Binding { get; set; }

        public string PlaceHolder { get; set; }
        
        public string Mask { get; set; }

        protected override string GetCustomAttributes()
        {
            var result = base.GetCustomAttributes();
            result += IsAutoFocused
                ? AngularAttributeTemplates.AutoFocus
                : string.Empty;

            result += string.IsNullOrEmpty(Mask)
                ? string.Empty
                : string.Format(AngularAttributeTemplates.InputMask, Mask);

            result += string.IsNullOrEmpty(PlaceHolder)
                ? string.Empty
                : string.Format(AngularAttributeTemplates.PlaceHolder, PlaceHolder);

            return result;
        }
    }
}
