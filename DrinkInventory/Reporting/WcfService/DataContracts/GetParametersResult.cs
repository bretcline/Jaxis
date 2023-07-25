using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    public class GetParametersResult : SuccessResult
    {
        public GetParametersResult()
        {
        }

        public GetParametersResult(IEnumerable<IParameter> _parameters)
        {
            Parameters = _parameters.Cast<Parameter>( ).ToList( );
        }

        [DataMember]
        public List<Parameter> Parameters { get; set; }
    }
}
