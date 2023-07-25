using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JaxisInterfaces;

namespace BucketCollectionLib
{
    public class PourMessage : IMessage
    {
        public int DeviceID;
        public int TagID;
        public DateTime PourStop { get; set; }
        public float Temperature;
        public Dictionary<AngleBucket, double> Buckets { get; set; }

        public MessageType Type
        {
            get { return MessageType.RawData; }
        }

        public DateTime ReadTime
        {
            //ReadTime == StartTime
            get;
            set;
        }

        public PourMessage()
        {
            DeviceID = 12345;
            TagID = 54321;
            Temperature = 74.2F;
            Buckets = new Dictionary<AngleBucket, double>();
            foreach (AngleBucket AB in Enum.GetValues(typeof(AngleBucket)))
            {
                Buckets[AB] = 0.0;
            }
        }
    }
}
