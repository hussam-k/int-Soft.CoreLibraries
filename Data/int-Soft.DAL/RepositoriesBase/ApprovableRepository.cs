using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using IntSoft.DAL.Common;
using IntSoft.DAL.Extensions;

namespace IntSoft.DAL.RepositoriesBase
{
    public class ApprovableRepository<TEntity> : Repository<TEntity> where TEntity : class, IApprovableEntity 
    {
        #region General Methods

        public virtual void Approve(Guid id, Guid approvedByUserId)
        {
            var entityToApprove = Get(id);

            if (entityToApprove == null)
                throw new ObjectNotFoundException();

            entityToApprove.ApprovedById = approvedByUserId;
            entityToApprove.ApproveDate = DateTime.Now;
            
            Save(entityToApprove);
        }

        #endregion

        #region Count Methods

        public override int GetCount(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? base.GetCount(GetApprovedPredicate()) : base.GetCount(predicate.And(GetApprovedPredicate()));
        }

        public virtual int GetDraftsCount()
        {
            return GetCount(GetDraftsPredicate());
        }

        public virtual int GetAllEntitiesCount()
        {
            return base.GetCount();
        }
        
        #endregion

        #region Approved Entities Method

        public override IEnumerable<TEntity> GetAll()
        {
            return base.GetAll(GetApprovedPredicate()).ToList();
        }
        public override IEnumerable<TEntity> GetAll(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? base.GetAll(pageNumber, pageSize, GetApprovedPredicate()) : base.GetAll(pageNumber, pageSize, predicate.And(GetApprovedPredicate()));
        }

        public override IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return base.GetAll(predicate.And(GetApprovedPredicate()));
        }
        
        #endregion

        #region Drafts Entites Methods

        public virtual IEnumerable<TEntity> GetAllDrafts()
        {
            return base.GetAll(GetDraftsPredicate());
        }

        public virtual IEnumerable<TEntity> GetAllDrafts(Expression<Func<TEntity, bool>> predicate)
        {
            return base.GetAll(predicate.And(GetDraftsPredicate()));
        }

        public virtual IEnumerable<TEntity> GetAllDrafts(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? base.GetAll(pageNumber, pageSize, GetDraftsPredicate()) : base.GetAll(pageNumber, pageSize, predicate.And(GetDraftsPredicate()));
        }
        
        #endregion

        #region Drafts and Approved

        public virtual IEnumerable<TEntity> GetAllEntities(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? base.GetAll(pageNumber, pageSize) : base.GetAll(pageNumber, pageSize, predicate);
        }

        public virtual IEnumerable<TEntity> GetAllEntities()
        {
            return base.GetAll();
        }

        public virtual IEnumerable<TEntity> GetAllEntities(Expression<Func<TEntity, bool>> predicate)
        {
            return base.GetAll(predicate);
        }
        
        #endregion

        #region helper Methods

        protected Expression<Func<TEntity, bool>> GetApprovedPredicate()
        {
            return entity => entity.ApprovedById != null;
        }

        protected Expression<Func<TEntity, bool>> GetDraftsPredicate()
        {
            return entity => entity.ApprovedById == null;
        } 

        #endregion
    }
}