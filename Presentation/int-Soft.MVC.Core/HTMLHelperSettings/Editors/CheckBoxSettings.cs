using intSoft.MVC.Core.Enumerations;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public class CheckBoxSettings : EditorSettingBase
    {
        public CheckBoxSettings()
        {
            DataType = DataDisplayType.CheckBox;
        }
        public string Label { get; set; }
    }
}