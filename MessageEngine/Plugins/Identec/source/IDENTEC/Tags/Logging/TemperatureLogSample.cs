namespace IDENTEC.Tags.Logging
{
    using System;
    using System.Globalization;

    public class TemperatureLogSample : RawLogSample, IComparable
    {
        public const float InvalidTemperature = float.MinValue;
        internal float m_temperatureDegreesC;

        public TemperatureLogSample()
        {
            this.m_temperatureDegreesC = float.MinValue;
        }

        public TemperatureLogSample(TemperatureLogSample sample) : base(sample)
        {
            this.m_temperatureDegreesC = float.MinValue;
            this.m_temperatureDegreesC = sample.m_temperatureDegreesC;
        }

        public int CompareTo(object obj)
        {
            TemperatureLogSample sample = obj as TemperatureLogSample;
            if (sample == null)
            {
                throw new ArgumentException("The tag can only be compared to a TemperatureLogSample object");
            }
            if (base.SampleTime != sample.SampleTime)
            {
                return base.SampleTime.CompareTo(sample.SampleTime);
            }
            return this.m_temperatureDegreesC.CompareTo(sample.m_temperatureDegreesC);
        }

        public override string ToString()
        {
            if (RegionInfo.CurrentRegion.IsMetric)
            {
                return string.Format("{0:F1}\x00b0C ({1})", this.m_temperatureDegreesC, base._time);
            }
            return string.Format("{0:F1}\x00b0F ({1})", this.DegreesFahrenheit, base._time);
        }

        public float DegreesCelsius
        {
            get
            {
                return this.m_temperatureDegreesC;
            }
        }

        public float DegreesFahrenheit
        {
            get
            {
                if (this.m_temperatureDegreesC == float.MinValue)
                {
                    return float.MinValue;
                }
                return ((1.8f * this.m_temperatureDegreesC) + 32f);
            }
        }
    }
}

