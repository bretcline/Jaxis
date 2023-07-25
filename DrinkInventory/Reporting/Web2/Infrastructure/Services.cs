
using System;
using System.Web;

namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public static class Services
    {
        private static ISessionProvider m_sessionProvider;
        private static string m_reportsPath;

        public static ISessionProvider SessionProvider
        {
            get { return m_sessionProvider ?? (m_sessionProvider = new SessionProvider()); }
            set { m_sessionProvider = value; }
        }

        public static string ReportsPath
        {
            get { return m_reportsPath ?? (m_reportsPath = HttpContext.Current.Server.MapPath("~/Reports")); }
            set { m_reportsPath = value; }
        }
    }
}

