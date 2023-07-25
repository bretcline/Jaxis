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
    public class AlienConfig : IConfigData
    {
        public int TcpPort { get; set; } // 4000
        public int UdpPort { get; set; } // 3988 
        public int TimeoutInterval { get; set; } // 30
    }
}
