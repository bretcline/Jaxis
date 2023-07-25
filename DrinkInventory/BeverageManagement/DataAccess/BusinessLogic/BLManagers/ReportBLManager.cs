using System;
using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public class ReportBLManager : BLManager<IReport, IBLReport>, IReportBLManager
    {

        #region IReportBLManager Members

        public System.Collections.Generic.IEnumerable<IBLReport> GetReportsByUser( Guid _SessionID )
        {
            IEnumerable<IBLReport> rc = new List<IBLReport>();
            try
            {
                rc = Report.All().ToList().Cast<IBLReport>().ToList();
            }
            catch
            {
                
            }
            return rc;
        }

        #endregion
    }
}