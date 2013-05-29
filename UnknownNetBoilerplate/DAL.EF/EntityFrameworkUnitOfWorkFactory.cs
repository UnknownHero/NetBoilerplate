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
       

        public EntityFrameworkUnitOfWorkFactory()
        {
        
        }

      
        public IUnitOfWork BeginUnitOfWork()
        {
            return new EntityFrameworkUnitOfWork(
                ApplicationContainer.Container.GetInstance<IDbContextGenerator>().GetContext()
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