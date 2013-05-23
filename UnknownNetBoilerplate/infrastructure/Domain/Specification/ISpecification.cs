using System;
using System.Linq.Expressions;

namespace Infrastructure.Domain.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> IsSatisfied();
        bool IsSatisfied(T entity);
    }
}