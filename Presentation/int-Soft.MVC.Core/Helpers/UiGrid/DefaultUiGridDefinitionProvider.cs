using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using intSoft.MVC.Core.Attributes;
using intSoft.MVC.Core.Common;
using intSoft.MVC.Core.Enumerations;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.HTMLHelpers.Listing;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.Helpers.UiGrid
{
    public class DefaultUiGridDefinitionProvider : IUiGridDefinitionProvider
    {
        public UiGridDefinition GetUiGridDefinition(Type type, bool isDrafts = false)
        {
            var listSetting = type.GetCustomAttribute<ListSetting>();
            if (listSetting == null)
                throw new Exception("Must define List setting attribute on class");

            var gridDef = new UiGridDefinition { ColumnDefinitions = new List<UiGridColumnDefinition>() };
            var properties = type.GetProperties();
            
            foreach (var property in properties)
            {
                var displayAtAttribute = property.GetCustomAttribute<DisplayAtAttribute>() ?? new DisplayAtAttribute();

                if (!displayAtAttribute.DisplayAt.HasFlag(isDrafts ? DisplayAt.Drafts : DisplayAt.List) &&
                    !displayAtAttribute.DisplayAt.HasFlag(DisplayAt.All))
                {
                    continue;
                }

                var displayAttr = property.GetCustomAttribute<DisplayAttribute>();
                var columnHeaderText = ResourceHelper.GetPropertyTitleFromDisplayAttribute(property.Name, displayAttr);

                var columnDefinition = new UiGridColumnDefinition(property.Name.ToCamelCase(), columnHeaderText);
                OnColumnDefinition(new UiGridColumnDefinitionContext { PropertyName = property.Name, ColumnDefinition = columnDefinition });

                if (property.PropertyType == typeof (DateTime))
                {
                    columnDefinition.Type = "date";
                    columnDefinition.CellFilter = "date:\'yyyy-MM-dd\'";
                }

                gridDef.ColumnDefinitions.Add(columnDefinition);
            }

            if (listSetting.HasActionColumn)
            {
                gridDef.ColumnDefinitions.Add(new UiGridColumnDefinition(DefaultValuesBase.ActionColumnName)
                {
                    CellTemplate = IntSoftUiGridExtension.BuildActionColumn(listSetting.ColumnActionItemSettingListProviderType).ToString(),
                    EnableCellEdit = false,
                    EnableFiltering = false,
                    EnableColumnResizing = false,
                    EnableSorting = false,
                    ShowHeader = false,
                    HeaderCellTemplate = "<div></div>"
                });
            }
            
            OnGridDefinition(new UiGridDefinitionContext()
            {
                GridDefinition = gridDef,
                Type = type
            });
            
            return gridDef;
        }

        public virtual void OnGridDefinition(UiGridDefinitionContext gridDefinitionContext)
        {
        }

        public virtual void OnColumnDefinition(UiGridColumnDefinitionContext definitionContext)
        {
        }
    }
}