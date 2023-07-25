using System;
using Jaxis.Inventory.Data;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;
using SubSonic.Extensions;

namespace BeverageManagement.Forms.Activity.Widgets
{
    internal class UIActivityLog : IUITagActivity
    {
        public UIActivityLog( string _tagNumber, string _location, DateTime _activityTime, int _type )
        {
            TagNumber = _tagNumber;
            ActivityTime = _activityTime;
            Type = (TagPhaseType)_type;
            Location = _location;
        }

        public UIActivityLog( IUIActivityLog _log )
        {
            TagNumber = _log.TagNumber;
            ActivityTime = _log.ActivityTime;
            Type = _log.Type;
            Location = _log.Location;
        }

        public UIActivityLog( IUITagActivity _log )
        {
            TagNumber = _log.TagNumber;
            ActivityTime = _log.ActivityTime;
            Type = _log.Type;
            Location = _log.Location;
        }

        public UIActivityLog( DataActivityLog _log )
        {
            //Log.Time("Create UIActivityLog", LogType.Debug, false, () =>
            {
                TagID = _log.TagID;
                ActivityTime = _log.ActivityTime;
                Type = (TagPhaseType)_log.ActivityTypeID;
                var loc = BLManagerFactory.Get().ManageLocations().Get(_log.LocationID); 
                if (null != loc)
                {
                    Location = loc.Name;
                }
            }//);
        }
        public UIActivityLog( DataTagActivity _log )
        {
            TagID = _log.TagID;
            ActivityTime = _log.ActivityTime;
            Type = (TagPhaseType)_log.ActivityType;
            var loc = BLManagerFactory.Get( ).ManageLocations( ).Get( _log.LocationID );
            if( null != loc )
            {
                Location = loc.Name;
            }
        }
        public UIActivityLog( DataTagAlert _log )
        {
            TagID = _log.TagID;
            ActivityTime = _log.AlertTime;
            Type = (TagPhaseType)_log.AlertType;
            var loc = BLManagerFactory.Get( ).ManageLocations( ).Get( _log.LocationID );
            if( null != loc )
            {
                Location = loc.Name;
            }
        }

        public void ReloadData(DataActivityLog _log)
        {
            TagID = _log.TagID;
            ActivityTime = _log.ActivityTime;
            Type = (TagPhaseType)_log.ActivityTypeID;
            var loc = BLManagerFactory.Get().ManageLocations().Get(_log.LocationID);
            if (null != loc)
            {
                Location = loc.Name;
            }
        }

        //public UIActivityLog( IActivityItem _log )
        //{
        //}

        protected Guid TagID
        {
            set
            {
                TagNumber = BLManagerFactory.Get().ManageTags().GetTagNumber(value);
            }
        }
        public string TagNumber { get; set; }
        public DateTime ActivityTime { get; set; }
        public TagPhaseType Type { get; set; }
        public string Location { get; set; }
    }
}