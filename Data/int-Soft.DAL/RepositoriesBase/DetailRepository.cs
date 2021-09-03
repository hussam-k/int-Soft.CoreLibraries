using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IntSoft.DAL.Common;

namespace IntSoft.DAL.RepositoriesBase
{
    public abstract class DetailRepository<TEntity> : Repository<TEntity>, IDetailRepository<TEntity>
        where TEntity : class, IEntity
    {
        public int GetCount(Guid masterId, Expression<Func<TEntity, bool>> predicate = null)
        {
            return Context.Set<TEntity>().Where(MasterSelectionPredicate(masterId)).Count(predicate ?? (entity => true));
        }

        public IEnumerable<TEntity> GetAll(Guid masterId)
        {
            return GetAll(MasterSelectionPredicate(masterId));
        }

        public IEnumerable<TEntity> GetAll(Guid masterId, int pageNumber, int pageSize,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            return GetAll(pageNumber, pageSize, MasterSelectionPredicate(masterId));
        }

        public IEnumerable<TEntity> GetAll(Guid masterId, Expression<Func<TEntity, bool>> predicate)
        {
            var body = Expression.AndAlso(MasterSelectionPredicate(masterId).Body, predicate.Body);
            return GetAll(Expression.Lambda<Func<TEntity, bool>>(body, MasterSelectionPredicate(masterId).Parameters[0]));
        }

        public abstract Expression<Func<TEntity, bool>> MasterSelectionPredicate(Guid masterId);
    }
}