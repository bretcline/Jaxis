using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    public class GetUpcResult : SuccessResult
    {
        public GetUpcResult(IUPC _upc)
        {
            Upc = ( UPC ) _upc;
        }

        [DataMember] public UPC Upc { get; set; }
    }
}
