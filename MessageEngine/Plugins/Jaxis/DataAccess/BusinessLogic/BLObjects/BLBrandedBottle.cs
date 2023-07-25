using System;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public class BLBrandedBottle : IBLBrandedBottle, IUIBrandedBottle, IBLAssociatedBottle, IUIAssociatedBottle
    {
        public BLBrandedBottle( )
        {
            BottleID = Guid.NewGuid( );
            Nozzle = BLManagerFactory.Get().ManageStandardNozzles().Create();
            NewInventory = false;
            BottleCount = 1;
        }

        public Guid BottleID { get; set; }
        public IBLTag Tag { get; set; }
        public string TagNumber { get { return (null == Tag) ? string.Empty : Tag.TagNumber; } }
        public IBLUPCItem UPC { get; set; }
        public string UPCName { get { return (null == UPC) ? string.Empty : UPC.Name; } }

        private double? m_Quantity;
        public double? Quantity
        {
            get
            {
                if (null != Tag)
                    m_Quantity = Tag.Quantity;
                return m_Quantity;
            }
            set
            {
                if (null != Tag)
                    Tag.Quantity = value;
                m_Quantity = value;
            }
        }

        public IBLStandardNozzle Nozzle { get; set; }

        //public string Location
        //{
        //    get
        //    {
        //        string rc = string.Empty;
        //        if (null != Tag)
        //        {
        //            rc = Tag.CurrentLocation.Name;
        //        }
        //        else if (null != CurrentLocation)
        //        {
        //            rc = CurrentLocation.Name;
        //        }
        //        return rc;
        //    }
        //}

        private IBLLocation m_FromLocation;
        public IBLLocation FromLocation 
        { 
            get { return m_FromLocation; }
            set { m_FromLocation = value; if( null != Tag ) Tag.LocationID = value.LocationID;} 
        }
        private IBLLocation m_ToLocation;
        public IBLLocation ToLocation
        {
            get { return m_ToLocation; }
            set { m_ToLocation = value; if (null != Tag) Tag.LocationID = value.LocationID; }
        }
        
        //private IBLLocation m_CurrentLocation;
        //public IBLLocation CurrentLocation
        //{
        //    get
        //    {
        //        return ( null != Tag ) ? Tag.CurrentLocation : m_CurrentLocation;
        //    } 
        //    set
        //    {
        //        m_CurrentLocation = value;
        //        if( null != Tag )
        //        {
        //            Tag.LocationID = value.LocationID;
        //        }
        //    }
        //}

        public int BottleCount { get; set; }

        public bool NewInventory { get; set; }

        private decimal m_Cost = 0.0m;
        public decimal Cost
        {
            get
            {
                if( null != Tag && m_Cost == 0.0m)
                {
                    var inventory = BLManagerFactory.Get().ManageInventory().GetInventoryByTag(Tag.TagID);
                    if (null != inventory)
                    {
                        m_Cost = inventory.Cost;
                    }
                    else if (null != UPC && UPC.UnitCost.HasValue )
                    {
                        m_Cost = UPC.UnitCost.Value;
                    }
                }
                return m_Cost;
            }
            set { m_Cost = value; }
        }
    }


    public class PourPoint : IUIPourPoint
    {
        public Guid TagID { get; set; }
        public string TagNumber { get; set; }
        public string Category { get; set; }
        public string UPCName { get; set; }
        public string Location { get; set; }

        private double m_Volume = 0.0;
        public double Volume
        {
            get { return m_Volume; }
            set 
            {
                m_Volume = BLManagerFactory.Get().ConvertPourToUnits(value);
                Units = BLManagerFactory.Get().GetDefaultSizeType().Abbreviation;
            }
        }
        public string Units { get; set; }
        public DateTime PourTime { get; set; }
    }
}