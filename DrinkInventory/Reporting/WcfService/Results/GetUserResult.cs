using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class GetUserResult : SuccessResult
    {
        public GetUserResult()
        {
        }

        public GetUserResult(IUser _user)
        {
            User = new User(_user);
        }

        [DataMember]
        public User User { get; set; }
    }
}
