using System;

namespace Jaxis.Readers.Identec.BeaconMessages
{
    public class InventoryMessage : BeaconMessage
    {
        public InventoryMessage(byte eventType, byte[] beacon)
        {
            base.EventType = eventType;
            // pour count
            byte[] pourCount = new byte[2];
            Array.Copy(beacon, 1, pourCount, 0, 2);
            Array.Reverse(pourCount);
            PourCount = BitConverter.ToUInt16(pourCount, 0);

            // attach duration
            byte[] attachDuration = new byte[2];
            Array.Copy(beacon, 3, attachDuration, 0, 2);
            Array.Reverse(attachDuration);
            AtDetachDuration = BitConverter.ToUInt16(attachDuration, 0);

            // reserve
            Reserve = beacon[5];

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

        public ushort AtDetachDuration;

        public byte Reserve;

        public double XGforce;

        public double YGforce;

        public double ZGforce;
    }
}
