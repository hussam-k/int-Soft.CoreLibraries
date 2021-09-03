using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Helpers;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelperSettings;
using intSoft.MVC.Core.HTMLHelperSettings.Lising;

namespace intSoft.MVC.Core.HTMLHelpers.Listing
{
    public static class IntSoftUiGridExtension
    {
        #region Html Template Helper

        public static MvcHtmlString UiGrid(this IntSoftExtension helper, UiGridSetting setting)
        {
            return
                new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.UiGrid), setting.UiGridDefinitionController,
                    setting.UiGridDefinitionAction, setting.Name, setting.Class, AngularAttributeTemplates.UiGridDefinitionName));
        }

        public static MvcHtmlString UiGridColumnAction(MvcHtmlString items)
        {
            return
                new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.UiGridColumnAction), items));
        }

        public static MvcHtmlString UiGridColumnAction(string items)
        {
            return UiGridColumnAction(MvcHtmlString.Create(items));
        }

        public static MvcHtmlString UiGridColumnActionItem(TableColumnActionItemSetting setting)
        {
            return
                new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.UiGridColumnActionItem),
                    setting.FunctionName,
                    setting.Class,
                    setting.Title,
                    setting.Attributes));
        }

        public static MvcHtmlString BuildActionColumn(Type columnActionItemSettingListProviderType)
        {
            var columnActionItems = new StringBuilder();
            var provider = DependencyResolver.Current.GetService(columnActionItemSettingListProviderType) as IColumnActionItemSettingListProvider;
            var columnActionItemSettingList = provider != null ? provider.GetColumnActionItemSettingList() : new List<TableColumnActionItemSetting>();

            foreach (var actionItemSetting in columnActionItemSettingList)
                columnActionItems.Append(UiGridColumnActionItem(actionItemSetting));

            return UiGridColumnAction(columnActionItems.ToString());
        }


        #endregion
    }
}