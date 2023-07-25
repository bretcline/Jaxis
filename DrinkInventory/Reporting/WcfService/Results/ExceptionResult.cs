using System.Runtime.Serialization;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class ExceptionResult : ServiceResult
    {
        [DataMember]
        public string Message { get; set; }
    }
}
