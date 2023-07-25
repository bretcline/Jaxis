using System;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;

namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public interface IServiceWrapper<TSrv> : IDisposable
    {
        ServiceResult WithService(Func<TSrv, ServiceResult> _action);
    }
}
