using System.Collections.Generic;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.Helpers.ButtonListProviders
{
    public class ModalButtonListProvider : IFormButtonListProvider
    {
        public IEnumerable<ButtonSetting> GetButtonList()
        {
            var buttonList = new List<ButtonSetting>
            {
                ButtonSetting.CancelButtonSetting(),
                ButtonSetting.SaveButtonSetting()
            };
            
            return buttonList;
        }
    }
}