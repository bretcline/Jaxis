using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServiceTest
{
    class Program
    {
        static void Main( string[] args )
        {
            try
            {
                svcPourConfig.PourEngineConfigClient T1 = new svcPourConfig.PourEngineConfigClient( );
                T1.StartEvent( 1, null );

                svcPourEngineConfig.PourEngineConfig Test = new svcPourEngineConfig.PourEngineConfig( );
                Test.StartEvent( 1, true, null );
            }
            catch( Exception err )
            {
                Console.WriteLine( err.Message );
            }
        }
    }
}