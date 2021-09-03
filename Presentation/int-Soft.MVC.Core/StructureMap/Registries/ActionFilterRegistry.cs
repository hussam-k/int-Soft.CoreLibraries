using System;
using System.Web.Mvc;
using intSoft.MVC.Core.HTTPApplication;
using intSoft.MVC.Core.StructureMap.Base;
using StructureMap;
using StructureMap.TypeRules;

namespace intSoft.MVC.Core.StructureMap.Registries

{
    public class ActionFilterRegistry<T> : Registry
        where T : ApplicationConfiguration, new()
    {
        public ActionFilterRegistry(Func<IContainer> containerFactory)
        {
            For<IFilterProvider>().Use(new StructureMapFilterProvider(containerFactory));

            if (!containerFactory().Model.HasImplementationsFor(typeof(HttpApplicationBase<T>)))
            {
                throw new NotImplementedException("Type IApp doesn't registered with a proper class.");
            }

            var app = containerFactory().GetInstance(typeof(HttpApplicationBase<T>)) as HttpApplicationBase<T>;


            Policies.SetAllProperties(x =>
                x.Matching(p =>
                    p.DeclaringType.CanBeCastTo(typeof (ActionFilterAttribute)) &&
                    p.DeclaringType.Namespace.StartsWith(app.Configuration.Namespace) &&
                    !p.DeclaringType.IsPrimitive &&
                    p.PropertyType != typeof (string)
                    ));
        }
    }
}