using System;
using SubSonic.SqlGeneration.Schema;

namespace BevClasses
{
    public class Bottle
    {
        [SubSonicPrimaryKey]
        public Guid ID { get; set; }

        [SubSonicNullString]
        public string Tag { get; set; }

        [SubSonicNullString]
        public string Cart { get; set; }

        [SubSonicNullString]
        public string UPC { get; set; }

        [SubSonicIgnore]
        public Beverage Beverage { get; set; }
        public Guid BeverageID { get; set; }

        public double QuantityLeft { get; set; }

        public Bottle()
        {
            Beverage = new Beverage();
            ID = Guid.NewGuid();
        }
    }
}
