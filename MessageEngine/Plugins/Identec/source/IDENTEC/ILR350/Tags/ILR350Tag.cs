namespace IDENTEC.ILR350.Tags
{
    using IDENTEC.ILR350.Tags.Info;
    using IDENTEC.Tags;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ILR350Tag : IComparable
    {
        internal ushort _beaconCounter;
        private byte[] _beaconMessage;
        private DateTime _dtFirstSeen;
        private DateTime _dtLastSeen;
        private byte _manufacturerID;
        private byte _messageType;
        internal int _nBeaconsSinceLastReaderTransmission;
        private int _NbTimeSeen;
        private int _nLastSignal;
        private int _nMaxSignal;
        internal byte[] _SerialNumber;
        internal int _status;
        private byte _typeID;
        private static List<byte> AddedDescription = new List<byte>();
        public static CustomTagFactory CustomFactory;
        internal static Dictionary<byte, TagDescription> Description;
        protected static NLog.Logger log = LogManager.GetLogger("ILR350Tag");

        static ILR350Tag()
        {
            Dictionary<byte, TagDescription> dictionary = new Dictionary<byte, TagDescription>();
            dictionary.Add(1, new TagDescription(typeof(iQ350), "i-Q350 WAM", new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 2), new List<TagFeature>(new TagFeature[] { new RS232(), new LED(3) })));
            dictionary.Add(3, new TagDescription(typeof(iQ350), "i-Q350 HH", new TimeSpan(0, 0, 0, 0, 100), new TimeSpan(0, 0, 5), new List<TagFeature>(new TagFeature[] { new Temperature() })));
            dictionary.Add(4, new TagDescription(typeof(iB350), "i-B350", new TimeSpan(0, 0, 0, 0, 100), new TimeSpan(0, 0, 5), new List<TagFeature>(new TagFeature[0])));
            dictionary.Add(5, new TagDescription(typeof(iQ350), "i-Q350-WEIGHTLOG", new TimeSpan(0, 0, 0, 0, 100), new TimeSpan(0, 0, 5), new List<TagFeature>(new TagFeature[] { new RS232() })));
            dictionary.Add(6, new TagDescription(typeof(iQ350), "i-Q350", new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 2), new List<TagFeature>(new TagFeature[0])));
            dictionary.Add(0x20, new TagDescription(typeof(iQ350RTLS), "i-Q350 RTLS", new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 2), new List<TagFeature>(new TagFeature[0])));
            dictionary.Add(0x30, new TagDescription(typeof(iQ350RTLS), "i-Q350 RTLS PB", new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 2), new List<TagFeature>(new TagFeature[] { new DigitalIO() })));
            dictionary.Add(0x40, new TagDescription(typeof(iQ350), "i-Q350L", new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 2), new List<TagFeature>(new TagFeature[] { new Loop() })));
            dictionary.Add(0x41, new TagDescription(typeof(iQ350), "i-Q350L M", new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 5), new List<TagFeature>(new TagFeature[] { new Loop() })));
            dictionary.Add(80, new TagDescription(typeof(iQ350), "i-Q350L W PB", new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 5), new List<TagFeature>(new TagFeature[] { new Loop(), new DigitalIO() })));
            dictionary.Add(0x60, new TagDescription(typeof(iQ350RTLS), "i-Q350L RTLS", new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 2), new List<TagFeature>(new TagFeature[] { new Loop() })));
            dictionary.Add(0x70, new TagDescription(typeof(iQ350RTLS), "i-Q350L RTLS PB", new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 2), new List<TagFeature>(new TagFeature[] { new Loop(), new DigitalIO() })));
            dictionary.Add(0x80, new TagDescription(typeof(iQ350Logger), "i-Q350T", new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 2), new List<TagFeature>(new TagFeature[] { new Temperature(), new IDENTEC.ILR350.Tags.Info.Logger() })));
            dictionary.Add(0xc0, new TagDescription(typeof(iQ350Logger), "i-Q350TL", new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 2), new List<TagFeature>(new TagFeature[] { new Temperature(), new IDENTEC.ILR350.Tags.Info.Logger(), new Loop() })));
            dictionary.Add(0xd0, new TagDescription(typeof(iQ350Logger), "i-Q350TL PB", new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 2), new List<TagFeature>(new TagFeature[] { new Temperature(), new IDENTEC.ILR350.Tags.Info.Logger(), new Loop(), new DigitalIO() })));
            dictionary.Add(0xff, new TagDescription(typeof(iQ350), "i-Q350", new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 2), new List<TagFeature>(new TagFeature[0])));
            Description = dictionary;
            CustomFactory = null;
        }

        protected ILR350Tag()
        {
            this._manufacturerID = 0xff;
            this._typeID = 0xff;
            this._nMaxSignal = -128;
            this._nLastSignal = -128;
            this._status = -1;
        }

        protected ILR350Tag(ILR350Tag tag)
        {
            this._manufacturerID = 0xff;
            this._typeID = 0xff;
            this._nMaxSignal = -128;
            this._nLastSignal = -128;
            this._status = -1;
            this.SerialNumber = tag.SerialNumber;
            this._manufacturerID = tag._manufacturerID;
            this._typeID = tag._typeID;
            this._status = tag._status;
            if (tag._beaconMessage != null)
            {
                this._beaconMessage = new byte[tag._beaconMessage.Length];
                Array.Copy(tag._beaconMessage, 0, this._beaconMessage, 0, tag._beaconMessage.Length);
            }
            this._nBeaconsSinceLastReaderTransmission = tag._nBeaconsSinceLastReaderTransmission;
            this._NbTimeSeen = tag._NbTimeSeen;
            this._dtFirstSeen = tag._dtFirstSeen;
            this._dtLastSeen = tag._dtLastSeen;
            this._nMaxSignal = tag._nMaxSignal;
            this._nLastSignal = tag._nLastSignal;
            this._beaconCounter = tag._beaconCounter;
            this._messageType = tag._messageType;
        }

        public static bool AddDescription(byte TypeID, TagDescription des)
        {
            TagDescription description;
            if (AddedDescription.Contains(TypeID))
            {
                return false;
            }
            if (Description.TryGetValue(TypeID, out description))
            {
                return false;
            }
            Description.Add(TypeID, des);
            AddedDescription.Add(TypeID);
            AddedDescription.Sort();
            return true;
        }

        [Browsable(false)]
        public virtual int CompareTo(object obj)
        {
            ILR350Tag tag = obj as ILR350Tag;
            if (tag != null)
            {
                return this.SerialLabel.CompareTo(tag.SerialLabel);
            }
            string serialNumber = obj as string;
            if (serialNumber == null)
            {
                throw new ArgumentException("The tag can only be compared to a tag object or tag string");
            }
            uint num = Tag.CreateSerialNumber(serialNumber);
            return this.SerialNumber.CompareTo(num);
        }

        public override bool Equals(object obj)
        {
            ILR350Tag tag = obj as ILR350Tag;
            if (tag != null)
            {
                return this.SerialLabel.Equals(tag.SerialLabel);
            }
            string str = obj as string;
            if (str == null)
            {
                throw new ArgumentException("The tag can only be compared to a tag object");
            }
            return this.SerialLabel.Equals(str);
        }

        public TagFeature FindFeature(TagFeature feature)
        {
            TagFeature feature2 = null;
            this.description.Features.Sort();
            int num = this.description.Features.BinarySearch(feature);
            if (num >= 0)
            {
                feature2 = this.description.Features[num];
            }
            return feature2;
        }

        public override int GetHashCode()
        {
            return this.SerialNumber.GetHashCode();
        }

        public static bool RemoveDescription(byte TypeID)
        {
            if (AddedDescription.Contains(TypeID))
            {
                Description.Remove(TypeID);
                AddedDescription.Remove(TypeID);
                AddedDescription.Sort();
                return true;
            }
            return false;
        }

        [CLSCompliant(false)]
        public static ILR350Tag TagFactory(uint ID)
        {
            return TagFactory(0x49, TagType.UNKNOWN, ID);
        }

        [CLSCompliant(false)]
        public static ILR350Tag TagFactory(TagType tagType, uint ID)
        {
            return TagFactory(0x49, tagType, ID);
        }

        [CLSCompliant(false)]
        public static ILR350Tag TagFactory(byte Manufacturer, TagType tagType, uint ID)
        {
            ILR350Tag tag = null;
            TagDescription description;
            if (CustomFactory != null)
            {
                tag = CustomFactory(tagType);
            }
            if (tag != null)
            {
                tag.ManufacturerID = Manufacturer;
                tag.TypeID = (byte) tagType;
                tag.SerialNumber = ID;
                return tag;
            }
            if (Description.TryGetValue((byte) tagType, out description) && (description.type != null))
            {
                tag = description.TagFactory();
            }
            if (tag != null)
            {
                tag.ManufacturerID = Manufacturer;
                tag.TypeID = (byte) tagType;
                tag.SerialNumber = ID;
                return tag;
            }
            return new iQ350 { ManufacturerID = Manufacturer, TypeID = (byte) tagType, SerialNumber = ID };
        }

        public override string ToString()
        {
            return this.SerialLabel;
        }

        [Description("The alarm status 0 if ok"), Category("Appearance")]
        public IDENTEC.ILR350.Tags.AlarmStatus Alarm
        {
            get
            {
                if (this._status == -1)
                {
                    return IDENTEC.ILR350.Tags.AlarmStatus.Indeterminate;
                }
                if ((this.Status & 0x40) == 0)
                {
                    return IDENTEC.ILR350.Tags.AlarmStatus.OFF;
                }
                return IDENTEC.ILR350.Tags.AlarmStatus.ON;
            }
        }

        [Description("The alarm status 0 if ok"), Category("Appearance")]
        public IDENTEC.ILR350.Tags.AlarmStatus AlarmStatus
        {
            get
            {
                if (this._status == -1)
                {
                    return IDENTEC.ILR350.Tags.AlarmStatus.Indeterminate;
                }
                if ((this.Status & 0x40) == 0)
                {
                    return IDENTEC.ILR350.Tags.AlarmStatus.OFF;
                }
                return IDENTEC.ILR350.Tags.AlarmStatus.ON;
            }
        }

        [Description("The battery status 0 if ok"), Category("Appearance")]
        public IDENTEC.ILR350.Tags.BatteryStatus BatteryStatus
        {
            get
            {
                if (this._status == -1)
                {
                    return IDENTEC.ILR350.Tags.BatteryStatus.Indeterminate;
                }
                if ((this.Status & 0x80) == 0)
                {
                    return IDENTEC.ILR350.Tags.BatteryStatus.Good;
                }
                return IDENTEC.ILR350.Tags.BatteryStatus.Poor;
            }
        }

        public int BeaconCounterHighByte
        {
            get
            {
                return (this._beaconCounter >> 8);
            }
        }

        public int BeaconCounterLowByte
        {
            get
            {
                return (this._beaconCounter & 0xff);
            }
        }

        [Browsable(false)]
        public byte[] BeaconMessage
        {
            get
            {
                return this._beaconMessage;
            }
            set
            {
                this._beaconMessage = value;
            }
        }

        public virtual byte BeaconMessageType
        {
            get
            {
                return this._messageType;
            }
            set
            {
                this._messageType = value;
            }
        }

        [Browsable(false)]
        public TagDescription description
        {
            get
            {
                TagDescription description;
                if (Description.TryGetValue(this._typeID, out description))
                {
                    return description;
                }
                return null;
            }
        }

        public int LastSignal
        {
            get
            {
                return this._nLastSignal;
            }
            set
            {
                this._nLastSignal = value;
            }
        }

        [Description("Manufacturer ID"), Category("Appearance")]
        public virtual byte ManufacturerID
        {
            get
            {
                return this._manufacturerID;
            }
            set
            {
                this._manufacturerID = value;
            }
        }

        [Category("Appearance"), Description("Last communication RSSI level")]
        public int MaxSignal
        {
            get
            {
                return this._nMaxSignal;
            }
            set
            {
                this._nMaxSignal = value;
            }
        }

        public int SeenCount
        {
            get
            {
                return this._NbTimeSeen;
            }
            set
            {
                this._NbTimeSeen = value;
            }
        }

        [Category("Appearance"), Description("Serial number")]
        public string SerialLabel
        {
            get
            {
                return Tag.CreateLabel(this.SerialNumber);
            }
            set
            {
                uint num = Tag.CreateSerialNumber(value);
                this.SerialNumber = num;
            }
        }

        [CLSCompliant(false)]
        public uint SerialNumber
        {
            get
            {
                return BitConverter.ToUInt32(this._SerialNumber, 0);
            }
            set
            {
                this._SerialNumber = BitConverter.GetBytes(value);
            }
        }

        public int Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = (byte) value;
            }
        }

        [Description("Time Beacon first seen"), Category("Appearance")]
        public DateTime TimeFirstSeen
        {
            get
            {
                return this._dtFirstSeen;
            }
            set
            {
                this._dtFirstSeen = value;
            }
        }

        [Description("Time Beacon last seen"), Category("Appearance")]
        public DateTime TimeLastSeen
        {
            get
            {
                return this._dtLastSeen;
            }
            set
            {
                this._dtLastSeen = value;
            }
        }

        [Category("Appearance"), Description("Type ID")]
        public virtual byte TypeID
        {
            get
            {
                return this._typeID;
            }
            set
            {
                this._typeID = value;
            }
        }

        public delegate ILR350Tag CustomTagFactory(TagType tagType);
    }
}

