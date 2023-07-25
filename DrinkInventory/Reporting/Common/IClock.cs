using System;

namespace Jaxis.DrinkInventory.Reporting.Common
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}
