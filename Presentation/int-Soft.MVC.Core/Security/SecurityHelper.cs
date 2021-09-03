using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace intSoft.MVC.Core.Security
{
    public class SecurityHelper
    {

        public static IEnumerable<Type> GetExportedTypesFromExecutingAssembly()
        {
            return Assembly.GetExecutingAssembly().GetExportedTypes().ToList();
        }

        public static IEnumerable<Type> GetExportedTypesFromCallingAssembly()
        {
            return Assembly.GetCallingAssembly().GetExportedTypes();
        }

        public static IEnumerable<Type> GetTypesInNamespace(IEnumerable<Type> types, string nameSpace)
        {
            return
                types.Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                    .ToList()
                    .GroupBy(t => t.Name)
                    .Select(grp => grp.First())
                    .ToList();
        }

        public static IEnumerable<MethodInfo> GetPublicMethodsDecoratedBy(Type type, Type attribute)
        {
            return type.GetMethods()
                .Where(m => m.GetCustomAttributes(attribute, false).Length > 0 && m.IsPublic)
                .ToArray();
        }

        public static void SelectActionsThatNotAlwaysAllowed<T>(Type controllerType, List<MethodInfo> customActions) 
                        where T : CustomActionAuthorizationAttribute
        {
            for (int i = 0; i < customActions.Count(); i++)
            {
                var customAction = customActions[i];
                var custActionAttr = customAction.GetCustomAttributes(typeof(T), false);
                var attr = custActionAttr[0] as T;
                if (attr == null)
                    continue;

                if (attr.IsAlwaysAllowed)
                {
                    customActions.Remove(customAction);
                    --i;
                }
                else if (!string.IsNullOrEmpty(attr.OperationControllerName) && !string.IsNullOrEmpty(attr.OperationActionName))
                {
                    customActions.Remove(customAction);
                    --i;
                }
            }
        }

    }
}