using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Jaxis.DrinkInventory.Reporting.Web2.Models;
using Jaxis.Util.Log4Net;

namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public class UserSessionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext _filterContext)
        {
            var ctx = HttpContext.Current;
            var session = ctx.Session["UserSession"] as SessionModel;

            if (session == null)
            {
                Services.SessionProvider.Get((Controller) _filterContext.Controller).AttemptRestoreFromCookie();
                session = ctx.Session["UserSession"] as SessionModel;
            }

            if (session == null)
            {
                RedirectToLogon(_filterContext, ctx.Request.Url);
                return;
            }

            try
            {
                ServiceFactory.Reporting.WithService(_svc => _svc.SessionActivity(session.Session.SessionId));
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }

            base.OnActionExecuting(_filterContext);
        }

        private static void RedirectToLogon(ActionExecutingContext _filterContext, Uri _url)
        {
            _filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary {{ "Controller", "Account" },
                                          { "Action", "LogOn" },
                                          { "_returnUrl", _url }});
        }
    }
}
