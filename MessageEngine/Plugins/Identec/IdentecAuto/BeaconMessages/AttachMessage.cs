using System;

namespace Jaxis.Readers.Identec.BeaconMessages
{
    public class AttachMessage : BeaconMessage
    {
        private double m_XGforce;
        private double m_YGforce;
        private double m_ZGforce;

        public AttachMessage(byte eventType, byte[] beacon)
        {
            base.EventType = eventType;
            // pour count
            byte[] pourCount = new byte[2];
            Array.Copy(beacon, 1, pourCount, 0, 2);
            Array.Reverse(pourCount);
            PourCount = BitConverter.ToUInt16(pourCount, 0);

            // reserve
            Reserve = new byte[3];
            Array.Copy(beacon, 3, Reserve, 0, 3);

            // x-y-z-G-force
            XGforce = Convert.ToDouble((sbyte)beacon[6]) / 100.0;
            YGforce = Convert.ToDouble((sbyte)beacon[7]) / 100.0;
            ZGforce = Convert.ToDouble((sbyte)beacon[8]) / 100.0;

            // temperature
            byte[] temperature = new byte[2];
            Array.Copy(beacon, 9, temperature, 0, 2);
            Array.Reverse(temperature);
            Temperature = (double)BitConverter.ToInt16(temperature, 0) / 10.0;

            // battery voltage
            byte[] batteryVoltage = new byte[2];
            Array.Copy(beacon, 11, batteryVoltage, 0, 2);
            Array.Reverse(batteryVoltage);
            BatteryVoltage = (double)BitConverter.ToUInt16(batteryVoltage, 0) / 1000.0;

            // barkeeper id
            byte[] barkeeperId = new byte[2];
            Array.Copy(beacon, 13, barkeeperId, 0, 2);
            Array.Reverse(barkeeperId);
            BarkeeperId = BitConverter.ToUInt16(barkeeperId, 0);
        }

        public byte[] Reserve;

        /// <summary>
        /// 
        /// </summary>
        public double XGforce
        {
            get { return m_XGforce; }
            private set { m_XGforce = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double YGforce
        {
            get { return m_YGforce; }
            private set { m_YGforce = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double ZGforce
        {
            get { return m_ZGforce; }
            private set { m_ZGforce = value; }
        }
    }
}
