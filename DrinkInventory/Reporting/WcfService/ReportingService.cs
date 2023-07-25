using System;
using Jaxis.DrinkInventory.Reporting.Business;
using Jaxis.DrinkInventory.Reporting.BusinessInterfaces;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.WcfService.DataContracts;
using Jaxis.Util.Log4Net;

namespace Jaxis.DrinkInventory.Reporting.WcfService
{
    public class ReportingService : IReportingService
    {
        #region General ---------------------------------------------------------------------------

        private static IBusinessProvider m_businessProvider;

        public static void Init(IBusinessProvider _businessProvider)
        {
            m_businessProvider = _businessProvider;
        }

        private static ServiceResult HandleRequest(Func<ServiceResult> _action)
        {
            try
            {
                return _action();
            }
            catch (Exception exception)
            {
                Log.Exception(exception);
                return new ExceptionResult { Message = exception.Message };
            }
        }

        internal static IBusinessProvider BusinessProvider
        {
            // Should be the only reference to the concrete BusinessProvider.  Should be able to easily swap it out to use another
            get { return m_businessProvider ?? (m_businessProvider = new BusinessProvider()); }
        }

        #endregion

        #region Organization ----------------------------------------------------------------------

        public ServiceResult GetOrganizations(Guid _sessionId, Guid _parentId)
        {
            return HandleRequest(() =>
            {
                var organizations = BusinessProvider.GetOrganizations(_sessionId, _parentId);
                return new GetOrganizationsResult(organizations);
            });
        }

        public ServiceResult GetOrganization(Guid _sessionId, Guid _organizationId)
        {
            return HandleRequest(() =>
            {
                var organization = BusinessProvider.GetOrganization(_sessionId, _organizationId);
                return new GetOrganizationResult(organization);
            });
        }

        public ServiceResult NewOrganization(Guid _sessionId, Guid _parendId)
        {
            return HandleRequest(() =>
            {
                var org = BusinessProvider.NewOrganization(_sessionId, _parendId);
                return new GetOrganizationResult(org);
            });
        }

        public ServiceResult SaveOrganization(Guid _sessionId, Organization _organization)
        {
            return HandleRequest(() =>
            {
                var organization = BusinessProvider.SaveOrganization(_sessionId, _organization);
                return new GetOrganizationResult(organization);
            });
        }

        public ServiceResult DeleteOrganization(Guid _sessionId, Guid _organizationId)
        {
            return HandleRequest(() =>
            {
                BusinessProvider.DeleteOrganization(_sessionId, _organizationId);
                return new SuccessResult();
            });
        }

        #endregion

        #region UPC -------------------------------------------------------------------------------

        public ServiceResult GetUpcs(Guid _sessionId, string _filter, bool _showValidated)
        {
            return HandleRequest(() =>
            {
                var upcs = BusinessProvider.GetUpcs(_sessionId, _filter, _showValidated);
                return new GetUpcsResult(upcs);
            });
        }

        public ServiceResult GetUpc(Guid _sessionId, Guid _upcId)
        {
            return HandleRequest(() =>
            {
                var upc = BusinessProvider.GetUpc(_sessionId, _upcId);
                return new GetUpcResult(upc);
            });
        }

        public ServiceResult NewUpc(Guid _sessionId)
        {
            return HandleRequest(() =>
            {
                var upc = BusinessProvider.NewUpc(_sessionId);
                return new GetUpcResult(upc);
            });
        }

        public ServiceResult SaveUpc(Guid _sessionId, UPC _upc)
        {
            return HandleRequest(() =>
            {
                var upc = BusinessProvider.SaveUpc(_sessionId, _upc);
                return new GetUpcResult(upc);
            });
        }

        public ServiceResult DeleteUpc(Guid _sessionId, Guid _upcId)
        {
            return HandleRequest(() =>
            {
                BusinessProvider.DeleteUpc(_sessionId, _upcId);
                return new SuccessResult();
            });
        }

        #endregion

        #region User Group ------------------------------------------------------------------------

        public ServiceResult GetUserGroups(Guid _sessionId)
        {
            return HandleRequest(() =>
            {
                var userGroups = BusinessProvider.GetUserGroups(_sessionId);
                return new GetUserGroupsResult(userGroups);
            });
        }

        public ServiceResult GetUserGroup(Guid _sessionId, Guid _userGroupId)
        {
            return HandleRequest(() =>
            {
                var userGroup = BusinessProvider.GetUserGroup(_sessionId, _userGroupId);
                return new GetUserGroupResult(userGroup);
            });
        }

        public ServiceResult NewUserGroup(Guid _sessionId)
        {
            return HandleRequest(() =>
            {
                var userGroup = BusinessProvider.NewUserGroup(_sessionId);
                return new GetUserGroupResult(userGroup);
            });
        }

        public ServiceResult SaveUserGroup(Guid _sessionId, UserGroup _userGroup)
        {
            return HandleRequest(() =>
            {
                var savedGroup = BusinessProvider.SaveUserGroup(_sessionId, _userGroup);
                return new GetUserGroupResult(savedGroup);
            });
        }

