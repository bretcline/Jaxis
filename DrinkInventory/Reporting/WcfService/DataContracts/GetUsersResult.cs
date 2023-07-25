using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    public class GetUsersResult : SuccessResult
    {
        public GetUsersResult()
        {
        }
        
        public GetUsersResult(IEnumerable<IUser> _users)
        {
            Users = _users.Cast<User>( ).ToList( );
        }

        [DataMember]
        public List<User> Users { get; set; }
    }
}
