using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;
using Jaxis.DrinkInventory.Reporting.Web2.Models;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;

namespace Jaxis.DrinkInventory.Reporting.Web2.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet] public ActionResult LogOn(string _status)
        {
            return HandleAction(() =>
            {
                ViewBag.HideLogOn = true;
                return View();
            }, View());
        }

        [HttpPost] public ActionResult LogOn(LogOnModel _model, string _returnUrl)
        {
            return HandleAction(() =>
            {
                ViewBag.HideLogOn = true;
                var result = (LogOnResult)ServiceFactory.Reporting.WithService(_srv => _srv.LogOn(_model.UserName, _model.Password));
                
                if (result.Session == null)
                {
                    ModelState.AddModelError("", "The username/password is incorrect.");
                    return View(_model);
                }

                var session = InitSession(result);
                UserSession = session;

                if (_model.StayLoggedIn)
                {
                    CreateSessionCookie(result.Session.SessionId);
                }

                return RedirectToReferrer(_returnUrl);
            }, View());
        }

        public static SessionModel InitSession(LogOnResult _result)
        {
            var areasResult = (GetAreasResult)ServiceFactory.Reporting.WithService(_svc => _svc.GetAreas(_result.Session.SessionId));
            var session = new SessionModel
            {
                Session = _result.Session,
                MenuBarModel = new MenuBarModel { Items = new List<TabItem>() }
            };

            foreach (var area in areasResult.Areas)
            {
                var item = new TabItem { AreaId = area.AreaId, ControllerName = area.Controller, DisplayText = area.Name };
                session.MenuBarModel.Items.Add(item);
            }

            return session;
        }

        private void CreateSessionCookie(Guid _sessionId)
        {
            var myCookie = new HttpCookie("LoginSessionId")
            {
                Value = _sessionId.ToString(),
                Expires = DateTime.Now.AddDays(7)
            };

            Response.Cookies.Add(myCookie);
        }

        private ActionResult RedirectToReferrer(string _returnUrl)
        {
            if (Url.IsLocalUrl(_returnUrl) && _returnUrl.Length > 1)
            {
                return Redirect(_returnUrl);
            }

            return Redirect("~/");
        }

        public ActionResult LogOff()
        {
            ServiceFactory.Reporting.WithService(_svc => _svc.LogOff(UserSession.Session.SessionId));
            // ignore result of service call and just forget the session
            UserSession = null;
            return RedirectToAction("LogOn");
        }
    }
}
