using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.EF.Exceptions;
using Infrastructure.DAL;
using Infrastructure.Domain; 

namespace DAL.EF.GenericRepository
{

    public class GenericRepository<TEntity, TPrimaryKey> : IGenericRepository<TEntity, TPrimaryKey>
		where TEntity : Entity<TPrimaryKey>
	{
	    private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            dbContext.Configuration.LazyLoadingEnabled = true;
        }

        #region IRepository<T> Members

        public override int Count
        {
            get { return _dbSet.Count(); }
        }

        public override IQueryable<TEntity> All()
        {
            return _dbSet.AsQueryable();
        }

        public override IQueryable<TEntity> AsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }

        public override TEntity GetById(TPrimaryKey id)
        {
            return _dbSet.Find(id);
        }

        public override IQueryable<TEntity> Get( Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!String.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (string includeProperty in includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).AsQueryable();
            }
            else
            {
                return query.AsQueryable();
            }
        }

       

        public override IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        public override IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index*size;
            IQueryable<TEntity> resetSet = filter != null ? _dbSet.Where(filter).AsQueryable() : _dbSet.AsQueryable();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }

        public override bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Count(predicate) > 0;
        }

        public override TEntity Find(params object[] keys)
        {
            return _dbSet.Find(keys);
        }

        public override TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public override TEntity Create(TEntity entity)
        {
            TEntity newEntry = _dbSet.Add(entity);
            return newEntry;
        }

        public override void Delete(TPrimaryKey id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public override void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public override void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> entitiesToDelete = Filter(predicate);
            foreach (TEntity entity in entitiesToDelete)
            {
                if (_dbContext.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            }
        }

        public override void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot update a null entity.");
            }

            var entry = _dbContext.Entry<TEntity>(entity);

            // Retreive the Id through reflection
            var pkey = entity.GetType().GetProperty("Id").GetValue(entity, null);

            if (entry.State == EntityState.Detached)
            {
                var set = _dbContext.Set<TEntity>();
                TEntity attachedEntity = set.Find(pkey);  // You need to have access to key
                if (attachedEntity != null)
                {
                    var attachedEntry = _dbContext.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }
//            _dbSet.Local.Clear();
//            _dbSet.Attach(entity);
//            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        #endregion
	}
}
