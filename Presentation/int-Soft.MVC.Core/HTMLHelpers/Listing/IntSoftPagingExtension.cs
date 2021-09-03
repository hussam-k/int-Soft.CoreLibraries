using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelpers.Editors;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.HTMLHelpers.Listing
{
    public static class IntSoftPagingExtension
    {
        public static MvcHtmlString Pager(this IntSoftExtension helper, PagerSetting setting)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.Pager)));
        }
    }
}
