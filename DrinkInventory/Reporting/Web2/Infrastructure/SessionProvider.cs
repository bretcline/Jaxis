using System.Web.Mvc;

namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public class SessionProvider : ISessionProvider
    {
        public ISession Get(Controller _controller)
        {
            return new Session(_controller);
        }
    }
}
