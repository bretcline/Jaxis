namespace IDENTEC.Tags
{
    using IDENTEC;
    using System;

    public abstract class Tag : IComparable
    {
        public const int InvalidSignal = -128;
        private MultiAntennaSignals m_AntSignals;
        internal DateTime m_dt;
        private uint m_dwTagID;
        internal Frequency m_Freq;

        protected Tag()
        {
            this.m_Freq = Frequency.Indeterminate;
        }

        protected Tag(Tag t)
        {
            this.m_Freq = Frequency.Indeterminate;
            this.m_dt = t.m_dt;
            this.m_dwTagID = t.m_dwTagID;
            this.m_Freq = t.m_Freq;
            this.AntennaSignals.Copy(t.m_AntSignals);
        }

        [CLSCompliant(false)]
        protected Tag(uint id)
        {
            this.m_Freq = Frequency.Indeterminate;
            this.m_dwTagID = id;
        }

        internal Tag(uint id, DateTime dt, int signal)
        {
            this.m_Freq = Frequency.Indeterminate;
            this.m_dwTagID = id;
            this.m_dt = dt;
            this.Signal = signal;
        }

        public int CompareTo(object obj)
        {
            Tag tag = obj as Tag;
            if (tag == null)
            {
                throw new ArgumentException("The tag can only be compared to a tag object");
            }
            return this.m_dwTagID.CompareTo(tag.Number);
        }

        public static string CreateLabel(string serialNumber)
        {
            return CreateLabel(CreateSerialNumber(serialNumber));
        }

        [CLSCompliant(false)]
        public static string CreateLabel(uint serialNumber)
        {
            return CreateLabel(serialNumber, true);
        }

        [CLSCompliant(false)]
        public static string CreateLabel(uint serialNumber, bool leadingZero)
        {
            char[] chArray = serialNumber.ToString("0000000000").ToCharArray();
            char[] chArray2 = new char[] { chArray[0], '.', chArray[1], chArray[2], chArray[3], '.', chArray[4], chArray[5], chArray[6], '.', chArray[7], chArray[8], chArray[9] };
            if (leadingZero)
            {
                return new string(chArray2);
            }
            return new string(chArray2).TrimStart(new char[] { '0', '.' });
        }

        [CLSCompliant(false)]
        public static uint CreateSerialNumber(string serialNumber)
        {
            return uint.Parse(serialNumber.Replace(".", ""));
        }

        public override bool Equals(object obj)
        {
            Tag tag = obj as Tag;
            if (tag == null)
            {
                throw new ArgumentException("The tag can only be compared to a tag object");
            }
            return this.m_dwTagID.Equals(tag.Number);
        }

        public override int GetHashCode()
        {
            return this.m_dwTagID.GetHashCode();
        }

        public virtual int GetSignalStrength(int antenna)
        {
            return this.AntennaSignals[antenna];
        }

        internal void ResetSignals()
        {
            this.AntennaSignals.Invalidate();
        }

        internal void SetSignalStrength(int antenna, int signal)
        {
            this.AntennaSignals[antenna] = signal;
        }

        public override string ToString()
        {
            return this.Label;
        }

        public MultiAntennaSignals AntennaSignals
        {
            get
            {
                if (this.m_AntSignals == null)
                {
                    this.m_AntSignals = new MultiAntennaSignals();
                }
                return this.m_AntSignals;
            }
            set
            {
                this.m_AntSignals = value;
            }
        }

        public int BestReceiveAntenna
        {
            get
            {
                return this.AntennaSignals.BestReceiveAntenna;
            }
        }

        public DateTime ContactTime
        {
            get
            {
                return this.m_dt;
            }
            set
            {
                this.m_dt = value;
            }
        }

        internal string HexID
        {
            get
            {
                return string.Format("{0:X8}", this.m_dwTagID);
            }
        }

        public string Label
        {
            get
            {
                return CreateLabel(this.m_dwTagID, true);
            }
            set
            {
                this.Number = CreateSerialNumber(value);
            }
        }

        [CLSCompliant(false)]
        public uint Number
        {
            get
            {
                return this.m_dwTagID;
            }
            set
            {
                if (value == 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.m_dwTagID = value;
            }
        }

        public virtual Frequency Region
        {
            get
            {
                return this.m_Freq;
            }
            set
            {
                this.m_Freq = value;
            }
        }

        public int Signal
        {
            get
            {
                return this.AntennaSignals.BestReceiveSignal;
            }
            set
            {
                this.AntennaSignals.SetSignal(1, value);
            }
        }
    }
}

