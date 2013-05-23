using System;
using System.Linq.Expressions;
using Infrastructure.Domain.Specification;


namespace Infrastructure.Tests.Domain.Fakes
{
    public class MishaSpec : Specification<FakeEntity>
    { 
        public override Expression<Func<FakeEntity, bool>> IsSatisfied()
        {
            return en => en.Name == FakeNames.Misha;
        }
    }
}
