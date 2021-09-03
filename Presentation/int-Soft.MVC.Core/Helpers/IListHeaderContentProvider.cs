using System.Collections.Generic;
using System.Web.Mvc;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.Helpers
{
    public interface IListHeaderContentProvider
    {
        MvcHtmlString GetListHeaderContent(IntSoftExtension helper);
    }
}