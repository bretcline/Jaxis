using System;
using Jaxis.DrinkInventory.Reporting.Tools;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeClock : IClock
    {
        public FakeClock()
        {
            Now = DateTime.Now;
        }

        public DateTime Now { get; private set; }

        public void Advance(TimeSpan _timeSpan)
        {
            Now += _timeSpan;
        }
    }
}
