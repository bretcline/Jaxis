using System;

namespace BeverageMetrics.BeaconParser.BeaconMessages
{
    public class PourMessage : BeaconMessage
    {
        public PourMessage(byte eventType, byte[] beacon)
        {
            base.EventType = eventType;
            // pour count
            byte[] pourCount = new byte[2];
            Array.Copy(beacon, 1, pourCount, 0, 2);
            Array.Reverse(pourCount);
            PourCount = BitConverter.ToUInt16(pourCount, 0);

            PourDurationZoneA = (double)beacon[3] / 10.0;
            PourDurationZoneB = (double)beacon[4] / 10.0;
            PourDurationZoneC = (double)beacon[5] / 10.0;
            PourDurationZoneD = (double)beacon[6] / 10.0;
            PourDurationZoneE = (double)beacon[7] / 10.0;
            PourDurationZoneF = (double)beacon[8] / 10.0;
            PourDurationZoneG = (double)beacon[9] / 10.0;
            PourDurationZoneH = (double)beacon[10] / 10.0;
            PourDurationZoneI = (double)beacon[11] / 10.0;
            PourDurationZoneJ = (double)beacon[12] / 10.0;
            PourDurationZoneK = (double)beacon[13] / 10.0;
            PourDurationZoneL = (double)beacon[14] / 10.0;

            // temperature
            byte[] temperature = new byte[2];
            Array.Copy(beacon, 15, temperature, 0, 2);
            Array.Reverse(temperature);
            Temperature = (double)BitConverter.ToInt16(temperature, 0) / 10.0;

            // battery voltage
            byte[] batteryVoltage = new byte[2];
            Array.Copy(beacon, 17, batteryVoltage, 0, 2);
            Array.Reverse(batteryVoltage);
            BatteryVoltage = (double)BitConverter.ToUInt16(batteryVoltage, 0) / 1000.0;

            // barkeeper id
            byte[] barkeeperId = new byte[2];
            Array.Copy(beacon, 19, barkeeperId, 0, 2);
            Array.Reverse(barkeeperId);
            BarkeeperId = BitConverter.ToUInt16(barkeeperId, 0);
        }

        public double m_PourDurationZoneA;
        public double m_PourDurationZoneB;
        public double m_PourDurationZoneC;
        public double m_PourDurationZoneD;
        public double m_PourDurationZoneE;
        public double m_PourDurationZoneF;
        public double m_PourDurationZoneG;
        public double m_PourDurationZoneH;
        public double m_PourDurationZoneI;
        public double m_PourDurationZoneJ;
        public double m_PourDurationZoneK;
        public double m_PourDurationZoneL;

        /// <summary>
        /// 
        /// </summary>
        public double PourDurationZoneA
        {
            get { return m_PourDurationZoneA; }
            private set { m_PourDurationZoneA = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double PourDurationZoneB
        {
            get { return m_PourDurationZoneB; }
            private set { m_PourDurationZoneB = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double PourDurationZoneC
        {
            get { return m_PourDurationZoneC; }
            private set { m_PourDurationZoneC = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double PourDurationZoneD
        {
            get { return m_PourDurationZoneD; }
            private set { m_PourDurationZoneD = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double PourDurationZoneE
        {
            get { return m_PourDurationZoneE; }
            private set { m_PourDurationZoneE = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double PourDurationZoneF
        {
            get { return m_PourDurationZoneF; }
            private set { m_PourDurationZoneF = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double PourDurationZoneG
        {
            get { return m_PourDurationZoneG; }
            private set { m_PourDurationZoneG = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double PourDurationZoneH
        {
            get { return m_PourDurationZoneH; }
            private set { m_PourDurationZoneH = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double PourDurationZoneI
        {
            get { return m_PourDurationZoneI; }
            private set { m_PourDurationZoneI = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double PourDurationZoneJ
        {
            get { return m_PourDurationZoneJ; }
            private set { m_PourDurationZoneJ = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double PourDurationZoneK
        {
            get { return m_PourDurationZoneK; }
            private set { m_PourDurationZoneK = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double PourDurationZoneL
        {
            get { return m_PourDurationZoneL; }
            private set { m_PourDurationZoneL = value; }
        }
    }
}
