using System;
using intSoft.MVC.Core.Common;
using intSoft.MVC.Core.Controllers;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.Security;

namespace intSoft.MVC.Core.HTMLHelperSettings.MasterDetail
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MasterForDetailSetting : SettingsBase
    {
        public MasterForDetailSetting(string detailState)
        {
            _detailState = detailState;
        }

        public MasterForDetailSetting(Type detailControllerType)
        {
            Controller = detailControllerType.Name.RemoveSubString(DefaultValuesBase.ControllerNameSuffix);
            TabTitle = detailControllerType.IsSubClassOfGeneric(typeof(DetailCrudControllerBase<,,>))
                ? GetStringFromResourceType(detailControllerType.BaseType.GetGenericArguments()[0].Name)
                : string.Empty;
        }

        public string GenerateDetailStateName(string masterController)
        {
            if (!string.IsNullOrEmpty(_detailState)) return _detailState;
            return string.Format(AngularAttributeTemplates.DetailState,
                Controller.ToCamelCase(), masterController.ToCamelCase());
        }

        #region Properties

        private readonly string _detailState;
        public string Controller { get; set; }
        public string TabTitle { get; set; }
        public string TabIcon { get; set; }

        #endregion
    }
}