using System;
using System.Linq;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public class DeviceManager : DataManager<IDevice, Device>, IDeviceManager
    {

        public IQueryable<IDevice> GetAll()
        {
            return Device.All();
        }

        public IDevice Get(Guid ID)
        {
            return Device.GetByID(ID);
        }


    }
}