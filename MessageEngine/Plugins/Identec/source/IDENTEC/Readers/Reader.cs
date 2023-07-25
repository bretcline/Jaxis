namespace IDENTEC.Readers
{
    using IDENTEC;
    using System;

    public abstract class Reader
    {
        internal Frequency m_Freq = Frequency.Indeterminate;
        internal CompatibleRegion m_WorkingRegion = CompatibleRegion.Indeterminate;

        protected Reader()
        {
        }

        internal static int CalculateSlotSize(int nTags)
        {
            if (nTags <= 0x10)
            {
                if (nTags < 0)
                {
                    throw new ArgumentOutOfRangeException("nTags");
                }
                return 4;
            }
            if (nTags <= 0x20)
            {
                return 5;
            }
            if (nTags <= 0x40)
            {
                return 6;
            }
            if (nTags <= 0x80)
            {
                return 7;
            }
            if (nTags <= 0x100)
            {
                return 8;
            }
            if (nTags <= 0x200)
            {
                return 9;
            }
            if (nTags <= 0x400)
            {
                return 10;
            }
            if (nTags <= 0x800)
            {
                return 11;
            }
            return 12;
        }

        public abstract bool Disconnect();
        public static int GetElapsedTime(ref int start)
        {
            int num = Environment.TickCount - start;
            if (num < 0)
            {
                num = 0x7fffffff;
                start = Environment.TickCount - 0x4c4b40;
            }
            return num;
        }

        public static bool TimedOut(ref int start, int timeout)
        {
            int num = Environment.TickCount - start;
            if (num < 0)
            {
                num = 0x7fffffff;
                start = Environment.TickCount - 0x4c4b40;
                return true;
            }
            return (num > timeout);
        }

        public abstract bool Connected { get; }

        public CompatibleRegion WorkingRegion
        {
            get
            {
                return this.m_WorkingRegion;
            }
        }

        public enum CompatibleRegion
        {
            All = 2,
            EuropeanOnly = 0,
            Indeterminate = -1,
            NorthAmericanOnly = 1
        }
    }
}

