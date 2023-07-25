using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public partial class Report : IReport, IBLReport
    {

        #region IDataObject<IReport> Members


        public IEnumerable<IReport> GetAll( )
        {
            return All();
        }

        #endregion

        #region IReport Members


        public IList<IReportParameter> Parameters
        {
            get { return this.ReportParameters.ToList( ).Cast<IReportParameter>().ToList(); }
        }

        #endregion

        #region IBLReport Members

        IList<IBLReportParameter> IBLReport.Parameters
        {
            get { return this.ReportParameters.ToList( ).Cast<IBLReportParameter>( ).ToList( ); }
        }

        #endregion
    }


    public partial class ReportParameter : IReportParameter, IBLReportParameter
    {
        

    }
}