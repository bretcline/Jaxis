using System;
using Jaxis.DrinkInventory.Reporting.Test.Fakes;
using Jaxis.DrinkInventory.Reporting.WcfService;
using Jaxis.DrinkInventory.Reporting.WcfService.DataContracts;
using NUnit.Framework;

namespace Jaxis.DrinkInventory.Reporting.Test.WcfServiceTest
{
    [TestFixture]
    class ReportingServiceTest
    {
        [Test]
        public void GetUserGroupTest()
        {
            var businessProvider = new FakeBusinessProvider();
            ReportingService.Init(businessProvider);
            var service = new ReportingService();
            const string userName = "userName";
            const string password = "password";
            var session = service.LogOn(userName, password) as LogOnResult;
            Assert.IsNotNull(session);
            var userGroupId = Guid.Empty;
            var result = service.GetUserGroup(session.Session.SessionId, userGroupId) as GetUserGroupResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.UserGroup);
            //Assert.IsNotNull(result.UserGroup.Users);
        }
    }
}
