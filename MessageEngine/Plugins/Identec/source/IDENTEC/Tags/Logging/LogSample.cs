namespace IDENTEC.Tags.Logging
{
    using System;

    public abstract class LogSample
    {
        internal DateTime _time;

        protected LogSample()
        {
        }

        protected LogSample(LogSample sample)
        {
            this._time = sample._time;
        }

        public DateTime SampleTime
        {
            get
            {
                return this._time;
            }
        }
    }
}

