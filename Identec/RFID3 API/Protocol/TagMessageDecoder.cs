using System;

namespace IDENTEC.Protocol
{
	
	/// <summary> <p>Title: </p>
	/// <p>Description: handles the decoding of an input message stream into a series
	/// of Interrogator messages</p>
	/// <p>Copyright: Copyright (c) 2006</p>
	/// <p>Company: </p>
	/// </summary>
	/// <author>  Bruce Lawler
	/// </author>
	/// <version>  1.0
	/// </version>
	
	
	public class TagMessageDecoder
	{
        enum InputType { UNKNOWN, STANDARD_IN, ECHO};
        enum MsgPart { ADDR, CMD_CODE, COMM_STATUS, DEV_STATUS, TAG_STATUS, RSSI,  PROTO_ID, MESSAGE_LEN,  PAY_LEN, PAYLOAD, LRC, UNKNOWN };

		private void  InitBlock()
		{
 			mMsgFmt = new MsgFormat(this);
		}
		private System.IO.MemoryStream mBuffer = new System.IO.MemoryStream();
		
		//UPGRADE_NOTE: The initialization of  'type' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private InputType type = InputType.UNKNOWN;
		
		private static byte[] HEADER_IN = new byte[]{(byte) 0x17, (byte) 0x40};
		private const int ACK = 0x01;
		private const int NACK = 0x01;
		private const int ACK_BEEP_RESPONSE = 0x01;
		
		/// <summary> our message from the Interrogator will look like this
		/// <header 4b><int id 2b><messageLength 1b><messageId 1b><ResponseCode 1b><ResponsePayloadLen 1b><Payload variable><CRC 2b>
		/// </summary>
		
		/// <summary> MsgPart is used to tell us where we are in the incoming stream</summary>

		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MsgFormat' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> this class helps us keep track of current message</summary>
		private class MsgFormat
		{
			public MsgFormat(TagMessageDecoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TagMessageDecoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				mCurrentPart = MsgPart.PROTO_ID;
			}
			private TagMessageDecoder enclosingInstance;
			public TagMessageDecoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_NOTE: The initialization of  'mCurrentPart' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			internal MsgPart mCurrentPart;
			internal int partIndex; // starting byte index of current part
			internal int expectedBytes; // how many bytes we are looking for for the current part
			internal int totalMsgBytes; // how many bytes we are looking for in the message
			
			public virtual void  reset()
			{
				partIndex = 0;
				expectedBytes = 0;
				totalMsgBytes = 0;
				mCurrentPart = MsgPart.PROTO_ID;
			}
		}
		
		//UPGRADE_NOTE: The initialization of  'mMsgFmt' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private MsgFormat mMsgFmt;
		private int mIndex = 0;
		private DeviceMessage mMsg = new DeviceMessage();
		
		
		public TagMessageDecoder()
		{
			InitBlock();
		}
		

		
		
		/// <summary> sets incoming message bytes in the decoder.  The decoder is
		/// responsible for recognizing complete messages and then notifying the
		/// server via the message listener.
		/// </summary>
		public  void  addBytes(byte[] bytes)
		{
			for (int i = 0; i < bytes.Length; i++)
			{
				//System.out.println("adding byte " + i + ": " + bytes[i]);
				addByte(bytes[i]);
			}
		}
		
