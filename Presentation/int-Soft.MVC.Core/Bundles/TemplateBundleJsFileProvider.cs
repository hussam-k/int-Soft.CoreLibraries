using System.Collections.Generic;
using System.Web.Mvc;
using intSoft.MVC.Core.HTTPApplication;

namespace intSoft.MVC.Core.Bundles
{
    public class TemplateBundleJsFileProvider : IBundleJsFileProvider
    {
        public int Order
        {
            get { return int.MinValue +2; }
        }

        public string BasePath
        {
            get { return DependencyResolver.Current.GetService<ApplicationConfiguration>().ScriptsUrl; }
        }

        public IEnumerable<string> JsFiles
        {
            get
            {
                if (!DependencyResolver.Current.GetService<ApplicationConfiguration>().RegisterFrameworkDefaultBundleProviders)
                {
                    return new List<string>();
                }
                return new List<string>
                {
                    "/Rushan/Scripts/controllers/main.js",
                    "/Rushan/Scripts/services.js",
                    "/Rushan/Scripts/templates.js",
                    "/Rushan/Scripts/template.js",
                    "/Rushan/Scripts/controllers/ui-bootstrap.js",
                    "/Rushan/Scripts/controllers/table.js"
                };
            }
        }
    }
}