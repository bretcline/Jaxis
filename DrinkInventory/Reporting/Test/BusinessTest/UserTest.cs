using System.Linq;
using NUnit.Framework;

namespace Jaxis.DrinkInventory.Reporting.Test.BusinessTest
{
    [TestFixture]
    public class UserTest : BaseBusinessTest
    {
        [Test]
        public void ClearUsersGroups()
        {
            var userName = TestString();
            var password = TestString();
            var user = BusinessProvider.AddUser(SessionId, userName, password);
            var groupName = TestString();
            var group = BusinessProvider.NewUserGroup(SessionId);
            // KWC Add/Clear UserGroup are no longer in the app; this test may be unnecessary.
            //user.AddUserGroup(group.UserGroupId);
            //Assert.AreEqual(1, user.UserGroupIds.Count());
            //user.ClearUserGroups();
            //Assert.AreEqual(0, user.UserGroupIds.Count());
        }
    }
}
