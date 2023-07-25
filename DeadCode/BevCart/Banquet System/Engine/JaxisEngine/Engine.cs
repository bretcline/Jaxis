using System;
using System.Collections.Generic;
using JaxisInterfaces;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using JaxisEngine.Base;
using System.Reflection;

namespace JaxisEngine
{

    public class Engine
    {
        public delegate string Consume(IMessage _Message);

        private List<IDevice> m_Devices;

        public Engine()
        {
            LoadDevices( );
        }

        public T DeserializeObject<T>( String pXmlizedString ) where T : class
        {
            XmlSerializer xs = new XmlSerializer( typeof( T ) );
            UTF8Encoding encoding = new UTF8Encoding( );
            Byte[] byteArray = encoding.GetBytes( pXmlizedString );
            using( MemoryStream memoryStream = new MemoryStream( byteArray ) )
            {
                return (T)xs.Deserialize( memoryStream );
            }
        }

        protected void LoadDevices( )
        {
            DeviceConfigCollection Configs = null;
            m_Devices = new List<IDevice>( );

            string ConfigFile = System.Configuration.ConfigurationManager.AppSettings["ConfigFile"];

            using( StreamReader Reader = new StreamReader( ConfigFile ) )
            {
                string Data = Reader.ReadToEnd( );
                Configs = DeserializeObject<DeviceConfigCollection>( Data );
                if( null != Configs && null != Configs.Configs && 0 < Configs.Configs.Count )
                {
                    int i = 0;
                    Configs.Configs.ForEach( D =>
                    {
                        object[] Conf = new object[] { D };
                        Assembly Asm = Assembly.LoadFrom( D.AssemblyName );
                        Type[] Tps = Asm.GetTypes( );
                        Array.ForEach( Tps, T =>
                        {
                            if( D.AssemblyType.Equals( T.ToString( ) ) )
                            {
                                IDevice NewDevice = (IDevice)Activator.CreateInstance(Tps[0], Conf);
                                if (0 == i)
                                    NewDevice.Produce += Produce;
                                m_Devices.Add(NewDevice);
                            }
                        });
                        i++;
                    } );
                }
            }
        }

        public List<IDevice> GetDeviceList()
        {
            return m_Devices;
        }

        public void StopDevice (string DeviceID)
        {
            IDevice Dev = m_Devices.Find( D => D.Config.ID == DeviceID );
            if( null != Dev )
            {
                Dev.Stop();
            }
        }

        public void StartDevice(string DeviceID)
        {
            IDevice Dev = m_Devices.Find( D => D.Config.ID == DeviceID );
            if( null != Dev )
            {
                Dev.Start( );
            }
        }

        public void Start()
        {
            if (null != m_Devices)
            {
                m_Devices.ForEach(D => D.Start());
            }

        }

        public void Stop()
        {
            if (null != m_Devices)
            {
                m_Devices.ForEach(D => D.Stop());
            }
        }

        public void RegisterDevice(IDevice _Device )
        {
            if (DeviceType.DataProducer == _Device.Config.Type ||
                DeviceType.DataProducerConsumer == _Device.Config.Type)
            {
                _Device.Produce += Produce;
            }
            m_Devices.Add(_Device);
        }

        public string Produce(IMessage _Message)
        {
            string rc = null;

            if (null != m_Devices)
            {
                foreach (IDevice D in m_Devices)
                {
                    if ( D.Config.ConsumerMessageType == _Message.Type )
                    {
                        D.Consume(_Message);
                    }
                }
            }
            return rc;
        }
    }
}
