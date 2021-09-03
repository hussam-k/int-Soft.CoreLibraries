using System.Web.Mvc;

namespace intSoft.MVC.Core.HTMLHelpers.Base
{
    public class IntSoftExtension
    {
        internal IntSoftExtension(HtmlHelper htmlHelper)
        {
            HtmlHelper = htmlHelper;
        }

        public HtmlHelper HtmlHelper { get; private set; }

        internal static IntSoftExtension GetInstance(HtmlHelper htmlHelper)
        {
            return new IntSoftExtension(htmlHelper);
        }


    }

    public class IntSoftExtension<TModel> : IntSoftExtension
    {
        internal IntSoftExtension(HtmlHelper<TModel> htmlHelper) : base(
            htmlHelper)
        {
            HtmlHelper = htmlHelper;
        }

        public new HtmlHelper<TModel> HtmlHelper { get; private set; }

        internal static IntSoftExtension<TModel> GetInstance(HtmlHelper<TModel> htmlHelper)
        {
            return new IntSoftExtension<TModel>(htmlHelper);
        }
    }
}