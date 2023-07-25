using System.Runtime.Serialization;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class LogOnResult : SuccessResult
    {
        [DataMember]
        public Session Session { get; set; }
    }
}
