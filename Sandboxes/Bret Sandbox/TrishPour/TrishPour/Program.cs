using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageLibrary;

namespace TestPourParse
{
    class Program
    {

        protected string CRLF = new string( new char[] { (char)0x0d, (char)0x0a } );

        static void Main( string[] args )
        {
            Program P = new Program( );
//            string Data = @"2005-01-17,17:30:58,121,1001,000000.199,00.021,00
//2005-01-17,17:30:59,122,1002,000000.227,00.021,01
//2005-01-17,17:30:59,123,1003,000000.250,00.021,02
//2005-01-17,17:30:59,124,1004,000000.183,00.011,03
//2005-01-17,17:31:01,122,1001,000000.208,00.008,04
//2005-01-17,17:31:02,122,1004,000000.193,00.010,05
//2005-01-17,17:31:02,124,1003,000000.261,00.010,06
//2005-01-17,17:31:04,121,1002,000000.238,00.010,07
//2005-01-18,07:54:28,124,1004,000000.201,00.007,08
//2005-01-18,07:54:28,126,1003,000000.269,00.008,09
//Empty";
            string Data = @"2005-01-17,17:30:58,121,1001,000000.199,00.021,00
Empty";
            
            P.ProcessData( Data );
        }



        protected void ProcessData( string _Data )
        {
            try
            {
                if( !string.IsNullOrWhiteSpace( _Data ) )
                {
                    string[] Strips = _Data.Replace( CRLF, "|" ).Split( '|' );
                    foreach( string Strip in Strips )
                    {
                        if( !string.IsNullOrWhiteSpace( Strip ) &&
                            !Strip.Equals( "EMPTY", StringComparison.CurrentCultureIgnoreCase ) )
                        {
                            string[] Data = Strip.Split( ',' );

                            TrishPour Pour = new TrishPour( );
                            Pour.ReadTime = DateTime.Parse( string.Format( "{0} {1}", Data[0], Data[1] ) );
                            Pour.ServingBar = Convert.ToInt32( Data[2] );
                            Pour.PLUNumber = Convert.ToInt32( Data[3] );
                            Pour.TotalLiters = Convert.ToDouble( Data[4] );
                            Pour.PouredLiters = Convert.ToDouble( Data[5] );
                            Pour.Sequence = Convert.ToInt16( Data[6] );

                            //ProduceMessage( Pour );

                        }
                    }
                }
            }
            catch( Exception err )
            {
                Console.WriteLine( err.Message );
            }
        }

    }
}
