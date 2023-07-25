using System;
using SubSonic.SqlGeneration.Schema;

namespace BevClasses
{
    public class Pour
    {
        [SubSonicPrimaryKey]
        public Guid ID { get; set; }

        [SubSonicIgnore]
        public Bottle Bottle { get; set; }
        public Guid BottleID { get; set; }
        
        public TimeSpan Duration { get; set; }
        
        public double Amount { get; set; }

        public DateTime Time { get; set; }

        public Pour()
        {
            Bottle = new Bottle();
            ID = Guid.NewGuid();
        }
    }
}
