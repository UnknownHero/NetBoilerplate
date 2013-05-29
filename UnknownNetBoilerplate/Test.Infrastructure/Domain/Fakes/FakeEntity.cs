using System;
using Infrastructure.Domain;

namespace Test.Infrastructure.Domain.Fakes
{
    public class FakeEntity : Entity<Guid>
    {
        public string Name { get; set; }
    }
}