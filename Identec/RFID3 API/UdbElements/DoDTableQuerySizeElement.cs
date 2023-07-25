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
	public class DoDTableQuerySizeElement :UDBElement
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
				return this.tableQuerySizeData;
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
				return this.tableQuerySizeData.Length;
			}
			
		}
		/// <summary> getNumTableQueryElements
		/// 
		/// </summary>
		/// <returns> int
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.TableQuerySizeElement method
		/// </summary>
		virtual public int NumTableQueryElements
		{
			get
			{
				if (this.tableQuerySizeData.Length > 0)
				{
					return tableQuerySizeData[0] & 0xFF;
				}
				else
				{
					// error with the size of the data used to create this udb element
					return 0;
				}
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
				return UDBElement.ELEMENT_TABLE_QUERY_SIZE;
			}
			
		}
		
		private byte[] tableQuerySizeData;
		
		public DoDTableQuerySizeElement(byte[] tableQuerySize)
		{
			tableQuerySizeData = tableQuerySize;
		}
		
		public DoDTableQuerySizeElement(System.String tableQuerySize)
		{
			tableQuerySizeData = SupportClass.ToByteArray(tableQuerySize);
		}
	}
}