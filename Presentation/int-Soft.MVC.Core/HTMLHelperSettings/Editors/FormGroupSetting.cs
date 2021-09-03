using System.Web.Mvc;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public class FormGroupSetting : SettingsBase
    {
        public string FormName { get; set; }
        public string PropertyName { get; set; }
        public MvcHtmlString LabelContent { get; set; }
        public MvcHtmlString InputContent { get; set; }
        public MvcHtmlString ValidationMessageContent { get; set; }
    }

}
