using System;
using System.Collections.Generic;
using System.Data;
using Jaxis.DrinkInventory.Reporting.Tools;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using Jaxis.DrinkInventory.Reporting.Data.POCO;


namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeReportingService : IReportingService, IDisposable
    {
        static FakeReportingService()
        {
        }

        public ServiceResult LogOn(string _userName, string _password)
        {
            throw new NotImplementedException();
        }

        public ServiceResult LogOff(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult SessionActivity(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetAreas(Guid _sessionId)
        {
            return new GetAreasResult {Areas = new List<Area>( )};
        }

        public ServiceResult GetArea(Guid _sessionId, Guid _areaId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetSections(Guid _sessionId, Guid _areaId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetViews(Guid _sessionId, Guid _sectionId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetViewData(Guid _sessionId, Guid _viewId, object[] _parameters)
        {
            var result = new GetDataTableResult();
            result.Data = new DataTable("Table");
            return result;
        }

        public ServiceResult GetReportData(Guid _sessionId, Guid _reportId, List<object> _parameters)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetOrganization(Guid _sessionId, Guid _organizationId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult NewOrganization(Guid _sessionId, Guid _parentId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetAreaForSection(Guid _sessionId, Guid _sectionId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetParameters(Guid _sessionId, Guid _viewId)
        {
            var result = new GetParametersResult();
            result.Parameters = new List<Jaxis.DrinkInventory.Reporting.Data.POCO.Parameter>( );
            return result;
        }

        public ServiceResult AddOrganization(Guid _sessionId, Guid _parentId, string _name, string _shortName)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetOrganizations(Guid _sessionId, Guid _parentId)
        {
            var result = new GetOrganizationsResult();
            result.Organizations = new List<Jaxis.DrinkInventory.Reporting.Data.POCO.Organization>( );
            return result;
        }

        public ServiceResult SaveOrganization(Guid _sessionId, Jaxis.DrinkInventory.Reporting.Data.POCO.Organization _organization)
        {
            throw new NotImplementedException();
        }

        public ServiceResult DeleteOrganization(Guid _sessionId, Guid _organizationId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetUsers(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult AddUser(Guid _sessionId, string _userName, string _password)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetUser(Guid _sessionId, Guid _userId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetUserGroups(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetUserGroup(Guid _sessionId, Guid _userGroupId)
        {
            var result = new GetUserGroupResult
            {
                UserGroup = new UserGroup
                {
                    ModifiedOn = Services.Clock.Now,
                    Name = "Test User Group",
                    //Organizations = new Organization[0],
                    UserGroupId = Guid.NewGuid()
                }
            };

            return result;
        }

        public ServiceResult NewUserGroup(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult SaveUser(Guid _sessionId, User _user)
        {
            throw new NotImplementedException();
        }

        public ServiceResult DeleteUser(Guid _sessionId, Guid _userId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetSession(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult AddUserGroup(Guid _sessionId, string _name)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetVisibleWidgetIds(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult SetVisibleWidgetIds(Guid _sessionId, List<Guid> _widgetIds)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetRecentPours(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetUpcs(Guid _sessionId, string _filter, bool _showValidated)
        {
            throw new NotImplementedException();
        }

        public ServiceResult SaveUserGroup(Guid _sessionId, UserGroup _userGroup)
        {
            throw new NotImplementedException();
        }

        public ServiceResult DeleteUserGroup(Guid _sessionId, Guid _userGroupId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult UpdateOrganization(Guid _sessionId, Guid _organizationId, string _name, string _shortName)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetUpcs(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetUpc(Guid _sessionId, Guid _upcId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult NewUpc(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult DeleteUpc(Guid _sessionId, Guid _upcId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult SaveUpc(Guid _sessionId, UPC _upc)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetReports(Guid _sessionId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetReport(Guid _sessionId, Guid _reportId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #region IReportingService Members


        public ServiceResult SaveUser( Guid _sessionId, object _user )
        {
            throw new NotImplementedException( );
        }

        #endregion

        #region IReportingService Members


        public ServiceResult SaveOrganization( Guid _sessionId, object _organization )
        {
            throw new NotImplementedException( );
        }

        public ServiceResult SaveUserGroup( Guid _sessionId, object _userGroup )
        {
            throw new NotImplementedException( );
        }

        public ServiceResult SaveUpc( Guid _sessionId, object _upc )
        {
            throw new NotImplementedException( );
        }

        #endregion

        #region IReportingService Members


        public ServiceResult AddUpc( Guid _sessionId, string _name, string _itemNumber )
        {
            throw new NotImplementedException( );
        }

        #endregion
    }
}
