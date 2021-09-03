using System;
using System.Web.Mvc;
using IntSoft.DAL.Common;
using StructureMap.Attributes;

namespace intSoft.MVC.Core.ModelWrappersBase
{
    public class ModelWrapperBase<T> where T : class, new()
    {
        public ModelWrapperBase(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            Model = model;
        }

        public ModelWrapperBase()
        {
            Model = new T();
        }

        [HiddenInput]
        protected T Model { get; set; }

        public T GetModel()
        {
            return Model;
        }
    }
}
