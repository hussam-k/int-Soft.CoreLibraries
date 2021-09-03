using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IntSoft.DAL.RepositoriesBase
{
    public interface IDetailRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        int GetCount(Guid masterId, Expression<Func<TEntity, bool>> predicate = null);
        IEnumerable<TEntity> GetAll(Guid masterId);

        IEnumerable<TEntity> GetAll(Guid masterId, int pageNumber, int pageSize,
            Expression<Func<TEntity, bool>> predicate = null);

        IEnumerable<TEntity> GetAll(Guid masterId, Expression<Func<TEntity, bool>> predicate);
    }
}