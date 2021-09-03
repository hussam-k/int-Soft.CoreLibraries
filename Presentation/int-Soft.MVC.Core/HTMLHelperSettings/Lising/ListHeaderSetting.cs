using System;
using System.Web.Mvc;
using intSoft.MVC.Core.Common;
using intSoft.MVC.Core.Controllers;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.Helpers;
using intSoft.MVC.Core.Helpers.ListHeaderContentProviders;
using intSoft.MVC.Core.Security;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public class ListHeaderSetting : SettingsBase
    {
        public ListHeaderSetting()
        {
            ListHeaderContentProviderType = typeof (DefaultListHeaderContentProvider);
        }
        public string Title { get; set; }
        public string Legend { get; set; }
        public MvcHtmlString HeaderContent { get; set; }
        public Type ListHeaderContentProviderType { get; set; }
    }
}