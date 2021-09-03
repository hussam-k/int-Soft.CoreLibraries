using System.Collections.Generic;

namespace intSoft.MVC.Core.Helpers
{
    public interface ISelectListProvider
    {
        IDictionary<string, string> GetSelectList();
    }
}