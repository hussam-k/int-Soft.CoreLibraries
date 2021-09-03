using intSoft.MVC.Core.Enumerations;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public class StateButtonSetting : ButtonSetting
    {
        public StateButtonSetting()
        {
            Type = ButtonType.StateButton;
        }
        public string StateName { get; set; }
    }
}