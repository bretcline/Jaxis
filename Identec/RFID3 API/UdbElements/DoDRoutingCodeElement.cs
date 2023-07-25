using System;
namespace IDENTEC.UdbElements

{
	
	/// <summary> <p>Title: </p>
	/// 
	/// <p>Description: </p>
	/// 
	/// <p>Copyright: Copyright (c) 2007</p>
	/// 
	/// <p>Company: Battelle--PNNL</p>
	/// 
	/// </summary>
	/// <author>  Kevin Dorow
	/// </author>
	/// <version>  1.0
	/// </version>
	public class DoDRoutingCodeElement  :UDBElement
	{
        public override int getLength()
        {

            return Length;
        }

        public override byte[] getData()
        {
            return this.Data;
        }

		/// <summary> getData
		/// 
		/// </summary>
		/// <returns> byte[]
		/// </returns>
		/// <todo>  Implement this gov.pmjait.cai.tag.udbElements.UDBElement method </todo>
		virtual public byte[] Data
		{
			get
			{
				return this.routingCodeData;
			}
			
		}
		/// <summary> getLength
		/// 
		/// </summary>
		/// <returns> int
		/// </returns>
		/// <todo>  Implement this gov.pmjait.cai.tag.udbElements.UDBElement method </todo>
		virtual public int Length
		{
			get
			{
				return this.routingCodeData.Length;
			}
			
		}
		/// <summary> getRoutingCode
		/// 
		/// </summary>
		/// <returns> String
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.RoutingCodeElement method
		/// </summary>
		virtual public System.String RoutingCode
		{
			get
			{
				return new System.String(SupportClass.ToCharArray(this.routingCodeData));
			}
			
		}
		/// <summary> getType
		/// 
		/// </summary>
		/// <returns> String
		/// </returns>
		/// <todo>  Implement this gov.pmjait.cai.tag.udbElements.UDBElement method </todo>
		virtual public System.String Type
		{
			get
			{
				return UDBElement.ELEMENT_ROUTING_CODE;
			}
			
		}
		
		private byte[] routingCodeData;
		
		public DoDRoutingCodeElement(byte[] rcData)
		{
			this.routingCodeData = rcData;
		}
		
		public DoDRoutingCodeElement(System.String rcString)
		{
			this.routingCodeData = SupportClass.ToByteArray(rcString);
		}
	}
}