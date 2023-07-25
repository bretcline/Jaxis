using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PourConfigTest.svcPourEngineService;

namespace PourConfigTest
{


    //http://61.12.11.117:8080/Design_Time_Addresses/BEVMET.PourEngine/PourEngineService/
    //http://61.12.11.117:8080/Design_Time_Addresses/BEVMET.PourEngine/PourEngineService/
    class Program
    {
        static void Main( string[] args )
        {
            //try
            //{
            //    using( svcPour.PourEngineConfigClient PourClient = new svcPour.PourEngineConfigClient( ) )
            //    {
            //        PourClient.StartEvent( 10, null );
            //    }
            //}
            //catch( Exception err )
            //{
            //    System.Diagnostics.Debug.Write( err.Message );
            //}

            try
            {
                svcPourEngineService.PourEngineServiceClient Client = new svcPourEngineService.PourEngineServiceClient( );
                svcPourEngineService.PourData Data = new svcPourEngineService.PourData( );

                //EngineSettings Settings = Client.GetSettings( "READER1" );

                UPCData TestData = Client.GetUPCByTag( 123456 );
                //Data.DeviceID = "READER1";
                //Data.EventID = 5;
                //Data.TagID = 1234;
                //Data.PourAmount = 40;
                //Data.PourTime = DateTime.Now - DateTime.Now.AddSeconds( -5 );
                //Data.RawData = "RAWDATA";
                //Client.PushPourData( Data );

                //svcPourEngineService.TagPhase Phase = new TagPhase( );
                //Phase.BatteryVoltage = 2;
                //Phase.EventTime = DateTime.Now;
                //Phase.EventType = TagPhaseType.Connect;
                //Phase.TagID = 123;
                //Client.PushTagEvent( Phase );


                //Console.WriteLine( Data.PourTime );
            }
            catch( Exception err )
            {
                System.Diagnostics.Debug.Write( err.Message );
            }
        }
    }
}
