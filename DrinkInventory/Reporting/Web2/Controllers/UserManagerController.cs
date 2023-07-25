using System;
using System.Linq;
using System.Web.Mvc;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;

namespace Jaxis.DrinkInventory.Reporting.Web2.Controllers
{
    public class UserManagerController : BaseController
    {
        [UserSessionFilter] [HttpGet] public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUsers()
        {
            return HandleAjaxGetAction(() =>
            {
                var result = (GetUsersResult)ServiceFactory.Reporting.WithService(_svc => _svc.GetUsers(SessionId));
                var response = from u in result.Users select JsonSafe(u);
                return response;
            });
        }

        public ActionResult GetUser(Guid _userId)
        {
            return HandleAjaxGetAction(() =>
            {
                var result = (GetUserResult)ServiceFactory.Reporting.WithService(_svc => _svc.GetUser(SessionId, _userId));
                return JsonSafe(result.User);
            });
        }

        public JsonResult NewUser()
        {
            return HandleAjaxGetAction(() =>
            {
                var result = (GetUserResult)ServiceFactory.Reporting.WithService(
                    _svc => _svc.NewUser(SessionId));
                return JsonSafe(result.User);
            });
        }

        public ActionResult DeleteUser(Guid _userId)
        {
            return HandleAjaxPostAction(() =>
            {
                ServiceFactory.Reporting.WithService(_svc => _svc.DeleteUser(SessionId, _userId));
                return null;
            }, "User Deleted");
        }

        public ActionResult SaveUser(User _user)
        {
            return HandleAjaxPostAction(() =>
            {
                var result = (GetUserResult)ServiceFactory.Reporting.WithService(_svc => _svc.SaveUser(SessionId, _user));
                return result.User;
            }, "User Saved");
        }

        public static object JsonSafe(IUser _user)
        {
            return _user == null ? null :
            new
            {
                _user.UserName,
                _user.UserId,
                _user.IsNew,
                _user.Password,
                _user.VisibleWidgetIds,

                Organization = new
                {
                    _user.Organization.Name,
                    _user.Organization.OrganizationId,
                    _user.Organization.ParentId,
                    _user.Organization.ShortName
                }
            };
        }
    }
}
