using System;
using System.Runtime.Serialization;

namespace WFT.PSService.ServiceLibrary
{
	[DataContract]
	public partial class SyncContext
	{
		/// <summary>
		/// WebService parameter structure.
		/// </summary>
		[DataMember]
		public string DeviceName { get; set; } // Device Name is mapped on the server side to a Client Location

		[DataMember]
		public Guid Server { get; set; } // Direct Server ID of a WSM Server

		[DataMember]
		public string Version { get; set; }	// Version of the device (to check for compatibility with appserver version)

        //[DataMember]
        //public Guid AppServerID { get; set; } // unique identifier for appserver so device knows if it has been redirected to another appserver
		
		[DataMember]
		public int PageIndex { get; set; }

		[DataMember]
		public int PageSize { get; set; }

		[DataMember]
		public DateTime LastSync { get; set; }
	}
}
