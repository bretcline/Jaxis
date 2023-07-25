using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;

namespace Jaxis.DrinkInventory.Reporting.Web2.Controllers
{
    public class OrganizationManagerController : BaseController
    {
        [UserSessionFilter] [HttpGet] public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetOrganizations(Guid? _organizationId)
        {
            return HandleAjaxGetAction(() =>
            {
                var result = (GetOrganizationsResult)ServiceFactory.Reporting.WithService(
                    _svc => _svc.GetOrganizations(SessionId, _organizationId ?? Guid.Empty));
                return result.Organizations;
            });
        }

        public JsonResult GetOrganization(Guid _organizationId)
        {
            return HandleAjaxGetAction(() =>
            {
                var orgResult = (GetOrganizationResult)ServiceFactory.Reporting.WithService(
                    _svc => _svc.GetOrganization(SessionId, _organizationId));
                return GetOrgResponse(orgResult.Organization);
            });
        }

        private object GetOrgResponse(Organization _organization)
        {
            var groupsResult = (GetUserGroupsResult)ServiceFactory.Reporting.WithService(
                _svc => _svc.GetUserGroups(SessionId));

            var availableGroups = new List<UserGroup>();
            var memberGroups = new List<UserGroup>();

            foreach (var group in groupsResult.UserGroups)
            {
                if (_organization.UserGroupIds != null && _organization.UserGroupIds.Any(_id => _id == group.UserGroupId))
                {
                    memberGroups.Add(group);
                }
                else
                {
                    availableGroups.Add(group);
                }
            }

            return new
            {
                Organization = new
                {
                    _organization.OrganizationId,
                    _organization.ParentId,
                    _organization.Name,
                    _organization.ShortName,
                    UserGroups = memberGroups,
                    _organization.IsNew
                },
                AvailableGroups = availableGroups
            };
        }

        public JsonResult NewOrganization(Guid _parentId)
        {
            return HandleAjaxGetAction(() =>
            {
                var result = (GetOrganizationResult)ServiceFactory.Reporting.WithService(
                    _svc => _svc.NewOrganization(SessionId, _parentId));
                return GetOrgResponse(result.Organization);
            });
        }

        public JsonResult SaveOrganization(Organization _organization)
        {
            return HandleAjaxPostAction(() =>
            {
                FixPostedList(_organization.UserGroupIds);
                var result = (GetOrganizationResult)ServiceFactory.Reporting.WithService(
                    _svc => _svc.SaveOrganization(SessionId, _organization));
                return GetOrgResponse(result.Organization);
            }, "Organization Saved");
        }

        public JsonResult DeleteOrganization(Guid _orgId)
        {
            return HandleAjaxPostAction(() =>
            {
                ServiceFactory.Reporting.WithService(
                    _svc => _svc.DeleteOrganization(SessionId, _orgId));
                return null;
            }, "Organization Deleted");
        }

        public static object JsonSafe(IOrganization _organization)
        {
            return _organization == null ? null :
            new
            {
                _organization.OrganizationId,
                _organization.Name,
                _organization.Description,
                _organization.ParentId,
                _organization.Path,
                _organization.ShortName,
                _organization.IsNew
            };
        }
    }
}
