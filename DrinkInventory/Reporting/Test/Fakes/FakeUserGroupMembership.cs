using System;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeUserGroupMembership : FakeDomainObject, IUserGroupMembership
    {
        public FakeUserGroupMembership()
        {
            UserGroupMembershipId = Guid.NewGuid();
        }

        public Guid UserGroupMembershipId { get; set; }
        public Guid UserId { get; set; }
        public Guid UserGroupId { get; set; }

        public override Guid Id
        {
            get { return UserGroupMembershipId; }
            set { UserGroupMembershipId = value; }
        }
    }
}
