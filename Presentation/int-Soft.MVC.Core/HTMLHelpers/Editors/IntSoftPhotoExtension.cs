using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.HTMLHelpers.Editors
{
    public static class IntSoftPhotoExtension
    {
        public static MvcHtmlString Photo(this IntSoftExtension helper, FileSetting setting)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.Photo),
                setting.Name.ToCamelCase(),
                setting.AcceptedFileExtension,
                setting.MaxSize,
                setting.GetStringFromResourceType(setting.SelectFileText),
                setting.GetStringFromResourceType(setting.ChangeFileText),
                setting.GetStringFromResourceType(setting.RemoveText)
                ));
        }
    }
}