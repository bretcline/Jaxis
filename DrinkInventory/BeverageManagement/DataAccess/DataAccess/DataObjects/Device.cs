using System;
using System.Linq;

using System.Collections.Generic;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public partial class Device : IDevice, IBLDevice
    {

        #region IDevice Members


        #endregion

        #region IDataObject<IDevice> Members


        public IEnumerable<IDevice> GetAll( )
        {
            return All();
        }

        #endregion
    }


    public partial class DeviceAlert : IDeviceAlert
    {


        #region IDataObject<IDeviceAlert> Members


        public IEnumerable<IDeviceAlert> GetAll( )
        {
            return All( );
        }

        #endregion


        #region IMessage

        public string Driver { get; set; }
        public ulong Type { get; set; }
        public DateTime ReadTime { get; set; }

        #endregion
        
    }

}