using System.Web.Mvc;
using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeSessionProvider : ISessionProvider
    {
        private readonly FakeWebSession m_webSession;

        public FakeSessionProvider()
        {
            m_webSession = new FakeWebSession();
        }

        public ISession Get(Controller _controller)
        {
            return m_webSession;
        }
    }
}
