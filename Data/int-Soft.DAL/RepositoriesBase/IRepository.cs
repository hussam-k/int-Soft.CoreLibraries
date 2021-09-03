using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IntSoft.DAL.RepositoriesBase
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(Guid id);
        int GetCount(Expression<Func<TEntity, bool>> predicate = null);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Save(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}