using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class GetVisibleWidgetIdsResult : SuccessResult
    {
        public GetVisibleWidgetIdsResult()
        {
        }

        public GetVisibleWidgetIdsResult(IEnumerable<Guid> _widgetIds)
        {
            WidgetIds = _widgetIds.ToList();
        }

        [DataMember]
        public List<Guid> WidgetIds { get; set; }
    }
}
