using System;

namespace IDENTEC.Protocol
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
	public class DeviceMessage
	{
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> gets the message in a string of bytes to be sent to an interrogator</summary>
		/// <summary> sets the value of a message object from an input stream of bytes</summary>
		virtual public byte[] Message
		{
			get
			{
				return mMessage;
			}
			
			set
			{
				this.mMessage = value;
			}
			
		}
		virtual public byte[] MessagePayload
		{
			get
			{
				return mPayload;
			}
			
			set
			{
				this.mPayload = value;
			}
			
		}
		virtual public int InterrogatorID
		{
			get
			{
				return mDeviceID;
			}
			
			set
			{
				this.mDeviceID = value;
			}
			
		}
		virtual public int HostID
		{
			get
			{
				return this.mHostID;
			}
			
			set
			{
				this.mHostID = value;
			}
			
		}
		virtual public int MsgID
		{
			get
			{
				return this.mMsgID;
			}
			
			set
			{
				this.mMsgID = value;
			}
			
		}
		virtual public int TagID
		{
			get
			{
				return mTagID;
			}
			
			set
			{
				this.mTagID = value;
			}
			
		}
		virtual public int CRC
		{
			get
			{
				return mCRC;
			}
			
			set
			{
				this.mCRC = value;
			}
			
		}
		virtual public int ResultCode
		{
			get
			{
				return mResultCode;
			}
			
			set
			{
				this.mResultCode = value;
			}
			
		}
		virtual public int PayloadLength
		{
			get
			{
				return mPayloadLength;
			}
			
			set
			{
				this.mPayloadLength = value;
			}
			
		}
		virtual public int HeaderSize
		{
			get
			{
				return headerSize;
			}
			
			set
			{
				this.headerSize = value;
			}
			
		}
		virtual public int ACKFlag
		{
			get
			{
				return mACKFlag;
			}
			
			set
			{
				mACKFlag = value;
			}
			
		}
		virtual public int StatusFlag
		{
			get
			{
				return mStatusFlag;
			}
			
			set
			{
				mStatusFlag = value;
			}
			
		}
		private int mDeviceID;
		private int mHostID;
		private int mTagID;
		private int mMsgID;
		private int mCRC;
		private int mResultCode;
		private int mPayloadLength;
		private int mACKFlag;
		private int mStatusFlag;
		/// <summary> all of the bytes in the message</summary>
		protected internal byte[] mMessage;
		/// <summary> just the payload bytes of the message</summary>
		protected internal byte[] mPayload;
		protected internal int headerSize;
		
		/// <summary> </summary>
		public virtual bool validMessage()
		{
			// the CRC is based on the whole message (execept for the crc of course)
			/* this is commented out because the crc does not appear to be being sent from the 18007 tags
			byte[] bytes = getMessage();
			byte[] interestBytes = new byte[bytes.length - 2];
			for (int i = 0; i < interestBytes.length; i++) {
			interestBytes[i] = bytes[i];
			}
			
			String s = HexByteConversions.toHexString(interestBytes);
			int crc = crcCalc.CRC16CCITT(s);
			if( crc != getCRC() ) {
			System.err.println("CRC is not valid for " + s + ", expected " + crc + " but is " + getCRC());
			return false;
			}*/
			return true;
			
			//        // the CRC is based on the bytes after the header up to the crc
			//        byte[] bytes = getMessage();
			//        byte[] interestBytes = new byte[bytes.length - getHeaderSize() - 2];
			//        for (int i = getHeaderSize(); i < bytes.length - 2; i++) {
			//            interestBytes[i - getHeaderSize()] = bytes[i];
			//        }
			//
			//        String s = HexByteConversions.toHexString(interestBytes);
			//        int crc = crcCalc.CRC16CCITT(s);
			//        if( crc != getCRC() ) {
			//            System.err.println("CRC is not valid for " + s + ", expected " + crc + " but is " + getCRC());
			//            return false;
			//        }
			//        return true;
		}
		
		
		/// <summary> </summary>
		//public override System.String ToString()
		//{
		//	System.String s = "IntMessage: intID=" + mDeviceID + "; tagID=" + mTagID;
		//	if (mPayload != null)
		//	{
		//		s += ("; payload=" + HexByteConversions.toHexString(mPayload));
		//	}
		//	if (mMessage != null)
		//	{
		//		s += ("; complete msg=" + HexByteConversions.toHexString(mMessage));
		//	}
		//	
		//	return s;
		//}
		
		//public virtual System.String messageToString()
		//{
		//	return HexByteConversions.toHexString(mMessage);
		//}
	}
}