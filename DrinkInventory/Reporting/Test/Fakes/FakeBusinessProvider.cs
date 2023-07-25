using System;
using System.Collections.Generic;
using System.Data;
using Jaxis.DrinkInventory.Reporting.Business;
using Jaxis.DrinkInventory.Reporting.BusinessInterfaces;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using Jaxis.DrinkInventory.Reporting.Tools;
using Jaxis.DrinkInventory.Reporting.Data;
using Jaxis.DrinkInventory.Reporting.Data.POCO;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeBusinessProvider : IBusinessProvider
    {
        public ISession LogOn(string _userName, string _password)
        {
            var session = new Session();
            session.ExpirationTime = Services.Clock.Now.AddHours(1);
            session.ModifiedOn = Services.Clock.Now;
            session.SessionId = Guid.NewGuid();
            var fakeUser = new FakeBusinessUser();
            fakeUser.ModifiedOn = Services.Clock.Now;
            fakeUser.Password = "password";
            fakeUser.UserId = Guid.NewGuid();
            fakeUser.UserName = "username";
            fakeUser.VisibleWidgetIds = Guid.NewGuid().ToString();
            session.User = fakeUser;
            
            return session;
        }

        public void LogOff(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public void SessionActivity(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public bool SessionIsValid(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public IArea GetArea(Guid _sessionId, Guid _areaId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IArea> IBusinessProvider.GetAreas(Guid _sessionId)
        {
            return GetAreas(_sessionId);
        }

        public IList<IArea> GetAreas(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public IArea GetAreaForSection(Guid _sessionId, Guid _sectionId)
        {
            throw new NotImplementedException();
        }

        public IReport GetReport(Guid _sessionId, Guid _viewId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IReport> GetReports(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        IOrganization IBusinessProvider.GetOrganization(Guid _sessionId, Guid _organizationId)
        {
            throw new NotImplementedException();
        }

        public IOrganization NewOrganization(Guid _sessionId, Guid _parentId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IOrganization> IBusinessProvider.GetOrganizations(Guid _sessionId, Guid _parentId)
        {
            throw new NotImplementedException();
        }


        public IOrganization SaveOrganization(Guid _sessionId, IOrganization _organization)
        {
            throw new NotImplementedException();
        }

        public IList<IReport> GetViews(Guid _sessionId, Guid _sectionId)
        {
            throw new NotImplementedException();
        }

        public IOrganization GetOrganization(Guid _sessionId, Guid _organizationId)
        {
            throw new NotImplementedException();
        }

        public IList<IOrganization> GetOrganizations(Guid _sessionId, Guid _parentId)
        {
            throw new NotImplementedException();
        }

        public IOrganization AddOrganization(Guid _sessionId, Guid _parentId, string _name, string _shortName)
        {
            throw new NotImplementedException();
        }

        public void SaveOrganization(IOrganization _organization)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrganization(Guid _sessionId, Guid _organizationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUPC> GetUpcs(Guid _sessionId, string _filter, bool _showValidated)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IParameter> IBusinessProvider.GetParameters(Guid _sessionId, Guid _viewId)
        {
            return GetParameters(_sessionId, _viewId);
        }

        IEnumerable<IUser> IBusinessProvider.GetUsers(Guid _sessionId)
        {
            return GetUsers(_sessionId);
        }

        public IList<IParameter> GetParameters(Guid _sessionId, Guid _viewId)
        {
            throw new NotImplementedException();
        }

        public IList<IUser> GetUsers(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public IUser AddUser(Guid _sessionId, string _userName, string _password)
        {
            throw new NotImplementedException();
        }

        public IUser GetUser(Guid _sessionId, Guid _userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(Guid _sessionId, Guid _userId)
        {
            throw new NotImplementedException();
        }

        public void SaveUser(Guid _sessionId, IUser _user)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IUserGroup> IBusinessProvider.GetUserGroups(Guid _sessionId)
        {
            return GetUserGroups(_sessionId);
        }

        public IList<IUserGroup> GetUserGroups(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public ISession GetSession(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public IUserGroup AddUserGroup(Guid _sessionId, string _name)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Guid> IBusinessProvider.GetVisibleWidgetIds(Guid _sessionId)
        {
            return GetVisibleWidgetIds(_sessionId);
        }

        public IList<Guid> GetVisibleWidgetIds(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public void SetVisibleWidgetIds(Guid _sessionId, IEnumerable<Guid> _widgetIds)
        {
            throw new NotImplementedException();
        }

        public DataTable GetRecentPours(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public IUserGroup GetUserGroup(Guid _sessionId, Guid _userGroupId)
        {
            var userGroup = new FakeBusinessUserGroup();
            userGroup.ModifiedOn = Services.Clock.Now;
            userGroup.Name = "usergroup name";
            var fakeOrganization = new FakeBOrganization();
            var organizations = new List<IOrganization>();
            organizations.Add(fakeOrganization);
            userGroup.Organizations = organizations;
            userGroup.UserGroupId = Guid.NewGuid();
            return userGroup;
        }

        public IUserGroup NewUserGroup(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public IUserGroup SaveUserGroup(Guid _sessionId, IUserGroup _userGroup)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserGroup(Guid _sessionId, Guid _userGroupId)
        {
            throw new NotImplementedException();
        }

        public IList<IOrganization> GetOrganizationsForUserGroup(Guid _sessionId, Guid _userGroupId)
        {
            throw new NotImplementedException();
        }

        public IUPC GetUpc(string _itemNumber)
        {
            throw new NotImplementedException();
        }

        public IUPC GetUpc(Guid _sessionId, Guid _upcId)
        {
            throw new NotImplementedException();
        }

        public IUPC NewUpc(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public void DeleteUpc(Guid _sessionId, Guid _upcId)
        {
            throw new NotImplementedException();
        }

        public IUPC SaveUpc(Guid _sessionId, IUPC _upc)
        {
            throw new NotImplementedException();
        }

    }
}
