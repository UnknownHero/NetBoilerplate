using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Infrastructure.Domain; 

namespace Infrastructure.DAL
{

    public abstract class IGenericRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        public virtual int Count { get; private set; }

        public abstract IQueryable<TEntity> All();

        public abstract IQueryable<TEntity> AsNoTracking();

        public abstract TEntity GetById(TPrimaryKey id);

        public abstract IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
          
        public abstract IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

        public abstract IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50);

        public abstract bool Contains(Expression<Func<TEntity, bool>> predicate);

        public abstract TEntity Find(params object[] keys);

        public abstract TEntity Find(Expression<Func<TEntity, bool>> predicate);

        public abstract TEntity Create(TEntity entity);

        public abstract void Delete(TPrimaryKey id);

        public abstract void Delete(TEntity entity);

        public abstract void Delete(Expression<Func<TEntity, bool>> predicate);

        public abstract void Update(TEntity entity);
    }
}