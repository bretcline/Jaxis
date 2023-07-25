using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Engine;
using Jaxis.Interfaces;

namespace POSReaderImporter
{
    class POSImporter
    {
        protected EngineServiceDll m_Engine = null;

        static void Main( string[] args )
        {
            POSImporter P = new POSImporter( );
            while( true == true )
            {
                System.Threading.Thread.Sleep( 100 );
            }
        }

        public POSImporter( )
        {
            m_Engine = new EngineServiceDll( );

            var config = POSReaderImport.GetDefaultDeviceConfig();
            config.State = DeviceState.Started;
            var plugin = new POSReaderImport( config );

            m_Engine.RegisterDevice( plugin );
            m_Engine.Start( );
        }
    }
}
