using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace DataProvider
{
    class Program
    {
        static void Main( string[] args )
        {
            ServiceHost providerHost = new ServiceHost( typeof( DataProvider ) );
            providerHost.Open( );
            Console.WriteLine( "Server is running\nPress <ENTER> to terminate" );
            Console.ReadLine( );
        }
    }
}
