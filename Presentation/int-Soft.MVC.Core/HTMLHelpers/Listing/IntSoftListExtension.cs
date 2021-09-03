using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelpers.Editors;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.HTMLHelpers.Listing
{
    public static class IntSoftListExtension
    {
        public static MvcHtmlString List(this IntSoftExtension helper, ListSetting setting)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.List),
                setting.Controller,
                setting.CreateState,
                setting.UpdateState,
                setting.Class,
                setting.HeaderClass,
                setting.HeaderContent,
                setting.ListBodyClass,
                setting.ListBodyContent,
                setting.DraftsState,
                setting.ListServerAction,
                setting.ListState,
                setting.DisplayState,
                helper.AntiForgeryToken()
                ));
        }

        public static MvcHtmlString ListHeader(this IntSoftExtension helper, ListHeaderSetting setting)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.ListHeader),
                setting.Title,
                setting.Legend,
                setting.HeaderContent
                ));
        }

    }
}
