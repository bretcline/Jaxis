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
	public class DoDApplicationTLDElement :UDBElement
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
				return this.ApplicationTLDElement;
			}
			
		}
		virtual public byte[] Element
		{
			get
			{
				return ApplicationTLDElementData;
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
				return ApplicationTLDElement.Length;
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
		
		internal byte[] ApplicationTLDElement;
		internal byte[] ApplicationTLDElementData;
		internal int type = 0;
		internal int length = 0;
		
		public DoDApplicationTLDElement()
		{
		}
		
		internal DoDApplicationTLDElement(byte[] ApplicationTLDElement)
		{
			this.ApplicationTLDElement = ApplicationTLDElement;
			
			int currentIndex = 0;
			type = (int) (ApplicationTLDElement[currentIndex++] & 0xFF);
			length = (int) (ApplicationTLDElement[currentIndex++] & 0xFF);
			ApplicationTLDElementData = new byte[currentIndex + length];
            Array.Copy(ApplicationTLDElement, currentIndex,ApplicationTLDElementData,0, currentIndex + length);
		}
	}
}