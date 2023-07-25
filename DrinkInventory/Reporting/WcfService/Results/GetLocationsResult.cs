using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class GetLocationsResult : SuccessResult
    {
        public GetLocationsResult()
        {
        }

        public GetLocationsResult(IEnumerable<IOrganization> _locations)
        {
            var stupidSubsonic = (from o in _locations select o).ToList();
            Locations = (from o in stupidSubsonic select new Organization(o)).ToList();
        }

        [DataMember]
        protected List<Organization> Locations { get; set; }
    }
}
