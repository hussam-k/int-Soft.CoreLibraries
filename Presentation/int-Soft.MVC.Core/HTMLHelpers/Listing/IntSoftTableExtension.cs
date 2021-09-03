using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using intSoft.MVC.Core.Attributes;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.Enumerations;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.Helpers;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.HTMLHelpers.Listing
{
    public static class IntSoftTableExtension
    {
        public static MvcHtmlString TableForModel(this IntSoftExtension helper, bool isDrafts = false)
        {
            ModelMetadata modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            TableSetting tableSetting = modelMetadata.GetCustomAttribute<TableSetting>() ?? new TableSetting();

            return helper.TableForProperties(tableSetting, isDrafts);
        }
        public static MvcHtmlString TableForProperties(this IntSoftExtension helper, TableSetting tableSetting, bool isDrafts = false)
        {
            ModelMetadata modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            IEnumerable<ModelMetadata> propertiesModelMetaData = modelMetadata.Properties;

            var headerContentBuilder = new StringBuilder();
            var bodyContentBuilder = new StringBuilder();

            foreach (ModelMetadata property in propertiesModelMetaData.Where(x => !x.IsComplexType))
            {
                DisplayAtAttribute displayAtAttribute = property.GetCustomAttribute<DisplayAtAttribute>() ??
                                                        new DisplayAtAttribute();
                if (!displayAtAttribute.DisplayAt.HasFlag(isDrafts ? DisplayAt.Drafts : DisplayAt.List) &&
                    !displayAtAttribute.DisplayAt.HasFlag(DisplayAt.All))
                {
                    continue;
                }

                string columnHeaderText = property.GetPropertyTitleFromDisplayAttribute();

                headerContentBuilder.Append(helper.TableColumnHeader(columnHeaderText));
                bodyContentBuilder.Append(
                    helper.TableColumnData(
                        string.Format("model.{0}", property.PropertyName.ToCamelCase()).ToAngularExpression()));
            }

            MvcHtmlString columnAction = BuildActionColumn(helper, tableSetting.ColumnActionItemSettingListProviderType);

            bodyContentBuilder.Append(columnAction);

            tableSetting.BodyContent = MvcHtmlString.Create(bodyContentBuilder.ToString());
            tableSetting.HeaderContent = MvcHtmlString.Create(headerContentBuilder.ToString());

            return helper.Table(tableSetting);
        }
        public static MvcHtmlString BuildActionColumn(this IntSoftExtension helper, Type columnActionItemSettingListProviderType)
        {
            var columnActionItems = new StringBuilder();
            var provider = DependencyResolver.Current.GetService(columnActionItemSettingListProviderType) as IColumnActionItemSettingListProvider;
            var columnActionItemSettingList = provider != null ? provider.GetColumnActionItemSettingList() : new List<TableColumnActionItemSetting>();

            foreach (var actionItemSetting in columnActionItemSettingList)
                columnActionItems.Append(helper.TableColumnActionItem(actionItemSetting));

            return helper.TableColumnAction(columnActionItems.ToString());
        }

        #region Html Template Helper
        public static MvcHtmlString Table(this IntSoftExtension helper, TableSetting setting)
        {
            return
                new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.Table), setting.Class,
                    setting.HeaderContent, setting.BodyContent));
        }

        public static MvcHtmlString TableColumnHeader(this IntSoftExtension helper, string headerText)
        {
            return
                new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.TableColumnHeader),
                    headerText));
        }

        public static MvcHtmlString TableColumnData(this IntSoftExtension helper, string angularDataExpression)
        {
            return
                new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.TableColumnData),
                    angularDataExpression));
        }

        public static MvcHtmlString TableColumnAction(this IntSoftExtension helper, MvcHtmlString items)
        {
            return
                new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.TableColumnAction), items));
        }

        public static MvcHtmlString TableColumnAction(this IntSoftExtension helper, string items)
        {
            return helper.TableColumnAction(MvcHtmlString.Create(items));
        }

        public static MvcHtmlString TableColumnActionItem(this IntSoftExtension helper,
            TableColumnActionItemSetting setting)
        {
            return
                new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.TableColumnActionItem),
                    setting.FunctionName, 
                    setting.Class, 
                    setting.Title,
                    setting.Attributes));
        } 
        #endregion
    }
}