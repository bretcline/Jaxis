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
	public class DoDHardwareFaultStatusElement :UDBElement
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
				return this.hardFaultStatusData;
			}
			
		}
		/// <summary> getFirmwareResetCount
		/// 
		/// </summary>
		/// <returns> int
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.HardwareFaultStatusElement method
		/// </summary>
		virtual public int FirmwareResetCount
		{
			get
			{
				if (this.hardFaultStatusData != null)
				{
					if (this.hardFaultStatusData.Length >= 2)
					{
						return (hardFaultStatusData[1] & 0xFF);
					}
				}
				return 0;
			}
			
		}
		/// <summary> getHardwareResetCount
		/// 
		/// </summary>
		/// <returns> int
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.HardwareFaultStatusElement method
		/// </summary>
		virtual public int HardwareResetCount
		{
			get
			{
				if (this.hardFaultStatusData != null)
				{
					if (this.hardFaultStatusData.Length >= 1)
					{
						return (hardFaultStatusData[0] & 0xFF);
					}
				}
				return 0;
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
				return this.hardFaultStatusData.Length;
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
				return UDBElement.ELEMENT_HARDWARE_FAULT_STATUS;
			}
			
		}
		/// <summary> isLowBattDetected
		/// 
		/// </summary>
		/// <returns> boolean
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.HardwareFaultStatusElement method
		/// </summary>
		virtual public bool LowBattDetected
		{
			get
			{
				if (this.hardFaultStatusData != null)
				{
					if (this.hardFaultStatusData.Length >= 3)
					{
						if ((this.hardFaultStatusData[2] & 0x01) == 1)
						{
							return true;
						}
					}
				}
				return false;
			}
			
		}
		/// <summary> isMemoryCorruptionDetected
		/// 
		/// </summary>
		/// <returns> boolean
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.HardwareFaultStatusElement method
		/// </summary>
		virtual public bool MemoryCorruptionDetected
		{
			get
			{
				if (this.hardFaultStatusData != null)
				{
					if (this.hardFaultStatusData.Length >= 3)
					{
						if ((this.hardFaultStatusData[2] & 0x02) == 1)
						{
							return true;
						}
					}
				}
				return false;
			}
			
		}
		
		
		private byte[] hardFaultStatusData;
		
		public DoDHardwareFaultStatusElement(byte[] hardFaultStatus)
		{
			this.hardFaultStatusData = hardFaultStatus;
		}
		
		public DoDHardwareFaultStatusElement(System.String hardFaultStatus)
		{
			this.hardFaultStatusData = SupportClass.ToByteArray(hardFaultStatus);
		}
		
		/// <summary> isHardwareFaultBitmapSet
		/// 
		/// </summary>
		/// <param name="_int">int
		/// </param>
		/// <returns> boolean
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.HardwareFaultStatusElement method
		/// </summary>
		public virtual bool isHardwareFaultBitmapSet(int _int)
		{
			return false;
		}
	}
}