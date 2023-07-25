using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    public class GetAreasResult : SuccessResult
    {
        public GetAreasResult()
        {
        }

        public GetAreasResult(IEnumerable<IArea> _businessAreas)
        {
            Areas = _businessAreas.Cast<Area>( ).ToList( );
        }

        [DataMember]
        public List<Area> Areas { get; set; }
    }
}
