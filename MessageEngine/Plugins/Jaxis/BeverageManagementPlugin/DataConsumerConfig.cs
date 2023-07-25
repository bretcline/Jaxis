using System;
using Jaxis.Interfaces;

namespace Jaxis.BeverageManagement.Plugin
{
    public static class DataConsumerConfig
    {
        //static public string GetWCFPath( this IDeviceConfig _Config )
        //{
        //    return _Config.GetOptionData( 0, "http://localhost:8223/HostWCFService/PourEngineService/" );
        //}

        static public Single GetMaxSpoutDiameter( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 1, Single.Parse, 10.0f );
        }

        static public bool GetMultiPour( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 2, bool.Parse, false );
        }

        static public int GetAutoDetachVolume( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 3, int.Parse, 0 );
        }

        static public bool GetSimulator( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 4, bool.Parse, false );
        }

        static public bool GetHalfPour(this IDeviceConfig _Config)
        {
            return _Config.GetOptionData(5, bool.Parse, false);
        }
    }
}