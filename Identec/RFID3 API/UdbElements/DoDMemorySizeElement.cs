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
	public class DoDMemorySizeElement : UDBElement
	{

        public override int getLength()
        {

            return Length;
        }

        public override byte[] getData()
        {
            return this.Data;
        }

		/// <summary> getAvailableTableMemory
		/// 
		/// </summary>
		/// <returns> int
		/// </returns>
		/// <todo>  Implement this gov.pmjait.cai.tag.udbElements.MemorySizeElement </todo>
		/// <summary>   method
		/// </summary>
		virtual public int AvailableTableMemory
		{
			get
			{
				int availableMem = 0;
				if (memSizeData.Length >= 12)
				{
					availableMem = (memSizeData[8]) & 0xFF;
					availableMem = availableMem << 8;
					availableMem += ((memSizeData[9]) & 0xFF);
					availableMem = availableMem << 8;
					availableMem += ((memSizeData[10]) & 0xFF);
					availableMem = availableMem << 8;
					availableMem += ((memSizeData[11]) & 0xFF);
				}
				else
				{
					//not enough data passed to the element
				}
				return availableMem;
			}
			
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
				return this.memSizeData;
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
				return this.memSizeData.Length;
			}
			
		}
		/// <summary> getRWMemory
		/// 
		/// </summary>
		/// <returns> int
		/// </returns>
		/// <todo>  Implement this gov.pmjait.cai.tag.udbElements.MemorySizeElement </todo>
		/// <summary>   method
		/// </summary>
		virtual public int RWMemory
		{
			get
			{
				int RWMem = 0;
				if (memSizeData.Length >= 4)
				{
					RWMem = (memSizeData[0]) & 0xFF;
					RWMem = RWMem << 8;
					RWMem += ((memSizeData[1]) & 0xFF);
					RWMem = RWMem << 8;
					RWMem += ((memSizeData[2]) & 0xFF);
					RWMem = RWMem << 8;
					RWMem += ((memSizeData[3]) & 0xFF);
				}
				else
				{
					//not enough data passed to the element
				}
				return RWMem;
			}
			
		}
		/// <summary> getTotalTableMemory
		/// 
		/// </summary>
		/// <returns> int
		/// </returns>
		/// <todo>  Implement this gov.pmjait.cai.tag.udbElements.MemorySizeElement </todo>
		/// <summary>   method
		/// </summary>
		virtual public int TotalTableMemory
		{
			get
			{
				int totalMem = 0;
				if (memSizeData.Length >= 8)
				{
					totalMem = (memSizeData[4]) & 0xFF;
					totalMem = totalMem << 8;
					totalMem += ((memSizeData[5]) & 0xFF);
					totalMem = totalMem << 8;
					totalMem += ((memSizeData[6]) & 0xFF);
					totalMem = totalMem << 8;
					totalMem += ((memSizeData[7]) & 0xFF);
				}
				else
				{
					//not enough data passed to the element
				}
				return totalMem;
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
				return UDBElement.ELEMENT_MEMORY_SIZE;
			}
			
		}
		
		private byte[] memSizeData;
		
		public DoDMemorySizeElement(byte[] memSize)
		{
			this.memSizeData = memSize;
		}
		
		public DoDMemorySizeElement(System.String memSize)
		{
			this.memSizeData = SupportClass.ToByteArray(memSize);
		}
	}
}