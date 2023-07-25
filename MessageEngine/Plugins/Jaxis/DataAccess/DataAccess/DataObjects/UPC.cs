using System;
using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public partial class UPC : IBLUPCItem, IUIUPCItem, IUIUPCItemShort
    {
        public IEnumerable<IUPCItem> GetAll( )
        {
            return All( );
        }

        public override string ToString( )
        {
            return Name;
            //return string.Format( "{0} - {1}", Name, ItemNumber );
        }

        partial void OnCreated( )
        {
            PourModifier = 1;
        }

        public ISizeType SizeType
        {
            get
            {
                return Jaxis.Inventory.Data.SizeType.GetByID( this.SizeTypeID );
            }
            set { SizeTypeID = value.ObjectID; }
        }

        public string  CategoryName
        {
            get { return Category.Name; }
        }

        protected ICategory m_Category = null;
        public ICategory Category
        {
            get
            {
                if( null == m_Category )
                {
                    m_Category = Jaxis.Inventory.Data.Category.GetByID( this.CategoryID );
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

        public IStandardNozzle Nozzle
        {
            get
            {
                return this.StandardNozzlesItem.FirstOrDefault();
            }
            set
            {
                if (value == null)
                {
                    StandardNozzleID = null;
                }
                else
                {
                    StandardNozzleID = value.StandardNozzleID;
                }
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
                if (value == null)
                {
                    StandardNozzleID = null;
                }
                else
                {
                    StandardNozzleID = value.StandardNozzleID;
                }
            }
        }

        IBLStandardNozzle IUIUPCItem.Nozzle
        {
            get
            {
                return this.Nozzle as IBLStandardNozzle;
            }
            set
            {
                StandardNozzleID = value.StandardNozzleID;
            }
        }

        #endregion

        public string Manufacturer
        {
            get
            {
                var man = this.ManufacturersItem.FirstOrDefault();
                return (null != man) ? man.Name : string.Empty;
            }
        }

        public string QualityName { get; set; }
    }
}