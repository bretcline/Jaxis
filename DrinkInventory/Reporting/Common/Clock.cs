using System;

namespace Jaxis.DrinkInventory.Reporting.Common
{
    public class Clock : IClock
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
