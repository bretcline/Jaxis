using Jaxis.DrinkInventory.Reporting.Business;
using Jaxis.DrinkInventory.Reporting.Data;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using NUnit.Framework;

namespace Jaxis.DrinkInventory.Reporting.Test.BusinessTest
{
    [TestFixture]
    public class BusinessProviderToEntitiesTest : BusinessProviderBaseTest
    {
        protected override IDataManagerFactory CreateFactory()
        {
            return DataManagerFactory.Get();
        }

        protected override void InitBusinessProvider()
        {
            BusinessProvider.Init(null);
        }
    }
}




