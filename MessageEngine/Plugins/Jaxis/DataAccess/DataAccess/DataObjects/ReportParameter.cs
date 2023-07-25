using System.Collections.Generic;

namespace Jaxis.Inventory.Data
{
    public partial class ReportParameter : IReportParameter
    {

        #region IDataObject<IReportParameter> Members


        public IEnumerable<IReportParameter> GetAll( )
        {
            return All( );
        }

        #endregion
       
    }
}