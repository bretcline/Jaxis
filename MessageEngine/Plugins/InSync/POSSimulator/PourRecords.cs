using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.MessageLibrary;
using Jaxis.MessageLibrary.POS;

namespace Jaxis.Readers.InSync
{
    public class PourRecords
    {
        public List<PourData> PourList = new List<PourData>( );
    }

    public class POSRecords
    {
        public List<Ticket> TicketList = new List<Ticket>( );
    }
}
