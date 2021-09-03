using System.Web.Mvc;

namespace intSoft.MVC.Core.CustomValueProvider
{
    public class CreationDateUserIdValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            return DependencyResolver.Current.GetService<CreationDateUserIdValueProvider>();
        }
    }
}