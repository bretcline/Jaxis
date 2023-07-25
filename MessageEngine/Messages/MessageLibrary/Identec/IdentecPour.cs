using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;

namespace Jaxis.MessageLibrary
{
    public class IdentecPour : BaseMessage, ITagRead, IPour
    {
        public string DeviceID { get; set; }
        public string TagID { get; set; }
        public double SignalStrength { get; set; }
        public double Temperature { get; set; }
        public double BatteryVoltage { get; set; }
        public List<AngleSlot> Angles { get; set; }
        public byte[] RawData { get; set; }
        public int PourCount { get; set; }
        
        public IdentecPour( )
        {
            Angles = new List<AngleSlot>
            {
                new AngleSlot {Angle = (60 + 81)/2},
                new AngleSlot {Angle = (81 + 87)/2},
                new AngleSlot {Angle = (87 + 92)/2},
                new AngleSlot {Angle = (92 + 98)/2},
                new AngleSlot {Angle = (98 + 103)/2},
                new AngleSlot {Angle = (103 + 106)/2},
                new AngleSlot {Angle = (106 + 112)/2},
                new AngleSlot {Angle = (112 + 115)/2},
                new AngleSlot {Angle = (115 + 121)/2},
                new AngleSlot {Angle = (121 + 142)/2},
                new AngleSlot {Angle = (142 + 160)/2},
                new AngleSlot {Angle = (160 + 180)/2}
            };
        }

        public override string ToString( )
        {
            var total = new TimeSpan( );
            var builder = new StringBuilder( );
            try
            {
                builder.Append( string.Format( "DeviceID: {3} ID:{0} Temp:{1} Volt{2}", this.TagID, this.Temperature, this.BatteryVoltage, DeviceID ) );
                foreach( var slot in Angles )
                {
                    total += slot.Duration;
                    builder.Append( string.Format( " {0}:{1}", slot.Angle, slot.Duration ) );
                }
                builder.Append( string.Format( " Total: {0}", total.TotalSeconds ) );
            }
            catch( Exception err )
            {
                builder.Append( string.Format( "{0}{1}", System.Environment.NewLine, err.Message ) );
            }
            return builder.ToString( );
        }

        public TimeSpan Duration
        {
            get
            {
                var rc = new TimeSpan(0);
                rc = Angles.Aggregate(rc, (current, slot) => current + slot.Duration);
                return rc;
            }
        }
    }
}