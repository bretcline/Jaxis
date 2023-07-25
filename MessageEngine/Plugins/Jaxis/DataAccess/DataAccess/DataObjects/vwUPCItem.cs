using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;
using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the mobUPCs table in the BevMetMobile Database.
    /// </summary>
    public partial class vwUPCItem : IUPCItemView, IBLUPCItem, IUIUPCItem, IUIUPCItemShort
    {
        public IStandardNozzle Nozzle { get; set; }

        public IEnumerable<IUPCItem> GetAll( )
        {
            return All( );
        }

        public override string ToString( )
        {
            return Name;
            //return string.Format( "{0} - {1}", Name, ItemNumber );
        }

        public ISizeType SizeType
        {
            get
            {
                return Jaxis.Inventory.Data.SizeType.GetByID( this.SizeTypeID );
            }
            set { SizeTypeID = value.ObjectID; }
        }

        protected ICategory m_Category = null;
        public ICategory Category 
        {
            get
            {
                if( null == m_Category )
                {
                    m_Category = Jaxis.Inventory.Data.Category.GetByID( SubCategoryItemID );
                }
                return m_Category;
            }
            set
            {
                m_Category = value;
                CategoryID = value.ObjectID;
            }
        }

        #region IBLUPCItem Members


        IBLCategory IBLUPCItem.Category
        {
            get
            {
                return Category as IBLCategory;
            }
            set
            {
                CategoryID = value.ObjectID;
            }
        }

        IBLSizeType IBLUPCItem.SizeType
        {
            get
            {
                return SizeType as IBLSizeType;
            }
            set
            {
                SizeTypeID = value.ObjectID;
            }
        }


        IBLStandardNozzle IBLUPCItem.Nozzle
        {
            get
            {
                return this.Nozzle as IBLStandardNozzle;
            }
            set
            {
                Nozzle = value;
            }
        }



        IBLStandardNozzle IUIUPCItem.Nozzle
        {
            get
            {
                return this.Nozzle as IBLStandardNozzle;
            }
            set { Nozzle = value; }
        }
        #endregion

        //#region IUIUPCItem Members


        //IUICategory IUIUPCItem.Category
        //{
        //    get
        //    {
        //        return this.Category as IUICategory;
        //    }
        //}

        //#endregion



        //public IBLStandardPrice Price
        //{
        //    get { return null; }
        //    set { int i = 0; }
        //}


        //IStandardPrice IUPCItem.Price
        //{
        //    get { return null; }
        //    set { int i = 0; }
        //}

        bool IUPCItem.AllowHalfPour { get; set; }

        public Guid? ChildUPCID { get; set; }
        public int? BottleCount { get; set; }
    }
}