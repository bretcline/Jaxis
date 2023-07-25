using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using Jaxis.Interfaces;
using Jaxis.Engine.Base.Device;
using Jaxis.Readers.Sprint;
using Jaxis.MessageLibrary;

namespace TrishTest
{
    class Program
    {
        static void Main( string[] args )
        {
            Program P = new Program( );
            P.Start( args );
        }

        public void Start( string[] args )
        {
            DeviceConfig Config = new DeviceConfig( );
            Config.Options = new List<string>( );
            Config.Options.Add( args[0] );
            Config.Options.Add( args[1] );
            Config.Options.Add( args[2] );

            Trish T = new Trish( Config );
            T.Produce = WriteMessage;
            T.Start( );

        }

        public string WriteMessage( Jaxis.Interfaces.IMessage _Message )
        {
            string rc = string.Empty;

            TrishPour Pour = _Message as TrishPour;
            Console.WriteLine( string.Format( "{0} {1} {2} {3} {4}", Pour.PLUNumber, Pour.PouredLiters, Pour.TotalLiters, System.Environment.NewLine, Pour.RawData ) );
            return rc;
        }
    }
}
