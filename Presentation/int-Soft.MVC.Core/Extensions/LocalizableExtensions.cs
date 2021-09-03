using System.Web.Mvc;
using IntSoft.DAL.Common;

namespace intSoft.MVC.Core.Extensions
{
    public static class LocalizableExtensions
    {
        public static string LocalizedName(this ILocalizable localizable)
        {
            var isRtl = DependencyResolver.Current.GetService<IConfiguration>().IsRightToLeft;

            return isRtl ? localizable.Name : localizable.LatinName;
        }
    }
}
