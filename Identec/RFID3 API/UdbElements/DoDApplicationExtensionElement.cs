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
	public class DoDApplicationExtensionElement :UDBElement
	{
		/// <summary> getAppExtensionBlockCount
		/// 
		/// </summary>
		/// <returns> int
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.ApplicationExtensionElement method
		/// </summary>
        /// 

        public override int getLength()
        {

            return Length;
        }

        public override byte[] getData()
        {
            return this.Data;
        }

		virtual public int AppExtensionBlockCount
		{
			get
			{
				return appExtensionBlocks.Count;
			}
			
		}
		/// <summary> getAppExtensionBlocks
		/// 
		/// </summary>
		/// <returns> List
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.ApplicationExtensionElement method
		/// </summary>
		virtual public System.Collections.IList AppExtensionBlocks
		{
			get
			{
				return appExtensionBlocks;
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
				return extensionData;
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
				return extensionData.Length;
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
		
		internal byte[] extensionData;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		ArrayList appExtensionBlocks = new ArrayList();
		
		public DoDApplicationExtensionElement(byte[] appExtension)
		{
			this.extensionData = appExtension;
			
			DoDApplicationExtensionBlock block = new DoDApplicationExtensionBlock(appExtension);
			appExtensionBlocks.Add(block);
		}
		
		public DoDApplicationExtensionElement(System.String appExtension)
		{
			this.extensionData = SupportClass.ToByteArray(appExtension);
		}
		
		
		public DoDApplicationExtensionElement()
		{
		}
		
		/// <summary> getAppExtensionBlock
		/// 
		/// </summary>
		/// <param name="_int">int
		/// </param>
		/// <returns> ApplicationExtensionBlock
		/// </returns>
		/// <todo>  Implement this </todo>
		/// <summary>   gov.pmjait.cai.tag.udbElements.ApplicationExtensionElement method
		/// </summary>
		public virtual DoDApplicationExtensionBlock getAppExtensionBlock(int _int)
		{
			if (_int < appExtensionBlocks.Count)
			{
				return (DoDApplicationExtensionBlock)appExtensionBlocks[_int];
			}
			else
			{
				return null;
			}
		}
	}
}