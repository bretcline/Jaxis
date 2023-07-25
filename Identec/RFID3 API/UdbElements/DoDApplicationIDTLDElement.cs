using System;
namespace IDENTEC.UdbElements

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
	public class DoDApplicationIDTLDElement :UDBElement
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
				return applicationIDTLDElement;
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
				return length;
			}
			
		}
		virtual public int TLDIDType
		{
			get
			{
				return type;
			}
			
		}
		virtual public byte[] Element
		{
			get
			{
				return applicationIDTLDElementData;
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
				return UDBElement.ELEMENT_APPLICATION_EXTENSIONS;
			}
			
		}
		internal byte[] applicationIDTLDElement;
		internal byte[] applicationIDTLDElementData;
		internal int type = 0;
		internal int length = 0;
		
		
		public DoDApplicationIDTLDElement()
		{
		}
		
		public DoDApplicationIDTLDElement(byte[] applicationIDTLDElement)
		{
			this.applicationIDTLDElement = applicationIDTLDElement;
			
			int currentIndex = 0;
			type = (int) (applicationIDTLDElement[currentIndex++] & 0xFF);
			length = (int) (applicationIDTLDElement[currentIndex++] & 0xFF);
            applicationIDTLDElementData = new byte[currentIndex + length];
            Array.Copy(applicationIDTLDElement, currentIndex,applicationIDTLDElementData,0, currentIndex + length);
		}
	}
}