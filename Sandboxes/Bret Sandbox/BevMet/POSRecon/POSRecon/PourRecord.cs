using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSRecon
{
    public class PourRecord
    {
        public string PourData { get; set; }

        public PourRecord( string _Name )
        {
            PourData = _Name;
        }

        public override string ToString( )
        {
            return PourData;
        }
    }
}
