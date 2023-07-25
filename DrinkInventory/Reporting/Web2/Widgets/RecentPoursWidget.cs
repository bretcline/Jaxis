using System;
using System.Data;
using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;

namespace Jaxis.DrinkInventory.Reporting.Web2.Widgets
{
    public class RecentPoursWidget : IWidget
    {
        public Guid Id
        {
            get { return new Guid("F0FB9762-6140-4976-8F77-FBDEAE98A380"); }
        }

        public string Name
        {
            get { return "Recent Pours"; }
        }

        public string ViewName
        {
            get { return "_RecentPoursWidget"; }
        }

        public void UpdateData(Guid _sessionId)
        {
            var pours = (GetDataTableResult)ServiceFactory.Reporting.WithService(_s => _s.GetRecentPours(_sessionId));
            Pours = pours.Data;
        }

        public DataTable Pours { get; set; }
    }
}
