using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the secActions table in the BevMetMobile Database.
    /// </summary>
    public partial class SecurableItem : ISecurableItem, IBLSecurableItem, IUISecurableItem
    {
        public IEnumerable<ISecurableItem> GetAll( )
        {
            return All( );
        }
    }
}