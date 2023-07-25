using System;
using System.Collections;
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
	public class DoDApplicationExtensionBlock : UDBElement
	{
		/// <summary> getAppIDTLDElement
		/// 
		/// </summary>
		/// <returns> ApplicationIDTLDElement
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.ApplicationExtensionBlock method
		/// </summary>
		virtual public DoDApplicationIDTLDElement AppIDTLDElement
		{
			get
			{
				return idtldElement;
			}
			
		}
		/// <summary> getAppTLDElementCount
		/// 
		/// </summary>
		/// <returns> int
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.ApplicationExtensionBlock method
		/// </summary>
		virtual public int AppTLDElementCount
		{
			get
			{
				return tldElements.Count;
			}
			
		}
		/// <summary> getAppTLDElements
		/// 
		/// </summary>
		/// <returns> List
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.ApplicationExtensionBlock method
		/// </summary>
		virtual public System.Collections.IList AppTLDElements
		{
			get
			{
				return tldElements;
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
				return appExtensionData;
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
				return appExtensionData.Length;
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
				return UDBElement.ELEMENT_APPLICATION_EXTENSION_BLOCK;
			}
			
		}

        public override int getLength()
        {

            return Length;
        }

        public override byte[] getData()
        {
            return this.Data;
        }
		internal DoDApplicationIDTLDElement idtldElement;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		ArrayList tldElements = new ArrayList();
		internal byte[] appExtensionData;
		
		public DoDApplicationExtensionBlock(byte[] appExtension)
		{
			this.appExtensionData = appExtension;
			
			int currentIndex = 0;
			int curElementType = (int) (appExtension[currentIndex++] & 0xFF);
			int curElementLength = (int) (appExtension[currentIndex++] & 0xFF);
			
			
			int curElementIDType = (int) (appExtension[currentIndex++] & 0xFF);
			int curElementIDLength = (int) (appExtension[currentIndex++] & 0xFF);


            byte[] extIDdata = new byte[currentIndex + curElementIDLength];
            Array.Copy(appExtension, currentIndex - 2, extIDdata, 0, currentIndex + curElementIDLength);
			
			
			
			idtldElement = new DoDApplicationIDTLDElement(extIDdata);
			
			// and now the tld elements
			currentIndex += curElementIDLength;
			bool done = false;
			// just to make shure!
			if (currentIndex < (appExtension.Length - 1))
			{
				while (!done)
				{
					// parse tld from stream
					curElementType = (int) (appExtension[currentIndex++] & 0xFF);
					curElementLength = (int) (appExtension[currentIndex++] & 0xFF);

                    byte[] data = new byte[currentIndex + curElementLength];
                    Array.Copy(appExtension, currentIndex - 2,data,0, currentIndex + curElementLength);
					
					// create tld element
					DoDApplicationTLDElement element = new DoDApplicationTLDElement(data);
					
					// add to list
					tldElements.Add(element);
					//check if index is at the end
					currentIndex = currentIndex + curElementLength;
					if (currentIndex >= (appExtension.Length - 1))
					{
						done = true;
					}
				}
			}
		}
		
		public DoDApplicationExtensionBlock(System.String appExtension)
		{
			this.appExtensionData = SupportClass.ToByteArray(appExtension);
		}
		
		public DoDApplicationExtensionBlock()
		{
		}
		
		/// <summary> getAppTLDElement
		/// 
		/// </summary>
		/// <param name="_int">int
		/// </param>
		/// <returns> ApplicationTLDElement
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.ApplicationExtensionBlock method
		/// </summary>
		public virtual DoDApplicationTLDElement getAppTLDElement(int _int)
		{
			if (_int < tldElements.Count)
			{
				return (DoDApplicationTLDElement) tldElements[_int];
			}
			else
			{
				return null;
			}
		}
	}
}