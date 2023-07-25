using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    //public class CartDataManager : DataManager<ICart, Cart>, ICartDataManager
    //{
    //    #region IDataManager<ICart> Members


    //    public IQueryable<ICart> GetAll( )
    //    {
    //        return Cart.All();
    //    }

    //    public ICart Get( Guid ID )
    //    {
    //        return Cart.GetByID(ID);
    //    }

    //    #endregion
    //}

    public class DeviceDataManager : DataManager<IDevice, Device>, IDeviceDataManager
    {
        #region IDataManager<ICart> Members


        public IQueryable<IDevice> GetAll( )
        {
            return Device.All( );
        }

        public IDevice Get( Guid ID )
        {
            return Device.GetByID( ID );
        }

        #endregion
    }
}