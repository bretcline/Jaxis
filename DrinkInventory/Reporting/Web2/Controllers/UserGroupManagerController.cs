using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;

namespace Jaxis.DrinkInventory.Reporting.Web2.Controllers
{
    public class UserGroupManagerController : BaseController
    {
        [UserSessionFilter] [HttpGet] public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUserGroups()
        {
            return HandleAjaxGetAction(() =>
            {
                var result = (GetUserGroupsResult)ServiceFactory.Reporting.WithService(
                    _svc => _svc.GetUserGroups(SessionId));
                return result.UserGroups;
            });
        }

        public JsonResult GetUserGroup(Guid _userGroupId)
        {
            return HandleAjaxGetAction(() =>
            {
                var result = (GetUserGroupResult)ServiceFactory.Reporting.WithService(
                    _svc => _svc.GetUserGroup(SessionId, _userGroupId));
                return GetUserGroupResponse(result);
            });
        }

        public JsonResult NewUserGroup()
        {
            return HandleAjaxGetAction(() =>
            {
                var result = (GetUserGroupResult)ServiceFactory.Reporting.WithService(
                    _svc => _svc.NewUserGroup(SessionId));
                return GetUserGroupResponse(result);
            });
        }

        public JsonResult DeleteUserGroup(Guid _userGroupId)
        {
            return HandleAjaxPostAction(() =>
            {
                ServiceFactory.Reporting.WithService(_svc => _svc.DeleteUserGroup(SessionId, _userGroupId));
                return null;
            }, "User Group Deleted");
        }

        public JsonResult SaveUserGroup(UserGroup _userGroup)
        {
            return HandleAjaxPostAction(() =>
            {
                FixPostedList(_userGroup.AreaIds);
                FixPostedList(_userGroup.UserIds);
                var result = (GetUserGroupResult)ServiceFactory.Reporting.WithService(
                    _svc => _svc.SaveUserGroup(SessionId, _userGroup));
                return GetUserGroupResponse(result);
            }, "User Group Saved");
        }

        private object GetUserGroupResponse(GetUserGroupResult _getUserGroupResult)
        {
            var getUsersResult = (GetUsersResult)ServiceFactory.Reporting.WithService(
                _svc => _svc.GetUsers(SessionId));
            var getAreasResult = (GetAreasResult)ServiceFactory.Reporting.WithService(
                _svc => _svc.GetAreas(SessionId));

            var memberUsers = new List<object>();
            var availableUsers = new List<object>();
            var memberAreas = new List<Area>();
            var availableAreas = new List<Area>();

            foreach (var user in getUsersResult.Users)
            {
                if (_getUserGroupResult.UserGroup.UserIds != null && _getUserGroupResult.UserGroup.UserIds.Any(_id => _id == user.UserId))
                {
                    memberUsers.Add(UserManagerController.JsonSafe(user));
                }
                else
                {
                    availableUsers.Add(UserManagerController.JsonSafe(user));
                }
            }

            foreach (var area in getAreasResult.Areas)
            {
                if (_getUserGroupResult.UserGroup.AreaIds != null && _getUserGroupResult.UserGroup.AreaIds.Any(_id => _id == area.AreaId))
                {
                    memberAreas.Add(area);
                }
                else
                {
                    availableAreas.Add(area);
                }
            }

            var response = new
            {
                UserGroup = new
                {
                    _getUserGroupResult.UserGroup.UserGroupId,
                    _getUserGroupResult.UserGroup.Name,
                    Organization = OrganizationManagerController.JsonSafe(_getUserGroupResult.UserGroup.Organization),
                    Users = memberUsers,
                    Areas = memberAreas
                },
                AvailableAreas = availableAreas,
                AvailableUsers = availableUsers
            };

            return response;
        }
    }
}
