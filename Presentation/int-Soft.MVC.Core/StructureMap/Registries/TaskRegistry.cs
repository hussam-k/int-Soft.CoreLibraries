using System;
using intSoft.MVC.Core.Controllers;
using intSoft.MVC.Core.HTTPApplication;
using intSoft.MVC.Core.Tasks;
using StructureMap;
using StructureMap.Graph;

namespace intSoft.MVC.Core.StructureMap.Registries
{
    public class TaskRegistry<T> : Registry
        where T : ApplicationConfiguration, new()
    {
        public TaskRegistry(IContainer container)
        {
            if (!container.Model.HasImplementationsFor(typeof(HttpApplicationBase<T>)))
            {
                throw new NotImplementedException("Type IApp is not registered with a proper class.");
            }

            var app = container.GetInstance(typeof(HttpApplicationBase<T>)) as HttpApplicationBase<T>;

            Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory(
                    a => a.FullName.StartsWith(app.Configuration.Namespace));
                scan.AssemblyContainingType<ControllerBase>();
                scan.AddAllTypesOf<IRunAtInit>();
                scan.AddAllTypesOf<IRunAtStartup>();
                scan.AddAllTypesOf<IRunOnEachRequest>();
                scan.AddAllTypesOf<IRunOnErrors>();
                scan.AddAllTypesOf<IRunAtAfterEachRequest>();
            });
        }
    }
}