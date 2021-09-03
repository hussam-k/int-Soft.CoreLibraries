using System.Collections.Generic;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.Helpers.ColumnActionItemSettingListProviders
{
    public class DefaultColumnActionItemSettingListProvider : IColumnActionItemSettingListProvider
    {
        public virtual IEnumerable<TableColumnActionItemSetting> GetColumnActionItemSettingList()
        {
            return new List<TableColumnActionItemSetting>
            {
                TableColumnActionItemSetting.EditColumnActionItemSetting(),
                TableColumnActionItemSetting.DeleteColumnActionItemSetting(),
                TableColumnActionItemSetting.DisplayColumnActionItemSetting(),
            };
        }
    }
}