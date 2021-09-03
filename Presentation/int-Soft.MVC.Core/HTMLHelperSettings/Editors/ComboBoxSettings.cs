using System;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Enumerations;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.Helpers;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public class ComboBoxSettings : EditorSettingBase
    {
        public ComboBoxSettings(Type selectListProviderType)
        {
            DataType = DataDisplayType.ComboBox;
            Class = DefaultCssClasses.FormControl;
            SelectListProviderType = selectListProviderType;
            IsEnumProvider = selectListProviderType.IsSubClassOfGeneric(typeof(EnumSelectListProvider<>));

        }
        public ComboBoxSettings(string valuePropertyName, string displayTextProperty)
        {
            DataType = DataDisplayType.ComboBoxCascade;
            ValueProperty = valuePropertyName;
            DisplayTextProperty = displayTextProperty;
            Class = DefaultCssClasses.FormControl;
        }
        public ComboBoxSettings(string url, string valuePropertyName, string displayTextProperty)
        {
            DataType = DataDisplayType.ComboBoxRemote;
            Url = url;
            ValueProperty = valuePropertyName;
            DisplayTextProperty = displayTextProperty;
            Class = DefaultCssClasses.FormControl;
        }

        public Type SelectListProviderType { get; private set; }
        public bool IsEnumProvider { get; set; }
        public bool AllowNull { get; set; }
        public string DisplayTextProperty { get; set; }
        public string Url { get; private set; }
        public string ValueProperty { get; set; }
        public string PlaceHolder { get; set; }
        public bool AllowMultiple { get; set; }
        public string CascadeCallUrl { get; set; }
        public string CascadedParentPropertyName { get; set; }
        public string CascadeResultList { get { return string.Format(DefaultJavaScriptNames.CascadeListName, Name); } }

        public string MultipleExpression
        {
            get { return AllowMultiple ? AngularAttributeTemplates.ComboboxMultiple : string.Empty; }
        }
    }
}