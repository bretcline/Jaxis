using System;

namespace Jaxis.Inventory.Data
{
    public class BrandedBottle : IUIBrandedBottle, IUIAssociatedBottle
    {
        public BrandedBottle( )
        {
            BottleID = Guid.NewGuid( );
        }

        public Guid BottleID { get; set; }
        public ITag Tag { get; set; }
        public IUPCItem UPC { get; set; }

        public IEvent PreviousEvent{ get; set; }

        public double? Quantity { get { return Tag.Quantity; } set { Tag.Quantity = value; } }
        public double? NozzleDiameter { get { return Tag.NozzleDiameter; } set { Tag.NozzleDiameter = value; } }
        public IEvent Event { get { return Tag.CurrentEvent; } set { Tag.EventID = value.ObjectID; } }
    }
}