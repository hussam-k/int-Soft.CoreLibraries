using System.Collections.Generic;
using intSoft.MVC.Core.HTMLHelperSettings;

namespace intSoft.MVC.Core.Helpers.ColumnActionItemSettingListProviders
{
    public class ApprovableColumnActionItemSettingListProvider : DefaultColumnActionItemSettingListProvider
    {
        public override IEnumerable<TableColumnActionItemSetting> GetColumnActionItemSettingList()
        {
            var list = new List<TableColumnActionItemSetting>();
            list.AddRange(base.GetColumnActionItemSettingList());
            list.Add(TableColumnActionItemSetting.ApproveColumnActionItemSetting());

            return list;
        }
    }
}