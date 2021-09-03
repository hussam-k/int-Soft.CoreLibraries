using System;
using System.Web.Mvc;
using IntSoft.DAL.Common;

namespace intSoft.MVC.Core.ModelWrappersBase
{
    public class LocalizableModelWrapperBase<T> : ModelWrapperBase<T>
        where T : class, ILocalizable, new()
    {
        public LocalizableModelWrapperBase(T model) : base(model)
        {
        }

        public LocalizableModelWrapperBase()
        {
        }

        public string LocalizedName
        {
            get
            {
                var configuration = DependencyResolver.Current.GetService<IConfiguration>();
                return configuration.IsRightToLeft ? Model.Name : Model.LatinName;
            }
        }
    }
}
