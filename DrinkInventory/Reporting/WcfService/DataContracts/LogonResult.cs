using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.Data.POCO;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    public class LogOnResult : SuccessResult
    {
        [DataMember]
        public Session Session { get; set; }
    }
}
