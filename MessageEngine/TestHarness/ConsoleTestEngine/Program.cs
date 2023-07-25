using System;
using Jaxis.Engine;

namespace ConsoleTestEngine
{
    class TestEngine
    {
        protected EngineServiceDll m_Engine = null;

        static void Main( string[] args )
        {
            var config = SQLWatcher.SQLWatcher.GetDefaultDeviceConfig();
            Console.Write( config.ToString() );

            var P = new TestEngine( );
            while( true )
            {
                System.Threading.Thread.Sleep( 100 );
            }
        }

        public TestEngine( )
        {
            m_Engine = new EngineServiceDll( );
            m_Engine.Start( );
        }
    }
}
