using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using intSoft.MVC.Core.Common;
using intSoft.MVC.Core.HTMLHelperSettings;
using intSoft.MVC.Core.Identity;
using Microsoft.AspNet.Identity;

namespace intSoft.MVC.Core.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<SideBarItemSetting> GetAuthenticatedSideBarItems(
            this IEnumerable<SideBarItemSetting> list)
        {
            var roles = DependencyResolver.Current.GetService<ICurrentUser<IUser<Guid>>>().CurrentUserRoles;
            var user = DependencyResolver.Current.GetService<ICurrentUser<IUser<Guid>>>().User;
            if (user == null)
            {
                return list.Where(x => !x.RequiresAuthentication && !x.Roles.Any());
            }
            if (user.UserName == DefaultValuesBase.Admin)
            {
                return list;
            }
            return
                list.Where(
                    x => !x.Roles.Any() || x.Roles.Any(r => roles.Contains(r)));
        }
    }
}