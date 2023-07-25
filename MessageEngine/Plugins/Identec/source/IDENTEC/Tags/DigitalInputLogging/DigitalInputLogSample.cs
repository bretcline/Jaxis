namespace IDENTEC.Tags.DigitalInputLogging
{
    using System;

    public class DigitalInputLogSample : IComparable
    {
        internal bool m_state;
        internal DateTime m_time;

        public int CompareTo(object obj)
        {
            DigitalInputLogSample sample = obj as DigitalInputLogSample;
            if (sample == null)
            {
                throw new ArgumentException("The tag can only be compared to a LogSample object");
            }
            if (this.SampleTime != sample.m_time)
            {
                return this.SampleTime.CompareTo(sample.m_time);
            }
            return this.m_state.CompareTo(sample.m_state);
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", this.SampleTime.ToString("F"), this.SampleState);
        }

        public bool SampleState
        {
            get
            {
                return this.m_state;
            }
        }

        public DateTime SampleTime
        {
            get
            {
                return this.m_time;
            }
        }
    }
}

