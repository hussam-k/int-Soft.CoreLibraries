using System;
using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Helpers;
using intSoft.MVC.Core.HTTPApplication;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public abstract class SettingsBase : Attribute
    {
        protected SettingsBase()
        {
            ResourceType = DependencyResolver.Current.GetService<ApplicationConfiguration>().ResourceType;
            TooltipPlacement = TootTipPlacement.Right;
        }

        #region Properties

        public string Id { get; set; }

        public string Class { get; set; }

        public string Name { get; set; }

        public string TooltipText { get; set; }

        /// <summary>
        /// Takes its value from TootTipPlacement static class
        /// By Default TootTipPlacement.Right
        /// </summary>
        public string TooltipPlacement { get; set; }

        public Type ResourceType { get; set; }

        public string DisableExpression { get; set; }

        public string HideExpression { get; set; }

        public string ShowExpression { get; set; }

        public string ChangeExpression { get; set; }

        public string BlurExpression { get; set; }

        public virtual string Attributes { get { return GetCustomAttributes(); } }
        
        public string ContainerAttributes { get { return GetCustomContainerAttributes(); } }
        
        #endregion

        #region Methods

        public void AddClass(string cssClass)
        {
            if (!string.IsNullOrEmpty(cssClass))
            {
                Class = string.Format("{0} {1}", Class, cssClass);                
            }
        }

        protected virtual string GetCustomAttributes()
        {
            return string.Format(" {0} {1} {2} {3} {4} {5}",
                string.IsNullOrEmpty(DisableExpression)? string.Empty: string.Format(AngularAttributeTemplates.Disable, DisableExpression),
                string.IsNullOrEmpty(ShowExpression) ? string.Empty : string.Format(AngularAttributeTemplates.Show, ShowExpression),
                string.IsNullOrEmpty(HideExpression) ? string.Empty : string.Format(AngularAttributeTemplates.Hide, HideExpression),
                string.IsNullOrEmpty(ChangeExpression) ? string.Empty : string.Format(AngularAttributeTemplates.Change, ChangeExpression),
                string.IsNullOrEmpty(BlurExpression) ? string.Empty : string.Format(AngularAttributeTemplates.Blur, BlurExpression),
                string.IsNullOrEmpty(TooltipText) ? string.Empty : string.Format(AngularAttributeTemplates.Tooltip, TooltipPlacement, GetStringFromResourceType(TooltipText)));
        }
        private string GetCustomContainerAttributes()
        {
            return string.Format(" {0} {1} ",
                string.IsNullOrEmpty(ShowExpression) ? string.Empty : string.Format(AngularAttributeTemplates.Show, ShowExpression),
                string.IsNullOrEmpty(HideExpression) ? string.Empty : string.Format(AngularAttributeTemplates.Hide, HideExpression)
                );
        }

        public string GetStringFromResourceType(string key)
        {
            return ResourceHelper.GetStringFromResourceType(ResourceType, key);
        }

        #endregion
    }
}
