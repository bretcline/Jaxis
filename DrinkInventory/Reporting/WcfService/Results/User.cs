using System;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class User
    {
        public User()
        {
        }

        public User(IUser _user)
        {
            Id = _user.UserId;
            UserName = _user.UserName;
            Password = _user.Password;
            ModifiedOn = _user.ModifiedOn;
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public DateTime ModifiedOn { get; set; }

        public void Update(IUser _businessUser)
        {
            _businessUser.UserName = UserName;
            _businessUser.Password = Password;
        }
    }
}
