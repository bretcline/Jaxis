using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Jaxis.MessageLibrary.POS;

namespace Jaxis.Reader.POS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class POSClientInterface : IPOSClientInterface
    {
        public string SubmitTicket( string _Ticket )
        {
            return _Ticket;
        }

        public string SubmitTicket( Ticket _Ticket )
        {
            //_Ticket.Type = Interfaces.MessageType.RawData;
            return TicketPublisher.GetPublisher( ).Publish( _Ticket );
        }
    }
}
