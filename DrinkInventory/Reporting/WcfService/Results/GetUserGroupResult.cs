using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class GetUserGroupResult : SuccessResult
    {
        public GetUserGroupResult()
        {
        }

        public GetUserGroupResult(IUserGroup _userGroup)
        {
            UserGroup = new UserGroup(_userGroup);
        }

        [DataMember]
        public UserGroup UserGroup { get; set; }
    }
}
