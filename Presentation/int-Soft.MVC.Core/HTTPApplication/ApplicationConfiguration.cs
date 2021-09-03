using System;
using System.Threading;
using System.Web.Configuration;
using IntSoft.DAL.Common;
using intSoft.Res.DisplayNames;

namespace intSoft.MVC.Core.HTTPApplication
{
    public class ApplicationConfiguration : IConfiguration
    {
        public ApplicationConfiguration()
        {
            ResourceType = typeof(DisplayNames);
            PrivateCaptchaKey = string.IsNullOrEmpty(WebConfigurationManager.AppSettings["PrivateCaptchaKey"])
                ? "6LdAiicTAAAAANrgKsyA2HFpR3XJIbHD96kwMnFA"
                : WebConfigurationManager.AppSettings["PrivateCaptchaKey"];
            PublicCaptchaKey = string.IsNullOrEmpty(WebConfigurationManager.AppSettings["PublicCaptchaKey"])
                ? "6LdAiicTAAAAAEnwOefZJ3OuPMs-Wk0YfM4D83UA"
                : WebConfigurationManager.AppSettings["PublicCaptchaKey"];
            
            ActivationEmailFromAddress = string.IsNullOrEmpty(WebConfigurationManager.AppSettings["ActivationEmailFromAddress"])
                ? "Accounts@Mentoria.me"
                : WebConfigurationManager.AppSettings["ActivationEmailFromAddress"];
            
            EmailProviderHostAddress = string.IsNullOrEmpty(WebConfigurationManager.AppSettings["EmailProviderHostAddress"])
                ? "smtp.sendgrid.net"
                : WebConfigurationManager.AppSettings["EmailProviderHostAddress"];
            EmailProviderPassword = string.IsNullOrEmpty(WebConfigurationManager.AppSettings["EmailProviderPassword"])
                ? "sap@$*@$*!#!(()"
                : WebConfigurationManager.AppSettings["EmailProviderPassword"];
            EmailProviderUsername = string.IsNullOrEmpty(WebConfigurationManager.AppSettings["EmailProviderUsername"])
                ? "Hussam.k"
                : WebConfigurationManager.AppSettings["EmailProviderUsername"];
            EmailProviderPort = string.IsNullOrEmpty(WebConfigurationManager.AppSettings["EmailProviderPort"])
                ? 587
                : int.Parse(WebConfigurationManager.AppSettings["EmailProviderPort"]);
            EmailProviderUseSSL = string.IsNullOrEmpty(WebConfigurationManager.AppSettings["EmailProviderUseSSL"]) || bool.Parse(WebConfigurationManager.AppSettings["EmailProviderUseSSL"]);
            ScriptsUrl = string.IsNullOrEmpty(WebConfigurationManager.AppSettings["ScriptsUrl"])
                ? "http://localhost:44"
                : WebConfigurationManager.AppSettings["ScriptsUrl"];
            Localization = new LocalizationConfiguration();
            RegisterFrameworkDefaultBundleProviders = true;
        }

        public string Namespace { get; set; }

        public string PublicCaptchaKey { get; set; }

        public string PrivateCaptchaKey { get; set; }

        public string EmailProviderUsername { get; set; }

        public string EmailProviderHostAddress { get; set; }

        public string EmailProviderPassword { get; set; }

        public bool EmailProviderUseSSL { get; set; }

        public int EmailProviderPort { get; set; }

        public string SMSServiceUrl { get; set; }

        public string SMSKey { get; set; }

        public string SMSSecret { get; set; }

        public string ActivationEmailFromAddress { get; set; }

        public Type ResourceType { get; set; }

        public string ScriptsUrl { get; set; }

        public LocalizationConfiguration Localization { protected set; get; }

        public virtual bool IsRightToLeft
        {
            get { return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar"; }
        }
        public bool RegisterFrameworkDefaultBundleProviders { get; set; }
    }
}