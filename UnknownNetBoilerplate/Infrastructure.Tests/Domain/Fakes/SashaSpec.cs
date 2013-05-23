﻿using System;
using System.Linq.Expressions;
using Specification;

namespace Infrastructure.Tests.Domain.Fakes
{
    public class SashaSpec : Specification<FakeEntity>
    {
        public override bool is_satisfied_by(FakeEntity value)
        {
            return is_satisfied_by().Compile().Invoke(value);
        }

        public override Expression<Func<FakeEntity, bool>> is_satisfied_by()
        {
            return it => it.Name == FakeNames.Misha;
        }
    }
}
