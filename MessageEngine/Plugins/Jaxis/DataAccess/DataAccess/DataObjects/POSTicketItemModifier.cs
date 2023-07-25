using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    ///// <summary>
    ///// A class which represents the Carts table in the BevMetMobile Database.
    ///// </summary>
    //public partial class Cart : IBLCart, IUICart
    //{
    //    public IEnumerable<ICart> GetAll( )
    //    {
    //        return All( );
    //    }
    //}


    public partial class POSTicketItemModifier : IPOSTicketItemModifier
    {
        #region IDataObject<IPOSTicketItemModifier> Members


        public IEnumerable<IPOSTicketItemModifier> GetAll( )
        {
            return All();
        }

        #endregion
    }
}