using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;

namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public interface IServiceWrapperFactory
    {
        IServiceWrapper<IReportingService> CreateReportingService();
    }
}
