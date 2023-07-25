using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.BeverageManagement.Plugin
{
    public class PourInformation
    {
        public string TagNumber { get; set; }
		public double BatteryVoltage { get; set; }
		public string DeviceName { get; set; }
        public double PourAmount { get; set; }
		public double AmountLeft { get; set; }
		public int UPCSize { get; set; }
		public string ItemNumber { get; set; }
		public string UPCName { get; set; }
		public string Manufacturer { get; set; }
		public string Category { get; set; }
		public string RootCategory { get; set; }
		public DateTime PourTime { get; set; }
		public double Temperature { get; set; }
		public string LocationName { get; set; }
    }
}
