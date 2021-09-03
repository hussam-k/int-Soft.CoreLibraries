using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using intSoft.MVC.Core.Helpers;


namespace intSoft.MVC.Core.Extensions
{
    public static class MetadataExtensions
    {
        public static T GetCustomAttribute<T>(this ModelMetadata metadata)
            where T : Attribute
        {
            var attributes = metadata.GetCustomAttributes<T>();
            if (attributes != null && attributes.Any())
                return attributes.First();

            return null;
        }

        public static List<T> GetCustomAttributes<T>(this ModelMetadata metadata) where T : Attribute
        {
            if (metadata.ContainerType == null)
            {
                var attributes = metadata.ModelType.GetCustomAttributes<T>();
                return attributes == null ? null : attributes.ToList();
            }
            var memberInfo = metadata.ContainerType.GetMember(metadata.PropertyName).First();
            var temp = memberInfo.GetCustomAttributes(typeof(T), false);
            var casting = temp.Cast<T>();
            return casting.ToList();
        }

        public static string GetPropertyTitleFromDisplayAttribute(this ModelMetadata metadata, bool withAsterisk = false)
        {
            var displayAttr = metadata.GetCustomAttribute<DisplayAttribute>();
            var isRequired = metadata.GetCustomAttribute<RequiredAttribute>() != null;
            var title = ResourceHelper.GetPropertyTitleFromDisplayAttribute(metadata.PropertyName, displayAttr);

            return withAsterisk ? (title + (isRequired ? " *" : string.Empty)) : (title);
        }

    }
}
