using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelpers.Editors;
using intSoft.MVC.Core.HTMLHelpers.Listing;
using intSoft.MVC.Core.HTMLHelperSettings;
using intSoft.MVC.Core.HTMLHelperSettings.Lising;
using intSoft.MVC.Core.HTMLHelperSettings.MasterDetail;

namespace intSoft.MVC.Core.HTMLHelpers.MasterDetail
{
    public static class IntSoftDetailGridExtension
    {
        public static MvcHtmlString DetailGridForMaster(this IntSoftExtension helper)
        {
            var modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            var listSetting = modelMetadata.GetCustomAttribute<ListSetting>();

            if (listSetting == null) return null;

            var pagerSetting = modelMetadata.GetCustomAttribute<PagerSetting>();
            pagerSetting = pagerSetting ?? new PagerSetting();

            listSetting.HeaderContent = helper.GetListHeaderContent(listSetting.ListHeaderContentProviderType);
            listSetting.ListBodyContent = MvcHtmlString.Create(helper.TableForModel().ToString()  + helper.Pager(pagerSetting) + helper.AntiForgeryToken());

            return helper.DetailList(listSetting, modelMetadata.GetCustomAttribute<DetailForMasterSetting>());
        }

        public static MvcHtmlString DetailUiGridForMaster(this IntSoftExtension helper)
        {
            var modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            var listSetting = modelMetadata.GetCustomAttribute<ListSetting>();

            if (listSetting == null) return null;

            var pagerSetting = modelMetadata.GetCustomAttribute<PagerSetting>();
            pagerSetting = pagerSetting ?? new PagerSetting();

            listSetting.HeaderContent = helper.GetListHeaderContent(listSetting.ListHeaderContentProviderType);
            listSetting.ListBodyContent = MvcHtmlString.Create(helper.UiGrid(new UiGridSetting(listSetting.UiGridDefinitionController, listSetting.UiGridDefinitionAction)).ToString() + helper.Pager(pagerSetting) + helper.AntiForgeryToken());

            return helper.DetailList(listSetting, modelMetadata.GetCustomAttribute<DetailForMasterSetting>());
        }

        public static MvcHtmlString DetailList(this IntSoftExtension helper, ListSetting setting, 
            DetailForMasterSetting detailForMasterSetting)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.DetailList),
                setting.Controller,
                setting.CreateState,
                setting.UpdateState,
                setting.DisplayState,
                setting.ListServerAction,
                detailForMasterSetting.MasterPrimaryKeyPropertyName.ToCamelCase(),
                detailForMasterSetting.DetailForignKeyPropertyName.ToCamelCase(),
                setting.HeaderClass,
                setting.HeaderContent,
                setting.ListBodyClass,
                setting.ListBodyContent,
                helper.AntiForgeryToken()
                ));
        }

    }
}