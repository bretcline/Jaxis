using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.MessageLibrary;

namespace Jaxis.Inventory.Data
{
    public partial class DataTagAlert : ITagActivityItem
    {
        [DataMember]
        public string TagNumber { get; set; }
    }


    public partial class TagAlert : ITagAlert, IBLTagAlert
    {
        #region ITagAlert Members


        public IDevice CurrentDevice
        {
            get
            {
                return null;
                //throw new NotImplementedException( );
            }
            set
            {
                //throw new NotImplementedException( );
            }
        }

        public ILocation CurrentLocation
        {
            get
            {
                return null;
                //throw new NotImplementedException();
            }
            set
            {
                //throw new NotImplementedException( );
            }
        }

        #endregion

        #region IDataObject<ITagAlert> Members


        public IEnumerable<ITagAlert> GetAll( )
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

        public TagPhaseType Type
        {
            get { return (TagPhaseType)AlertType; }
        }

        DateTime IUITagActivity.ActivityTime
        {
            get
            {
                return this.AlertTime;
            }
        }

        public string Location
        {
            get { return ( null != this.CurrentLocation ) ? this.CurrentLocation.Name : string.Empty; }
        }
        #endregion


        #region IMessage

        public string Driver { get; set; }
        ulong IMessageWrapper.Type { get; set; }
        ulong IMessage.Type { get { return (this as IMessageWrapper).Type; } }
        public DateTime ReadTime { get; set; }
        #endregion

        #region Implementation of IAlertMessage

        AlertTypes IAlertMessage.AlertType { get; set; }

        public string AlertMessage { get; set; }

        #endregion
    }
}