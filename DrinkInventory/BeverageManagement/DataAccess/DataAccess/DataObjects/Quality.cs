using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public partial class Quality : IQuality, IBLQuality
    {
        public IEnumerable<IQuality> GetAll()
        {
            return All();
        }
    }
}