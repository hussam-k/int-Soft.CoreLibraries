using System.Collections.Generic;
using System.Web.Mvc;
using intSoft.MVC.Core.HTTPApplication;

namespace intSoft.MVC.Core.Bundles
{
    public class DependencyBundleJsFileProvider : IBundleJsFileProvider, IBundleStyleFileProvider
    {
        public int Order
        {
            get { return int.MinValue; }
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
                    "/Scripts/jquery-2.2.3.js",
                    "/Scripts/angular.js",
                    "/Scripts/angular-animate.js",
                    "/Scripts/angular-sanitize.js",
                    "/Scripts/angular-resource.js",
                    "/Scripts/angular-ui-router.js",
                    "/Scripts/angular-touch.js",
                    "/Scripts/angular-translate.js",
                    "/Rushan/Scripts/loading-bar.js",
                    "/Rushan/Scripts/ocLazyLoad.js",
                    "/Scripts/ui-grid.js",
                    "/Scripts/marked.js",
                    "/Scripts/markdown_preview.js",
                    "/Rushan/Scripts/ui-bootstrap-tpls.js",
                    "/Rushan/Scripts/autosize.js",
                    "/Rushan/Scripts/jquery.mousewheel.min.js",
                    "/Rushan/Scripts/jquery.mCustomScrollbar.js",
                    "/Rushan/Scripts/jquery.mCustomScrollbar.concat.min.js",
                    "/Rushan/Scripts/sweetalert2.js",
                    "/Rushan/Scripts/waves.js",
                    "/Rushan/Scripts/bootstrap-growl.min.js",
                    "/Rushan/Scripts/input-mask.min.js",
                    "/Rushan/Scripts/jquery.nouislider.js",
                    "/Rushan/Scripts/nouislider.js",
                    "/Rushan/Scripts/moment.js",
                    "/Rushan/Scripts/bootstrap-datetimepicker.min.js",
                    "/Rushan/Scripts/summernote.min.js",
                    "/Rushan/Scripts/fileinput.min.js",
                    "/Rushan/Scripts/chosen.jquery.js",
                    "/Rushan/Scripts/angular-chosen.js",
                    "/Rushan/Scripts/angular-farbtastic.js",
                    "/Rushan/Scripts/scrollbars.min.js",
                    "/Scripts/angular-recaptcha.js",
                    "/Scripts/ng-file-upload-all.js",
                    "/Scripts/ng-infinite-scroll.js",
                    "/Scripts/angular-scroll.js",

                };
            }
        }

        public IEnumerable<string> StyleFiles
        {
            get
            {
                if (!DependencyResolver.Current.GetService<ApplicationConfiguration>().RegisterFrameworkDefaultBundleProviders)
                {
                    return new List<string>();
                }
                return new List<string>
                {
                    "/Rushan/Styles/animate.css",
                    "/Rushan/Styles/material-design-iconic-font.css",
                    "/Rushan/Styles/sweetalert2.css",
                    "/Rushan/Styles/loading-bar.css",
                    "/Rushan/Styles/jquery.mCustomScrollbar.css",
                    "/Rushan/Styles/jquery.nouislider.css",
                    "/Rushan/Styles/farbtastic.css",
                    "/Rushan/Styles/summernote.css",
                    "/Rushan/Styles/bootstrap-datetimepicker.min.css",
                    "/Rushan/Styles/chosen.min.css",
                    "/Rushan/Styles/fullcalendar.min.css",
                    "/Rushan/Styles/app.min.1.css",
                    "/Rushan/Styles/app.min.2.css",
                    "/Rushan/Styles/demo.css",
                    "/Content/ui-grid.css"
                };
            }
        }
    }
}