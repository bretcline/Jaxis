using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Jaxis.MessageLibrary.POS;

namespace Jaxis.Reader.POS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPOSClientInterface" in both code and config file together.
    [ServiceContract]
    public interface IPOSClientInterface
    {
        [OperationContract]
        //string SubmitTicket( string _Ticket );
        string SubmitTicket( Ticket _Ticket );
    }
}
