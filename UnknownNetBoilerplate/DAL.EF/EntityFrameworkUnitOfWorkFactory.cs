using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Infrastructure.DAL;

namespace DAL.EF
{
    /// <summary>
    ///     Unit of work factory implementation for Entity Framework.
    ///     Note: implementation based on extension Feature CTP4.
    /// </summary>
    public class EntityFrameworkUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private DbContext _dbContext;

        public EntityFrameworkUnitOfWorkFactory(string connectionString, DbModel dbModel)
        {
            ConnectionString = connectionString;
            DbModel = dbModel;
        }

        public EntityFrameworkUnitOfWorkFactory( DbContext dbContext)
        {
       
            _dbContext = dbContext;
        }

        protected string ConnectionString { get; private set; }

        protected DbModel DbModel { get; private set; }

        public IUnitOfWork BeginUnitOfWork()
        {
            CreateDbContext();
            return new EntityFrameworkUnitOfWork(
                _dbContext
                );
        }

        public void EndUnitOfWork(IUnitOfWork unitOfWork)
        {
            var linqToSqlUnitOfWork = unitOfWork as EntityFrameworkUnitOfWork;
            if (linqToSqlUnitOfWork != null)
            {
                linqToSqlUnitOfWork.Dispose();
                linqToSqlUnitOfWork = null;
            }
        }

        public void Dispose()
        {
            ConnectionString = null;
            DbModel = null;
        }

        private void CreateDbContext()
        {
            if (_dbContext == null && DbModel != null)
            {
                _dbContext = new DbContext(
                    ConnectionString
                    , DbModel.Compile()
                    );
            }
        }
    }
}