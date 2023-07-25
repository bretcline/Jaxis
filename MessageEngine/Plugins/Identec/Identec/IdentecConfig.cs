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

            //return _Config.Options[0];
        }

        /// <summary>
        /// Reader Timeout 
        /// </summary>
        /// <param name="_Config"></param>
        /// <returns></returns>
        static public int GetTimeout( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 1, int.Parse, 5 * 60 * 60 );
            //int rc = -1;
            //if( !string.IsNullOrWhiteSpace( _Config.Options[1] ) )
            //{
            //    rc = Convert.ToInt32( _Config.Options[1] );
            //}
            //return rc;
        }


        static public Frequency GetFrequency( this IDeviceConfig _Config )
        {
            string Value = _Config.GetOptionData(2, "NorthAmerican");
            return (Frequency)Enum.Parse( typeof( Frequency ), Value );
            
            //Frequency rc = Frequency.NorthAmerican;
            //if( !string.IsNullOrWhiteSpace( _Config.Options[2] ) )
            //{
            //    rc =(Frequency)Enum.Parse( typeof( Frequency ), _Config.Options[2] );
            //}
            //return rc;
        }

        static public RFBaudRate GetBaudRate( this IDeviceConfig _Config )
        {
            string Value = _Config.GetOptionData( 3, "RF_115200" );
            return (RFBaudRate)Enum.Parse( typeof( RFBaudRate ), Value );

            //RFBaudRate rc = RFBaudRate.RF_115200;
            //if( !string.IsNullOrWhiteSpace( _Config.Options[3] ) )
            //{
            //    rc = (RFBaudRate)Enum.Parse( typeof( RFBaudRate ), _Config.Options[3] );
            //}
            //return rc;
        }

        static public bool GetPublishHeartbeats( this IDeviceConfig _Config )
        {
            return _Config.GetOptionData( 4, bool.Parse, true );

            //bool rc = true;
            //if( !string.IsNullOrWhiteSpace( _Config.Options[4] ) )
            //{
            //    rc = bool.Parse( _Config.Options[4] );
            //}
            //return rc;
        }

        static public bool GetPublishPours(this IDeviceConfig _Config)
        {
            return _Config.GetOptionData(5, bool.Parse, true);

            //bool rc = true;
            //if( !string.IsNullOrWhiteSpace( _Config.Options[5] ) )
            //{
            //    rc = bool.Parse( _Config.Options[5] );
            //}
            //return rc;
        }

        static public string GetDefaultEvent(this IDeviceConfig _Config)
        {
            return _Config.GetOptionData(6, "0000");

            //bool rc = true;
            //if( !string.IsNullOrWhiteSpace( _Config.Options[5] ) )
            //{
            //    rc = bool.Parse( _Config.Options[5] );
            //}
            //return rc;
        }
    }
}