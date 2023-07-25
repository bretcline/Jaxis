using System;
using System.Linq;
using Jaxis.Util.Log4Net;
using Jaxis.Inventory.Data.Reports;

namespace Jaxis.Inventory.Data
{
    public class ReportDataManager : DataManager<IReport, Report>, IReportDataManager, IDataManager
    {


        #region IDataManager<IReport> Members


        public IQueryable<IReport> GetAll( )
        {
            return Report.All( );
        }

        public IReport Get( Guid ID )
        {
            IReport rc = null;
            Log.Time( "GetReport", LogType.Debug, true, ( ) => { rc = Report.GetByID( ID ); } );
            return rc;
        }

        #endregion
    }

    public class PourReportDataManager : DataManager<IRPourTotals, rptPourTotal>, IPourReportDataManager, IDataManager
    {


        #region IDataManager<IReport> Members


        public IQueryable<IRPourTotals> GetAll()
        {
            return rptPourTotal.All();
        }

        public IRPourTotals Get(Guid ID)
        {
            IRPourTotals rc = null;
            //Log.Time("GetReport", LogType.Debug, true, () => { rc = rptPourTotal.GetByID(ID); });
            return rc;
        }

        #endregion
    }

    public class TagPourReportDataManager : DataManager<IRTagPourTotals, rptTagPourTotal>, ITagPourReportDataManager, IDataManager
    {


        #region IDataManager<IReport> Members


        public IQueryable<IRTagPourTotals> GetAll()
        {
            return rptTagPourTotal.All();
        }

        public IRTagPourTotals Get(Guid ID)
        {
            IRTagPourTotals rc = null;
            //Log.Time("GetReport", LogType.Debug, true, () => { rc = rptPourTotal.GetByID(ID); });
            return rc;
        }

        #endregion
    }

}