using System.Web.Mvc;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.Helpers.ColumnActionItemSettingListProviders;
using intSoft.MVC.Core.Helpers.ListHeaderContentProviders;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.HTMLHelpers.Listing
{
    public static class IntSoftDraftListModelExtension
    {
        public static MvcHtmlString DraftsForModel(this IntSoftExtension helper)
        {
            var modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            var listSetting = modelMetadata.GetCustomAttribute<ListSetting>();
            var tableSetting = modelMetadata.GetCustomAttribute<TableSetting>() ?? new TableSetting();

            listSetting.ListServerAction = listSetting.DraftsServerAction;
            tableSetting.ColumnActionItemSettingListProviderType = typeof (ApprovableColumnActionItemSettingListProvider);

            listSetting.HeaderContent = helper.ListHeaderForModel(new ListHeaderSetting
            {
                Title = listSetting.Title,
                Legend = listSetting.Legend,
                ListHeaderContentProviderType = typeof (DraftListHeaderContentProvider)
            });

            listSetting.ListBodyContent = helper.TableForProperties(tableSetting, true);

            return helper.List(listSetting);
        }
    }
}