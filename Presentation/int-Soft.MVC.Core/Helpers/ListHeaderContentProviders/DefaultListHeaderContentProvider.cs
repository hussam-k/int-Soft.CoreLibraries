using System.Web.Mvc;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelpers.Common;
using intSoft.MVC.Core.HTMLHelpers.Editors;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.Helpers.ListHeaderContentProviders
{
    public class DefaultListHeaderContentProvider : IListHeaderContentProvider
    {
        public MvcHtmlString GetListHeaderContent(IntSoftExtension helper)
        {
            return helper.Div("row", new MvcHtmlString(string.Format("{0}", helper.Div("col-md-12", helper.Button(ButtonSetting.CreateButtonSetting())))));
        }
    }
}
