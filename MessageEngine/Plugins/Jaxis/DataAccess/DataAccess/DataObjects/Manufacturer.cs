using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public partial class Manufacturer : IManufacturer, IBLManufacturer
    {
        public IEnumerable<IManufacturer> GetAll()
        {
            return All();
        }
    }
}