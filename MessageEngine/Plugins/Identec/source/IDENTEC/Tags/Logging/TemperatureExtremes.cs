namespace IDENTEC.Tags.Logging
{
    using System;

    public class TemperatureExtremes
    {
        internal bool m_bSuccess;
        internal DateTime m_dtLogEnd;
        internal DateTime m_dtLogStart;
        internal float m_MaximumDegreesCelsius;
        internal float m_MinimumDegreesCelsius;

        public DateTime LogEnd
        {
            get
            {
                return this.m_dtLogEnd;
            }
        }

        public DateTime LogStart
        {
            get
            {
                return this.m_dtLogStart;
            }
        }

        public float MaximumDegreesCelsius
        {
            get
            {
                return this.m_MaximumDegreesCelsius;
            }
        }

        public float MaximumDegreesFahrenheit
        {
            get
            {
                return ((1.8f * this.MaximumDegreesCelsius) + 32f);
            }
        }

        public float MinimumDegreesCelsius
        {
            get
            {
                return this.m_MinimumDegreesCelsius;
            }
        }

        public float MinimumDegreesFahreheit
        {
            get
            {
                return ((1.8f * this.MinimumDegreesCelsius) + 32f);
            }
        }

        public bool Success
        {
            get
            {
                return this.m_bSuccess;
            }
        }
    }
}

