using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSRecon
{
    public class Reconciled
    {
        public POSRecord POS{ get; set; }
        public PourRecord Pour{ get; set; }

        public override string ToString( )
        {
            return string.Format( "{0} - {1}", POS, Pour );
        }
    }
}
