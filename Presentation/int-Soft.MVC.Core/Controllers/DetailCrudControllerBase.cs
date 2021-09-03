using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using intSoft.MVC.Core.Filters;
using intSoft.MVC.Core.ModelWrappersBase;
using intSoft.MVC.Core.Security;
using IntSoft.DAL.Common;
using IntSoft.DAL.RepositoriesBase;

namespace intSoft.MVC.Core.Controllers
{
    public abstract class DetailCrudControllerBase<TModel, TWrapper, TRepository> :
        CrudControllerBase<TModel, TWrapper, TRepository>
        where TModel : class, IEntity, new()
        where TWrapper : ModelWrapperBase<TModel>, new() where TRepository : IDetailRepository<TModel>
    {
        [HttpGet]
        [CustomActionAuthorization]
        public override async Task<ActionResult> Create()
        {
            return await Task.FromResult(PartialView("DetailCreate", CreateModelWrapper(new TModel())));
        }

        [HttpGet]
        [CustomActionAuthorization]
        public override async Task<ActionResult> Index()
        {
            return await Task.FromResult(PartialView("DetailTemplate", CreateModelWrapper(new TModel())));
        }

        [HttpPost]
        [CustomValidateAntiForgeryToken]
        [CustomActionAuthorization]
        [Obsolete]
        public override async Task<ActionResult> List(int? pageNumber = null, int? pageSize = null)
        {
            throw new Exception("Not allowed operation request! Use ListByMaster instead");
        }


        [HttpPost]
        [CustomValidateAntiForgeryToken]
        [CustomActionAuthorization]
        public virtual async Task<ActionResult> ListByMaster(Guid masterId, int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                var totalNumberOfItems = Repository.GetCount(masterId);
                var result = pageNumber.HasValue && pageSize.HasValue
                    ? Repository.GetAll(masterId, pageNumber.Value, pageSize.Value)
                    : Repository.GetAll(masterId);

                return await Task.FromResult(
                    JsonSuccess(new {
                    TotalNumberOfItems = totalNumberOfItems,
                    List = result.Select(CreateModelWrapper)}));
            }
            catch (Exception ex)
            {
                return  JsonError(ex.Message);
            }
        }
    }
}