using System;
using System.Linq;
using Jaxis.Inventory.Data;
using Jaxis.Util.Log4Net;

namespace Jaxis.Inventory.Data
{
    public class StandardNozzleDataManager : DataManager<IStandardNozzle, StandardNozzle>, IStandardNozzleDataManager, IDataManager
    {
        #region IDataManager<IReport> Members


        public IQueryable<IStandardNozzle> GetAll()
        {
            return StandardNozzle.All();
        }

        public IStandardNozzle Get(Guid ID)
        {
            IStandardNozzle rc = null;
            Log.Time("GetStandardNozzle", LogType.Debug, true, () => { rc = StandardNozzle.GetByID(ID); });
            return rc;
        }

        #endregion
    }

    public class StandardPourDataManager : DataManager<IStandardPour, StandardPour>, IStandardPourDataManager, IDataManager
    {
        #region IDataManager<IReport> Members


        public IQueryable<IStandardPour> GetAll( )
        {
            return StandardPour.All( );
        }

        public IStandardPour Get( Guid ID )
        {
            IStandardPour rc = null;
            Log.Time( "GetStandardPour", LogType.Debug, true, ( ) => { rc = StandardPour.GetByID( ID ); } );
            return rc;
        }

        #endregion
    }


    public class StandardPriceDataManager : DataManager<IStandardPrice, StandardPrice>, IStandardPriceDataManager, IDataManager
    {
        #region IDataManager<IReport> Members


        public IQueryable<IStandardPrice> GetAll()
        {
            return StandardPrice.All();
        }

        public IStandardPrice Get(Guid ID)
        {
            IStandardPrice rc = null;
            Log.Time("GetStandardPrice", LogType.Debug, true, () => { rc = StandardPrice.GetByID(ID); });
            return rc;
        }

        #endregion
    }

}