using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.MessageLibrary;

namespace Jaxis.Inventory.Data
{
    public partial class DataActivityLog : ITagActivityItem
    {
        [DataMember]
        public string TagNumber { get; set; }
    }

    public partial class ActivityLog : IBLActivityLog 
    {

        #region IActivityLog Members

        public IDevice CurrentDevice
        {
            get
            {
                return DevicesItem.FirstOrDefault();
            }
            set 
            { 
                this.DeviceID = value.DeviceID; 
            }
        }

        public ILocation CurrentLocation
        {
            get
            {
                return LocationsItem.FirstOrDefault();
            }
            set
            {
                this.LocationID = value.ObjectID;
            }
        }


        #endregion

        #region IDataObject<IActivityLog> Members


        public IEnumerable<IActivityLog> GetAll( )
        {
            return All( );
        }

        #endregion

        #region IUIActivityLog Members

        public string TagNumber
        {
            get
            {
                if (string.IsNullOrEmpty(m_Internal.TagNumber))
                {
                    m_Internal.TagNumber = TagsItem.FirstOrDefault( ).TagNumber;
                }
                return m_Internal.TagNumber;
            }
            set { this.m_Internal.TagNumber = value; }
        }

        public TagPhaseType Type
        {
            get { return (TagPhaseType)ActivityType; }
        }

        DateTime IUIActivityLog.ActivityTime
        {
            get
            {
                return ActivityTime;
            }
        }
        #endregion

        #region IUIActivityLog Members


        public string Location
        {
            get { return this.CurrentLocation.Name; }
        }

        #endregion

        #region IMessageWrapper

        public string Driver { get; set; }
        ulong IMessageWrapper.Type { get; set; }
        ulong IMessage.Type { get { return (this as IMessageWrapper).Type; } }
        public DateTime ReadTime { get; set; }

        #endregion
        
    }
}