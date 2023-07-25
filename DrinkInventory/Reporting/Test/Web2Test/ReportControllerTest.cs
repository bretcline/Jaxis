using System;
using System.Web.Mvc;
using Jaxis.DrinkInventory.Reporting.Test.Fakes;
using Jaxis.DrinkInventory.Reporting.Web2.Controllers;
using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;
using Jaxis.DrinkInventory.Reporting.Web2.Models;
using Jaxis.DrinkInventory.Reporting.Web2.Models.Report;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;
using NUnit.Framework;
//using Session = Jaxis.DrinkInventory.Reporting.Web2.ReportingService.Session;
using Session = Jaxis.DrinkInventory.Reporting.Data.POCO.Session;

namespace Jaxis.DrinkInventory.Reporting.Test.Web2Test
{
    [TestFixture]
    class ReportControllerTest
    {
        [SetUp]
        public void SetUp()
        {
            Services.SessionProvider = new FakeSessionProvider();
            ServiceFactory.WrapperFactory = new FakeServiceWrapperFactory();
            var sessionModel = new SessionModel {Session = new Session {SessionId = Guid.NewGuid()}};
            Services.SessionProvider.Get(null)["UserSession"] = sessionModel;
            Services.ReportsPath = "Web2Test\\ReportFiles";
        }

        [Test]
        public void LoadsReportFromRepxFile()
        {
            //// arrange
            //var viewId = Guid.NewGuid();
            
            //var view = new View
            //{
            //    ViewId = viewId,
            //    ReportClassName = "Report1.repx"
            //};

            //FakeReportingService.Views.Add(view.ViewId, view);
            //viewId = view.ViewId;
            //var controller = new ReportController();
            
            //// act
            //var result = controller.RefreshView(viewId);
            
            //// examine
            //Assert.IsInstanceOf(typeof (PartialViewResult), result);
            //Assert.IsNotNull(result);
            //var partialViewResult = result as PartialViewResult;
            //Assert.IsNotNull(partialViewResult);
            //var model = partialViewResult.Model as ReportViewModel;
            //Assert.IsNotNull(model);
            //Assert.IsNotNull(model.Report);
            //Assert.AreEqual("Report1", model.Report.Name);
            //Assert.AreEqual("Recent Pours", model.Report.DisplayName);
            Assert.Fail("need to fix this");
        }
    }
}
