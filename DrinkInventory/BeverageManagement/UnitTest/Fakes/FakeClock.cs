using System;
using BeverageManagement.Forms.Reconcile;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes
{
    class FakeClock : IClock
    {
        public FakeClock()
        {
            Now = DateTime.Now;
        }

        public DateTime Now { get; set; }
    }
}
