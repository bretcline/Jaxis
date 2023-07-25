namespace IDENTEC.Tags.BeaconTags
{
    using IDENTEC.Tags;
    using System;

    public class iB2Tag : BeaconTag
    {
        internal iB2AntennaDetection[] m_AnntennaInfo;
        internal byte[] m_byData;
        internal int m_flags;
        internal byte m_HFProtID;
        internal IDENTEC.Tags.LoopData m_loop;
        internal int m_nHighByteAgeCount;
        internal int m_nLowByteAgeCount;

        public iB2Tag()
        {
            this.m_flags = 0x100;
            this.m_AnntennaInfo = new iB2AntennaDetection[1];
        }

        public iB2Tag(iB2Tag t) : base(t)
        {
            this.m_flags = 0x100;
            this.m_AnntennaInfo = new iB2AntennaDetection[1];
            this.m_nHighByteAgeCount = t.m_nHighByteAgeCount;
            this.m_nLowByteAgeCount = t.m_nLowByteAgeCount;
            this.m_flags = t.m_flags;
            this.m_HFProtID = t.m_HFProtID;
            if (t.Data != null)
            {
                this.m_byData = new byte[t.Data.Length];
                Array.Copy(t.Data, 0, this.m_byData, 0, t.Data.Length);
            }
            if (t.m_loop != null)
            {
                this.m_loop = new IDENTEC.Tags.LoopData(t.m_loop);
            }
            if (t.m_AnntennaInfo != null)
            {
                this.m_AnntennaInfo = new iB2AntennaDetection[t.m_AnntennaInfo.Length];
                for (int i = 0; i < t.m_AnntennaInfo.Length; i++)
                {
                    this.m_AnntennaInfo[i] = new iB2AntennaDetection(t.m_AnntennaInfo[i]);
                }
                this.ConfigureForSingleAntennaProperties();
            }
        }

        [CLSCompliant(false)]
        public iB2Tag(uint id) : base(id)
        {
            this.m_flags = 0x100;
            this.m_AnntennaInfo = new iB2AntennaDetection[1];
            this.m_AnntennaInfo[0] = new iB2AntennaDetection();
        }

        internal iB2Tag(uint id, DateTime dt, int signal) : base(id, dt, signal)
        {
            this.m_flags = 0x100;
            this.m_AnntennaInfo = new iB2AntennaDetection[1];
            this.m_AnntennaInfo[0] = new iB2AntennaDetection();
            this.m_AnntennaInfo[0].m_AntennaNumber = 1;
            this.m_AnntennaInfo[0].m_FirstSeen = dt;
            this.m_AnntennaInfo[0].m_LastSeen = dt;
            this.m_AnntennaInfo[0].m_DetectionCount = 1;
            this.m_AnntennaInfo[0].m_Signal = signal;
        }

        internal void ConfigureForSingleAntennaProperties()
        {
            if (this.AnntennaInfo.Length == 0)
            {
                base.m_dt = DateTime.Now;
            }
            foreach (iB2AntennaDetection detection in this.AnntennaInfo)
            {
                if (base.m_dt < detection.LastSeen)
                {
                    base.m_dt = detection.LastSeen;
                }
            }
        }

        internal void CreateLoopData()
        {
            this.m_loop = IDENTEC.Tags.LoopData.CreateLoopData(base.Number, this.m_byData, base.ContactTime);
        }

        public override int GetSignalStrength(int antenna)
        {
            foreach (iB2AntennaDetection detection in this.m_AnntennaInfo)
            {
                if (detection.AntennaNumber == antenna)
                {
                    return detection.m_Signal;
                }
            }
            return -128;
        }

        public iB2AntennaDetection[] AnntennaInfo
        {
            get
            {
                if (this.m_AnntennaInfo == null)
                {
                    this.m_AnntennaInfo = new iB2AntennaDetection[0];
                }
                return this.m_AnntennaInfo;
            }
        }

        public IDENTEC.Tags.BatteryStatus Battery
        {
            get
            {
                if (this.m_flags == 0x100)
                {
                    return IDENTEC.Tags.BatteryStatus.Indeterminate;
                }
                if ((this.m_flags & 0x80) == 0)
                {
                    return IDENTEC.Tags.BatteryStatus.Good;
                }
                return IDENTEC.Tags.BatteryStatus.Poor;
            }
        }

        public byte[] Data
        {
            get
            {
                if (this.m_byData == null)
                {
                    this.m_byData = new byte[0];
                }
                return this.m_byData;
            }
        }

        public int DetectedCount
        {
            get
            {
                int detectionCount = 0;
                foreach (iB2AntennaDetection detection in this.AnntennaInfo)
                {
                    if (detectionCount < detection.m_DetectionCount)
                    {
                        detectionCount = detection.m_DetectionCount;
                    }
                }
                return detectionCount;
            }
        }

        public DateTime FirstSeen
        {
            get
            {
                DateTime now = DateTime.Now;
                foreach (iB2AntennaDetection detection in this.AnntennaInfo)
                {
                    if (now > detection.m_FirstSeen)
                    {
                        now = detection.m_FirstSeen;
                    }
                }
                return now;
            }
        }

        public int Flags
        {
            get
            {
                return (this.m_flags & 0xff);
            }
        }

        public int HighByteAgeCount
        {
            get
            {
                return this.m_nHighByteAgeCount;
            }
        }

        public IDENTEC.Tags.LoopData LoopData
        {
            get
            {
                return this.m_loop;
            }
        }

        public int LowByteAgeCount
        {
            get
            {
                return this.m_nLowByteAgeCount;
            }
        }

        public int MaxSignal
        {
            get
            {
                int signalMax = -128;
                foreach (iB2AntennaDetection detection in this.AnntennaInfo)
                {
                    if (signalMax < detection.m_SignalMax)
                    {
                        signalMax = detection.m_SignalMax;
                    }
                }
                if (signalMax == -128)
                {
                    return base.Signal;
                }
                return signalMax;
            }
        }

        public byte ProtocolID
        {
            get
            {
                return this.m_HFProtID;
            }
        }
    }
}

