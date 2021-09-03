using System;
using System.Collections.Generic;
using System.Web.Mvc;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.Helpers;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelpers.Editors;
using intSoft.MVC.Core.HTMLHelperSettings;
using intSoft.MVC.Core.HTMLHelperSettings.Lising;

namespace intSoft.MVC.Core.HTMLHelpers.Listing
{
    public static class IntSoftListModelExtension
    {
        public static MvcHtmlString ListingForModel(this IntSoftExtension helper)
        {
            var modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            var listSetting = modelMetadata.GetCustomAttribute<ListSetting>();
            
            if (listSetting == null) return null;

            PrepareListSetting(helper, listSetting, helper.TableForModel());

            return helper.List(listSetting);
        }

        public static MvcHtmlString UiGridListingForModel(this IntSoftExtension helper)
        {
            var modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            var listSetting = modelMetadata.GetCustomAttribute<ListSetting>();

            if (listSetting == null) return null;

            PrepareListSetting(helper, listSetting, helper.UiGrid(new UiGridSetting(listSetting.UiGridDefinitionController, listSetting.UiGridDefinitionAction)));

            return helper.List(listSetting);
        }
        public static MvcHtmlString ListHeaderForModel(this IntSoftExtension helper, ListHeaderSetting listHeaderSetting)
        {
            listHeaderSetting.Title = listHeaderSetting.GetStringFromResourceType(listHeaderSetting.Title);
            listHeaderSetting.Legend = listHeaderSetting.GetStringFromResourceType(listHeaderSetting.Legend);
            listHeaderSetting.HeaderContent =
                helper.GetListHeaderContent(listHeaderSetting.ListHeaderContentProviderType);

            return helper.ListHeader(listHeaderSetting);
        }
        public static MvcHtmlString GetListHeaderContent(this IntSoftExtension helper,Type listHeaderContentProviderType)
        {
            MvcHtmlString listHeaderContent = MvcHtmlString.Empty;
            var listHeaderContentProvider =
                DependencyResolver.Current.GetService(listHeaderContentProviderType) as IListHeaderContentProvider;

            if (listHeaderContentProvider != null)
                listHeaderContent = listHeaderContentProvider.GetListHeaderContent(helper);

            return listHeaderContent;
        }

        private static void PrepareListSetting(IntSoftExtension helper, ListSetting listSetting, MvcHtmlString listBodyContent)
        {
            var modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            var pagerSetting = modelMetadata.GetCustomAttribute<PagerSetting>();

            pagerSetting = pagerSetting ?? new PagerSetting();

            listSetting.HeaderContent = helper.ListHeaderForModel(new ListHeaderSetting
            {
                Title = listSetting.Title,
                Legend = listSetting.Legend,
                ListHeaderContentProviderType = listSetting.ListHeaderContentProviderType
            });

            listSetting.ListBodyContent = MvcHtmlString.Create(listBodyContent.ToString() + helper.Pager(pagerSetting) + helper.AntiForgeryToken());
        }
    }

}