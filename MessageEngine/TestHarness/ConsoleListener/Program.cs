using System;
using System.Text;
using Jaxis.Engine;
using Jaxis.Engine.Base;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;

namespace ConsoleListener
{
    class Program
    {
        protected EngineServiceDll m_Engine = null;

        static void Main( string[] args )
        {
            Program P = new Program( );
            while( true == true )
            {
                System.Threading.Thread.Sleep( 100 );
            }
        }

        public Program( )
        {
            IDeviceConfig Config = new DeviceConfig { Name = "Console Sniffer", ID = Guid.NewGuid( ).ToString( ) };

            MessageSniffer m_Con = new MessageSniffer( Config );
            m_Con.DisplayData = ProcessData; // Using an IDevice Produce event to get data to Form...
            m_Engine = new EngineServiceDll( );
            m_Engine..RegisterDevice( m_Con );

            m_Engine.Start( );
        }

        public string ProcessData( IMessage _Data )
        {
            StringBuilder Builder = new StringBuilder( );
            if( _Data is ITagRead )
            {
                ITagRead Read = _Data as ITagRead;
                Builder.AppendFormat( "{5} - {6} - {0} {1} {2} {3} {4}", Read.DeviceID, Read.TagID, Read.Type, Read.SignalStrength, Read.ReadTime, _Data.GetType( ), _Data.Driver );
            }
            else
            {
                Builder.AppendFormat( "{3} - {0} {1} {2}", _Data.Driver, _Data.Type, _Data.ReadTime, _Data.GetType( ) );
            }
            Console.WriteLine( Builder.ToString( ) );
            return string.Empty;
        }
    }
}