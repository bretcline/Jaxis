using System;
using Jaxis.DrinkInventory.Reporting.Business;
using Jaxis.DrinkInventory.Reporting.BusinessInterfaces;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using Jaxis.DrinkInventory.Reporting.Test.Fakes;
using NUnit.Framework;

namespace Jaxis.DrinkInventory.Reporting.Test.BusinessTest
{
    public class BaseBusinessTest
    {
        public BusinessProvider BusinessProvider { get; set; }
        public ISession Session { get; set; }
        public Guid SessionId { get; set; }
        public string TestUserName { get; set; }
        public string TestPassword { get; set; }

        [SetUp]
        public void SetUp()
        {
            BusinessProvider.Init(() => new FakeDataManagerFactory());
            BusinessProvider = new BusinessProvider();
            TestUserName = Guid.NewGuid().ToString();
            TestPassword = Guid.NewGuid().ToString();

            using (var factory = CreateFactory())
            {
                var user = factory.Manage<IUser>().Create();
                user.UserName = TestUserName;
                user.Password = TestPassword;
                factory.Manage<IUser>().Save(user);
            }

            Session = BusinessProvider.LogOn(TestUserName, TestPassword);
            SessionId = Session.SessionId;
        }

        protected virtual IDataManagerFactory CreateFactory()
        {
            return new FakeDataManagerFactory();
        }

        protected string TestString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
