using System.Data;
using System.Runtime.Serialization;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class GetDataTableResult : SuccessResult
    {
        public GetDataTableResult()
        {
        }

        public GetDataTableResult(DataTable _data)
        {
            Data = _data;
        }

        [DataMember]
        public DataTable Data { get; set; }
    }
}
