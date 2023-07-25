using System;

namespace Jaxis.DrinkInventory.Reporting.Tools
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}
