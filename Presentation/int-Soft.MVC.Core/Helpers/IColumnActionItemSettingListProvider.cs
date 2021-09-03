using System.Collections.Generic;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.Helpers
{
    public interface IColumnActionItemSettingListProvider
    {
        IEnumerable<TableColumnActionItemSetting> GetColumnActionItemSettingList();
    }
}