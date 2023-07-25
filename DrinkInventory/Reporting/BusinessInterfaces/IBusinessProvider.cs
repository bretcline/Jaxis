using System;
using System.Collections.Generic;
using System.Data;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.BusinessInterfaces
{
    public interface IBusinessProvider
    {
        // Organization
        IEnumerable<IOrganization> GetOrganizations(Guid _sessionId, Guid _parentId);
        IOrganization GetOrganization(Guid _sessionId, Guid _organizationId);
        IOrganization NewOrganization(Guid _sessionId, Guid _parentId);
        IOrganization SaveOrganization(Guid _sessionId, IOrganization _organization);
        void DeleteOrganization(Guid _sessionId, Guid _organizationId);

        // Session
        ISession LogOn(string _userName, string _password);
        void LogOff(Guid _sessionId);
        void SessionActivity(Guid _sessionId);
        bool SessionIsValid(Guid _sessionId);
        ISession GetSession(Guid _sessionId);

        // Area
        IArea GetArea(Guid _sessionId, Guid _areaId);
        IEnumerable<IArea> GetAreas(Guid _sessionId);

        // Report
        IReport GetReport(Guid _sessionId, Guid _reportId);
        IEnumerable<IReport> GetReports(Guid _sessionId);
        IEnumerable<IParameter> GetParameters(Guid _sessionId, Guid _viewId);

        // User Group
        IEnumerable<IUserGroup> GetUserGroups(Guid _sessionId);
        IUserGroup GetUserGroup(Guid _sessionId, Guid _userGroupId);
        IUserGroup NewUserGroup(Guid _sessionId);
        IUserGroup SaveUserGroup(Guid _sessionId, IUserGroup _userGroup);
        void DeleteUserGroup(Guid _sessionId, Guid _userGroupId);

        // User
        IEnumerable<IUser> GetUsers(Guid _sessionId);
        IUser GetUser(Guid _sessionId, Guid _userId);
        IUser NewUser(Guid _sessionId);
        IUser SaveUser(Guid _sessionId, IUser _user);
        void DeleteUser(Guid _sessionId, Guid _userId);
        IEnumerable<Guid> GetVisibleWidgetIds(Guid _sessionId);
        void SetVisibleWidgetIds(Guid _sessionId, IEnumerable<Guid> _widgetIds);

        // Pour
        DataTable GetRecentPours(Guid _sessionId);
        
        // UPC
        IEnumerable<IUPC> GetUpcs(Guid _sessionId, string _filter, bool _showValidated);
        IUPC GetUpc(Guid _sessionId, Guid _upcId);
        IUPC NewUpc(Guid _sessionId);
        IUPC SaveUpc(Guid _sessionId, IUPC _upc);
        void DeleteUpc(Guid _sessionId, Guid _upcId);
    }
}
