using System;
using intSoft.MVC.Core.Common;
using intSoft.MVC.Core.Controllers;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Extensions;

namespace intSoft.MVC.Core.HTMLHelperSettings.MasterDetail
{
    public class MasterListSetting : SettingsBase
    {
        public MasterListSetting(string controllerName)
        {
            Controller = controllerName.RemoveSubString(DefaultValuesBase.ControllerNameSuffix);
            string controllerNameCamelCase = Controller.ToCamelCase();
            Name = string.Format(DefaultJavaScriptNames.FormName, controllerNameCamelCase);
            CreateState = string.Format(DefaultJavaScriptNames.CreateState, controllerNameCamelCase);
            DraftsState = string.Format(DefaultJavaScriptNames.DraftsState, controllerNameCamelCase);
            ListState = controllerNameCamelCase;
            ListServerAction = DefaultValuesBase.ListAction;
            DraftsServerAction = DefaultValuesBase.DraftsAction;
            Class = DefaultCssClasses.List;
            ShowMasterHeader = true;
            PageSize = 10;
        }

        public MasterListSetting(Type controllerType)
            : this(controllerType.Name)
        {
            HasApprove = controllerType.IsSubClassOfGeneric(typeof (ApprovableCrudControllerBase<,,>));
        }

        public string Controller { get; set; }
        public string CreateState { get; set; }
        public string DraftsState { get; set; }
        public string ListState { get; set; }
        public string DetailState { get; set; }
        public string ListServerAction { get; set; }
        public string DraftsServerAction { get; set; }
        public bool HasApprove { get; set; }
        public bool ShowMasterHeader { get; set; }

        /// <summary>
        ///     Access properties in current item using {{item.PROPERTY_NAME}}
        /// </summary>
        public string ItemTitleExpression { get; set; }

        /// <summary>
        ///     Access properties in current item using {{item.PROPERTY_NAME}}
        /// </summary>
        public string ItemLegendExpression { get; set; }

        public int PageSize { get; set; }
        public string ItemIcon { get; set; }

        /// <summary>
        ///     Access properties in selected master using {{currentModel.PROPERTY_NAME}}
        /// </summary>
        public string MasterHeaderTitleExpression { get; set; }

        public string MasterDisplayTabTitle { get; set; }
        public string MasterDisplayTabIcon { get; set; }
        public string MasterListTitle { get; set; }
    }
}