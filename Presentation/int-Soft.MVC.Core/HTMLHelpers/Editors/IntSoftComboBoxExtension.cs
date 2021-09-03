using System.Linq;
using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.DefaultTemplates.Utility;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.Helpers;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelperSettings;
using Newtonsoft.Json;

namespace intSoft.MVC.Core.HTMLHelpers.Editors
{
    public static class IntSoftComboBoxExtension
    {
        public static MvcHtmlString ComboBox(this IntSoftExtension helper, ComboBoxSettings settings)
        {
            string selectOptions;
            if (settings.IsEnumProvider)
            {
                var selectListProvider =
                    DependencyResolver.Current.GetService(settings.SelectListProviderType) as IEnumSelectListProvider;
                var selectList = selectListProvider.GetSelectList();
                selectOptions = string.Format("[{0}]",
                    selectList.Aggregate("", (current, option) => current + JsonConvert.SerializeObject(option) + ","));

            }
            else
            {
                var selectListProvider =
                    DependencyResolver.Current.GetService(settings.SelectListProviderType) as ISelectListProvider;
                var selectList = selectListProvider.GetSelectList();
                selectOptions = string.Format("[{0}]", selectList.Aggregate("", (current, option) =>
                    current + JsonConvert.SerializeObject(option) + ","));

            }
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.Combobox),
                settings.Name.ToCamelCase(),
                settings.Class,
                settings.MultipleExpression,
                settings.Validations,
                settings.Attributes,
                settings.AllowMultiple ? string.Format(AngularAttributeTemplates.ChosenMutliplePlaceHolder, settings.GetStringFromResourceType(settings.PlaceHolder))
                : string.Format(AngularAttributeTemplates.ChosenSinglePlaceHolder, settings.GetStringFromResourceType(settings.PlaceHolder)),
                selectOptions));
        }

        public static MvcHtmlString RemoteComboBox(this IntSoftExtension helper, ComboBoxSettings settings)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.ComboboxRemote),
                settings.Name.ToCamelCase(),
                settings.Class,
                settings.MultipleExpression,
                settings.Validations,
                settings.Attributes,
                settings.AllowMultiple ? string.Format(AngularAttributeTemplates.ChosenMutliplePlaceHolder, settings.GetStringFromResourceType(settings.PlaceHolder))
                : string.Format(AngularAttributeTemplates.ChosenSinglePlaceHolder, settings.GetStringFromResourceType(settings.PlaceHolder)),
                settings.Url,
                settings.ValueProperty.ToCamelCase(),
                settings.DisplayTextProperty.ToCamelCase()));
        }

        public static MvcHtmlString CascadeComboBox(this IntSoftExtension helper, ComboBoxSettings settings)
        {
            return MvcHtmlString.Create(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.ComboboxCascade),
                settings.Name.ToCamelCase(),
                settings.Class,
                settings.MultipleExpression,
                settings.Validations,
                settings.Attributes,
                settings.AllowMultiple ? string.Format(AngularAttributeTemplates.ChosenMutliplePlaceHolder, settings.GetStringFromResourceType(settings.PlaceHolder))
                : string.Format(AngularAttributeTemplates.ChosenSinglePlaceHolder, settings.GetStringFromResourceType(settings.PlaceHolder)),
                settings.CascadeResultList.ToCamelCase(),
                settings.ValueProperty.ToCamelCase(),
                settings.DisplayTextProperty.ToCamelCase(),
                settings.CascadedParentPropertyName.ToCamelCase(),
                settings.CascadeResultList.ToCamelCase(),
                settings.CascadeCallUrl
                ));
        }

    }
}