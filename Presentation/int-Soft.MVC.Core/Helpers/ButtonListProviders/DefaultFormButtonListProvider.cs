using System.Collections.Generic;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.Helpers.ButtonListProviders
{
    public class DefaultFormButtonListProvider : IFormButtonListProvider
    {
        public virtual IEnumerable<ButtonSetting> GetButtonList()
        {
            var buttonList = new List<ButtonSetting>
            {
                ButtonSetting.CancelButtonSetting(),
                ButtonSetting.SubmitButtonSetting()
            };
            
            return buttonList;
        }
    }
}