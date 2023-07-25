using System;
using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the mobTags table in the BevMetMobile Database.
    /// </summary>
    public partial class Tag : IBLTag, IUITag
    {
        //private IBLInventory m_CurrentInventory = null;
        //public IBLInventory CurrentInventory
        //{
        //    get
        //    {
        //        if (null == m_CurrentInventory)
        //        {
        //            m_CurrentInventory = BLManagerFactory.Get().ManageInventory().GetInventoryByTag(this.TagID);
        //            //if (null == m_CurrentInventory)
        //            //{
        //            //    BLManagerFactory.Get().ManageInventory().TagInventory(this.UPC, this, this.CurrentLocation);
        //            //    m_CurrentInventory = BLManagerFactory.Get().ManageInventory().GetInventoryByTag(this.TagID);
        //            //}
        //        }
        //        //if (null == m_CurrentInventory)
        //        //{
        //        //    m_CurrentInventory = BLManagerFactory.Get().ManageInventory().Create();
        //        //    m_CurrentInventory.TagID = this.TagID;
        //        //    m_CurrentInventory.LocationID = this.LocationID;
        //        //    m_CurrentInventory.TagDate = m_CurrentInventory.EnterDate = DateTime.Now;
        //        //    m_CurrentInventory.UPCID = (this.UPCID.HasValue) ? this.UPCID.Value : Guid.Empty;
        //        //    m_CurrentInventory.Cost = this.UPC.Price.SinglePrice;
        //        //}
        //        return m_CurrentInventory;
        //    }
        //}

        partial void OnSaving()
        {
            var inventory = BLManagerFactory.Get().ManageInventory().GetInventoryByTag(this.TagID);
            if (null != inventory)
            {
                inventory.ExitReason = (int?) this.ExitReason;
                inventory.Memo = this.Memo;
                inventory.LocationID = this.LocationID;
                inventory.TagID = this.TagID;
                inventory.Amount = (this.Quantity.HasValue) ? this.Quantity.Value : this.UPC.Size;
                BLManagerFactory.Get().ManageInventory().Save(inventory);
            }
            //var inventory = this.Inventories.Where(I => I.ExitDate == null).ToList();
            //foreach (var item in inventory)
            //{
            //    item.TagID = this.TagID;
            //    item.LocationID = this.LocationID;
            //    item.Save();
            //}
        }

        public IEnumerable<ITag> GetAll( )
        {
            return All( );
        }

        public override string ToString( )
        {
            return TagNumber;
        }

        #region ITag Members



        public IBLUPCItem UPC
        {
            get
            {
                var inv = Inventories.Where(i => i.ExitDate == null).FirstOrDefault();
                return (null != inv ) ? inv.UPC : BLManagerFactory.Get().ManageUPCs().Get(Guid.Empty);
            }
            //set
            //{
            //    UPCID = ( null == value ) ? Guid.Empty : value.ObjectID;
            //}
        }
        
        public IBLLocation CurrentLocation
        {
            get
            {
                return LocationsItem.FirstOrDefault();
            }
            set
            {
                LocationID = ( null == value ) ? Guid.Empty : value.ObjectID;
            }
        }

        //public IBLEvent CurrentEvent
        //{
        //    get { return EventsItem.FirstOrDefault(); }
        //    set { EventID = ( null == value ) ? (Guid?)null : value.ObjectID; }
        //}

        #endregion

        #region IBLTag Members

        //IBLCart IBLTag.Cart
        //{
        //    get
        //    {
        //        throw new NotImplementedException( );
        //    }
        //    set
        //    {
        //        throw new NotImplementedException( );
        //    }
        //}

        IBLLocation IBLTag.CurrentLocation
        {
            get { return CurrentLocation as IBLLocation; }
            set { CurrentLocation = value; }

        }

        //IBLEvent IBLTag.CurrentEvent
        //{
        //    get { return CurrentEvent as IBLEvent; }
        //    set { CurrentEvent = value; }
        //}

        //IBLUPCItem IBLTag.UPC
        //{
        //    get { return UPC as IBLUPCItem; }
        //    set { UPC = value; }
        //}

        #endregion

        public IStandardNozzle Nozzle
        {
            get
            {
                return this.StandardNozzlesItem.FirstOrDefault( );
            }
            set
            {
                StandardNozzleID = value.StandardNozzleID;
            }
        }




        IBLStandardNozzle IBLTag.Nozzle
        {
            get
            {
                return this.StandardNozzlesItem.FirstOrDefault();
            }
            set
            {
                StandardNozzleID = value.StandardNozzleID;
            }
        }



        protected double? m_Quantity;
        public double? Quantity
        {
            get
            {
                if( !m_Quantity.HasValue )
                {
                    var inventory = this.Inventories.Where(i => i.ExitDate == null).FirstOrDefault();
                    if (null != inventory)
                    {
                        m_Quantity = inventory.Amount;
                    }
                }
                return m_Quantity;
            }
            set
            {
                var inventory = this.Inventories.Where(i => i.ExitDate == null).FirstOrDefault();

                if (null != inventory && value.HasValue)
                {
                    inventory.Amount = value.Value;
                }
                m_Quantity = value.Value;
            }
        }

        public IBLInventory CurrentInventory
        {
            get
            {
                return BLManagerFactory.Get().ManageInventory().GetInventoryByTag(this.TagID);
            }
        }

        IInventory ITag.CurrentInventory
        {
            get
            {
                return BLManagerFactory.Get().ManageInventory().GetInventoryByTag(this.TagID);
            }
        }

        public ExitReasons ExitReason { get; set; }
        public string Memo { get; set; }
    }
}