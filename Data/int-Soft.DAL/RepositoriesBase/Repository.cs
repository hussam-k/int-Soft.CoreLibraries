using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using IntSoft.DAL.Common;
using StructureMap.Attributes;

namespace IntSoft.DAL.RepositoriesBase
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity 
    {
        [SetterProperty]
        public DbContext Context { get; set; }

        public virtual TEntity Get(Guid id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> predicate = null)
        {
            return Context.Set<TEntity>().Count(predicate ?? (entity => true));
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }
        public virtual IEnumerable<TEntity> GetAll(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null
                ? Context.Set<TEntity>().OrderBy(x => x.Id).Skip((pageNumber - 1)*pageSize).Take(pageSize).ToList()
                : Context.Set<TEntity>()
                    .Where(predicate)
                    .OrderBy(x => x.Id)
                    .Skip((pageNumber - 1)*pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Any(predicate);
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public virtual void Save(TEntity entity)
        {
            if (entity.Id == default(Guid))
            {
                entity.Id = Guid.NewGuid();
            }
            Context.Set<TEntity>().AddOrUpdate(entity);
            Save();
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
            Save();
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            Save();
        }
        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
            Save();
        }

        protected void Save()
        {
            Context.SaveChanges();
        }
    }
}