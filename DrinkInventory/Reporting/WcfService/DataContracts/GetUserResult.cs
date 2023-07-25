using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    public class GetUserResult : SuccessResult
    {
        public GetUserResult()
        {
        }

        public GetUserResult(IUser _user)
        {
            User = ( User ) _user;
        }

        [DataMember]
        public User User { get; set; }
    }
}
