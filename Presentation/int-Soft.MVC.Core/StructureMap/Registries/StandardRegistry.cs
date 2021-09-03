using intSoft.MVC.Core.Controllers;
using StructureMap;
using StructureMap.Graph;

namespace intSoft.MVC.Core.StructureMap.Registries
{
    public class StandardRegistry : Registry
    {
        public StandardRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AssemblyContainingType<ControllerBase>();
                scan.AssembliesFromApplicationBaseDirectory();
                scan.WithDefaultConventions();
            });
        }
    }
}