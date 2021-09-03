using System.Web.Mvc;
using intSoft.MVC.Core.HTMLHelpers.Base;

namespace intSoft.MVC.Core.Helpers.ListHeaderContentProviders
{
    public class EmptyListHeaderContentProvider : IListHeaderContentProvider
    {
        public MvcHtmlString GetListHeaderContent(IntSoftExtension helper)
        {
            return MvcHtmlString.Empty;
        }
    }
}