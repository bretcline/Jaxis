using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class GetAreasResult : SuccessResult
    {
        public GetAreasResult()
        {
        }

        public GetAreasResult(IEnumerable<IArea> _businessAreas)
        {
            Areas = (from bs in _businessAreas select new Area(bs)).ToList();
        }

        [DataMember]
        public List<Area> Areas { get; set; }
    }
}
