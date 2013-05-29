using System;
using System.Collections.Generic;
 
using Infrastructure.Domain;

namespace Infrastructure.DAL
{
    /// <summary>
    /// The interface represents unit of work pattern implementation.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Flushes content of unit of work to the underlying data storage. Causes unsaved
        /// entities to be written to the data storage.
        /// </summary>
        void Commit();
         
        IGenericRepository<TEntity, TId> GetRepository<TEntity, TId>( )
            where TEntity : Entity<TId>;

    }
}