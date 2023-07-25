using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Jaxis.Interfaces;
using Jaxis.Util.Log4Net;

namespace Jaxis.Engine.Base.Device
{
    public enum EngineState
    {
        Started = 0,
        Stopped = 1
    }

    public class DeviceConfig : IDeviceConfig, IDeviceConfigView
    {
        public static T DeserializeObject<T>( String pXmlizedString ) where T : class
        {
            try
            {
                var xs = new XmlSerializer( typeof( T ) );
                var encoding = new UTF8Encoding( );
                Byte[] byteArray = encoding.GetBytes( pXmlizedString );
                using( var memoryStream = new MemoryStream( byteArray ) )
                {
                    return (T)xs.Deserialize( memoryStream );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: DeserializeObject", exp );
                return null;
            }
        }

        public static void SerializeObject<T>( StreamWriter Writer, T data ) where T : class
        {
            try
            {
                var xs = new XmlSerializer( typeof( T ) );
                var encoding = new UTF8Encoding( );
                xs.Serialize( Writer, data );
            }
            catch( Exception exp )
            {
                Log.WriteException( "Engine:: SerializeObject", exp );
            }
        }

        public DeviceState State { get; set; }

        public string AssemblyName { get; set; }

        public string AssemblyType { get; set; }

        public string AssemblyVersion { get; set; }

        public string ID { get; set; }

        public string Name { get; set; }

        public DeviceType Type { get; set; }

        public UInt64 ConsumerMessageType { get; set; }

        public UInt64 ProducerMessageType { get; set; }

        public List<DeviceConfigOption> Options { get; set; }

        public List<FilterConfig> Filters { get; set; }

        public DeviceConfig( )
        {
            Options = new List<DeviceConfigOption>();
            Filters = new List<FilterConfig>( );
        }

        public T GetOptionData<T>(int _Index, Func<string, T> _Parser, T _Default)
        {
            T rc = _Default;
            if( _Index < Options.Count && !string.IsNullOrWhiteSpace( Options[_Index].Value ) )
            {
                if( null != _Parser )
                {
                    rc = _Parser( Options[_Index].Value );
                }
            }
            return rc;
        }

        public string GetOptionData(int _Index)
        {
            return GetOptionData(_Index, string.Empty );
        }

        public string GetOptionData(int _Index, string _Default)
        {
            string rc = _Default;
            if( _Index < Options.Count && !string.IsNullOrWhiteSpace( Options[_Index].Value ) )
            {
                rc = Options[_Index].Value;
            }
            return rc;
        }
    }
}