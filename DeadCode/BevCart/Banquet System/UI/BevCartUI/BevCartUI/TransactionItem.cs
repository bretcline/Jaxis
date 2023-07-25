using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BevCartUI
{
    class TransactionItem
    {
        public System.Drawing.Bitmap Image { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Amount { get; set; }
        public object Tag { get; set; }
    }
}
