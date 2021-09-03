using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Helpers;
using intSoft.MVC.Core.HTTPApplication;
using intSoft.Res.DisplayNames;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public class TableColumnActionItemSetting : SettingsBase
    {
        public string FunctionName { get; set; }
        public string Title { get; set; }

        #region Factory Methods
        public static TableColumnActionItemSetting EditColumnActionItemSetting()
        {
            return new TableColumnActionItemSetting()
            {
                Class = DefaultCssClasses.ListEditButton,
                FunctionName = DefaultJavaScriptNames.Update,
                Title = ResourceHelper.GetStringFromResourceType(DependencyResolver.Current.GetService<ApplicationConfiguration>().ResourceType, "Edit"),
            };
        }
        public static TableColumnActionItemSetting DeleteColumnActionItemSetting()
        {
            return new TableColumnActionItemSetting()
            {
                Class = DefaultCssClasses.ListDeleteButton,
                FunctionName = DefaultJavaScriptNames.Delete,
                Title = ResourceHelper.GetStringFromResourceType(DependencyResolver.Current.GetService<ApplicationConfiguration>().ResourceType, "Delete"),
            };
        }
        public static TableColumnActionItemSetting DisplayColumnActionItemSetting()
        {
            return new TableColumnActionItemSetting()
            {
                Class = DefaultCssClasses.ListDisplayButton,
                FunctionName = DefaultJavaScriptNames.Display,
                Title = ResourceHelper.GetStringFromResourceType(DependencyResolver.Current.GetService<ApplicationConfiguration>().ResourceType, "Details"),
            };
        }
        public static TableColumnActionItemSetting ApproveColumnActionItemSetting()
        {
            return new TableColumnActionItemSetting()
            {
                Class = DefaultCssClasses.ListApproveButton,
                FunctionName = DefaultJavaScriptNames.Approve,
                Title = ResourceHelper.GetStringFromResourceType(DependencyResolver.Current.GetService<ApplicationConfiguration>().ResourceType, "Approve"),
            };
        } 
        #endregion
    }
}