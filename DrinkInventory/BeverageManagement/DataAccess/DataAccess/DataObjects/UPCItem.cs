using System;
using System.Collections.Generic;
using System.Threading;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{

    public partial class vwManufacturer : IBLManufacturerView
    {

        public IEnumerable<IManufacturerView> GetAll()
        {
            return All( );
        }
    }
}