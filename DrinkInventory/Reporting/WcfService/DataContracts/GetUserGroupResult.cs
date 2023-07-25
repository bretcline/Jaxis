using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    public class GetUserGroupResult : SuccessResult
    {
        public GetUserGroupResult()
        {
        }

        public GetUserGroupResult(IUserGroup _userGroup)
        {
            UserGroup = ( UserGroup ) _userGroup;
        }

        [DataMember]
        public UserGroup UserGroup { get; set; }
    }
}
