using System;
using System.Collections.Generic;
using Jaxis.DrinkInventory.Reporting.BusinessInterfaces;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeBOrganization : IOrganization
    {
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public Guid? ParentId { get; set; }

        public IEnumerable<IUserGroup> UserGroups
        {
            get { throw new NotImplementedException(); }
        }

        public void Save(IDataManagerFactory _factory)
        {
            throw new NotImplementedException();
        }

        public void ClearUserGroups()
        {
            throw new NotImplementedException();
        }

        public void AddUserGroup(Guid _userGroupId)
        {
            throw new NotImplementedException();
        }

        #region IOrganization Members


        public string Path
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

        public DateTime ModifiedOn
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

        #region IOrganization Members


        List<IUserGroup> IOrganization.UserGroups
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
