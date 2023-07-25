using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class GetAreaResult : ServiceResult
    {
        
        public GetAreaResult()
        {
        }

        public GetAreaResult(IArea _area)
        {
            Area = new Area(_area);
        }

        [DataMember]
        public Area Area { get; set; }
    }
}
