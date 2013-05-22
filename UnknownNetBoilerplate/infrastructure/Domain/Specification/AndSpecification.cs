using System;
using System.Linq.Expressions;

namespace Infrastructure.Domain.Specification
{
    internal class AndSpecification<TEntity> : Specification<TEntity>
    {
        private Specification<TEntity> Spec1;
        private Specification<TEntity> Spec2;

        internal AndSpecification(Specification<TEntity> s1, Specification<TEntity> s2)
        {
            Spec1 = s1;
            Spec2 = s2;
        }
         
        public override Expression<Func<TEntity, bool>> GetExpression()
        {
            
            return it => Spec1.GetExpression().Compile().Invoke(it) && Spec2.GetExpression().Compile().Invoke(it);
        }
    }
}