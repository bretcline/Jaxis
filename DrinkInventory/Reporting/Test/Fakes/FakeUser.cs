using System;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeUser : FakeDomainObject, IUser
    {
        public FakeUser()
        {
            UserId = Guid.NewGuid();
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VisibleWidgetIds { get; set; }

        public Guid OrganizationId
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override Guid Id
        {
            get { return UserId; }
            set { UserId = value; }
        }

        #region IUser Members


        public System.Collections.Generic.List<IUserGroupMembership> UserGroupMemberships
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

        #region IUser Members


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

        #region IUser Members


        public void ClearUserGroups( )
        {
            throw new NotImplementedException( );
        }

        public void AddUserGroup( Guid _userGroupId )
        {
            throw new NotImplementedException( );
        }

        #endregion

        #region IUser Members


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
