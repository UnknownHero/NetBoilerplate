using System;
using System.Linq.Expressions;
using Infrastructure.Domain.Specification;
using Infrastructure.Tests.Domain.Fakes; 
namespace Infrastructure.Tests.Domain.Fakes

{
    public class UraSpec : Specification<FakeEntity>
    {
        public override Expression<Func<FakeEntity, bool>> IsSatisfied()
        {
            return en => en.Name == FakeNames.Ura;
        }
    }
}