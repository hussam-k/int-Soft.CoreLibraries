using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace intSoft.MVC.Core.Bundles
{
    public static class BundleRenderer
    {
        public static IHtmlString RenderScripts()
        {
            if (!HttpContext.Current.IsDebuggingEnabled) return Scripts.Render("~/intSoft/scripts");
            var bundleFileProviders =
                DependencyResolver.Current.GetServices<IBundleJsFileProvider>().OrderBy(x => x.Order);
            var result = "";
            foreach (var provider in bundleFileProviders)
            {
                foreach (var file in provider.JsFiles)
                {
                    var fileName = file.Split('/').Last();
                    result += Scripts.Render(string.Format("~/{0}", fileName));
                }
            }
            return new HtmlString(result);
        }

        public static IHtmlString RenderStyles()
        {
            if (!HttpContext.Current.IsDebuggingEnabled) return Styles.Render("~/intSoft/styles");
            var bundleFileProviders =
                DependencyResolver.Current.GetServices<IBundleStyleFileProvider>().OrderBy(x => x.Order);
            var result = "";
            foreach (var provider in bundleFileProviders)
            {
                result += provider.StyleFiles.Select(style => style.Split('/').Last())
                    .Aggregate("", (current, fileName) => current + Styles.Render(string.Format("~/{0}", fileName)));
            }
            return new HtmlString(result);
        }
    }
}