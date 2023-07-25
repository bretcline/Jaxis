using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;

namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public class ServiceWrapperFactory : IServiceWrapperFactory
    {
        public IServiceWrapper<IReportingService> CreateReportingService()
        {
            return new ServiceWrapper<ReportingServiceClient,IReportingService>();
        }
    }
}