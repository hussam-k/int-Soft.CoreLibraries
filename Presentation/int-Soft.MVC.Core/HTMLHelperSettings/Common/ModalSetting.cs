using System;
using System.Web.Mvc;
using intSoft.MVC.Core.Helpers.ButtonListProviders;

namespace intSoft.MVC.Core.HTMLHelperSettings.Common
{
    public class ModalSetting : SettingsBase
    {
        public ModalSetting()
        {
            ButtonListProvider = typeof(ModalButtonListProvider);
        }
        public MvcHtmlString HeaderContent { get; set; }
        public MvcHtmlString BodyContent { get; set; }
        public MvcHtmlString FooterContent { get; set; }
        public Type ButtonListProvider { get; set; }
    }
}
