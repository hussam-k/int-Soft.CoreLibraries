using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace intSoft.MVC.Core.Helpers
{
    public static class ResourceHelper
    {
        public static string GetStringFromResourceType(Type resourceType, string key)
        {
            if (resourceType == null || string.IsNullOrWhiteSpace(key))
                return string.Empty;

            var resourceManager = new ResourceManager(resourceType);
            return resourceManager.GetString(key) ?? key;
        }

        public static string GetStringFromValidationAttribute(ValidationAttribute validationAttribute)
        {
            if (validationAttribute == null)
                return string.Empty;

            if (validationAttribute.ErrorMessageResourceType == null && !string.IsNullOrWhiteSpace(validationAttribute.ErrorMessage))
                return validationAttribute.ErrorMessage;

           return GetStringFromResourceType(validationAttribute.ErrorMessageResourceType, validationAttribute.ErrorMessageResourceName);
        }

        public static string GetPropertyTitleFromDisplayAttribute(string propertyName, DisplayAttribute displayAttr)
        {
            if (displayAttr == null || (displayAttr.ResourceType == null && string.IsNullOrWhiteSpace(displayAttr.Name)))
                return propertyName;

            if (displayAttr.ResourceType == null && !string.IsNullOrWhiteSpace(displayAttr.Name))
            {
                return displayAttr.Name;
            }

            return GetStringFromResourceType(displayAttr.ResourceType, displayAttr.Name);
        }

        public static Dictionary<string, string> ResourceToDictionary(Type resource, CultureInfo culture)
        {
            var rm = new ResourceManager(resource);

            var props = resource.GetProperties(BindingFlags.Public | BindingFlags.Static);
            var values = from pi in props
                         where pi.PropertyType == typeof(string)
                         select new KeyValuePair<string, string>(pi.Name, rm.GetString(pi.Name, culture));

            var dictionary = values.ToDictionary(k => k.Key, v => v.Value);

            return dictionary;
        }
    }
}