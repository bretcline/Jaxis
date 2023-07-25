using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;

namespace Jaxis.MessageLibrary
{
    public class TrishPour : BaseMessage, IPour
    {
        public string DeviceID { get; set; }
        public int ServingBar { get; set; }
        public int PLUNumber { get; set; }
        public double TotalLiters { get; set; }
        public double PouredLiters { get; set; }
        public short Sequence { get; set; }

        public Int16 Temperature { get; set; }
        public Int16 Pressure { get; set; }

        public string RawData { get; set; }

        public override string ToString( )
        {
            StringBuilder Builder = new StringBuilder( );
            Builder.Append( string.Format( "DeviceID {0}{1}", DeviceID, System.Environment.NewLine ) );
            Builder.Append( string.Format( "Type {0}{1}", Type, System.Environment.NewLine ) );
            Builder.Append( string.Format( "Serving Bar {0}{1}", ServingBar, System.Environment.NewLine ) );
            Builder.Append( string.Format( "PLU Number {0}{1}", PLUNumber, System.Environment.NewLine ) );
            Builder.Append( string.Format( "Total Liters {0}{1}", TotalLiters, System.Environment.NewLine ) );
            Builder.Append( string.Format( "Poured Liters {0}L {1}ml {2}", PouredLiters, PouredLiters * 1000, System.Environment.NewLine ) );
            Builder.Append( string.Format( "Sequence {0}{1}", Sequence, System.Environment.NewLine ) );
            Builder.Append( string.Format( "Temperature {0}{1}", Temperature, System.Environment.NewLine ) );
            Builder.Append( string.Format( "Pressure {0}{1}", Pressure, System.Environment.NewLine ) );
            Builder.Append( string.Format( "Raw Data {0}{1}", RawData, System.Environment.NewLine ) );
            return Builder.ToString( );
        }
    }
}
