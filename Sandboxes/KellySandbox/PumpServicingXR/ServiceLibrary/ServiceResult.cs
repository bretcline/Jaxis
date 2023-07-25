using System;
using System.Runtime.Serialization;

namespace WFT.PSService.ServiceLibrary
{
	[DataContract]
	public class ServiceResult
	{
		[DataMember]
		public Guid ObjectGuid { get; set; }

		[DataMember]
		public string ObjectKey { get; set; }

		[DataMember]
		public bool Success { get; set; }

		[DataMember]
		public string Message { get; set; }
	}
}
