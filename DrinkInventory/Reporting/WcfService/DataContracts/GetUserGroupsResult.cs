using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    public class GetUserGroupsResult : SuccessResult
    {
        public GetUserGroupsResult()
        {
        }

        public GetUserGroupsResult(IEnumerable<IUserGroup> _userGroups)
        {
            UserGroups = _userGroups.Cast<UserGroup>( ).ToList( );
        }

        [DataMember]
        public List<UserGroup> UserGroups { get; set; }
    }
}
