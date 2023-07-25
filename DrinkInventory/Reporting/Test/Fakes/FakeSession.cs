using System;
using System.Linq;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeSession : FakeDomainObject, ISession
    {
        public FakeSession()
        {
            SessionId = Guid.NewGuid();
        }

        public Guid SessionId { get; set; }
        public IUser User
        {
            get
            {
                return (from u in FakeUserDataManager.Objects where u.UserId == UserId select u).FirstOrDefault();
            }
            set
            {
                UserId = value.UserId;
            }
        }

        public DateTime ExpirationTime { get; set; }
        public Guid UserId { get; set; }

        public override Guid Id
        {
            get { return SessionId; }
            set { SessionId = value; }
        }

        #region ISession Members


        public string UserName
        {
            get
            {
                throw new NotImplementedException( );
            }
            set
            {
                throw new NotImplementedException( );
            }
        }

        #endregion
    }
}
