using System;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeAreaMembership : FakeDomainObject, IAreaMembership
    {
        public FakeAreaMembership()
        {
            AreaMembershipId = Guid.NewGuid();
        }

        public Guid AreaMembershipId { get; set; }
        public Guid AreaId { get; set; }
        public Guid UserGroupId { get; set; }

        public override Guid Id
        {
            get { return AreaMembershipId; }
            set { AreaMembershipId = value; }
        }

        #region IAreaMembershipBasic Members


        public string Description
        {
            get
            {
                throw new NotImplementedException( );
            }
            set
            {
                throw new NotImplementedException( );
            }
        }

        public string ShortName
        {
            get
            {
                throw new NotImplementedException( );
            }
            set
            {
                throw new NotImplementedException( );
            }
        }

        #endregion
    }
}
