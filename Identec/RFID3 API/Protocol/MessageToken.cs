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
	public class MessageToken
	{
		virtual public int TokenLength
		{
			get
			{
				return tokenLength;
			}
			
		}
		internal int tokenLength = 0; // high order first 4 bits of the first byte (not including the first byte)
		internal byte[] Token;
		internal bool ValidToken = false;
		
		public MessageToken()
		{
		}
		
		public MessageToken(byte[] token)
		{
			
			if (token != null)
			{
				tokenLength = (int) (token[0] & 0xFF);
				//tokenLength = tokenLength >> 4;
			}
			else
			{
				tokenLength = 0;
                return;
			}
			if (token.Length == tokenLength + 1)
			{
				Token = token;
				ValidToken = true;
			}
			else
			{
				if (token.Length > tokenLength + 1)
				{
					Token = subArray(token, 0, tokenLength + 1);
					ValidToken = true;
				}
				else
				{
					Token = null;
					ValidToken = false;
				}
			}
			//System.out.println("Token length: " + tokenLength);
		}
		public MessageToken(int[] token)
		{
		}
		
		public virtual bool validToken()
		{
			return ValidToken;
		}
		public virtual byte[] getToken()
		{
			return Token;
		}
		public virtual int setToken(byte[] token)
		{
			if (token != null)
			{
				tokenLength = (int) (token[0] & 0xFF);
				tokenLength = tokenLength >> 4;
			}
			else
			{
				tokenLength = 0;
			}
			if (token.Length == tokenLength + 1)
			{
				Token = token;
				ValidToken = true;
			}
			else
			{
				if (token.Length > tokenLength + 1)
				{
					Token = subArray(token, 0, tokenLength + 1);
					ValidToken = true;
				}
				else
				{
					Token = null;
					ValidToken = false;
				}
			}
			//System.out.println("Token length: " + tokenLength);
			return tokenLength;
		}
		
		public virtual byte[] subArray(byte[] initialArray, int startIndex, int endIndex)
		{
			if (startIndex < endIndex)
			{
				byte[] endArray = new byte[endIndex - startIndex];
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
		
		public override System.String ToString()
		{
			return System.Text.Encoding.ASCII.GetString(Token);
		}
	}
}