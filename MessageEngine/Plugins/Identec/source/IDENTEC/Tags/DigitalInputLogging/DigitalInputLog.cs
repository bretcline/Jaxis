namespace IDENTEC.Tags.DigitalInputLogging
{
    using IDENTEC.Readers;
    using System;

    public class DigitalInputLog
    {
        internal bool m_bWrapped;
        internal byte[] m_byBuffer;
        internal DateTime m_dtEnd;
        internal DateTime m_dtStart;
        private DigitalInputSampleCollection m_samples;

        internal static DigitalInputLogSample ConvertRawToSample(uint sample)
        {
            return new DigitalInputLogSample { m_time = DateTimeConvertor.Convert_time_t(sample & 0x7fffffff), m_state = (sample & 0x80000000) != 0x80000000 };
        }

        internal void CreateInputSamples()
        {
            if (this.m_byBuffer != null)
            {
                if (this.m_samples == null)
                {
                    this.m_samples = new DigitalInputSampleCollection(this.SampleCount);
                }
                this.m_samples.Clear();
                for (int i = 0; i < this.m_byBuffer.Length; i += 4)
                {
                    this.m_samples.Add(ConvertRawToSample(BitConverter.ToUInt32(this.m_byBuffer, i)));
                }
            }
        }

        public DateTime End
        {
            get
            {
                return this.m_dtEnd;
            }
        }

        public int SampleCount
        {
            get
            {
                if (this.m_byBuffer != null)
                {
                    return (this.m_byBuffer.Length / 4);
                }
                return 0;
            }
        }

        public DigitalInputSampleCollection Samples
        {
            get
            {
                if (this.m_samples == null)
                {
                    this.m_samples = new DigitalInputSampleCollection();
                }
                return this.m_samples;
            }
        }

        public DateTime Start
        {
            get
            {
                return this.m_dtStart;
            }
        }

        public bool Wrapped
        {
            get
            {
                return this.m_bWrapped;
            }
        }
    }
}

