namespace intSoft.MVC.Core.Helpers.UiGrid
{
    public class UiGridColumnSelectOption
    {
        public UiGridColumnSelectOption(string lable, int value)
        {
            Label = lable;
            Value = value;
        }
        public string Label { get; set; }
        public int Value { get; set; }
    }
}