using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelpers.Common;
using intSoft.MVC.Core.HTMLHelpers.Editors;
using intSoft.MVC.Core.HTMLHelperSettings.MasterDetail;

namespace intSoft.MVC.Core.HTMLHelpers.MasterDetail
{
    public static class IntSoftMasterDetailListModelExtension
    {
        public static MvcHtmlString MasterDetailListForModel(this IntSoftExtension helper)
        {
            var masterListBody = helper.MasterListForModel();
            var masterDisplayBody = helper.DisplayForModel();
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.MasterDetail),
                masterListBody,
                masterDisplayBody));
        }

        public static MvcHtmlString MasterListForModel(this IntSoftExtension helper)
        {
            var modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            var listSetting = modelMetadata.GetCustomAttribute<MasterListSetting>();
            if (listSetting == null) return null;
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.MasterList),
                listSetting.Controller.ToCamelCase(),
                listSetting.CreateState,
                listSetting.DraftsState,
                listSetting.ListServerAction,
                listSetting.PageSize,
                listSetting.ItemTitleExpression,
                listSetting.ItemLegendExpression,
                helper.AntiForgeryToken(),
                listSetting.ItemIcon,
                listSetting.GetStringFromResourceType(listSetting.MasterListTitle)
                ));
        }
    }
}