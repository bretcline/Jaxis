using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class GetUserGroupsResult : SuccessResult
    {
        public GetUserGroupsResult()
        {
        }

        public GetUserGroupsResult(IEnumerable<IUserGroup> _userGroups)
        {
            UserGroups = (from g in _userGroups select new UserGroup(g)).ToList();
        }

        [DataMember]
        public List<UserGroup> UserGroups { get; set; }
    }
}
