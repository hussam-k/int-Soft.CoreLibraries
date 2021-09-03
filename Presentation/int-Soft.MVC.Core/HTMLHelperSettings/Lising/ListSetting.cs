using System;
using System.Web.Mvc;
using intSoft.MVC.Core.Common;
using intSoft.MVC.Core.Controllers;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.Helpers.ColumnActionItemSettingListProviders;
using intSoft.MVC.Core.Helpers.ListHeaderContentProviders;
using intSoft.MVC.Core.Security;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public class ListSetting : SettingsBase
    {
        private string _listServerAction;

        public ListSetting(Type controllerType)
            : this(controllerType.Name)
        {
            HasApprove = controllerType.IsSubClassOfGeneric(typeof (ApprovableCrudControllerBase<,,>));
            IsDetailList = controllerType.IsSubClassOfGeneric(typeof(DetailCrudControllerBase<,,>));
            ColumnActionItemSettingListProviderType = typeof (DefaultColumnActionItemSettingListProvider);
            ListHeaderContentProviderType = HasApprove
                ? typeof (ApprovableListHeaderContentProvider)
                : typeof (DefaultListHeaderContentProvider);
        }

        private ListSetting(string controllerName)
        {
            Controller = controllerName.RemoveSubString(DefaultValuesBase.ControllerNameSuffix);
            Legend = string.Format(DefaultValuesBase.LegendPattern, Controller);
            Title = string.Format(DefaultValuesBase.TitlePattern, Controller);
            UiGridDefinitionController = Controller;
            UiGridDefinitionAction = DefaultValuesBase.DefaultUiGridDefinitionAction;
            string controllerNameCamelCase = Controller.ToCamelCase();
            Name = string.Format(DefaultJavaScriptNames.FormName, controllerNameCamelCase);
            CreateState = string.Format(DefaultJavaScriptNames.CreateState, controllerNameCamelCase);
            UpdateState = string.Format(DefaultJavaScriptNames.UpdateState, controllerNameCamelCase);
            DisplayState = string.Format(DefaultJavaScriptNames.DisplayState, controllerNameCamelCase);
            DraftsState = string.Format(DefaultJavaScriptNames.DraftsState, controllerNameCamelCase);
            ListState = controllerNameCamelCase;
            DraftsServerAction = DefaultValuesBase.DraftsAction;
            Class = DefaultCssClasses.List;
            HeaderClass = DefaultCssClasses.ListHeader;
            ListBodyClass = DefaultCssClasses.ListBody;
            HasActionColumn = true;
        }

        public string Controller { get; set; }
        public string UiGridDefinitionController { get; set; }
        public string UiGridDefinitionAction { get; set; }
        public string CreateState { get; set; }
        public string UpdateState { get; set; }
        public string DisplayState { get; set; }
        public string DraftsState { get; set; }
        public string ListState { get; set; }
        public string DetailState { get; set; }
        public string HeaderClass { get; set; }

        public string ListServerAction
        {
            get
            {
                return string.IsNullOrEmpty(_listServerAction)
                    ? (IsDetailList ? DefaultValuesBase.DetailListAction : DefaultValuesBase.ListAction)
                    : _listServerAction;
            }
            set { _listServerAction = value; }
        }

        public string DraftsServerAction { get; set; }
        public string Title { get; set; }
        public string Legend { get; set; }
        public string ListBodyClass { get; set; }
        public bool HasApprove { get; set; }
        public MvcHtmlString HeaderContent { get; set; }
        public MvcHtmlString ListBodyContent { get; set; }
        public Type ListHeaderContentProviderType { get; set; }
        public Type ColumnActionItemSettingListProviderType { get; set; }
        public bool IsDetailList { get; set; }

        public bool HasActionColumn { get; set; }
    }
}