using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class GetLocationResult : SuccessResult
    {
        public GetLocationResult()
        {
        }

        public GetLocationResult(IOrganization _location)
        {
            Location = new Organization(_location);
        }

        [DataMember]
        public Organization Location { get; set; }
    }
}
