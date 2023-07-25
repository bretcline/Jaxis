using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Jaxis.Interfaces;
using Jaxis.Util.Log4Net;

namespace Jaxis.Readers.AlienRFID
{
    public static class AlienConfig
    {
        static public int GetTcpPort( this IDeviceConfig _Config )
        {
#warning MLF --- should we do this in all the get config stuff????
            // return Convert.ToInt32(_Config.Options.Where(O => O.Name == "TcpPort").First().Value);
            return Convert.ToInt32( _Config.Options[0].Value ) * 1000;
        }

        static public int GetUdpPort( this IDeviceConfig _Config )
        {
            return Convert.ToInt32( _Config.Options[1].Value ) * 1000;
        }

        static public int GetTimeoutInterval( this IDeviceConfig _Config )
        {
            return Convert.ToInt32( _Config.Options[2].Value ) * 1000;
        }
    }
}