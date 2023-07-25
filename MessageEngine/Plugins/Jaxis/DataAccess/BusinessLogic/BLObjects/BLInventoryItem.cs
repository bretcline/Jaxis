using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data.BusinessLogic.BLObjects
{
    public class BLInventoryItem : IBLInventoryItem, IUIInventoryItem
    {
        public IBLInventory Inventory { get; set; }
        public IBLTag Tag { get; set; }

        #region IBLInventoryItem Members

        private IBLUPCItem m_UPC = null;
        public IBLUPCItem UPC
        {
            get
            {
                return m_UPC;
            }
            set
            {
                m_UPC = value;
            }
        }

        private string m_Name = string.Empty;
        public string Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value;
            }
        }

        protected IBLLocation m_Location = null;
        public IBLLocation Location
        {
            get
            {
                return m_Location;
            }
            set
            {
                m_Location = value;
                if (null != Inventory)
                {
                    Inventory.Location = m_Location;
                }
                else if (null != Tag)
                {
                    Tag.CurrentLocation = m_Location;
                }
            }

        }

        public string LocationName { get { return m_Location.Name; } }

        public long TotalQuantity
        {
            get
            {
                return TaggedQuantity + StockQuantity;
            }
        }

        private long? m_StockQuantity = null;
        public long StockQuantity
        {
            get
            {
                if (null == m_StockQuantity)
                {
                    if (null != Inventory)
                    {
                        var Man = BLManagerFactory.Get().ManageInventory();
                        var items = Man.GetAll().Where(I => I.UPC.UPCID == m_UPC.ObjectID && I.LocationID == m_Location.ObjectID && I.TagID == null && I.ExitDate == null);
                        if (null != items)
                        {
                            m_StockQuantity = items.Count();
                        }
                    }
                    else
                    {
                        m_StockQuantity = 0;
                    }
                }
                return m_StockQuantity.Value;
            }
        }

        private long? m_TaggedQuantity = null;
        public long TaggedQuantity
        {
            get
            {
                if (null == m_TaggedQuantity)
                {
                    if (null != Inventory)
                    {
                        var Man = BLManagerFactory.Get().ManageInventory();
                        var items = Man.GetAll().Where(I => I.UPC.UPCID == m_UPC.ObjectID && I.LocationID == m_Location.ObjectID && I.TagID != null && I.ExitDate == null);
                        if (null != items)
                        {
                            m_TaggedQuantity = items.Count();
                        }
                    }
                    else
                    {
                        m_TaggedQuantity = 0;
                    }
                }
                return m_TaggedQuantity.Value;
            }
        }

        #endregion
    }
}