using System;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeUserGroupXOrganization : FakeDomainObject, IUserGroupXOrganization
    {
        public override Guid Id { get; set; }
        public Guid UserGroupXOrganizationId { get; set; }
        public Guid UserGroupId { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
