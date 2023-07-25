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

namespace Jaxis.Readers.POS
{
    public static class POSConfig
    {
        static public int GetTcpPort( this IDeviceConfig _Config )
        {
            return Convert.ToInt32( _Config.Options[0].Value );
        }

        static public int GetTimeoutInterval( this IDeviceConfig _Config )
        {
            return Convert.ToInt32( _Config.Options[1].Value ) * 1000;
        }

        static public string GetParserConfig( this IDeviceConfig _Config )
        {
            return _Config.Options[2].Value;
        }

        static public string GetBarID( this IDeviceConfig _Config )
        {
            return _Config.Options[3].Value;
        }

        static public bool GetAppendToTicket( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 4, bool.Parse, false );
        }
        
        //static public POSType GetPOSType( this IDeviceConfig _Config )
        //{
        //    POSType rc = POSType.Unknown;
        //    Enum.TryParse<POSType>( _Config.Options[2].Value, out rc );
        //    return rc;
        //}
    }
}