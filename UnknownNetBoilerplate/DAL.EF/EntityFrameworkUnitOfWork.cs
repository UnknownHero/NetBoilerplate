using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using DAL.EF.GenericRepository;
using Infrastructure.DAL;
using Infrastructure.Domain;

namespace DAL.EF
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private DbTransaction _trans = null;

        public EntityFrameworkUnitOfWork(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public DbContext DbContext { get; protected set; }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
         
        public IGenericRepository<TEntity, TPkey> GetRepository<TEntity, TPkey>() where TEntity : Entity<TPkey>
        {
            return new GenericRepository<TEntity, TPkey>(DbContext);
        }

        public void BeginTransaction()
        {
            DbContext.Database.ExecuteSqlCommand("BEGIN TRANSACTION"); 
        }

        public void CommitTransaction()
        {
            DbContext.Database.ExecuteSqlCommand("COMMIT TRANSACTION");  
        }

        public void CancelTransaction()
        {
            DbContext.Database.ExecuteSqlCommand("ROLLBACK TRANSACTION");
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