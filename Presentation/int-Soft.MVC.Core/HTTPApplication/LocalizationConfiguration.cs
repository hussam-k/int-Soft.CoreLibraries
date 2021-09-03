using System.Collections.Generic;
using System.Linq;
using intSoft.MVC.Core.Common;

namespace intSoft.MVC.Core.HTTPApplication
{
    public class LocalizationConfiguration
    {
        /// <summary>
        /// The default language of the application, by default it is "en"
        /// </summary>
        public string PreferredLanguage { get; set; }

        /// <summary>
        /// a comma seperated list of languages' shortcuts such that "en,ar,de".
        /// by default it is "en,ar"
        /// </summary>
        public string AcceptedLanguages { get; set; }

        public LocalizationConfiguration()
        {
            PreferredLanguage = DefaultValuesBase.DefaultLanguage;
            AcceptedLanguages = DefaultValuesBase.DefaultAcceptedLanguages;
        }
    }
}