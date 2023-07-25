using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;

namespace Jaxis.Readers.InSync
{
    public static class POSSimulatorConfig
    {
        static public string GetPourFile( this IDeviceConfig _Config )
        {
            return _Config.Options[0].Value;
        }

        static public string GetPOSFile( this IDeviceConfig _Config )
        {
            return _Config.Options[1].Value;
        }

        static public int GetSleepTime( this IDeviceConfig _Config )
        {
            return Convert.ToInt32( _Config.Options[2].Value );
        }
    }
}
