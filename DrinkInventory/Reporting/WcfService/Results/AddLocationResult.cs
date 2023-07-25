using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    class AddLocationResult : SuccessResult
    {
        public AddLocationResult()
        {
        }

        public AddLocationResult(IOrganization _location)
        {
            Location = new Organization(_location);
        }

        [DataMember]
        public Organization Location { get; set; }
    }
}
