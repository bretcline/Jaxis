using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    public class GetOrganizationsResult : SuccessResult
    {
        public GetOrganizationsResult()
        {
        }

        public GetOrganizationsResult(IEnumerable<IOrganization> _organizations)
        {
            Organizations = _organizations.Cast<Organization>( ).ToList( );
        }

        [DataMember]
        protected List<Organization> Organizations { get; set; }
    }
}
