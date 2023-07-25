using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public partial class StandardPrice : IBLStandardPrice
    {
        public IEnumerable<IStandardPrice> GetAll()
        {
            return All();
        }
        
    }
}