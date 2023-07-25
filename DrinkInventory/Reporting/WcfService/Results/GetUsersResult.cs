using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class GetUsersResult : SuccessResult
    {
        public GetUsersResult()
        {
        }
        
        public GetUsersResult(IEnumerable<IUser> _users)
        {
            Users = (from u in _users select new User(u)).ToList();
        }

        [DataMember]
        public List<User> Users { get; set; }
    }
}
