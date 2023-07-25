using System;
using Jaxis.Interfaces;
using IDENTEC;
using IDENTEC.ILR350;

namespace Jaxis.Readers.Identec
{
    public static class IdentecConfig
    {
        /// <summary>
        /// IP Addres or COM Port for the reader
        /// </summary>
        /// <param name="_Config"></param>
        /// <returns></returns>
        static public string GetAddress( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 0 );
        }

        /// <summary>
        /// Reader Timeout 
        /// </summary>
        /// <param name="_Config"></param>
        /// <returns></returns>
        static public int GetTimeout( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 1, int.Parse, 5 * 60 * 60 );
        }


        static public Frequency GetFrequency( this IDeviceConfig _Config )
        {
            string Value = _Config.GetOptionData(2, "NorthAmerican");
            return (Frequency)Enum.Parse( typeof( Frequency ), Value );
        }

        static public RFBaudRate GetBaudRate( this IDeviceConfig _Config )
        {
            string Value = _Config.GetOptionData( 3, "RF_115200" );
            return (RFBaudRate)Enum.Parse( typeof( RFBaudRate ), Value );
        }

        static public bool GetPublishHeartbeats( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 4, bool.Parse, true );
        }

        static public bool GetPublishPours( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 5, bool.Parse, true );
        }
    }
}