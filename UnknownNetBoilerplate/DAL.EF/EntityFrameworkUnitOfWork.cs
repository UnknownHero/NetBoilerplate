using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using DAL.EF.GenericRepository;
using Infrastructure.DAL;
using Infrastructure.Domain;

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

      
         
        public IGenericRepository<TEntity, TPkey> GetRepository<TEntity, TPkey>() where TEntity : Entity<TPkey>
        {
            return new GenericRepository<TEntity, TPkey>(DbContext);
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