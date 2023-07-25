using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;

namespace Jaxis.MessageLibrary.InSync
{
    public class PourData : BaseMessage, IPourData
    {
        public string TagID { get; set; }

        public string DeviceID { get; set; }

        public double BatteryVoltage { get; set; }

        public double PourAmount { get; set; }

        public long PourTime { get; set; }

        public int PourCount { get; set; }

        public double Temperature { get; set; }

        public string RawData { get; set; }
    }

    public interface IUPCData
    {
        string TagID { get; }

        Dictionary<int, double> ViscocityByTemperature { get; }

        Int16 BottleSize { get; }

        double AmountInBottle { get; }
        Single NozzleDiameter { get; }
    }

    public class UPCData : IUPCData
    {
        public string TagID { get; set; }

        public Dictionary<int, double> ViscocityByTemperature { get; set; }

        public Int16 BottleSize { get; set; }

        public double AmountInBottle { get; set; }
        public Single NozzleDiameter { get; set; }

        public UPCData( )
        {
            ViscocityByTemperature = new Dictionary<int, double>( );
        }
    }
}
