using intSoft.MVC.Core.Enumerations;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public class MultilineTextBoxSetting : TextBoxSetting
    {
        public MultilineTextBoxSetting()
        {
            DataType = DataDisplayType.MultilineText;
            Rows = 5;
        }
        public int Columns { get; set; }
        public int Rows { get; set; }
    }
}