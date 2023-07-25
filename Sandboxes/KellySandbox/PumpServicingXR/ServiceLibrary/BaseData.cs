using System;
using System.Runtime.Serialization;
using LFI.Sync.DataManager;
using WFT.PS.Shared;

namespace WFT.PSService.ServiceLibrary
{
	[DataContract]
	public class BaseData<T> : IBaseData
	{
		public object PrimaryKey { get; set; }
		public static DataTag DataTag { get; protected set; }
		public static string ConstantTypeID { get { return DataTag.ToString( "G" ); } }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public bool Deleted { get; set; }

		[DataMember]
		public DateTime LastModified { get; set; }
	}
}
