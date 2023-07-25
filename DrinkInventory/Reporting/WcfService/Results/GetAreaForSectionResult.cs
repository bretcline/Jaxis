using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    class GetAreaForSectionResult : SuccessResult
    {
        public GetAreaForSectionResult()
        {
        }

        public GetAreaForSectionResult(IArea _area)
        {
            Area = new Area(_area);
        }

        [DataMember]
        public Area Area { get; set; }
    }
}
