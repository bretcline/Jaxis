using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    public class GetUpcsResult : SuccessResult
    {
        public GetUpcsResult()
        {
        }

        public GetUpcsResult(IEnumerable<IUPC> _upcs)
        {
            Upcs = _upcs.Cast<UPC>( ).ToList( );
        }

        [DataMember]
        public List<UPC> Upcs { get; set; }
    }
}
