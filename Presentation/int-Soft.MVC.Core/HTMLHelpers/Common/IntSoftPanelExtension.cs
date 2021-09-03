using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using intSoft.MVC.Core.DefaultTemplates;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.Helpers;
using intSoft.MVC.Core.HTMLHelpers.Base;
using intSoft.MVC.Core.HTMLHelperSettings;
using intSoft.MVC.Core.Identity;
using Microsoft.AspNet.Identity;

namespace intSoft.MVC.Core.HTMLHelpers.Common
{
    public static class IntSoftPanelExtension
    {
        public static MvcHtmlString Header(this IntSoftExtension helper, HeaderSetting settings)
        {
            return new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.Header),
                settings.Id,
                settings.Skin,
                settings.Controller
                ));
        }

        public static MvcHtmlString SideBar(this IntSoftExtension helper, SideBarSetting settings)
        {
            var isAutenticated = DependencyResolver.Current.GetService<HttpContextBase>().User.Identity.IsAuthenticated;
            var sidebarList =
                DependencyResolver.Current.GetService<ISideBarItemsProvider>()
                    .GetSideBarItems()
                    .GetAuthenticatedSideBarItems();
            var items = sidebarList.Aggregate("", (current, sideBarItem) => current + helper.SideBarItem(sideBarItem));
            return new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.SideBar),
                settings.Id,
                settings.NgClass,
                isAutenticated ? helper.SideBarProfile().ToString() : string.Empty,
                items
                ));
        }

        public static MvcHtmlString SideBarProfile(this IntSoftExtension helper)
        {
            var user = DependencyResolver.Current.GetService<ICurrentUser<IUser<Guid>>>().User;
            return new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.SideBarProfile),
                user.UserName
                ));
        }

        public static MvcHtmlString SideBarItem(this IntSoftExtension helper, SideBarItemSetting item)
        {
            if (item.Items.Any())
            {
                return helper.SideBarMenuItem(item);
            }
            return new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.SideBarItem),
                item.NavigationState,
                item.GetStringFromResourceType(item.Title),
                item.IconClass,
                item.GetStringFromResourceType(item.Tooltip)
                ));
        }

        public static MvcHtmlString SideBarMenuItem(this IntSoftExtension helper, SideBarItemSetting item)
        {
            var items = item.Items.Aggregate("", (current, sideBarItem) => current + helper.SideBarSubItem(sideBarItem));
            return new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.SideBarMenuItem),
                item.GetStringFromResourceType(item.Title),
                item.IconClass,
                items
                ));
        }

        public static MvcHtmlString SideBarSubItem(this IntSoftExtension helper, SideBarItemSetting item)
        {
            return new MvcHtmlString(string.Format(HTMLTemplateProvider.GetTemplate(HTMLTemplates.SideBarSubItem),
                item.NavigationState,
                item.GetStringFromResourceType(item.Title)
                ));
        }
    }
}