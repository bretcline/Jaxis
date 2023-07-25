using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the Locations table in the BevMetMobile Database.
    /// </summary>
    public partial class Location : ILocation, IBLLocation, IUILocation
    {
        public IEnumerable<ILocation> GetAll( )
        {
            return All( );
        }

        public override string ToString( )
        {
            return this.Name;
        }
    }
}