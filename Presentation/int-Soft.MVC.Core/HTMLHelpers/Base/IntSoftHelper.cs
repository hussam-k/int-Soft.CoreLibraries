using System.Web.Mvc;

namespace intSoft.MVC.Core.HTMLHelpers.Base
{
    public static class IntSoftHelper
    {
        public static IntSoftExtension IntSoft(this HtmlHelper htmlHelper)
        {
            return IntSoftExtension.GetInstance(htmlHelper);
        }

        public static IntSoftExtension<TModel> IntSoft<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            return IntSoftExtension<TModel>.GetInstance(htmlHelper);
        }
    }
}