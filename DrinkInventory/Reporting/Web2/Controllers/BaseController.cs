using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;
using Jaxis.DrinkInventory.Reporting.Web2.Models;
using Jaxis.Util.Log4Net;

namespace Jaxis.DrinkInventory.Reporting.Web2.Controllers
{
    public abstract class BaseController : Controller
    {
        protected SessionModel UserSession
        {
            get
            {
                var session = Services.SessionProvider.Get(this);
                var model = session["UserSession"] as SessionModel;
                if (model == null)
                {
                    session.AttemptRestoreFromCookie();
                    model = session["UserSession"] as SessionModel;

                    if (model == null)
                    {
                        throw new NoSessionException();
                    }
                }

                return model;
            }
            set
            {
                Services.SessionProvider.Get(this)["UserSession"] = value;
            }
        }

        protected Guid SessionId
        {
            get
            {
                return UserSession.Session.SessionId;
            }
        }

        protected ActionResult HandleAction(Func<ActionResult> _action, ActionResult _defaultResult = null)
        {
            try
            {
                return _action();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorTime = DateTime.Now.ToString("MMM dd yyy, hh:mm:ss tt");
                Log.Exception(ex);
            }

            return _defaultResult ?? new EmptyResult();
        }

        protected JsonResult HandleAjaxGetAction(Func<object> _action, string _successMessage = null)
        {
            return HandleAjaxAction(JsonRequestBehavior.AllowGet, _action, _successMessage);
        }

        protected JsonResult HandleAjaxPostAction(Func<object> _action, string _successMessage = null)
        {
            return HandleAjaxAction(JsonRequestBehavior.DenyGet, _action, _successMessage);
        }

        private JsonResult HandleAjaxAction(JsonRequestBehavior _behavior, Func<object> _action, string _successMessage)
        {
            try
            {
                return Json(ServerResult.SuccessResult(_action(), _successMessage), _behavior);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);

                if (ex is NoSessionException)
                {
                    return Json(ServerResult.NoSessionResult(Url.Action("LogOn", "Account")), JsonRequestBehavior.AllowGet);
                }
                
                return Json(ServerResult.FailureResult(ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        // There seems to be an issue with model binding empty lists/arrays.  If it is empty,
        // I get a list with one element == Guid.Empty.  There's probably a more correct fix,
        // but this works.   Some discussion located here:
        // http://stackoverflow.com/questions/6759967/how-to-post-an-empty-array-of-ints-jquery-mvc-3

        // UPDATE: i found that this seems to have become less of a problem once I switched out the function
        // i was using in javascript to post back data.  Now, the list will come back as null if it is empty,
        // which is more in line with the way other things work.  In any case, I'm leaving thise for now
        // just in case.
        protected static void FixPostedList<T>(List<T> _list)
        {
            if (_list != null && _list.Count == 1 && _list[0].Equals(default(T)))
                _list.Clear();
        }
    }
}
