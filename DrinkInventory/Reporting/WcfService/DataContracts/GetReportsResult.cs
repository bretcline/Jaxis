using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    public class GetReportsResult : SuccessResult
    {
        public GetReportsResult(IEnumerable<IReport> _reports)
        {
            Reports = _reports.Cast<Report>( ).ToList( );
        }

        [DataMember]
        public List<Report> Reports { get; set; }
    }
}
