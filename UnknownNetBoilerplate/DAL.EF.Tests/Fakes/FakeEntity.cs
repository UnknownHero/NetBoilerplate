using System;
using Infrastructure.Domain;

namespace DAL.EF.Tests.Fakes
{
    public class FakeEntity : Entity<Guid>
    {
        public string Name { get; set; }
    }
}