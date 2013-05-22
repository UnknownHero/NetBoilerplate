using System;
using System.Linq.Expressions;
using Extensions;

namespace Infrastructure.Domain.Specification
{
    public abstract class  Specification<T>
    {
        public  bool IsSatisfiedBy(T entity)
        {
            var expression = GetExpression();

            return  expression.Compile().Invoke(entity);

        }

        public abstract Expression<Func<T, bool>> GetExpression();

    }
}
