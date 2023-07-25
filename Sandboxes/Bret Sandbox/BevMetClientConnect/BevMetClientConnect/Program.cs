using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BevMetClientConnect
{
    class Program
    {
        static void Main( string[] args )
        {
            using( svcPourEngineService.PourEngineServiceClient C = new svcPourEngineService.PourEngineServiceClient( ) )
            {
                svcPourEngineService.EngineSettings E =  C.GetSettings( "12345" );
            }
        }
    }
}
