using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;
using SubSonic.Query;

namespace Jaxis.Inventory.Data
{
    public class LocationBLManager : BLManager<ILocation, IBLLocation>, ILocationBLManager
    {
        private Dictionary<Guid, IBLLocation> m_Locations = new Dictionary<Guid,IBLLocation>();
        private static IBLLocation m_NewInventoryLocation = null;
        private static readonly Guid m_NewInventoryLocationID = new Guid("00000000-0000-0000-0000-000000000001");

        public override bool Save(IBLLocation _item)
        {
            bool rc = false;
            rc = base.Save(_item);
            if (true == rc)
            {
                m_Locations[_item.LocationID] = _item;
            }
            return rc;
        }

        public override bool Delete(IBLLocation _item)
        {
            m_Locations.Remove(_item.LocationID);
            return base.Delete(_item);
        }

        public override IBLLocation Get(Guid _id)
        {
            return m_Locations.ContainsKey(_id) ? m_Locations[_id] : base.Get(_id);
        }

        public IBLLocation NewInventoryLocation
        {
            get
            {
                if (null == m_NewInventoryLocation)
                {
                    m_NewInventoryLocation = BLManagerFactory.Get().ManageLocations().Create();
                    m_NewInventoryLocation.LocationID = m_NewInventoryLocationID;
                    m_NewInventoryLocation.Name = "New Inventory";
                }
                return m_NewInventoryLocation;
            }
        }

        public IEnumerable<IBLLocation> GetStorageLocations( )
        {
            if (0 == m_Locations.Count)
            {
                CodingHorror horror =new CodingHorror(
                        "SELECT LocationID FROM Locations WHERE LocationID NOT IN (SELECT DISTINCT ParentID FROM Locations WHERE ParentID IS NOT NULL) UNION SELECT LocationID FROM Inventories I WHERE I.ExitDate IS NULL");
                using (var reader = horror.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = BLManagerFactory.Get().ManageLocations().Get(reader.GetGuid(0));
                        m_Locations[item.LocationID] = item;
                    }
                }
            }
            return m_Locations.Values.ToList();
        }

        public string GetLocationByDeviceID( string _deviceID )
        {
            string rc = "Unknown";
            var item = from loc in DataManagerFactory.Get().Manage<ILocation>().GetAll()
                           join dev in DataManagerFactory.Get().Manage<IDevice>().GetAll() on loc.DeviceID equals
                               dev.DeviceID
                           where dev.HardwareID.Equals(_deviceID)
                           select loc.Name;
            if (0 < item.Count())
            {
                rc = item.FirstOrDefault();
            }
            return rc;

        }
    }
}
