using System;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;

namespace Jaxis.MessageLibrary
{
    public enum TagPhaseType
    {
        Heartbeat = 0,
        Connect = 1,
        Disconnect = 2,
        Dormant = 3,
        MissedPour = 4,
        MissedMsg = 5,
        BadBottleAttach = 6,
        TagMoved = 7,
        HeartbeatDetached = 8,

        NonEmptyBottle = 127,
    }

    public class PhaseMessage : BaseMessage, ITagRead
    {
        public string DeviceID { get; set; }

        public string TagID { get; set; }

        public double SignalStrength { get; set; }

        public byte[] RawData { get; set; }

        public TagPhaseType EventType { get; set; }

        public double BatteryVoltage { get; set; }

        public double Temperature { get; set; }

        public bool UpsideDown { get; set; }
    }
}