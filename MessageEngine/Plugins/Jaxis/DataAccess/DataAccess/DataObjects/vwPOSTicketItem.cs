using System;
using System.Collections.Generic;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IDataItems;
using Jaxis.Inventory.Data.IUIDataItems;

namespace Jaxis.Inventory.Data
{
    public partial class vwPOSTicketItem : IPOSTicketItemView, IBLPOSTicketItemView, IUIPOSTicketItemView
    {
        public IEnumerable<IPOSTicketItemView> GetAll()
        {
            return vwPOSTicketItem.All();
        }

        partial void OnSaving()
        {
        }

        public string StatusTicketItem
        {
            get
            {
                string status = string.Empty;
                if (this.Status == 2)
                {
                    status = "Pending";
                }
                else
                {
                    status = "Complete";
                }
                return status;
            }
        }
    }
}