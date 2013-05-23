using System;
using System.Linq.Expressions;

namespace Infrastructure.Domain.Specification
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract Expression<Func<T, bool>> IsSatisfied();

        public bool IsSatisfied(T entity)
        {
            return IsSatisfied().Compile().Invoke(entity);
        }

        public Specification<T> And(ISpecification<T> otherSpecification)
        {
            return new AndSpecification<T>(this, otherSpecification);
        }

        public Specification<T> Or(ISpecification<T> otherSpecification)
        {
            return new OrSpecification<T>(this, otherSpecification);
        }

        public Specification<T> Negated(ISpecification<T> otherSpecification)
        {
            return new NegatedSpecification<T>(this);
        }
    }
}