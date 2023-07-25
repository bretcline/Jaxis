using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    public class GetReportResult : SuccessResult
    {
        public GetReportResult() { }

        public GetReportResult(IReport _report)
        {
            Report = ( Report ) _report;
        }

        [DataMember]
        public Report Report { get; set; }

    }
}
