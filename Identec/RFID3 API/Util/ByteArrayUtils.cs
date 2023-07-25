using System;
namespace IDENTEC.Util
{
	
	/// <summary> <p>Title: </p>
	/// 
	/// <p>Description: </p>
	/// 
	/// <p>Copyright: Copyright (c) 2008</p>
	/// 
	/// <p>Company: </p>
	/// 
	/// </summary>
	/// <author>  not attributable
	/// </author>
	/// <version>  1.0
	/// </version>
	public class ByteArrayUtils
	{
		public ByteArrayUtils()
		{
		}
		
		public static sbyte[] subArray(sbyte[] initialArray, int startIndex, int endIndex)
		{
			if (startIndex < endIndex)
			{
				sbyte[] endArray = new sbyte[endIndex - startIndex];
				if (endIndex <= initialArray.Length)
				{
					int j = 0;
					for (int i = startIndex; i < endIndex; i++)
					{
						endArray[j] = initialArray[i];
						j++;
					}
				}
				else
				{
					int maxendIndex = initialArray.Length;
					int j = 0;
					for (int i = startIndex; i < maxendIndex; i++)
					{
						endArray[j] = initialArray[i];
						j++;
					}
				}
				return endArray;
			}
			return null;
		}
		
		public static void  checkStatus(IDENTEC.Tags.ISO18000Tag tag, sbyte[] msgBytes)
		{
			checkStatus(tag, msgBytes, false);
		}

        public static void checkStatus(IDENTEC.Tags.ISO18000Tag tag, sbyte[] msgBytes, bool writeCable)
		{
			int i = 1;
			if (writeCable)
			{
				i = 2;
			}
			int highByte = (int) (msgBytes[i] & 0xFF);
			int lowByte = (int) (msgBytes[i + 1] & 0xFF);
			//System.out.println(highByte);
			//System.out.println(lowByte);
			if (((highByte & 0x08) != 0) && ((lowByte & 0x01) != 0))
			{
				//tag.setTagStatus(ActiveTag.STATUS_SERVICE_AND_ALARM_SET);
				//tag.ServiceBit = true;
			}
			else if ((highByte & 0x08) != 0)
			{
				//tag.setTagStatus(ActiveTag.STATUS_ALARM_SET);
			}
			else if ((lowByte & 0x01) != 0)
			{
				//tag.setTagStatus(ActiveTag.STATUS_SERVICE_SET);
				//tag.ServiceBit = true;
			}
			else
			{
				//tag.setTagStatus(ActiveTag.STATUS_OK);
			}
		}
		
		public static bool checkACK(sbyte[] msgBytes, bool writeCable)
		{
			int index = 1;
			if (writeCable)
			{
				index = 2;
			}
			int highStatusByte = (int) (msgBytes[index] & 0xFF);
			int lowStatusByte = (int) (msgBytes[index + 1] & 0xFF);
			if (((highStatusByte & 0x01) != 0))
			{
				return false; // NACK found
			}
			else
			{
				return true;
			}
		}


  
	}
}