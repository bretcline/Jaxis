using System;
using System.Web.Mvc;
using Jaxis.DrinkInventory.Reporting.Web2.Controllers;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;

namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public class Session : ISession
    {
        private readonly Controller m_controller;

        public Session(Controller _controller)
        {
            m_controller = _controller;
        }

        public object this[string _key]
        {
            get
            {
                return m_controller.Session[_key];
            }
            set { m_controller.Session[_key] = value; }
        }

        public void AttemptRestoreFromCookie()
        {
            try
            {
                var cookie = m_controller.Request.Cookies["LoginSessionId"];
                if (cookie != null)
                {
                    Guid id;
                    if (Guid.TryParse(cookie.Value, out id))
                    {
                        var getSessionResult = (LogOnResult)ServiceFactory.Reporting.WithService(_s => _s.GetSession(id));
                        if (getSessionResult.Session != null)
                        {
                            var session = AccountController.InitSession(getSessionResult);
                            m_controller.Session["UserSession"] = session;
                        }
                    }
                }
            }

            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception) { }
            // ReSharper restore EmptyGeneralCatchClause
        }
    }
}
