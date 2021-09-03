using System;
using System.Linq;
using System.Web.Mvc;
using intSoft.MVC.Core.Attributes;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.Enumerations;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.Helpers;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelpers.Editors;
using intSoft.MVC.Core.HTMLHelpers.MasterDetail;
using intSoft.MVC.Core.HTMLHelperSettings;
using intSoft.MVC.Core.HTMLHelperSettings.MasterDetail;

namespace intSoft.MVC.Core.HTMLHelpers.Common
{
    public static class IntSoftModelDisplayExtension
    {
        public static MvcHtmlString DisplayForModel(this IntSoftExtension helper)
        {
            var modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            var displaySettings = modelMetadata.GetCustomAttribute<DisplaySetting>();
            if (displaySettings == null)
            {
                throw new Exception("Missing DisplaySetting");
            }
            var masterSettings = modelMetadata.GetCustomAttribute<MasterListSetting>();
            return helper.Display(displaySettings, masterSettings);
        }

        public static MvcHtmlString Display(this IntSoftExtension helper, DisplaySetting setting, MasterListSetting masterSetting = null)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.Display),
                setting.Controller,
                setting.CreateState,
                setting.UpdateState,
                setting.ListState,
                helper.DisplayHeader(setting),
                masterSetting == null ? helper.DisplayForProperties() : helper.DetailTabsForMaster(masterSetting)
                ));
        }

        public static MvcHtmlString DisplayForProperties(this IntSoftExtension helper)
        {
            var modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            var propertiesModelMetaData = modelMetadata.Properties;
            var propertyEditors = "";
            foreach (var metadata in propertiesModelMetaData)
            {
                var displayAtAttribute = metadata.GetCustomAttribute<DisplayAtAttribute>() ?? new DisplayAtAttribute();
                if (!displayAtAttribute.DisplayAt.HasFlag(DisplayAt.Display) &&
                    !displayAtAttribute.DisplayAt.HasFlag(DisplayAt.All)) continue;
                propertyEditors = propertyEditors + helper.DisplayForProperty(metadata);
            }
            return new MvcHtmlString(propertyEditors + helper.AntiForgeryToken());
        }

        public static MvcHtmlString DisplayForProperty(this IntSoftExtension helper, ModelMetadata property)
        {
            var labelText = property.GetPropertyTitleFromDisplayAttribute();
            var labelContent = helper.Label(property.PropertyName, labelText);
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.PropertyDisplay),
                property.PropertyName.ToCamelCase(),
                labelContent));
        }
        public static MvcHtmlString DisplayHeaderProperties(this IntSoftExtension helper, DisplaySetting setting)
        {
            var modelMetadata = helper.HtmlHelper.ViewData.ModelMetadata;
            var propertiesModelMetaData = modelMetadata.Properties;
            var propertyEditors = "";
            foreach (var metadata in propertiesModelMetaData)
            {
                var displayAtAttribute = metadata.GetCustomAttribute<DisplayAtAttribute>() ?? new DisplayAtAttribute();
                if (!displayAtAttribute.DisplayAt.HasFlag(DisplayAt.DisplayHeader) &&
                    !displayAtAttribute.DisplayAt.HasFlag(DisplayAt.All)) continue;
                propertyEditors = propertyEditors + helper.DisplayHeaderPropertiy(metadata, setting);
            }
            return new MvcHtmlString(propertyEditors + helper.AntiForgeryToken());
        }

        public static MvcHtmlString DisplayHeaderPropertiy(this IntSoftExtension helper, ModelMetadata property, DisplaySetting setting)
        {
            var labelText = property.GetPropertyTitleFromDisplayAttribute();
            var labelContent = helper.Label(property.PropertyName, labelText);
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.PropertyHeaderDisplay),
                property.PropertyName.ToCamelCase(),
                setting.ShowLabelInHeader,
                labelContent));
        }

        public static MvcHtmlString DisplayHeader(this IntSoftExtension helper, DisplaySetting setting)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.DisplayHeader),
                setting.TitleExpression,
                helper.DisplayHeaderProperties(setting),
                helper.DisplayActions(setting),
                setting.ActionListProvider == null
                ));
        }

        public static MvcHtmlString DisplayActions(this IntSoftExtension helper, DisplaySetting setting)
        {
            var actions = "";
            var buttonListProvider = setting.ActionListProvider == null
                ? null
                : DependencyResolver.Current.GetService(setting.ActionListProvider) as IActionListProvider;
            if (buttonListProvider != null)
            {
                var buttonList = buttonListProvider.GetActionList();
                actions = buttonList.Aggregate(actions,
                    (current, button) => current + (helper.Action(button) + " "));
            }
            return MvcHtmlString.Create(actions);
        }

        public static MvcHtmlString Action(this IntSoftExtension helper, ActionSetting settings)
        {
            return new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.Action),
                settings.Text,
                settings.NavigationState,
                settings.NavigationStateParameters,
                settings.ClickAction));
        }
    }
}