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
	public class DoDUserIDElement :UDBElement
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
				return this.uIDData;
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
				return this.uIDData.Length;
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
				return UDBElement.ELEMENT_USER_ID;
			}
			
		}
		/// <summary> getUserID
		/// 
		/// </summary>
		/// <returns> String
		/// </returns>
		/// <todo>  Implement this gov.pmjait.cai.tag.udbElements.UserIDElement </todo>
		/// <summary>   method
		/// </summary>
		virtual public System.String UserID
		{
			get
			{
				return new System.String(SupportClass.ToCharArray(this.uIDData));
			}
			
		}
		
		private byte[] uIDData;
		
		public DoDUserIDElement(byte[] userIDData)
		{
			this.uIDData = userIDData;
		}
		
		public DoDUserIDElement(System.String userID)
		{
			this.uIDData = SupportClass.ToByteArray(userID);
		}
	}
}