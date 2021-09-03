using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.HTTPApplication;

namespace intSoft.MVC.Core.Helpers
{
    public class EnumSelectListProvider<TEnum> : IEnumSelectListProvider
        where TEnum : struct, IConvertible, IFormattable, IComparable
    {
        public EnumSelectListProvider()
        {
            ReourceType = DependencyResolver.Current.GetService<ApplicationConfiguration>().ResourceType;

        }
        public Type ReourceType { get; set; }

        public virtual IDictionary<int, string> GetSelectList()
        {
            return
                Enum.GetValues(typeof(TEnum))
                    .Cast<object>()
                    .ToDictionary<object, int, string>(enumValue => Convert.ToInt16(enumValue),
                        enumValue => ((Enum)enumValue).GetLocalizedDescription(ReourceType));
        }
    }
}