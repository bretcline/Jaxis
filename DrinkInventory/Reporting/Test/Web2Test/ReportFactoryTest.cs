using DevExpress.XtraReports.UI;
using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;
using NUnit.Framework;

namespace Jaxis.DrinkInventory.Reporting.Test.Web2Test
{
    [TestFixture]
    public class ReportFactoryTest
    {
        //private View m_view;

        //[SetUp]
        //public void SetUp()
        //{
        //    m_view = new View();
        //    Services.ReportsPath = "Web2Test\\ReportFiles";
        //}

        //[Test]
        //public void CreateReportFromFile()
        //{
        //    var report = CreateReportFromFile("Report1.repx");
        //    Assert.AreEqual("Report1", report.Name);
        //    Assert.AreEqual("Recent Pours", report.DisplayName);
        //}

        //[Test]
        //public void CreateReportUsesViewFileName()
        //{
        //    var report = CreateReportFromFile("Report2.repx");
        //    Assert.AreEqual("Report2", report.Name);
        //    Assert.AreEqual("DisplayNameReport2", report.DisplayName);
        //}

        //private XtraReport CreateReportFromFile(string _fileName)
        //{
        //    m_view.ReportClassName = _fileName;
        //    var report = ReportFactory.CreateReport(m_view);
        //    Assert.IsNotNull(report);
        //    return report;
        //}
    }
}
