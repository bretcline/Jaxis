namespace IDENTEC.Tags.Logging
{
    using System;

    public abstract class LogData
    {
        protected LogData()
        {
        }

        public abstract TimeSpan ElapsedTimeSinceLastSample { get; }

        public abstract DateTime End { get; }

        public abstract DateTime LoggerStarted { get; }

        public abstract DateTime LoggerStopped { get; }

        public abstract TimeSpan LoggingInterval { get; }

        public abstract int SampleCount { get; }

        public abstract DateTime Start { get; }

        public abstract bool Wrapped { get; }
    }
}

