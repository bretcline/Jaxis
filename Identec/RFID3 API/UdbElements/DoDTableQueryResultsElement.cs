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
	public class DoDTableQueryResultsElement :UDBElement
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
				return this.tableQueryResultsData;
			}
			
		}
		/// <summary> getFirstMatchedRecIndex
		/// 
		/// </summary>
		/// <returns> int
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.TableQueryResultsElement method
		/// </summary>
		virtual public int FirstMatchedRecIndex
		{
			get
			{
				int firstIndex = 0;
				if (tableQueryResultsData.Length >= 7)
				{
					firstIndex = (tableQueryResultsData[5]) & 0xFF;
					firstIndex = firstIndex << 8;
					firstIndex += ((tableQueryResultsData[6]) & 0xFF);
				}
				else
				{
					//not enough data passed to the element
				}
				return firstIndex;
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
				return this.tableQueryResultsData.Length;
			}
			
		}
		/// <summary> getNumRecsMatched
		/// 
		/// </summary>
		/// <returns> int
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.TableQueryResultsElement method
		/// </summary>
		virtual public int NumRecsMatched
		{
			get
			{
				int numMatched = 0;
				if (tableQueryResultsData.Length >= 5)
				{
					numMatched = (tableQueryResultsData[3]) & 0xFF;
					numMatched = numMatched << 8;
					numMatched += ((tableQueryResultsData[4]) & 0xFF);
				}
				else
				{
					//not enough data passed to the element
				}
				return numMatched;
			}
			
		}
		/// <summary> getTableID
		/// 
		/// </summary>
		/// <returns> int
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.TableQueryResultsElement method
		/// </summary>
		virtual public int TableID
		{
			get
			{
				int tableID = 0;
				if (tableQueryResultsData.Length >= 3)
				{
					tableID = (tableQueryResultsData[1]) & 0xFF;
					tableID = tableID << 8;
					tableID += ((tableQueryResultsData[2]) & 0xFF);
				}
				else
				{
					//not enough data passed to the element
				}
				return tableID;
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
				return UDBElement.ELEMENT_TABLE_QUERY_RESULTS;
			}
			
		}
		virtual public int QueryStatus
		{
			get
			{
				int queryStatus = 0;
				if (tableQueryResultsData.Length >= 1)
				{
					queryStatus = (tableQueryResultsData[0]) & 0xFF;
				}
				else
				{
					// not enough data passed to the element
				}
				return queryStatus;
			}
			
		}
		
		private byte[] tableQueryResultsData;
		
		public DoDTableQueryResultsElement(byte[] tableQueryResults)
		{
			this.tableQueryResultsData = tableQueryResults;
		}
		
		public DoDTableQueryResultsElement(System.String tableQueryResults)
		{
			this.tableQueryResultsData = SupportClass.ToByteArray(tableQueryResults);
		}
	}
}