using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace DataConsumer
{
    class Program
    {
        static void Main( string[] args )
        {
            DataProviderClient client = new DataProviderClient( );
            Console.WriteLine( client.TestMethod( ) );
            Console.WriteLine( "Press <ENTER> to terminate" );
            Console.ReadLine( );
        }
    }
}
