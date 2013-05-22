using System;
using Infrastructure.Domain;

namespace Infrastructure.Tests.Domain.Fakes
{
    public class FakeEntity : Entity<Guid>
    {
        public string Name { get; set; }
    }
}