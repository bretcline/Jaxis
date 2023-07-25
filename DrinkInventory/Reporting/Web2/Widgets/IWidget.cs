using System;

namespace Jaxis.DrinkInventory.Reporting.Web2.Widgets
{
    public interface IWidget
    {
        Guid Id { get; }
        string Name { get; }
        string ViewName { get; }
        void UpdateData(Guid _sessionId);
    }
}
