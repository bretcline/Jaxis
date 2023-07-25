using System.IO;
using DevExpress.XtraReports.UI;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;

namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public class ReportFactory
    {
        public static XtraReport CreateReport(Jaxis.DrinkInventory.Reporting.Data.POCO.Report _report)
        {
            var path = Path.Combine(Services.ReportsPath, _report.ReportClassName);
            var report = XtraReport.FromFile(path, true);
            return report;
        }
    }
}
