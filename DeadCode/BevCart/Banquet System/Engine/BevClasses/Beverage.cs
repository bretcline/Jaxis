using System;
using SubSonic.SqlGeneration.Schema;

namespace BevClasses
{
    public class Beverage
    {
        [SubSonicPrimaryKey]
        public Guid ID { get; set; }

        [SubSonicNullString]
        public string Label { get; set; } // Maybe brand or product is a better name for this.

        public double Size { get; set; }

        public double Price { get; set; }

        [SubSonicNullString]
        public string PicFile { get; set; }

        public Beverage()
        {
            ID = Guid.NewGuid();
        }
    }
}
