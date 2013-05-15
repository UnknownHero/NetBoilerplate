using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using DAL.EF.GenericRepository;
using Infrastructure.DAL;

namespace DAL.EF
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        public EntityFrameworkUnitOfWork(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public DbContext DbContext { get; protected set; }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public ITransaction BeginTransaction()
        {
            return new EntityFrameworkTransaction(this);
        }

        public void EndTransaction(ITransaction transaction)
        {
            if (transaction != null)
            {
                (transaction).Dispose();
                transaction = null;
            }
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            DbContext.Set<TEntity>().Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot update a null entity.");
            }

            var entry = DbContext.Entry<TEntity>(entity);

            // Retreive the Id through reflection
            var pkey = entity.GetType().GetProperty("Id").GetValue(entity, null);

            if (entry.State == EntityState.Detached)
            {
                var set = DbContext.Set<TEntity>();
                TEntity attachedEntity = set.Find(pkey);  // You need to have access to key
                if (attachedEntity != null)
                {
                    var attachedEntry = DbContext.Entry(attachedEntity);
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

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        public TEntity GetById<TEntity, TPrimaryKey>(TPrimaryKey id) where TEntity : class
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public IDbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return DbContext.Set<TEntity>();
        }

        public IList<TEntity> GetAll<TEntity>() where TEntity : class
        { 
            return DbContext.Set<TEntity>().ToList();
        }

        public IGenericRepository<TEntity, TGuid> GetRepository<TEntity, TGuid>(ISpecificationLocator locator) where TEntity : class
        {
            return new GenericRepository<TEntity, TGuid>(this,locator);
        }

        public IGenericRepository<TEntity, TGuid> GetRepository<TEntity, TGuid>( ) where TEntity : class
        {
            return new GenericRepository<TEntity, TGuid>(this);
        }

        public void Dispose()
        {
            if (DbContext != null)
            {
                DbContext.SaveChanges();
                (DbContext as IDisposable).Dispose();
                DbContext = null;
            }
        }
    }
}