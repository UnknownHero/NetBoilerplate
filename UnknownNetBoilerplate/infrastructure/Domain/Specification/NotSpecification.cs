using System;
using System.Linq.Expressions;

namespace Infrastructure.Domain.Specification
{
    internal class NotSpecification<TEntity> : Specification<TEntity>
    { 

        private Specification<TEntity> Wrapped;

        internal NotSpecification(Specification<TEntity> x)
        {
            Wrapped = x;
        }
  
        public override Expression<Func<TEntity, bool>> GetExpression()
        { 
            var expression = Wrapped.GetExpression(); 

            return it => !expression.Compile().Invoke(it);
        } 
    }
}