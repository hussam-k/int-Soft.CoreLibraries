using System.Reflection;
using intSoft.MVC.Core.Bundles;
using intSoft.MVC.Core.ExceptionHandling;
using StructureMap;
using StructureMap.Pipeline;

namespace intSoft.MVC.Core.StructureMap.Registries
{
    public class CoreLibsRegistry : Registry
    {
        public CoreLibsRegistry()
        {
            Scan(scan =>
            {
                scan.Assembly(Assembly.GetAssembly(typeof(CoreLibsRegistry)));
                scan.WithDefaultConventions();
                scan.AddAllTypesOf<IBundleFileProvider>();
                scan.AddAllTypesOf<IBundleJsFileProvider>();
                scan.AddAllTypesOf<IBundleStyleFileProvider>();
                For<SqlExceptionHandler>().Use<SqlExceptionHandler>().SetLifecycleTo(new SingletonLifecycle());
            });
        }
    }
}