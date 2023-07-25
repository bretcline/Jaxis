using System;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeArea : FakeDomainObject, IArea
    {
        public FakeArea()
        {
            AreaId = Guid.NewGuid();
        }

        public Guid AreaId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string ShortName { get; set; }
        public string Controller { get; set; }

        public override Guid Id
        {
            get { return AreaId; }
            set { AreaId = value; }
        }
    }
}
