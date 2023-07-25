using System.Web.Mvc;

namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public interface ISessionProvider
    {
        ISession Get(Controller _controller);
    }
}
