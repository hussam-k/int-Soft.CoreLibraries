using System.Linq;
using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.Helpers;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelpers.Common;
using intSoft.MVC.Core.HTMLHelperSettings.MasterDetail;
using intSoft.MVC.Core.HTTPApplication;

namespace intSoft.MVC.Core.HTMLHelpers.MasterDetail
{
    public static class IntSoftMasterDisplayExtension
    {
        public static MvcHtmlString DetailTabsForMaster(this IntSoftExtension helper, MasterListSetting setting)
        {
            var masterInformationTab = helper.MasterInformationTab(setting);
            var detailTabs = helper.DetailTabs(setting);
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.DetailsTabSet),
                masterInformationTab,
                detailTabs));
        }

        public static MvcHtmlString MasterInformationTab(this IntSoftExtension helper, MasterListSetting setting)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.MasterInfoTab),
                ResourceHelper.GetStringFromResourceType(DependencyResolver.Current.GetService<ApplicationConfiguration>().ResourceType, setting.MasterDisplayTabTitle),
                string.IsNullOrEmpty(setting.MasterDisplayTabIcon) ? setting.ItemIcon : setting.MasterDisplayTabIcon,
                helper.DisplayForProperties()));
        }

        public static MvcHtmlString DetailTabs(this IntSoftExtension helper, MasterListSetting setting)
        {
            var modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            var detailList = modelMetadata.GetCustomAttributes<MasterForDetailSetting>();
            var detailTabs = detailList.Aggregate("",
                (current, detailModelSetting) => current + helper.DetailTab(setting, detailModelSetting));
            return MvcHtmlString.Create(detailTabs);
        }

        public static MvcHtmlString DetailTab(this IntSoftExtension helper, MasterListSetting masterSetting
            , MasterForDetailSetting masterForDetailSetting)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.DetailsTab),
                ResourceHelper.GetStringFromResourceType(DependencyResolver.Current.GetService<ApplicationConfiguration>().ResourceType, masterForDetailSetting.TabTitle),
                masterForDetailSetting.TabIcon,
                masterForDetailSetting.GenerateDetailStateName(masterSetting.Controller)));
        }
    }
}