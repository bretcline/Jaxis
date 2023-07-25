using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data.DataAccess.DataObjects;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.Reports;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the Category table in the BevMetMobile Database.
    /// </summary>
    public partial class Category : ICategory, IBLCategory, IUICategory
    {

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

        public IEnumerable<ICategory> GetAll( )
        {
            return All( );
        }
        public override string ToString( )
        {
            return Name;
        }


        IBLStandardNozzle IBLCategory.Nozzle
        {
            get
            {
                return Nozzle as IBLStandardNozzle;
            }
            set
            {
                Nozzle = value as IStandardNozzle;
            }
        }

    }

    public partial class rptPourTotal : IRPourTotals
    {

    }

    public partial class rptTagPourTotal : IRTagPourTotals
    {

    }
}