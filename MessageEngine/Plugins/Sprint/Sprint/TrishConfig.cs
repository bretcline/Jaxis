using System;
using System.IO.Ports;
using Jaxis.Interfaces;

namespace Jaxis.Readers.Sprint
{
    public static class TrishConfig
    {

        static public string GetPortName( this IDeviceConfig _Config )
        {
            return _Config.Options[0].Value;
        }

        static public int GetFrequency( this IDeviceConfig _Config )
        {
            return Convert.ToInt32( _Config.Options[1].Value );
        }

        static public int GetOverPour( this IDeviceConfig _Config )
        {
            return Convert.ToInt32( _Config.Options[2].Value );
        }

        static public int GetEventID( this IDeviceConfig _Config )
        {
            return Convert.ToInt32( _Config.Options[3].Value );
        }

        static public bool GetSimulator( this IDeviceConfig _Config )
        {
            bool rc = false;

            if( 4 < _Config.Options.Count )
            {
                rc = bool.Parse( _Config.Options[4].Value );
            }

            return rc;
        }

        static public int GetBaudRate( this IDeviceConfig _Config )
        {
            return 9600;
        }

        static public Parity GetParity( this IDeviceConfig _Config )
        {
            return Parity.None;
        }

        static public StopBits GetStopBits( this IDeviceConfig _Config )
        {
            return StopBits.One;
        }

        static public int GetDataBits( this IDeviceConfig _Config )
        {
            return 8;
        }
    }
}