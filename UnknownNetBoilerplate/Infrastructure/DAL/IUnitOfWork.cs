using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        ITransaction BeginTransaction();

        /// <summary>
        /// Ends transaction.
        /// Note: suggested pattern to manage a transaction is via *using* construct.
        /// You should set input param to null after calling the method.
        /// </summary>
        /// <example>
        /// using ( var tnx = uow.BeginTransaction() ) { /* do some work */ }
        /// </example>
        /// See also <seealso cref="ITransaction"/> interface for more details.
        void EndTransaction(ITransaction transaction);

         
        IGenericRepository<TEntity, TId> GetRepository<TEntity, TId>( )
            where TEntity : Entity<TId>;

    }
}