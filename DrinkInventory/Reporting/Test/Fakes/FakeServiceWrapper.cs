using System;
using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeServiceWrapper<TConcrete, TInterface> : IServiceWrapper<TInterface> where TConcrete : class, TInterface, IDisposable, new()
    {
        private TConcrete m_service;

        public FakeServiceWrapper()
        {
            m_service = Activator.CreateInstance(typeof (TConcrete)) as TConcrete;
        }

        public ServiceResult WithService(Func<TInterface, ServiceResult> _action)
        {
            return _action(m_service);
        }

        public void Dispose()
        {
            m_service.Dispose();
        }
    }
}