        public ServiceResult DeleteUserGroup(Guid _sessionId, Guid _userGroupId)
        {
            return HandleRequest(() =>
            {
                BusinessProvider.DeleteUserGroup(_sessionId, _userGroupId);
                return new SuccessResult();
            });
        }

        #endregion

        #region User ------------------------------------------------------------------------------

        public ServiceResult GetUsers(Guid _sessionId)
        {
            return HandleRequest(() =>
            {
                var users = BusinessProvider.GetUsers(_sessionId);
                return new GetUsersResult(users);
            });
        }

        public ServiceResult GetUser(Guid _sessionId, Guid _userId)
        {
            return HandleRequest(() =>
            {
                var user = BusinessProvider.GetUser(_sessionId, _userId);
                return new GetUserResult(user);
            });
        }

        public ServiceResult NewUser(Guid _sessionId)
        {
            return HandleRequest(() =>
            {
                var user = BusinessProvider.NewUser(_sessionId);
                return new GetUserResult(user);
            });
        }
        
        public ServiceResult SaveUser(Guid _sessionId, User _user)
        {
            return HandleRequest(() =>
            {
                var user = BusinessProvider.SaveUser(_sessionId, _user);
                return new GetUserResult(user);
            });
        }

        public ServiceResult DeleteUser(Guid _sessionId, Guid _userId)
        {
            return HandleRequest(() =>
            {
                BusinessProvider.DeleteUser(_sessionId, _userId);
                return new SuccessResult();
            });
        }

        public ServiceResult GetVisibleWidgetIds(Guid _sessionId)
        {
            return HandleRequest(() =>
            {
                var widgetIds = BusinessProvider.GetVisibleWidgetIds(_sessionId);
                return new GetVisibleWidgetIdsResult(widgetIds);
            });
        }

        public ServiceResult SetVisibleWidgetIds(Guid _sessionId, Guid[] _widgetIds)
        {
            return HandleRequest(() =>
            {
                BusinessProvider.SetVisibleWidgetIds(_sessionId, _widgetIds);
                return new SuccessResult();
            });
        }

        #endregion

        #region Session ---------------------------------------------------------------------------

        public ServiceResult LogOn(string _userName, string _password)
        {
            return HandleRequest(() =>
            {
                var businessSession = BusinessProvider.LogOn(_userName, _password);
                var result = new LogOnResult { Session = businessSession == null ? null : ( Session ) businessSession };
                return result;
            });
        }

        public ServiceResult LogOff(Guid _sessionId)
        {
            return HandleRequest(() =>
            {
                BusinessProvider.LogOff(_sessionId);
                return new SuccessResult();
            });
        }

        public ServiceResult SessionActivity(Guid _sessionId)
        {
            return HandleRequest(() =>
            {
                BusinessProvider.SessionActivity(_sessionId);
                return new SuccessResult();
            });
        }

        public ServiceResult GetSession(Guid _sessionId)
        {
            return HandleRequest(() =>
            {
                var businessSession = BusinessProvider.GetSession(_sessionId);
                var result = new LogOnResult { Session = businessSession == null ? null : (Session)businessSession };
                return result;
            });
        }

        #endregion

        #region Area ------------------------------------------------------------------------------

        public ServiceResult GetAreas(Guid _sessionId)
        {
            return HandleRequest(() =>
            {
                var areas = BusinessProvider.GetAreas(_sessionId);
                return new GetAreasResult(areas);
            });
        }

        public ServiceResult GetArea(Guid _sessionId, Guid _areaId)
        {
            return HandleRequest(() =>
            {
                var area = BusinessProvider.GetArea(_sessionId, _areaId);
                return new GetAreaResult(area);
            });
        }
        
        #endregion

        #region Report ----------------------------------------------------------------------------

        public ServiceResult GetReports(Guid _sessionId)
        {
            return HandleRequest(() =>
            {
                var reports = BusinessProvider.GetReports(_sessionId);
                return new GetReportsResult(reports);
            });
        }

        public ServiceResult GetReport(Guid _sessionId, Guid _reportId)
        {
            return HandleRequest(() =>
            {
                var report = BusinessProvider.GetReport(_sessionId, _reportId);
                return new GetReportResult(report);
            });
        }

        public ServiceResult GetReportData(Guid _sessionId, Guid _viewId, object[] _parameters)
        {
            return HandleRequest(() =>
            {
                var view = BusinessProvider.GetReport(_sessionId, _viewId);
                var data = view.GetData(_parameters);
                return new GetDataTableResult(data);
            });
        }

        public ServiceResult GetParameters(Guid _sessionId, Guid _viewId)
        {
            return HandleRequest(() =>
            {
                var parameters = BusinessProvider.GetParameters(_sessionId, _viewId);
                return new GetParametersResult(parameters);
            });
        }

        #endregion

        #region Pour ------------------------------------------------------------------------------

        public ServiceResult GetRecentPours(Guid _sessionId)
        {
            return HandleRequest(() =>
            {
                var pours = BusinessProvider.GetRecentPours(_sessionId);
                return new GetDataTableResult(pours);
            });
        }

        #endregion
    }
}
