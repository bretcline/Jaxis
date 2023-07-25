using System;
using System.Collections.Generic;
using Jaxis.DrinkInventory.Reporting.BusinessInterfaces;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    class FakeBusinessUserGroup : IUserGroup
    {
        public DateTime ModifiedOn { get; set; }

        public IEnumerable<IArea> Areas
        {
            get { return new List<IArea>(); }
        }

        public IEnumerable<IUser> Users
        {
            get { throw new NotImplementedException(); }
        }

        public Guid UserGroupId { get; set; }
        public string Name { get; set; }
        public void ClearOrganizations()
        {
            throw new NotImplementedException();
        }

        public void AddOrganization(Guid _organizationId)
        {
            throw new NotImplementedException();
        }

        public IOrganization Organization
        {
            get { throw new NotImplementedException(); }
        }

        public void ClearUsers()
        {
            throw new NotImplementedException();
        }

        public void AddUser(Guid _userId)
        {
            throw new NotImplementedException();
        }

        public void AddArea(Guid _areaId)
        {
            throw new NotImplementedException();
        }

        public void ClearAreas()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IOrganization> Organizations { get; set; }
        
        public void Save(IDataManagerFactory _factory)
        {
            var dataUserGroup = _factory.Manage<IUserGroup>().Get(UserGroupId);
            dataUserGroup.Name = Name;
            _factory.Manage<IUserGroup>().Save(dataUserGroup);
        }

        #region IUserGroup Members


        public Guid OrganizationId
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

        #region IUserGroup Members


        IOrganization IUserGroup.Organization
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


        public List<Guid> AreaIds
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

        public List<Guid> OrganizationIds
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

        public List<Guid> UserIds
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


        List<IArea> IUserGroup.Areas
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

        List<IOrganization> IUserGroup.Organizations
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

        List<IUser> IUserGroup.Users
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
