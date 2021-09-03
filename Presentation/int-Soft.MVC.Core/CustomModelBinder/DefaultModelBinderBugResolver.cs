using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using intSoft.MVC.Core.Extensions;

namespace intSoft.MVC.Core.CustomModelBinder
{
    public static class DefaultModelBinderBugResolver
    {
        /// <summary>
        /// ASP Mvc adds RequiredAttribute automatically to non-nullabe value type properties and of course you 
        /// can disable that by using the following flag.
        /// DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false; in Application_Start
        /// However, the problem MVC sounds like it adds RequiredAttribute even for nullabe value types such as DateTime? causing wrong validation error
        /// so I add the following fix which deletes the validation from properties that are decalred as non-required and 
        /// the model binder consider them required.
        /// </summary>
        /// <param name="modelType">The type of the model</param>
        /// <param name="modelState">The model state dictionary that is modified generally by the Model Binder</param>
        public static void HandleAspMvcBug(Type modelType, ModelStateDictionary modelState)
        {
            var properties = modelType.GetProperties();

            foreach (var prop in properties)
            {
                var isRequiredProperty = prop.GetCustomAttribute<RequiredAttribute>() != null;

                if (isRequiredProperty)
                    continue;

                if (modelState.ContainsKey(prop.Name))
                {
                    var errorToRemove = modelState[prop.Name].Errors.FirstOrDefault(e => e.ErrorMessage.Contains("The value 'null' is not valid for"));
                    if (errorToRemove != null)
                    {
                        modelState[prop.Name].Errors.Remove(errorToRemove);
                    }
                }
            }
        }
    }
}
