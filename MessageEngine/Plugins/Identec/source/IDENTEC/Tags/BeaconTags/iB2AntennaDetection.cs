namespace IDENTEC.Tags.BeaconTags
{
    using System;

    public class iB2AntennaDetection
    {
        internal int m_AntennaNumber;
        internal int m_DetectionCount;
        internal DateTime m_FirstSeen;
        internal DateTime m_LastSeen;
        internal int m_Signal;
        internal int m_SignalMax;

        public iB2AntennaDetection()
        {
            this.m_FirstSeen = DateTime.Now;
            this.m_LastSeen = DateTime.Now;
            this.m_Signal = -128;
            this.m_SignalMax = -128;
        }

        public iB2AntennaDetection(iB2AntennaDetection b)
        {
            this.m_FirstSeen = DateTime.Now;
            this.m_LastSeen = DateTime.Now;
            this.m_Signal = -128;
            this.m_SignalMax = -128;
            this.m_AntennaNumber = b.m_AntennaNumber;
            this.m_FirstSeen = b.m_FirstSeen;
            this.m_LastSeen = b.m_LastSeen;
            this.m_Signal = b.m_Signal;
            this.m_SignalMax = b.m_SignalMax;
            this.m_DetectionCount = b.DetectionCount;
        }

        public int AntennaNumber
        {
            get
            {
                return this.m_AntennaNumber;
            }
        }

        public int DetectionCount
        {
            get
            {
                return this.m_DetectionCount;
            }
        }

        public TimeSpan EstimatedBeaconRate
        {
            get
            {
                if (this.DetectionCount == 0)
                {
                    throw new InvalidOperationException("The tag has not been detected");
                }
                TimeSpan span = (TimeSpan) (this.LastSeen - this.FirstSeen);
                return new TimeSpan(0, 0, 0, 0, (int) (span.TotalMilliseconds / ((double) this.DetectionCount)));
            }
        }

        public DateTime FirstSeen
        {
            get
            {
                return this.m_FirstSeen;
            }
        }

        public DateTime LastSeen
        {
            get
            {
                return this.m_LastSeen;
            }
            set
            {
                this.m_LastSeen = value;
            }
        }

        public int Signal
        {
            get
            {
                return this.m_Signal;
            }
            set
            {
                this.m_Signal = value;
            }
        }

        public int SignalMax
        {
            get
            {
                return this.m_SignalMax;
            }
        }
    }
}

