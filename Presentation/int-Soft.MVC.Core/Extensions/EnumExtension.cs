using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using intSoft.MVC.Core.Helpers;

namespace intSoft.MVC.Core.Extensions
{
    public static class EnumExtension
    {
        public static string GetLocalizedDescription(this Enum enumValue, Type resourceType)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            IEnumerable<DescriptionAttribute> attributes = fi.GetCustomAttributes<DescriptionAttribute>();

            if (attributes != null && attributes.Any())
                return ResourceHelper.GetStringFromResourceType(resourceType, attributes.FirstOrDefault().Description);

            return ResourceHelper.GetStringFromResourceType(resourceType, enumValue.ToString());
        }
    }
}