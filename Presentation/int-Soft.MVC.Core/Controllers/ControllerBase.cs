using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using IntSoft.DAL.Common;
using intSoft.MVC.Core.ActionResults;
using intSoft.MVC.Core.Helpers.UiGrid;
using intSoft.MVC.Core.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Web.Mvc;
using StructureMap.Attributes;

namespace intSoft.MVC.Core.Controllers
{
    /// <summary>
    ///     This class is going to be the base of all the controllers that uses this framework
    /// </summary>
    //[IntSoftAuthorize]
    public abstract class ControllerBase : Controller
    {
        [SetterProperty]
        public ICurrentUser<IUser<Guid>> CurrentUser { get; set; }

        [SetterProperty]
        public IConfiguration Configuration { get; set; }
        
        protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action)
            where TController : Controller
        {
            return ControllerExtensions.RedirectToAction(this, action);
        }

        #region Actions
        public virtual IUiGridDefinitionProvider CreateUiGridDefinitionProvider()
        {
            return new DefaultUiGridDefinitionProvider();
        }
        #endregion


        #region JsonActionResult

        [Obsolete("DO not use the original Json helpers. USE StandardJsonActionResult instead")]
        protected JsonResult Json<T>(T data)
        {
            throw new InvalidOperationException(
                "DO NOT use the original Json helpers. USE StandardJsonActionResult instead");
        }

        protected StandardJsonActionResult JsonValidationError()
        {
            var result = new StandardJsonActionResult();
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                result.AddError(error.ErrorMessage);
            }
            return result;
        }

        protected StandardJsonActionResult JsonError(string errorMessage)
        {
            var result = new StandardJsonActionResult();
            result.AddError(errorMessage);
            return result;
        }

        protected StandardJsonActionResult<T> JsonSuccess<T>(T data, bool isCamelCase=true)
        {
            return new StandardJsonActionResult<T>(isCamelCase) { Data = data };
        }

        #endregion
    }
}