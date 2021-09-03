using System.Collections.Generic;

namespace intSoft.MVC.Core.Helpers
{
    public interface IEnumSelectListProvider
    {
        IDictionary<int, string> GetSelectList();
    }
}