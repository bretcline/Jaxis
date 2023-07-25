using System;
using System.Linq;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeOrganization : FakeDomainObject, IOrganization
    {
        public FakeOrganization()
        {
            OrganizationId = Guid.NewGuid();
        }

        public Guid OrganizationId { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }

        public IQueryable<IOrganization> Children
        {
            get { return (from c in FakeOrganizationDataManager.Objects where c.ParentId == OrganizationId select c).AsQueryable(); }
        }

        public IOrganization Parent
        {
            get { return ( from p in FakeOrganizationDataManager.Objects where p.OrganizationId == ParentId select p ).FirstOrDefault( ); }
        }

        public string Path { get; set; }

        public override Guid Id
        {
            get { return OrganizationId; }
            set { OrganizationId = value; }
        }

        #region IOrganization Members


        public System.Collections.Generic.List<IUserGroup> UserGroups
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

        #region IOrganization Members


        public void ClearUserGroups( )
        {
            throw new NotImplementedException( );
        }

        public void AddUserGroup( Guid _userGroupId )
        {
            throw new NotImplementedException( );
        }

        #endregion

        #region IOrganizationBasic Members


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

        #endregion

        #region IOrganization Members


        public System.Collections.Generic.List<Guid> UserGroupIds
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
