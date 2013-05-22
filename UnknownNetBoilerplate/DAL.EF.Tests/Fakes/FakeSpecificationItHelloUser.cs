using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Domain.Specification;

namespace DAL.EF.Tests.Fakes
{
    public class FakeSpecificationItHelloUser : Specification<FakeEntity>
    {
        public override Expression<Func<FakeEntity, bool>> GetExpression()
        {
            return it => it.Name == "Hello";
        }
    }
}
