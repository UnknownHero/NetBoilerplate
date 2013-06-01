using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Infrastructure.DAL;
using Infrastructure.Root;

namespace DAL.EF
{
    /// <summary>
    ///     Unit of work factory implementation for Entity Framework.
    ///     Note: implementation based on extension Feature CTP4.
    /// </summary>
    public class EntityFrameworkUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private DbContext _context;

        public EntityFrameworkUnitOfWorkFactory()
        {
           
           
            
        }

      
        public IUnitOfWork BeginUnitOfWork()
        {
            _context = ApplicationContainer.Container.GetInstance<IDbContextGenerator>().GetContext();
             
            return new EntityFrameworkUnitOfWork(
               _context
                );
        }

        public void EndUnitOfWork(IUnitOfWork unitOfWork)
        {
            unitOfWork.Dispose();
        }

        public void Dispose()
        {
           
        }

         
    }
}