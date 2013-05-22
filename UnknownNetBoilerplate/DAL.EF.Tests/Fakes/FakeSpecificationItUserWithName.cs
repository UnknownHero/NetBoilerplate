using System;
using System.Linq.Expressions;
using Infrastructure.Domain.Specification;

namespace DAL.EF.Tests.Fakes
{
    public class FakeSpecificationItUserWithName : Specification<FakeEntity>
    {
        private string name;

        public FakeSpecificationItUserWithName(string hello)
        {
            name = hello;
        }

        public override Expression<Func<FakeEntity, bool>> GetExpression()
        {
            return it => it.Name == name;
        }
    }
}