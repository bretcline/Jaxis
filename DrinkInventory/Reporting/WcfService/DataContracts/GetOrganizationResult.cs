using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    public class GetOrganizationResult : SuccessResult
    {
        public GetOrganizationResult()
        {
        }

        public GetOrganizationResult(IOrganization _organization)
        {
            Organization = ( Organization ) _organization;
        }

        [DataMember]
        public Organization Organization { get; set; }
    }
}
