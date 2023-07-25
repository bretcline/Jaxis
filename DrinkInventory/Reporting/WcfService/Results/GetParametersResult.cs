using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class GetParametersResult : SuccessResult
    {
        public GetParametersResult()
        {
        }

        public GetParametersResult(IEnumerable<IParameter> _parameters)
        {
            Parameters = (from p in _parameters select new Parameter(p)).ToList();
        }

        [DataMember]
        public List<Parameter> Parameters { get; set; }
    }
}
