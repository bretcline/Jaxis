namespace Jaxis.Readers.Identec.BeaconMessages
{
    public class BeaconMessage
    {
        private byte m_EventType;
        private ushort m_PourCount;
        private double m_Temperature;
        private double m_BatteryVoltage;
        private ushort m_BarkeeperId;

        /// <summary>
        /// 
        /// </summary>
        public byte EventType
        {
            get { return m_EventType; }
            protected set { m_EventType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ushort PourCount
        {
            get { return m_PourCount; }
            protected set { m_PourCount = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Temperature
        {
            get { return m_Temperature; }
            protected set { m_Temperature = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double BatteryVoltage
        {
            get { return m_BatteryVoltage; }
            protected set { m_BatteryVoltage = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ushort BarkeeperId
        {
            get { return m_BarkeeperId; }
            protected set { m_BarkeeperId = value; }
        }
    }
}