		/// <summary> adds a single byte to our buffer.</summary>
		private void  addByte(byte b)
		{
			// read until we read the whole header
			if (type == InputType.UNKNOWN)
			{
                type = InputType.STANDARD_IN; 
                    //readHeader(b, mIndex);
			}
			
			mIndex++;
			mBuffer.WriteByte((byte) b);
			
			switch (type)
			{
				
				case InputType.STANDARD_IN: 
					handleStandardMessageByte(b);
					break;
				}
		}
		/// <summary> handle the standard message response from the interrogator.
		/// 
		/// 
		/// </summary>
		private void  handleStandardMessageByte(byte b)
		{
			int cnt = (int) mBuffer.Length;
			bool msgFinished = false;
			// check for end of header
			if (mMsgFmt.mCurrentPart == MsgPart.PROTO_ID && cnt >= HEADER_IN.Length)
			{
				mMsgFmt.mCurrentPart = MsgPart.TAG_STATUS;
				mMsgFmt.partIndex = HEADER_IN.Length;
				mMsgFmt.expectedBytes = 2;
			}
			else
			{
				// Once we have the header, we can now check for end of each section
				if ((cnt - mMsgFmt.partIndex) == mMsgFmt.expectedBytes)
				{
					// handle each section end
					switch (mMsgFmt.mCurrentPart)
					{
						
						case MsgPart.TAG_STATUS:  {
								//System.out.println("Tag Status " + Integer.toString(b));
								mMsgFmt.mCurrentPart = MsgPart.MESSAGE_LEN;
								mMsgFmt.expectedBytes = 1;
								mMsgFmt.partIndex = cnt;
								mMsgFmt.totalMsgBytes = (int) b & 0xFF;
								break;
							}

                        case MsgPart.MESSAGE_LEN:
                            {
								//System.out.println("Message Length " + Integer.toString(b));
								mMsgFmt.mCurrentPart = MsgPart.CMD_CODE;
								mMsgFmt.expectedBytes = 1;
								mMsgFmt.partIndex = cnt;
								mMsgFmt.totalMsgBytes = (int) b & 0xFF;
								break;
							}

                        case MsgPart.CMD_CODE:
                            {
								//System.out.println("Commnad Code " + Integer.toString(b));
								//logical here based on the command code
								int cmd = (int) b & 0xFF; // if we are collecting skip the pay_len and consider all part of the payload
								mMsg.ResultCode = cmd;
								if (b == 0x1F || b == 0x0C || b == 0x0E || b == 0x70 || b == 0x00 || b == 0x26)
								{
									// collect with UDB, read model, read firmware, read UDB  -- may be others
									mMsgFmt.mCurrentPart = MsgPart.PAYLOAD;
									mMsgFmt.expectedBytes = mMsgFmt.totalMsgBytes - (int) cnt - 1;
									mMsgFmt.partIndex = cnt;
									//System.out.println("Expected Bytes: " + Integer.toString(mMsgFmt.expectedBytes));
									if (mMsgFmt.expectedBytes == 0)
									{
										//System.out.println("Only the LRC remains Finish Message");
										mMsgFmt.mCurrentPart = MsgPart.LRC;
										mMsgFmt.expectedBytes = 1;
										mMsgFmt.partIndex = cnt;
										mMsg.PayloadLength = 0;
									}
								}
								//if we just wrote to the tag this is just a confirmation message no payload we are done go to the LRC
								else if (cmd == 224 || cmd == 147 || cmd == 137 || cmd == 142 || cmd == 225)
								{
									//0xE0 0x93 and 0x89, 0x8e, 0xe1 write mem, write user id, write routing code, delete writeable, beep on/off - may be others
									mMsgFmt.mCurrentPart = MsgPart.LRC;
									mMsgFmt.expectedBytes = 1;
									mMsgFmt.partIndex = cnt;
									mMsg.PayloadLength = 0;
								}
								else
								{
									mMsgFmt.mCurrentPart = MsgPart.PAY_LEN;
									mMsgFmt.expectedBytes = 1;
									mMsgFmt.partIndex = cnt;
								}
								break;
							}

                        case MsgPart.PAY_LEN:
                            {
								if (b == 0)
								{
									mMsgFmt.mCurrentPart = MsgPart.LRC;
									mMsgFmt.partIndex = cnt;
									mMsgFmt.expectedBytes = 1; //length of the LRC
									mMsg.PayloadLength = 0;
								}
								else
								{
									mMsgFmt.mCurrentPart = MsgPart.PAYLOAD;
									mMsgFmt.partIndex = cnt;
									mMsgFmt.expectedBytes = (int) b & 0xFF;
									mMsg.PayloadLength = mMsgFmt.expectedBytes;
								}
								break;
							}

                        case MsgPart.PAYLOAD:
                            {
								//System.out.println("Payload " + Integer.toString(b));
								// copy just the payload bytes over
								byte[] bytes = new byte[mMsgFmt.expectedBytes];
								byte[] src = mBuffer.ToArray();
								for (int i = mMsgFmt.partIndex; i < src.Length; i++)
								{
									bytes[i - mMsgFmt.partIndex] = src[i];
								}
								mMsg.MessagePayload = bytes;
								
								mMsgFmt.mCurrentPart = MsgPart.LRC;
								mMsgFmt.partIndex = cnt;
								mMsgFmt.expectedBytes = 1;
								break;
							}

                        case MsgPart.LRC:
                            {
								//System.out.println("LRC " + Integer.toString(b));
								byte[] allBytes = mBuffer.ToArray();
								int lrc = allBytes[allBytes.Length - 1] & 0xFF;
								mMsg.Message = allBytes;
								mMsg.CRC = lrc;
								mMsg.InterrogatorID = 0;
								msgFinished = true;
								break;
							}
						}
				}
			}
			
			if (msgFinished)
			{
				//System.out.println("Message Fully Decoded");
				//notifyNewMessage(mMsg);
				//reset();
			}
		}
		
		/// <summary> reads the header</summary>
		private InputType readHeader(byte b, int index)
		{
			//System.out.println("Processing header byte: " + b);
			if (index < HEADER_IN.Length)
			{
				if (b != HEADER_IN[index])
				{
					throw new System.IO.IOException("Unexpected data received.  " + b);
				}
			}
			if (index > HEADER_IN.Length)
			{
				throw new System.IO.IOException("Unexpected data received.  " + b);
			}
			
			if (index == HEADER_IN.Length)
			{
				// this is a response from the interrogator
				// we just read the header, prepare for the next field
				mMsgFmt.partIndex = HEADER_IN.Length;
				mMsgFmt.expectedBytes = 2;
				mMsgFmt.mCurrentPart = MsgPart.TAG_STATUS;
				mMsg.HeaderSize = HEADER_IN.Length;
				return InputType.STANDARD_IN;
			}
			return InputType.UNKNOWN;
		}
		private void  handleUnknownMessageByte(byte b)
		{
			
			byte[] allBytes = mBuffer.ToArray();

		}


        public DeviceMessage getMessage()
        {
            return this.mMsg;
        }
	}
}