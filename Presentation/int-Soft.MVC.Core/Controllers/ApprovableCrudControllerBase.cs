using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using IntSoft.DAL.Common;
using intSoft.MVC.Core.ModelWrappersBase;
using intSoft.MVC.Core.Security;
using IntSoft.DAL.RepositoriesBase;

namespace intSoft.MVC.Core.Controllers
{
    public abstract class ApprovableCrudControllerBase<TModel, TWrapper, TRepository> : CrudControllerBase<TModel, TWrapper, TRepository>
        where TModel : class, IApprovableEntity, new()
        where TWrapper: ModelWrapperBase<TModel>, new() where TRepository: ApprovableRepository<TModel>
    {
        [HttpGet]
        [CustomActionAuthorization]
        public virtual async Task<ActionResult> Drafts()
        {
            return await Task.FromResult(PartialView(CreateModelWrapper(new TModel())));
        }

        [HttpPost]
        [CustomActionAuthorization]
        public virtual async Task<ActionResult> GetDrafts()
        {
            var totalNumberOfItems = Repository.GetDraftsCount();

            return await Task.FromResult(JsonSuccess(
                new
                {
                    TotalNumberOfItems = totalNumberOfItems,
                    List = Repository.GetAllDrafts().Select(CreateModelWrapper)
                }));
        }

        [HttpPost]
        [CustomActionAuthorization]
        public override async Task<ActionResult> List(int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                var totalNumberOfItems = Repository.GetCount();
                
                var result = pageNumber.HasValue && pageSize.HasValue
                    ? Repository.GetAll(pageNumber.Value, pageSize.Value)
                    : Repository.GetAll();

                return await Task.FromResult(JsonSuccess(new
                {
                    TotalNumberOfItems = totalNumberOfItems,
                    List = result.Select(CreateModelWrapper)
                }));
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }

        [HttpPost]
        [CustomActionAuthorization]
        public virtual async Task<ActionResult> Approve(Guid id)
        {
            Repository.Approve(id, CurrentUser.User.Id);
            
            return await Task.FromResult(new HttpStatusCodeResult(HttpStatusCode.OK));
        }
    }
}
