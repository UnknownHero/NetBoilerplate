using System;
using System.Linq.Expressions;
using Infrastructure.Domain.Specification.Extensions;

namespace Infrastructure.Domain.Specification
{
    public class OrSpecification<T> : Specification<T>
    {
        readonly ISpecification<T> _first;
        readonly ISpecification<T> _second;

        public OrSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            _first = first;
            _second = second;
        }

        public override Expression<Func<T, bool>> IsSatisfied()
        {
            return _first.IsSatisfied().Or(_second.IsSatisfied());
        }
    }
}