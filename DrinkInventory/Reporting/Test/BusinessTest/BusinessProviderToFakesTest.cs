using Jaxis.DrinkInventory.Reporting.Business;
using Jaxis.DrinkInventory.Reporting.Common;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using Jaxis.DrinkInventory.Reporting.Test.Fakes;
using NUnit.Framework;

namespace Jaxis.DrinkInventory.Reporting.Test.BusinessTest
{
    [TestFixture]
    public class BusinessProviderToFakesTest : BusinessProviderBaseTest
    {
        protected override IDataManagerFactory CreateFactory()
        {
            return new FakeDataManagerFactory();
        }

        protected override void InitBusinessProvider()
        {
            BusinessProvider.Init(() => new FakeDataManagerFactory());
            using (var factory = new FakeDataManagerFactory())
            {
                var root = factory.Manage<IOrganization>().Create();
                root.Name = "Root Organization";
                root.ShortName = "ROOT";
                root.OrganizationId = Constants.RootOrganizationId;
                root.ParentId = null;
                root.Path = Constants.RootOrganizationId + "/";
                factory.Manage<IOrganization>().Save(root);
            }
        }
    }
}










