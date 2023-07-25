using System;
using System.Collections.Generic;
using System.Text;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;

namespace Jaxis.MessageLibrary
{
    public class AngleSlot
    {
        public int Angle { get; set; }
        public TimeSpan Duration { get; set; }
    }

    public class PourMessage : BaseMessage, ITagRead, IPour
    {
        public string DeviceID { get; set; }

        public string TagID { get; set; }

        public double SignalStrength { get; set; }

        public double Temperature { get; set; }

        public double BatteryVoltage { get; set; }

        public List<AngleSlot> Angles { get; set; }

        //public string ReaderID { get; set; }
        public byte[] RawData { get; set; }

        public int PourCount { get; set; }

        public PourMessage( )
        {
            Angles = new List<AngleSlot>( );
        }

        public override string ToString( )
        {
            TimeSpan Total = new TimeSpan( );
            StringBuilder Builder = new StringBuilder( );
            try
            {
                Builder.Append(string.Format("DeviceID: {3} ID:{0} Temp:{1} Volt{2}", this.TagID, this.Temperature, this.BatteryVoltage, this.DeviceID));
                foreach( AngleSlot Slot in Angles )
                {
                    Total += Slot.Duration;
                    Builder.Append( string.Format( " {0}:{1}", Slot.Angle, Slot.Duration ) );
                }
                Builder.Append( string.Format( " Total: {0}", Total.TotalSeconds ) );
            }
            catch( Exception err )
            {
                Builder.Append( string.Format( "{0}{1}", System.Environment.NewLine, err.Message ) );
            }
            return Builder.ToString( );
        }
    }
}