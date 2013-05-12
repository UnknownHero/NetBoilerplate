using System.Linq;

namespace Infrastructure.DAL
{
    /// <summary>
    /// Helper interface to convert <see cref="IUnitOfWork"/> to various data providers.
    /// </summary>
    public interface IUnitOfWorkConvertor
    {
        /// <summary>
        /// Gets <see cref="IQueryable"/> from given <see cref="IUnitOfWork"/> implementation.
        /// </summary>
        IQueryable<TEntity> ToQueryable<TEntity>(IUnitOfWork unitOfWork) where TEntity : class;
    }
}