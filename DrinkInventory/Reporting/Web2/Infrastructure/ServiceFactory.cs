using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;

namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public static class ServiceFactory
    {
        private static IServiceWrapperFactory m_wrapperFactory;
        private static IServiceWrapper<IReportingService> m_reportingWrapper;

        public static IServiceWrapperFactory WrapperFactory
        {
            get { return m_wrapperFactory ?? (m_wrapperFactory = new ServiceWrapperFactory()); }
            set { m_wrapperFactory = value; }
        }

        public static IServiceWrapper<IReportingService> Reporting
        {
            get { return m_reportingWrapper ?? (m_reportingWrapper = WrapperFactory.CreateReportingService()); }
        }
    }
}
