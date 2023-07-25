using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the secGroup table in the BevMetMobile Database.
    /// </summary>
    public partial class Group : IGroup, IBLGroup, IUIGroup
    {
        public IEnumerable<IGroup> GetAll( )
        {
            return All( );
        }
    }
}