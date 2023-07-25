using System;

namespace Jaxis.DrinkInventory.Reporting.Tools
{
    public class Clock : IClock
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
