using System.Collections.Generic;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.Helpers.ColumnActionItemSettingListProviders
{
    public class ApproveRejectColumnActionItemSettingListProvider : IColumnActionItemSettingListProvider
    {
        public IEnumerable<TableColumnActionItemSetting> GetColumnActionItemSettingList()
        {
            return new List<TableColumnActionItemSetting>()
            {
                TableColumnActionItemSetting.ApproveColumnActionItemSetting(),
                TableColumnActionItemSetting.DeleteColumnActionItemSetting()
            };
        }
    }
}