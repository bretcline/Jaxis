namespace IDENTEC.Tags.Logging
{
    using System;
    using System.Globalization;

    public class Temperature : IComparable
    {
        internal float m_temperatureDegreesC = float.MinValue;

        public int CompareTo(object obj)
        {
            Temperature temperature = obj as Temperature;
            if (temperature == null)
            {
                throw new ArgumentException("The tag can only be compared to a Temperature object");
            }
            return this.m_temperatureDegreesC.CompareTo(temperature.m_temperatureDegreesC);
        }

        public override string ToString()
        {
            if (RegionInfo.CurrentRegion.IsMetric)
            {
                return string.Format("{0:F2}\x00b0C ", this.m_temperatureDegreesC);
            }
            return string.Format("{0:F2}\x00b0F ", this.DegreesFahrenheit);
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

