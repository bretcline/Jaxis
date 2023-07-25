using System;
using Jaxis.Interfaces;

namespace Jaxis.BeverageManagement.Plugin
{
    public static class HeartbeatMonitorConfig
    {
        static public Single GetMissingBottleInterval( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 0, Single.Parse, 10.0f );
        }
    }
}