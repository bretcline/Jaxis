using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class GetViewsResult : SuccessResult
    {
        public GetViewsResult(IEnumerable<IView> _views)
        {
            Views = (from v in _views select new GetViewResult(v)).ToList();
        }

        [DataMember]
        public List<GetViewResult> Views { get; set; }
    }
}
