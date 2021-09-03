using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using IntSoft.DAL.Common;
using intSoft.MVC.Core.CustomModelBinder;
using intSoft.MVC.Core.Extensions;
using intSoft.MVC.Core.Filters;
using intSoft.MVC.Core.ModelWrappersBase;
using intSoft.MVC.Core.Security;
using IntSoft.DAL.RepositoriesBase;
using intSoft.Res.ClientSide;
using intSoft.Res.DisplayNames;
using StructureMap.Attributes;

namespace intSoft.MVC.Core.Controllers
{
    public abstract class CrudControllerBase<TModel, TWrapper, TRepository> : ControllerBase 
        where TModel : class, IEntity, new()
        where TWrapper: ModelWrapperBase<TModel>, new() where TRepository: IRepository<TModel>
    {
        [SetterProperty]
        public TRepository Repository { get; set; }

        public abstract TWrapper CreateModelWrapper(TModel model = null);
        
        [HttpGet]
        [CustomActionAuthorization]
        public virtual async Task<ActionResult> Create()
        {
            return await Task.FromResult(PartialView(CreateModelWrapper(new TModel())));
        }

        [HttpGet]
        [CustomActionAuthorization]
        public virtual async Task<ActionResult> Display()
        {
            return await Task.FromResult(PartialView(CreateModelWrapper(new TModel())));
        }

        [HttpPost]
        [CustomValidateAntiForgeryToken]
        [CustomActionAuthorization]
        public virtual async Task<ActionResult> Save(TWrapper newViewModel)
        {
            DefaultModelBinderBugResolver.HandleAspMvcBug(newViewModel.GetType(), ModelState);

            if (!ModelState.IsValid)
                return JsonValidationError();

            Repository.Save(newViewModel.GetModel());

            return await SaveActionResult();
        }
        
        [NonAction]
        protected virtual async Task<ActionResult> SaveActionResult()
        {
            return await Task.FromResult(JsonSuccess(new {Message = ClientSide.SAVED_SUCCESSFULLY}));
        }

        [HttpGet]
        [CustomActionAuthorization]
        public virtual async Task<ActionResult> GetModel(Guid id)
        {
            var model = Repository.Get(id);
            return await Task.FromResult(JsonSuccess(CreateModelWrapper(model)));
        }

        [HttpGet]
        [CustomActionAuthorization]
        public virtual async Task<ActionResult> Index()
        {
            return await Task.FromResult(PartialView(CreateModelWrapper(new TModel())));
        }

        [HttpPost]
        [CustomValidateAntiForgeryToken]
        [CustomActionAuthorization]
        public virtual async Task<ActionResult> List(int? pageNumber = null, int? pageSize = null)
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
        [CustomValidateAntiForgeryToken]
        [CustomActionAuthorization]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            var entityToDelete = Repository.FirstOrDefault(entity => entity.Id == id);

            if (entityToDelete == null)
                return JsonError(DisplayNames.EntityNotFound);

            Repository.Remove(entityToDelete);

            return await Task.FromResult(new HttpStatusCodeResult(HttpStatusCode.OK));
        }


        [HttpGet]
        [CustomActionAuthorization(IsAlwaysAllowed = true)]
        public virtual ActionResult UiGridDefinition()
        {
            return Content(CreateUiGridDefinitionProvider().GetUiGridDefinition(typeof (TWrapper)).ToJson());
        }

        [HttpGet]
        public virtual async Task<ActionResult> GetDefaultInstance()
        {
            var model = new TWrapper();
            return await Task.FromResult(JsonSuccess(model));
        }
    }
}
