using System;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using Jaxis.DrinkInventory.Reporting.Tools;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public abstract class FakeDomainObject : IDomainObject
    {
        public abstract Guid Id { get; set; }

        protected FakeDomainObject()
        {
            ModifiedOn = Services.Clock.Now;
        }

        public DateTime ModifiedOn { get; set; }

        public bool IsNew { get; set; }
    }
}
