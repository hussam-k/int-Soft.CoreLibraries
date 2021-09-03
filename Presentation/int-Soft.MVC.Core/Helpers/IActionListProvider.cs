using System.Collections.Generic;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.Helpers
{
    public interface IActionListProvider
    {
        IEnumerable<ActionSetting> GetActionList();
    }
}