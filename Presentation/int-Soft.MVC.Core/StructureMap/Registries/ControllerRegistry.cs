using System;
using System.Web.Mvc;
using intSoft.MVC.Core.HTTPApplication;
using StructureMap;
using StructureMap.Graph;

namespace intSoft.MVC.Core.StructureMap.Registries
{
    public class ControllerRegistry<T> : Registry 
        where T : ApplicationConfiguration, new()
    {
        public ControllerRegistry(IContainer container)
        {
            if (!container.Model.HasImplementationsFor(typeof(HttpApplicationBase<T>)))
            {
                throw new NotImplementedException("Type IApp doesn't registered with a proper class.");
            }


            var app = container.GetInstance(typeof(HttpApplicationBase<T>));

            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AssemblyContainingType<ControllerBase>();
                scan.AssemblyContainingType(app.GetType());
                scan.With(new ControllerConvention());
            });
        }
   }
}