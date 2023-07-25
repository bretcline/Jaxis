using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFT.PSService.ServiceLibrary
{
    public partial class SyncContext
    {
        public bool lastSyncFieldSpecified { get; set; }
        public SyncContext( )
        {
            this.LastSync = new DateTime( 1900, 1, 1 );
            lastSyncFieldSpecified = true;
        }
    }
}

