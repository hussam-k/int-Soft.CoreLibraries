using System.Collections.Generic;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.Helpers.ColumnActionItemSettingListProviders
{
    public class DeleteColumnActionItemSettingListProvider : IColumnActionItemSettingListProvider
    {
        public virtual IEnumerable<TableColumnActionItemSetting> GetColumnActionItemSettingList()
        {
            return new List<TableColumnActionItemSetting>
            {
                TableColumnActionItemSetting.DeleteColumnActionItemSetting(),
            };
        }
    }
}