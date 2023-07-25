namespace IDENTEC.Tags.Logging
{
    using System;

    public class LogInfoData
    {
        internal bool m_bLogging;
        internal bool m_bWrapped;
        internal DateTime m_dtLoggerStartTime;
        internal DateTime m_dtLoggerStopTime;
        internal DateTime m_dtReadTime = DateTime.Now;
        internal byte m_logFormat;
        internal ushort m_LogSampleSize;
        internal uint m_LogSize;
        internal uint m_MaxSize;
        internal TimeSpan m_tsLogInterval;
        internal TimeSpan m_tsTimeSinceLastLog;

        public TimeSpan ElapsedTimeSinceLastSample
        {
            get
            {
                return this.m_tsTimeSinceLastLog;
            }
        }

        public bool IsLogging
        {
            get
            {
                return this.m_bLogging;
            }
        }

        public DateTime LoggerStarted
        {
            get
            {
                return this.m_dtLoggerStartTime;
            }
        }

        public DateTime LoggerStopped
        {
            get
            {
                return this.m_dtLoggerStopTime;
            }
        }

        public TimeSpan LoggingInterval
        {
            get
            {
                return this.m_tsLogInterval;
            }
        }

        public int TagSampleCount
        {
            get
            {
                return (int) (this.m_LogSize / this.m_LogSampleSize);
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

