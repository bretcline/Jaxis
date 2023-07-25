using System;
using System.Collections.Generic;
using System.Linq;
using Jaxis.Interfaces;

namespace Jaxis.Readers.PureRFID
{
    static class PureRFConfig
    {
        static public int GetPollTime( this IDeviceConfig _Config )
        {
            return Convert.ToInt32( _Config.Options[0].Value ) * 1000;
        }

        static public string GetIPAddress( this IDeviceConfig _Config )
        {
            return _Config.Options[1].Value;
        }

        static public string GetPort( this IDeviceConfig _Config )
        {
            return _Config.Options[2].Value;
        }

        static public List<int> GetDeviceList( this IDeviceConfig _Config )
        {
            IEnumerable<int> Out = from O in _Config.Options[3].Value.Split( ',' )
                                   select int.Parse( O );

            return Out.ToList( );
        }
    }
}