using System.Runtime.Serialization;

namespace kson
{
    [DataContract]
    public class ServiceParameters
    {
        public ServiceParameters(string[] _siteCustomers = null)
        {
            SiteCustomers = _siteCustomers ?? new string[0];
        }

        [DataMember(Name="siteCustomers")]
        public string[] SiteCustomers { get; private set; }
    }
}
