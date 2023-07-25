using System.Runtime.Serialization;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    public class ExceptionResult : ServiceResult
    {
        [DataMember]
        public string Message { get; set; }
    }
}
