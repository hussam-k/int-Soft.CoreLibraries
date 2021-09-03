using System;
using System.Reflection;

namespace intSoft.MVC.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsSubClassOfGeneric(this Type type, Type generic)
        {
            Type toCheck = type;

            while (toCheck != null && toCheck != typeof (object))
            {
                Type cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;

                if (generic == cur)
                    return true;

                toCheck = toCheck.BaseType;
            }

            return false;
        }

        public static T GetCustomAttribute<T>(this PropertyInfo propertyInfo) where T : Attribute
        {
            return Attribute.GetCustomAttribute(propertyInfo, typeof(T)) as T;
        }
    }
}