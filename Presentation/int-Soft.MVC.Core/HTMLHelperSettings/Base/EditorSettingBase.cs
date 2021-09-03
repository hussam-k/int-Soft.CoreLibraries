using intSoft.MVC.Core.Enumerations;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public abstract class EditorSettingBase : SettingsBase
    {
        public DataDisplayType DataType { get; set; }
        public string Validations { get; set; }
    }
}