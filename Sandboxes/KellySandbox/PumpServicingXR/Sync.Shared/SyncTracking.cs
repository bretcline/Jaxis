using System;
using LFI.Sync.DataManager;

namespace LFI.Sync.Shared
{
    public class SyncTracking : IBaseData
    {
        /// <summary>
        /// Gets or sets the sync tracking ID.
        /// </summary>
        /// <value>The sync tracking ID.</value>
        public Guid? SyncTrackingID { get; set; }

        /// <summary>
        /// Gets or sets the data tag ID.
        /// </summary>
        /// <value>The data tag ID.</value>
        public Guid DataTagID { get; set; }

        public string DataTag { get; set; }
        /// <summary>
        /// Gets or sets the last sync.
        /// </summary>
        /// <value>The last sync.</value>
        public DateTime LastSync { get; set; }

        /// <summary>
        /// Gets or sets the type of the sync.
        /// </summary>
        /// <value>Logical OR or SyncType values in the SyncType class to determine what sync actions to conduct</value>
        public int SyncType { get; set; }

		public Guid? DataSourceID { get; set; }

        #region IBaseData Members

        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        /// <value>The primary key.</value>
        public object PrimaryKey
        {
            get { return SyncTrackingID; }
            set { SyncTrackingID = (Guid?) value; }
        }

        #endregion
    }
}