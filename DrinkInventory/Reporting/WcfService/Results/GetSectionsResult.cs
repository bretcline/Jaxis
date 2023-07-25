using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class GetSectionsResult : SuccessResult
    {
        public GetSectionsResult() { }

        public GetSectionsResult(IEnumerable<ISection> _businessSections)
        {
            Sections = (from bs in _businessSections select new Section(bs)).ToList();
        }

        [DataMember]
        public List<Section> Sections { get; set; }
    }
}
