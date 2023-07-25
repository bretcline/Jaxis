using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeServiceWrapperFactory : IServiceWrapperFactory
    {
        public IServiceWrapper<IReportingService> CreateReportingService()
        {
            return new FakeServiceWrapper<FakeReportingService,IReportingService>();
        }
    }
}
