using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSRecon
{
    public class POSRecord
    {
        public string PosData { get; set; }

        public POSRecord( string _Name )
        {
            PosData = _Name;
        }

        public override string ToString( )
        {
            return PosData;
        }
    }
}
