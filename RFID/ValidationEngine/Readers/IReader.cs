using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Xml.Linq;
using Jaxis.Util.Log4Net;
using System.Threading;

namespace ValidationEngine.Readers
{

    public interface IConfigData
    {

    }

    public interface IReader
    {
        Action<IValidationKey> SubmitForProcessing { get; set; }
        void Start( );
        void Stop( );
    }
}
