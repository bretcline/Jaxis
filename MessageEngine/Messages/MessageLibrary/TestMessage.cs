using System;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;

namespace Jaxis.MessageLibrary
{
    public class TestMessage : BaseMessage, ITagRead //IDeviceMessage
    {
        public string DeviceID { get; set; }

        public string TagID { get; set; }

        public TimeSpan TestData { get; set; }

        public double SignalStrength { get; set; }

        public byte[] RawData { get; set; }

        public TestMessage( )
        {
            TestData = new TimeSpan( );
        }
    }
}