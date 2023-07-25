namespace IDENTEC.Tags.Logging
{
    using System;

    public class RawLogData : LogData
    {
        internal byte[] m_byBuffer;
        internal DateTime m_dtEnd;
        internal DateTime m_dtStart;
        internal LogInfoData m_LogInfo = new LogInfoData();
        private RawLogSampleCollection m_rawSamples;
        internal ushort[] m_wBuffer;

        internal void ConvertRawBufferToWords()
        {
            if (this.m_byBuffer == null)
            {
                this.m_wBuffer = new ushort[0];
            }
            else if (this.m_byBuffer.Length == 0)
            {
                this.m_wBuffer = new ushort[0];
            }
            else
            {
                this.m_wBuffer = new ushort[this.m_byBuffer.Length / 2];
                int num = 0;
                for (int i = 0; i < this.m_byBuffer.Length; i += 2)
                {
                    this.m_wBuffer[num++] = BitConverter.ToUInt16(this.m_byBuffer, i);
                }
            }
        }

        public override TimeSpan ElapsedTimeSinceLastSample
        {
            get
            {
                return this.m_LogInfo.ElapsedTimeSinceLastSample;
            }
        }

        public override DateTime End
        {
            get
            {
                return this.m_dtEnd;
            }
        }

        public override DateTime LoggerStarted
        {
            get
            {
                return this.m_LogInfo.LoggerStarted;
            }
        }

        public override DateTime LoggerStopped
        {
            get
            {
                return this.m_LogInfo.LoggerStopped;
            }
        }

        public bool Logging
        {
            get
            {
                return this.m_LogInfo.IsLogging;
            }
        }

        public override TimeSpan LoggingInterval
        {
            get
            {
                return this.m_LogInfo.LoggingInterval;
            }
        }

        public LogInfoData LogInfo
        {
            get
            {
                return this.m_LogInfo;
            }
        }

        public RawLogSampleCollection RawSamples
        {
            get
            {
                if (this.m_rawSamples == null)
                {
                    this.m_rawSamples = new RawLogSampleCollection(this.SampleCount);
                    if (this.SampleCount != 0)
                    {
                        TimeSpan span = (TimeSpan) (this.m_dtEnd - this.m_dtStart);
                        double num1 = span.TotalSeconds / ((double) (this.SampleCount - 1));
                        int totalSeconds = (int) this.m_LogInfo.m_tsLogInterval.TotalSeconds;
                        DateTime time = this.m_dtEnd.AddSeconds((double) (-1 * (totalSeconds * (this.SampleCount - 1))));
                        for (int i = 0; i < this.SampleCount; i++)
                        {
                            RawLogSample sample = this.m_rawSamples.Add();
                            sample._time = time.AddSeconds((double) (totalSeconds * i));
                            sample._wSample = this.m_wBuffer[i];
                        }
                    }
                }
                return this.m_rawSamples;
            }
        }

        public override int SampleCount
        {
            get
            {
                if (this.m_wBuffer != null)
                {
                    return this.m_wBuffer.Length;
                }
                return 0;
            }
        }

        public override DateTime Start
        {
            get
            {
                int totalSeconds = (int) this.m_LogInfo.m_tsLogInterval.TotalSeconds;
                return this.m_dtEnd.AddSeconds((double) (-1 * (totalSeconds * (this.SampleCount - 1))));
            }
        }

        public override bool Wrapped
        {
            get
            {
                return this.m_LogInfo.Wrapped;
            }
        }
    }
}

