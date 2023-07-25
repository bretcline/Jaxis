using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the SizeType table in the BevMetMobile Database.
    /// </summary>
    public partial class SizeType : ISizeType, IBLSizeType, IUISizeType, INamedObject
    {
        public IEnumerable<ISizeType> GetAll( )
        {
            return All( );
        }

        public override string ToString( )
        {
            return Name;
        }
    }
}