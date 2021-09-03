using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using intSoft.MVC.Core.Common;
using intSoft.MVC.Core.Extensions;
using IntSoft.DAL.Models;
using IntSoft.DAL.RepositoriesBase;
using StructureMap.Attributes;

namespace intSoft.MVC.Core.Security
{
    public class OperationsHelper<TModel, TRepository>
        where TRepository : IRepository<TModel>
        where TModel : class, IOperationBase, new()
    {
        private IEnumerable<Type> _controllerTypes;

        [SetterProperty]
        public TRepository OperationRepository { get; set; }

        public void Execute(IEnumerable<Type> controllerTypes)
        {
            _controllerTypes = controllerTypes;
            var operations = new List<TModel>();
            InsertOperations(operations);
            SaveChangesIfAny(operations);
        }

        private void InsertOperations(List<TModel> operations)
        {
            foreach (var controllerType in _controllerTypes)
            {
                var controllerTypeName = controllerType.Name.RemoveSubString(DefaultValuesBase.ControllerNameSuffix);
                var customActions =
                    SecurityHelper.GetPublicMethodsDecoratedBy(controllerType,
                        typeof (CustomActionAuthorizationAttribute)).ToList();

                SecurityHelper.SelectActionsThatNotAlwaysAllowed<CustomActionAuthorizationAttribute>(controllerType,
                    customActions);

                operations.AddRange(customActions.Select(customAction => new TModel
                {
                    Category = controllerTypeName,
                    Name = string.Format("{0}_{1}", controllerTypeName, customAction.Name)
                }).Distinct());
            }
        }

        private void SaveChangesIfAny(IEnumerable<TModel> operations)
        {
            foreach (var operation in operations)
            {
                var tempOperation = operation;
                var opRecord =
                    OperationRepository.FirstOrDefault(
                        op => op.Name == tempOperation.Name && op.Category == tempOperation.Category);

                if (opRecord != null)
                    continue;

                var entity = DependencyResolver.Current.GetService<TModel>();
                entity.Id = Guid.NewGuid();
                entity.Name = operation.Name;
                entity.Category = operation.Category;
                OperationRepository.Save(entity);
            }
        }
    }
}