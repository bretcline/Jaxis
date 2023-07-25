namespace IDENTEC.Tags.Logging
{
    using System;
    using System.Collections;

    public class CompareTemperatureLogSamplesByLowestTemperature : IComparer
    {
        public int Compare(object x, object y)
        {
            TemperatureLogSample sample = x as TemperatureLogSample;
            TemperatureLogSample sample2 = y as TemperatureLogSample;
            if (sample.m_temperatureDegreesC != sample2.m_temperatureDegreesC)
            {
                return sample.m_temperatureDegreesC.CompareTo(sample2.m_temperatureDegreesC);
            }
            return sample._time.CompareTo(sample2._time);
        }
    }
}

