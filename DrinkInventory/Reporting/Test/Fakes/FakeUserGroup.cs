using System;
using System.Linq;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeUserGroup : FakeDomainObject, IUserGroup
    {
        public FakeUserGroup()
        {
            UserGroupId = Guid.NewGuid();
        }

        public Guid UserGroupId { get; set; }
        public string Name { get; set; }

        public IQueryable<IOrganization> Organizations
        {
            get { throw new NotImplementedException(); }
        }

        public Guid OrganizationId { get; set; }

        public override Guid Id
        {
            get { return UserGroupId; }
            set { UserGroupId = value; }
        }

        #region IUserGroup Members


        public IOrganization Organization
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

        #region IUserGroup Members


        public System.Collections.Generic.List<IArea> Areas
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

        #region IUserGroup Members


        public System.Collections.Generic.List<IUser> Users
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

        #region IUserGroup Members


        public void AddOrganization( Guid _organizationId )
        {
            throw new NotImplementedException( );
        }

        public void ClearOrganizations( )
        {
            throw new NotImplementedException( );
        }

        public void AddArea( Guid _areaId )
        {
            throw new NotImplementedException( );
        }

        public void ClearAreas( )
        {
            throw new NotImplementedException( );
        }

        public void AddUser( Guid _userId )
        {
            throw new NotImplementedException( );
        }

        public void ClearUsers( )
        {
            throw new NotImplementedException( );
        }

        #endregion

        #region IUserGroup Members


        public System.Collections.Generic.List<Guid> AreaIds
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

        public System.Collections.Generic.List<Guid> OrganizationIds
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

        public System.Collections.Generic.List<Guid> UserIds
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

        #region IUserGroup Members


        System.Collections.Generic.List<IOrganization> IUserGroup.Organizations
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
