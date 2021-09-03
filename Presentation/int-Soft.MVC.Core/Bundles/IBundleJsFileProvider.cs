using System.Collections.Generic;

namespace intSoft.MVC.Core.Bundles
{
    public interface IBundleJsFileProvider : IBundleFileProvider
    {
        IEnumerable<string> JsFiles { get; }
    }
}