using System;
using System.ServiceModel;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.WcfService.DataContracts;

namespace Jaxis.DrinkInventory.Reporting.WcfService
{
    [ServiceContract]
    public interface IReportingService
    {

        // Organization
        [OperationContract] ServiceResult GetOrganizations(Guid _sessionId, Guid _parentId);
        [OperationContract] ServiceResult GetOrganization(Guid _sessionId, Guid _organizationId);
        [OperationContract] ServiceResult NewOrganization(Guid _sessionId, Guid _parentId);
        [OperationContract] ServiceResult SaveOrganization(Guid _sessionId, Organization _organization);
        [OperationContract] ServiceResult DeleteOrganization(Guid _sessionId, Guid _organizationId);

        // UPC
        [OperationContract] ServiceResult GetUpcs(Guid _sessionId, string _filter, bool _showValidated);
        [OperationContract] ServiceResult GetUpc(Guid _sessionId, Guid _upcId);
        [OperationContract] ServiceResult NewUpc(Guid _sessionId);
        [OperationContract] ServiceResult SaveUpc(Guid _sessionId, UPC _upc);
        [OperationContract] ServiceResult DeleteUpc(Guid _sessionId, Guid _upcId);

        // User Group
        [OperationContract] ServiceResult GetUserGroups(Guid _sessionId);
        [OperationContract] ServiceResult GetUserGroup(Guid _sessionId, Guid _userGroupId);
        [OperationContract] ServiceResult NewUserGroup(Guid _sessionId);
        [OperationContract] ServiceResult SaveUserGroup(Guid _sessionId, UserGroup _userGroup);
        [OperationContract] ServiceResult DeleteUserGroup(Guid _sessionId, Guid _userGroupId);

        // User
        [OperationContract] ServiceResult GetUsers(Guid _sessionId);
        [OperationContract] ServiceResult GetUser(Guid _sessionId, Guid _userId);
        [OperationContract] ServiceResult NewUser(Guid _sessionId);
        [OperationContract] ServiceResult SaveUser(Guid _sessionId, User _user);
        [OperationContract] ServiceResult DeleteUser(Guid _sessionId, Guid _userId);
        // not sure if i want to keep these special widget operations but they are handy i guess
        [OperationContract] ServiceResult GetVisibleWidgetIds(Guid _sessionId); 
        [OperationContract] ServiceResult SetVisibleWidgetIds(Guid _sessionId, Guid[] _widgetIds);
        
        // Session
        [OperationContract] ServiceResult LogOn(string _userName, string _password);
        [OperationContract] ServiceResult LogOff(Guid _sessionId);
        [OperationContract] ServiceResult SessionActivity(Guid _sessionId);
        [OperationContract] ServiceResult GetSession(Guid _sessionId);

        // Area
        [OperationContract] ServiceResult GetAreas(Guid _sessionId);
        [OperationContract] ServiceResult GetArea(Guid _sessionId, Guid _areaId);
        
        // Report
        [OperationContract] ServiceResult GetReports(Guid _sessionId);
        [OperationContract] ServiceResult GetReport(Guid _sessionId, Guid _reportId);
        [OperationContract] ServiceResult GetReportData(Guid _sessionId, Guid _reportId, object[] _parameters);
        [OperationContract] ServiceResult GetParameters(Guid _sessionId, Guid _reportId);
        
        // Pour
        [OperationContract] ServiceResult GetRecentPours(Guid _sessionId);
    }
}
