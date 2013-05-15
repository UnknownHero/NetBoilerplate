﻿using System;
using System.Transactions;
using Infrastructure.DAL;

namespace DAL.EF
{
    /// <summary>
    /// Entity framework implementation of the transaction.
    /// </summary>
    public class EntityFrameworkTransaction : ITransaction
    {
        public EntityFrameworkTransaction(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
            this.TransactionScope = new TransactionScope();
        }

        protected IUnitOfWork UnitOfWork { get; private set; }

        protected TransactionScope TransactionScope { get; private set; }

        /// <summary>
        /// Flushes unit of work and commits the transaction scope.
        /// </summary>
        public void Commit()
        {
            this.UnitOfWork.Commit();
            this.TransactionScope.Complete();
        }

        /// <summary>
        /// Rolls back transaction.
        /// Actually the transaction rollback is handled automatically with Dispose method if
        /// transaction scope was not commited.
        /// </summary>
        public void Rollback()
        {
        }

        public void Dispose()
        {
            if (this.TransactionScope != null)
            {
                (this.TransactionScope as IDisposable).Dispose();
                this.TransactionScope = null;
                this.UnitOfWork = null;
            }
        }
    }
}
