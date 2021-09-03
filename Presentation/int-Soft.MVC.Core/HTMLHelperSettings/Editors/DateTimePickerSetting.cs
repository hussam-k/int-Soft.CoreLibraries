using System;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Enumerations;

namespace intSoft.MVC.Core.HTMLHelperSettings.Editors
{
    public class DateTimePickerSetting : EditorSettingBase
    {
        public DateTimePickerSetting()
        {
            Class = DefaultCssClasses.FormControl;
            DataType = DataDisplayType.Date;
            InitialDateTime = DateTime.Now;
            Format = AngularAttributeTemplates.DefaultDateFormat;
            MinYear = 1900;
            MaxYear = 2100;
            MinMonth = MinDay = MaxMonth = MaxDay = 1;
        }

        public bool IsAutoFocused { get; set; }
        public string Binding { get; set; }
        public string PlaceHolder { get; set; }

        /// <summary>
        ///     Default "dd/MM/yyyy"
        /// </summary>
        public string Format { get; set; }

        public bool ShowTime { get; set; }
        public DateTime InitialDateTime { get; set; }

        /// <summary>
        ///     Default 1900
        /// </summary>
        public int MinYear { get; set; }

        /// <summary>
        ///     Default 1
        /// </summary>
        public int MinMonth { get; set; }

        /// <summary>
        ///     Default 1
        /// </summary>
        public int MinDay { get; set; }

        /// <summary>
        ///     Default 1900/1/1
        /// </summary>
        public DateTime MinDateTime
        {
            get { return new DateTime(MinYear, MinMonth, MinDay); }
        }

        /// <summary>
        ///     Default 2100
        /// </summary>
        public int MaxYear { get; set; }

        /// <summary>
        ///     Default 1
        /// </summary>
        public int MaxMonth { get; set; }

        /// <summary>
        ///     Default 1
        /// </summary>
        public int MaxDay { get; set; }

        /// <summary>
        ///     Default 2100/1/1
        /// </summary>
        public DateTime MaxDateTime
        {
            get { return new DateTime(MaxYear, MaxMonth, MaxDay); }
        }

        public string CloseLabel { get; set; }
        public string TodayLabel { get; set; }

        /// <summary>
        ///     Default blank (green) other values("dp-blue", "dp-red")
        /// </summary>
        public string PopupColorClass { get; set; }

        public bool UseNestedComboBoxes { get; set; }
    }
}