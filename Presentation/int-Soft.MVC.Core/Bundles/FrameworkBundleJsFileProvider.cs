using System.Collections.Generic;
using System.Web.Mvc;
using intSoft.MVC.Core.HTTPApplication;

namespace intSoft.MVC.Core.Bundles
{
    public class FrameworkBundleJsFileProvider : IBundleJsFileProvider, IBundleStyleFileProvider
    {
        public int Order
        {
            get { return int.MinValue +1; }
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
                    "/IntSoft/Scripts/intSoftApp.js",
                    "/IntSoft/Scripts/intSoftConfig.js",

                    "/IntSoft/Scripts/interceptors/intSoftHTTPInterceptor.js",
                    "/IntSoft/Scripts/providers/IntSoftModalStateProvider.js",
                    "/IntSoft/Scripts/providers/IntSoftValuesProvider.js",
                    "/IntSoft/Scripts/dialog/translationConfig.js",

                    "/IntSoft/Scripts/services/intSoftAjaxService.js",
                    "/IntSoft/Scripts/services/intSoftCrudService.js",
                    "/IntSoft/Scripts/services/intSoftLocalizationService.js",
                    "/IntSoft/Scripts/services/intSoftNotificationService.js",
                    "/IntSoft/Scripts/services/intSoftAlertService.js",
                    "/IntSoft/Scripts/services/intSoftLoginService.js",
                    "/IntSoft/Scripts/services/intSoftModalService.js",
                    "/IntSoft/Scripts/services/intSoftMarkdownService.js",
                    
                    "/IntSoft/Scripts/directives/intSoftListDirective.js",
                    "/IntSoft/Scripts/directives/intSoftMasterListDirective.js",
                    "/IntSoft/Scripts/directives/intSoftDetailListDirective.js",
                    "/IntSoft/Scripts/directives/intSoftFormDirective.js",
                    "/IntSoft/Scripts/directives/intSoftDisplayDirective.js",
                    "/IntSoft/Scripts/directives/intSoftSidebarDirective.js",
                    "/IntSoft/Scripts/directives/intSoftDateDirective.js",
                    "/IntSoft/Scripts/directives/validationDirectives/intSoftCompareDirective.js",
                    "/IntSoft/Scripts/directives/intSoftAccountLoginDirective.js",
                    "/IntSoft/Scripts/directives/intSoftAccountReactivateDirective.js",
                    "/IntSoft/Scripts/directives/intSoftAccountConfirmEmailDirective.js",
                    "/IntSoft/Scripts/directives/intSoftAccountForgotPasswordDirective.js",
                    "/IntSoft/Scripts/directives/intSoftAccountResetPasswordDirective.js",
                    "/IntSoft/Scripts/directives/intSoftAccountVerifyPhoneDirective.js",
                    "/IntSoft/Scripts/directives/intSoftNestedComboboxDateDirective.js",
                    "/IntSoft/Scripts/directives/intSoftUiGridDirective.js",

                    "/IntSoft/Scripts/controllers/intSoftMainController.js",
                    "/IntSoft/Scripts/controllers/intSoftHeaderController.js",
                    "/IntSoft/Scripts/controllers/intSoftAccountChangeEmailController.js",
                    "/IntSoft/Scripts/controllers/intSoftAccountChangePasswordController.js",
                    "/IntSoft/Scripts/controllers/intSoftAccountChangePhoneNumberController.js",


                    "/Rushan/Scripts/services/rushan.services.scrollService.js",
                    "/Rushan/Scripts/directives/rushan.directives.inputMask.js",
                    "/Rushan/Scripts/directives/rushan.directives.fgLine.js",
                    "/Rushan/Scripts/directives/rushan.directives.cOverflow.js"

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
                    "/IntSoft/Styles/intSoft.app1.css"
                };
            }
        }
    }
}