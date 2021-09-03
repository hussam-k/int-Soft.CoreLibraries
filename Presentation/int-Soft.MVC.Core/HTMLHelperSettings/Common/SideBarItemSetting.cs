using System.Collections.Generic;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    public class SideBarItemSetting : SettingsBase
    {
        public SideBarItemSetting()
        {
            Roles = new List<string>();
            Items = new List<SideBarItemSetting>();
        }
        public string Title { get; set; }

        public string Tooltip { get; set; }

        public string NavigationState { get; set; }

        public string IconClass { get; set; }

        public bool RequiresAuthentication { get; set; }

        public List<SideBarItemSetting> Items { get; set; }

        public List<string> Roles { get; set; }

    }
}