namespace intSoft.MVC.Core.Bundles
{
    public interface IBundleFileProvider
    {
        int Order { get; }
        string BasePath { get; }
    }
}