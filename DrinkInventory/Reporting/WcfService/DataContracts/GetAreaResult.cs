using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    public class GetAreaResult : ServiceResult
    {
        
        public GetAreaResult()
        {
        }

        public GetAreaResult(IArea _area)
        {
            Area = ( Area ) _area;
        }

        [DataMember]
        public Area Area { get; set; }
    }
}
