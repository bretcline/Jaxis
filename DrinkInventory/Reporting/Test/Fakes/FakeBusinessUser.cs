using System;
using System.Collections.Generic;
using Jaxis.DrinkInventory.Reporting.BusinessInterfaces;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeBusinessUser : IUser
    {
        public DateTime ModifiedOn { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VisibleWidgetIds { get; set; }

        public Guid OrganizationId
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IEnumerable<IUserGroup> UserGroups
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IArea> Areas
        {
            get { throw new NotImplementedException(); }
        }

        public void Save(IDataManagerFactory _factory)
        {
            throw new NotImplementedException();
        }

        public void AddUserGroup(Guid _userGroupId)
        {
            throw new NotImplementedException();
        }

        public void ClearUserGroups()
        {
            throw new NotImplementedException();
        }

        public IOrganization Organization
        {
            get { throw new NotImplementedException(); }
        }

        public void ClearAreas()
        {
            throw new NotImplementedException();
        }

        public void AddArea(Guid _busAreaId)
        {
            throw new NotImplementedException();
        }

        #region IDomainObject Members

        public Guid Id
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

        #region IDomainObject Members


        public bool IsNew
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


        IOrganization IUser.Organization
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


        public List<Guid> UserGroupIds
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


        List<IUserGroup> IUser.UserGroups
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
