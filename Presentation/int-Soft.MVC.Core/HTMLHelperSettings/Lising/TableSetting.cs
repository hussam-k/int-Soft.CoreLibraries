using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Helpers;
using intSoft.MVC.Core.Helpers.ColumnActionItemSettingListProviders;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public class TableSetting : SettingsBase
    {
        public TableSetting()
        {
            Class = DefaultCssClasses.Table;
            ColumnActionItemSettingListProviderType = typeof(DefaultColumnActionItemSettingListProvider);
        }
        public MvcHtmlString HeaderContent { get; set; }
        public MvcHtmlString BodyContent { get; set; }
        public Type ColumnActionItemSettingListProviderType { get; set; }

    }

}
