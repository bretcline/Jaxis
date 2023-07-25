using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public partial class StandardPour : IBLStandardPour
    {
        public IEnumerable<IStandardPour> GetAll()
        {
            return All();
        }
    }
}