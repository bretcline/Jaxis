using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;

namespace Jaxis.MessageLibrary
{
    public enum OriginalMessageType
    {
        Identec = 1,
        Trish = 2,
    }

    public class CalcPour : BaseMessage, IPourData//, IUIPourPoint
    {
        public Guid TagID { get; set; }

        public string TagNumber { get; set; }

        public string DeviceID { get; set; }

        public double BatteryVoltage { get; set; }

        public double TotalPoured { get; set; }

        public double PourAmount { get; set; }

        public long PourDuration { get; set; }

        public int PourCount { get; set; }

        public double Temperature { get; set; }

        public string RawData { get; set; }

        public string UPCName { get; set; }

        public string Category { get; set; }

        public OriginalMessageType OriginalType { get; set; }

        public string Location { get; set; }
    }
}
