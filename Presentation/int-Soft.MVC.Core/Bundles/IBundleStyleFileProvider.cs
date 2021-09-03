using System.Collections.Generic;

namespace intSoft.MVC.Core.Bundles
{
    public interface IBundleStyleFileProvider : IBundleFileProvider
    {
        IEnumerable<string> StyleFiles { get; }
    }
}