using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BevCartUI
{
    class InventoryItem
    {
        public System.Drawing.Bitmap Image { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public object Tag { get; set; }
    }
}

