using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public partial class DataTagActivity : ITagActivityItem
    {
        [DataMember]
        public string TagNumber { get; set; }

    }



    public partial class TagActivity : ITagActivity, IBLTagActivity, IUITagActivity
    {


        #region ITagActivity Members


        public IDevice CurrentDevice
        {
            get
            {
                return null;
                //throw new NotImplementedException( );
            }
            set
            {
                int i = 0;
                //throw new NotImplementedException( );
            }
        }

        public ILocation CurrentLocation
        {
            get
            {
                return DataManagerFactory.Get().Manage<ILocation>().Get(LocationID);
            }
            set
            {
                int i = 0;
                //throw new NotImplementedException( );
            }
        }

        #endregion

        #region IDataObject<ITagActivity> Members


        public IEnumerable<ITagActivity> GetAll( )
        {
            return All( );
        }

        #endregion

        #region IUITagActivity Members

        public string TagNumber
        {
            get
            {
                if (string.IsNullOrEmpty(m_Internal.TagNumber))
                {
                    m_Internal.TagNumber = TagsItem.FirstOrDefault().TagNumber;
                }
                return m_Internal.TagNumber;
            }
            set { this.m_Internal.TagNumber = value; }
        }

        public MessageLibrary.TagPhaseType Type
        {
            get { return (MessageLibrary.TagPhaseType) ActivityType; }
        }

        public string Location
        {
            get { return this.CurrentLocation.Name; }
        }

        #endregion    

        
        #region IMessage

        public string Driver { get; set; }
        ulong IMessageWrapper.Type { get; set; }
        ulong IMessage.Type { get { return (this as IMessageWrapper).Type; } }
        public DateTime ReadTime { get; set; }

        #endregion
        
    }
}