using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BeverageMetrics.BeaconParser.BeaconMessages
{
    public class DormantMessage : BeaconMessage
    {
        private double m_XGforce;
        private double m_YGforce;
        private double m_ZGforce;

        public DormantMessage(byte eventType, byte[] beacon)
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
            XGforce = Convert.ToDouble((sbyte)beacon[6], CultureInfo.InvariantCulture) / 100.0;
            YGforce = Convert.ToDouble((sbyte)beacon[7], CultureInfo.InvariantCulture) / 100.0;
            ZGforce = Convert.ToDouble((sbyte)beacon[8], CultureInfo.InvariantCulture) / 100.0;

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

            // Tag software version
            VersionMajor = beacon[13];
            VersionMinor = beacon[14];
            /*
            // barkeeper id
            byte[] barkeeperId = new byte[2];
            Array.Copy(beacon, 13, barkeeperId, 0, 2);
            Array.Reverse(barkeeperId);
            BarkeeperId = BitConverter.ToUInt16(barkeeperId, 0);
             */
        }

        public byte[] Reserve;
        private byte VersionMajor;
        private byte VersionMinor;

        /// <summary>
        /// 
        /// </summary>
        public string TagFirmwareVersion
        {
            get { return VersionMajor.ToString() + "." + VersionMinor.ToString(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public double XGforce
        { 
            get{ return m_XGforce;}
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
