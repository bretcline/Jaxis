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
	/// <author>  Bruce Lawler
	/// </author>
	/// <version>  1.0
	/// </version>
	public class DoDOptionalCommandListElement :UDBElement
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
				return this.optCmdData;
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
				return this.optCmdData.Length;
			}
			
		}
		/// <summary> getOpCodeList
		/// 
		/// </summary>
		/// <returns> List of bytes
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.OptionalCommandListElement method
		/// </summary>
		virtual public System.Collections.IList OpCodeList
		{
			get
			{
				System.Collections.IList opCodes = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
				for (int i = 0; i < optCmdData.Length; i++)
				{
					byte opCode = optCmdData[i];
					opCodes.Add(opCode);
				}
				
				return opCodes;
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
				return UDBElement.ELEMENT_OPTIONAL_COMMAND_LIST;
			}
			
		}
		
		private byte[] optCmdData;
		
		public DoDOptionalCommandListElement(byte[] optCmnd)
		{
			this.optCmdData = optCmnd;
		}
		
		public DoDOptionalCommandListElement(System.String optCmnd)
		{
			this.optCmdData = SupportClass.ToByteArray(optCmnd);
		}
	}
}