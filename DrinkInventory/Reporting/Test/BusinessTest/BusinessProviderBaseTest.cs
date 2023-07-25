using System;
using System.Linq;
using Jaxis.DrinkInventory.Reporting.Business;
using Jaxis.DrinkInventory.Reporting.BusinessInterfaces;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using Jaxis.DrinkInventory.Reporting.Test.Fakes;
using Jaxis.DrinkInventory.Reporting.Tools;
using Jaxis.Util.Log4Net;
using NUnit.Framework;

namespace Jaxis.DrinkInventory.Reporting.Test.BusinessTest
{
    public abstract class BusinessProviderBaseTest
    {
        protected abstract IDataManagerFactory CreateFactory();
        protected abstract void InitBusinessProvider();

        protected IBusinessProvider Business;
        protected FakeClock TestClock;
        protected IUser TestUser;
        protected IUser OtherUser;
        protected IOrganization TestOrganization;
        protected IUserGroup TestUserGroup;
        protected IUserGroupXOrganization TestUserGroupXOrganization;
        protected IUserGroupMembership TestUserGroupMembership;

        [SetUp]
        public void SetUp()
        {
            InitBusinessProvider();
            Business = new BusinessProvider();
            TestOrganization = CreateOrganization(null, TestHelpers.TestString(), TestHelpers.TestString());
            TestUserGroup = CreateUserGroup();
            TestUserGroupXOrganization = CreateUserGroupXOrganization(TestUserGroup.Id, TestOrganization.Id);
            Log.Info(string.Format("Created UserGroupXOrganization {0}", TestUserGroupXOrganization.Id));
            TestUser = CreateUser();
            TestUserGroupMembership = CreateUserGroupMembership(TestUserGroup, TestUser);
            OtherUser = CreateUser();
            Services.Clock = TestClock = new FakeClock();
        }
        
        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Status != TestStatus.Failed)
            {
                using (var factory = CreateFactory())
                {
                    factory.Manage<IUserGroupXOrganization>().Delete(TestUserGroupXOrganization.Id);
                    Log.Info(string.Format("Deleted UserGroupXOrganization {0}", TestUserGroupXOrganization.Id));
                    factory.Manage<IUserGroupMembership>().Delete(TestUserGroupMembership.Id);
                }

                using (var factory = CreateFactory())
                {
                    Log.Info(string.Format("TestUser.Id = {0}", TestUser.Id));
                    factory.Manage<IUser>().Delete(TestUser.Id);
                    Log.Info(string.Format("OtherUser.Id = {0}", OtherUser.Id));
                    factory.Manage<IUser>().Delete(OtherUser.Id);

                    factory.Manage<IUserGroup>().Delete(TestUserGroup.Id);
                }

                using (var factory = CreateFactory())
                {
                    factory.Manage<IOrganization>().Delete(TestOrganization.Id);
                }
            }
        }

        [Test]
        public void DoNothing()
        {
            
        }

        private IUser CreateUser()
        {
            using (var factory = CreateFactory())
            {
                var user = factory.Manage<IUser>().Create();
                user.Password = TestHelpers.TestString();
                user.UserName = TestHelpers.TestString();
                user.OrganizationId = TestOrganization.Id;
                factory.Manage<IUser>().Save(user);
                return user;
            }
        }

        [Test]
        public void LogOn()
        {
            var session = Business.LogOn(TestUser.UserName, TestUser.Password);
            Assert.IsNotNull(session);
            Business.LogOff(session.SessionId);
        }

        [Test]
        public void GetUserGroups()
        {
            var userGroup = CreateUserGroup();

            var session = OpenSession();
            var userGroups = from g in Business.GetUserGroups(session.SessionId) 
                where g.UserGroupId == userGroup.Id select g;
            
            Assert.AreEqual(1, userGroups.Count());
            CloseSession(session);
            DeleteUserGroup(userGroup);
        }

        private void DeleteUserGroup(IUserGroup _userGroup)
        {
            using (var factory = CreateFactory())
            {
                factory.Manage<IUserGroup>().Delete(_userGroup.Id);
            }
        }

        private void CloseSession(ISession _session)
        {
            Business.LogOff(_session.Id);
        }

        [Test]
        public void GetAreas1()
        {
            var session = OpenSession();
            var areas = Business.GetAreas(session.SessionId);
            Assert.IsNotNull(areas);
            Assert.AreEqual(0, areas.ToList().Count);
            Business.LogOff(session.Id);
        }

        [Test]
        public void AddMembershipToUser()
        {
            Guid userGroupId;

            using (var factory = CreateFactory())
            {
                var userGroupManager = factory.Manage<IUserGroup>();
                var userGroup = userGroupManager.Create();
                userGroup.Name = "Test";
                userGroup.OrganizationId = TestOrganization.Id;
                userGroupManager.Save(userGroup);
                userGroupId = userGroup.UserGroupId;
            }

            var session = Business.LogOn(TestUser.UserName, TestUser.Password);
            Assert.IsNotNull(session);
            var user = Business.GetUser(session.SessionId, session.User.UserId);
            Assert.IsNotNull(user);
            var busUserGroup = Business.GetUserGroup(session.SessionId, userGroupId);
            //user.AddUserGroup(busUserGroup.UserGroupId);
            user.UserGroupIds.Add( busUserGroup.UserGroupId );
            busUserGroup.UserGroupId = userGroupId;
            Business.SaveUser(session.SessionId, user);

            using (var factory = CreateFactory())
            {
                var userGroupMemberships = from m in factory.Manage<IUserGroupMembership>().GetAll()
                                           where m.UserId == TestUser.UserId
                                           select m;

                Assert.IsNotNull(userGroupMemberships);
                var userGroup = userGroupMemberships.FirstOrDefault();
                Assert.IsNotNull(userGroup);
            }

            Business.DeleteUserGroup(session.Id, userGroupId);
            Business.LogOff(session.Id);
        }

        [Test]
        public void SaveWidgetIds()
        {
            var session = OpenSession();
            Assert.IsNotNull(session);
            var widgetIds = new System.Collections.Generic.List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            Business.SetVisibleWidgetIds(session.SessionId, widgetIds);
        }

        [Test]
        public void LogOnKnownUserGoodPassword()
        {
            var session = OpenSession();
            Assert.IsNotNull(session);
            Assert.IsNotNull(session.User);
            Assert.AreEqual(TestUser.UserName, session.User.UserName);
            CloseSession(session);
        }

        [Test]
        public void LogOnKnownUserBadPassword()
        {
            var session = Business.LogOn(TestUser.UserName, "xxxxxxx");
            Assert.IsNull(session);
        }

        [Test]
        public void LogOnUnknownUser()
        {
            var session = Business.LogOn("BadUser", "Doesn'tMatter");
            Assert.IsNull(session);
        }

        [Test]
        public void SessionIsValid()
        {
            var session = Business.LogOn(TestUser.UserName, TestUser.Password);
            Assert.IsTrue(Business.SessionIsValid(session.SessionId));
        }

        [Test]
        public void EmptySessionIdIsNotValid()
        {
            Assert.IsFalse(Business.SessionIsValid(Guid.Empty));
        }

        [Test]
        public void SessionExpires()
        {
            var session = Business.LogOn(TestUser.UserName, TestUser.Password);
            TestClock.Advance(new TimeSpan(1, 0, 1));
            Assert.IsFalse(Business.SessionIsValid(session.SessionId));
        }

        [Test]
        public void LogOff()
        {
            var session = Business.LogOn(TestUser.UserName, TestUser.Password);
            Assert.IsTrue(Business.SessionIsValid(session.SessionId));
            Business.LogOff(session.SessionId);
            Assert.IsFalse(Business.SessionIsValid(session.SessionId));
        }

        [Test]
        public void SessionDoesNotExpireIfActivity()
        {
            var session = Business.LogOn(TestUser.UserName, TestUser.Password);
            TestClock.Advance(new TimeSpan(0, 30, 0));
            Business.SessionActivity(session.SessionId);
            TestClock.Advance(new TimeSpan(0, 30, 0));
            Assert.IsTrue(Business.SessionIsValid(session.SessionId));
        }

        [Test]
        public void GetAreas()
        {
            var session = OpenSession();
            var sections = Business.GetAreas(session.SessionId);
            Assert.IsNotNull(sections);
            Business.LogOff(session.Id);
        }

        [Test]
        public void GetViews()
        {
            //var session = OpenSession();
            //var sectionId = Guid.Empty;
            //var views = Business.GetReports(session.SessionId, sectionId);
            //Assert.IsNotNull(views);
        }

        private ISession OpenSession()
        {
            var session = Business.LogOn(TestUser.UserName, TestUser.Password);
            return session;
        }

        [Test]
        public void NoAreaMembership()
        {
            using (var factory = CreateFactory())
            {
                var area = factory.Manage<IArea>().Create();
                area.Name = Guid.NewGuid().ToString();
                area.ShortName = Guid.NewGuid().ToString().Substring(0,30);
                area.Controller = Guid.NewGuid().ToString();
                factory.Manage<IArea>().Save(area);
            }

            var session = Business.LogOn(OtherUser.UserName, OtherUser.Password);
            var areas = Business.GetAreas(session.SessionId).ToList();
            Assert.AreEqual(0, areas.Count);
            Business.LogOff(session.Id);
        }

        [Test]
        public void OneAreaMembership()
        {
            var area = CreateArea(10, "Area");
            var userGroup = CreateUserGroup();
            var areaMembership = CreateAreaMembership(area, userGroup);
            var userGroupMembership = CreateUserGroupMembership(userGroup, TestUser);
            var session = OpenSession();
            var areas = Business.GetAreas(session.SessionId).ToList();
            Assert.AreEqual(1, areas.Count);
            CloseSession(session);
            DeleteUserGroupMembership(userGroupMembership);
            DeleteAreaMembership(areaMembership);
            DeleteUserGroup(userGroup);
            DeleteArea(area);
        }

        private void DeleteArea(IArea _area)
        {
            using (var factory = CreateFactory())
            {
                factory.Manage<IArea>().Delete(_area.Id);
            }
        }

        private void DeleteAreaMembership(IAreaMembership _areaMembership)
        {
            using (var factory = CreateFactory())
            {
                factory.Manage<IAreaMembership>().Delete(_areaMembership.Id);
            }
        }

        private void DeleteUserGroupMembership(IUserGroupMembership _userGroupMembership)
        {
            using (var factory = CreateFactory())
            {
                factory.Manage<IUserGroupMembership>().Delete(_userGroupMembership.Id);
            }
        }

        [Test]
        public void OrdersAreas()
        {
            var area2 = CreateArea(20, "Area2");
            var area1 = CreateArea(10, "Area1");
            var userGroup = CreateUserGroup();
            var areaMembership2 = CreateAreaMembership(area2, userGroup);
            var areaMembership1 = CreateAreaMembership(area1, userGroup);
            var userGroupMembership = CreateUserGroupMembership(userGroup, TestUser);
            var session = Business.LogOn(TestUser.UserName, TestUser.Password);
            
            var areas = Business.GetAreas(session.SessionId).ToList();
            
            Assert.AreEqual(2, areas.Count);
            Assert.AreEqual("Area1", areas[0].ShortName);
            Assert.AreEqual("Area2", areas[1].ShortName);
            
            CloseSession(session);
            DeleteUserGroupMembership(userGroupMembership);
            DeleteAreaMembership(areaMembership1);
            DeleteAreaMembership(areaMembership2);
            DeleteUserGroup(userGroup);
            DeleteArea(area1);
            DeleteArea(area2);
        }

        [Test]
        public void OrdersSections()
        {
            //var area = CreateArea(10, "Area");
            //var userGroup = CreateUserGroup();
            //CreateAreaMembership(area, userGroup);
            //CreateUserGroupMembership(userGroup, TestUser);
            //var session = Business.LogOn(TestUser.UserName, TestUser.Password);
            //CreateTestSection(area, "Section2", 20);
            //CreateTestSection(area, "Section1", 10);
            //var sections = Business.GetSections(session.SessionId, area.AreaId).ToList();
            //Assert.AreEqual("Section1", sections[0].ShortName);
            //Assert.AreEqual("Section2", sections[1].ShortName);
        }

        [Test]
        public void OrdersViews()
        {
            //var area = CreateArea(10, "Area");
            //var userGroup = CreateUserGroup();
            //CreateAreaMembership(area, userGroup);
            //CreateUserGroupMembership(userGroup, TestUser);
            //var session = Business.LogOn(TestUser.UserName, TestUser.Password);
            //var section = CreateTestSection(area, "Section", 10);
            //CreateTestView(section, "View2", 20);
            //CreateTestView(section, "View1", 10);
            //var views = Business.GetReports(session.SessionId, section.SectionId).ToList();
            //Assert.AreEqual("View1", views[0].ShortName);
            //Assert.AreEqual("View2", views[1].ShortName);
        }

        [Test]
        public void GetRootOrganization()
        {
            var rootOrganization = CreateOrganization(null, "TEST", "Test Parent");
            var session = Business.LogOn(TestUser.UserName, TestUser.Password);
            var organization = Business.GetOrganization(session.SessionId, rootOrganization.OrganizationId);
            Assert.IsNotNull(organization);
            Business.LogOff(session.Id);
        }

        [Test]
        public void GetOrganizationChildren()
        {
            var session = OpenSession();
            var org1 = CreateOrganization(TestOrganization.OrganizationId, "Org1", "Org1");
            var org2 = CreateOrganization(TestOrganization.OrganizationId, "Org2", "Org2");
            var organizations = Business.GetOrganizations(session.SessionId, TestOrganization.OrganizationId);
            Assert.AreEqual(2, organizations.ToList().Count);
            DeleteOrganization(org1);
            DeleteOrganization(org2);
            Business.LogOff(session.Id);
        }

        private IOrganization CreateOrganization(Guid? _parentId, string _shortName, string _name)
        {
            using (var factory = CreateFactory())
            {
                var organization = factory.Manage<IOrganization>().Create();
                organization.ShortName = _shortName;
                organization.Name = _name;
                organization.ParentId = _parentId;
                factory.Manage<IOrganization>().Save(organization);
                return organization;
            }
        }

        //private ISection CreateTestSection(IArea _area, string _shortName, int _order)
        //{
        //    using (var factory = CreateFactory())
        //    {
        //        var section = factory.Manage<ISection>().Create();
        //        section.AreaId = _area.AreaId;
        //        section.ShortName = _shortName;
        //        section.Order = _order;
        //        section.Name = Guid.NewGuid().ToString();
        //        factory.Manage<ISection>().Save(section);
        //        return section;
        //    }
        //}

        //private void CreateTestView(ISection _section, string _shortName, int _order)
        //{
        //    using (var factory = CreateFactory())
        //    {
        //        var view = factory.Manage<IReport>().Create();
        //        view.SectionId = _section.SectionId;
        //        view.ShortName = _shortName;
        //        view.Name = Guid.NewGuid().ToString();
        //        view.Type = Guid.NewGuid().ToString().Substring(0, 20);
        //        view.SelectCommand = Guid.NewGuid().ToString();
        //        view.ReportClassName = Guid.NewGuid().ToString();
        //        view.Order = _order;
        //        factory.Manage<IReport>().Save(view);
        //    }
        //}

        private IUserGroupMembership CreateUserGroupMembership(IUserGroup _userGroup, IUser _user)
        {
            using (var factory = CreateFactory())
            {
                var userGroupMembership = factory.Manage<IUserGroupMembership>().Create();
                userGroupMembership.UserId = _user.UserId;
                userGroupMembership.UserGroupId = _userGroup.UserGroupId;
                factory.Manage<IUserGroupMembership>().Save(userGroupMembership);
                return userGroupMembership;
            }
        }

        private IAreaMembership CreateAreaMembership(IArea _area, IUserGroup _userGroup)
        {
            using (var factory = CreateFactory())
            {
                var areaMembership = factory.Manage<IAreaMembership>().Create();
                areaMembership.AreaId = _area.AreaId;
                areaMembership.UserGroupId = _userGroup.UserGroupId;
                factory.Manage<IAreaMembership>().Save(areaMembership);
                return areaMembership;
            }
        }

        private IUserGroup CreateUserGroup()
        {
            using (var factory = CreateFactory())
            {
                var userGroup = factory.Manage<IUserGroup>().Create();
                userGroup.Name = TestHelpers.TestString();
                userGroup.OrganizationId = TestOrganization.Id;
                factory.Manage<IUserGroup>().Save(userGroup);
                return userGroup;
            }
        }

        private IUserGroupXOrganization CreateUserGroupXOrganization(Guid _userGroupId, Guid _organizationId)
        {
            using (var factory = CreateFactory())
            {
                var membership = factory.Manage<IUserGroupXOrganization>().Create();
                membership.OrganizationId = _organizationId;
                membership.UserGroupId = _userGroupId;
                factory.Manage<IUserGroupXOrganization>().Save(membership);

                return membership;
            }
        }

        private IArea CreateArea(int _order, string _shortName)
        {
            using (var factory = CreateFactory())
            {
                var area = factory.Manage<IArea>().Create();
                area.Order = _order;
                area.Name = Guid.NewGuid().ToString();
                area.Controller = Guid.NewGuid().ToString();
                area.ShortName = _shortName;
                factory.Manage<IArea>().Save(area);
                return area;
            }
        }

        //[Test]
        //public void AddChildOrganization()
        //{
        //    var session = Business.LogOn(TestUser.UserName, TestUser.Password);
        //    Assert.IsNotNull(session);
        //    var parent = CreateOrganization(null, "TEST", "Test Parent");
        //    Assert.IsNotNull(parent);
        //    var name = Guid.NewGuid().ToString();
        //    var shortName = Guid.NewGuid().ToString();
        //    var child = Business.AddOrganization(session.SessionId, parent.OrganizationId, name, shortName);
        //    Assert.IsNotNull(child);
        //    Log.Info(string.Format("Child id = {0}", child.OrganizationId));
        //    child = Business.GetOrganization(session.SessionId, child.OrganizationId);
        //    Assert.IsNotNull(child);
        //    Assert.AreEqual(name, child.Name);
        //    Assert.AreEqual(shortName, child.ShortName);
        //    Assert.AreEqual(parent.OrganizationId, child.ParentId);
        //    Business.DeleteOrganization(session.Id, child.Id);
        //    Business.DeleteOrganization(session.Id, parent.Id);
        //    Business.LogOff(session.Id);
        //}

        [Test]
        public void SaveUser()
        {
            var session = OpenSession();
            var user = Business.GetUser(session.SessionId, TestUser.UserId);
            Assert.IsNotNull(user);
            user.UserName = "Fred";
            Business.SaveUser(session.SessionId, user);
            user = Business.GetUser(session.SessionId, TestUser.UserId);
            Assert.IsNotNull(user);
            Assert.AreEqual("Fred", user.UserName);
            CloseSession(session);
        }

        //[Test]
        //public void AddUserGroup()
        //{
        //    var org = CreateOrganization(TestOrganization.Id, TestHelpers.TestString(), TestHelpers.TestString());

        //    var session = Business.LogOn(TestUser.UserName, TestUser.Password);
        //    Log.Info(string.Format("Session id is {0}", session.Id));
        //    var addedGroup = Business.AddUserGroup(session.SessionId, Guid.NewGuid().ToString());
        //    Log.Info("Added User Group");

        //    using (var factory = CreateFactory())
        //    {
        //        Log.Info("Created factory");
        //        var userGroup = factory.Manage<IUserGroup>().Get(addedGroup.UserGroupId);
        //        Log.Info("Got user group");
        //        Assert.IsNotNull(userGroup);
        //    }

        //    Business.DeleteUserGroup(session.Id, addedGroup.Id);

        //    Log.Info(string.Format("Logging off session {0}", session.Id));
        //    Business.LogOff(session.Id);

        //    DeleteOrganization(org);
        //}

        private void DeleteOrganization(IOrganization _org)
        {
            using (var factory = CreateFactory())
            {
                factory.Manage<IOrganization>().Delete(_org.Id);
            }
        }

        [Test]
        public void GetRootOrganizations()
        {
            var session = OpenSession();
            Assert.IsNotNull(session);
            var organizations = Business.GetOrganizations(session.SessionId, Guid.Empty);
            Assert.IsNotNull(organizations);
            var rootOrg = (from o in organizations where o.OrganizationId == TestOrganization.Id select o).FirstOrDefault();
            Assert.IsNotNull(rootOrg);
            Business.LogOff(session.Id);
        }

        [Test]
        public void SavesUserGroup()
        {
            Guid userGroupId;
            using (var factory = CreateFactory())
            {
                var dataUserGroup = factory.Manage<IUserGroup>().Create();
                dataUserGroup.Name = TestHelpers.TestString();
                dataUserGroup.OrganizationId = TestOrganization.Id;
                factory.Manage<IUserGroup>().Save(dataUserGroup);
                userGroupId = dataUserGroup.Id;
            }
            
            var session = Business.LogOn(TestUser.UserName, TestUser.Password);
            Assert.IsNotNull(session);
            
            var userGroup = Business.GetUserGroup(session.SessionId, userGroupId);
            Assert.IsNotNull(userGroup);
            
            var userGroupName = TestHelpers.TestString();
            userGroup.Name = userGroupName;
            Business.SaveUserGroup(session.SessionId, userGroup);
            Business.LogOff(session.SessionId);

            using (var factory = CreateFactory())
            {
                var dataUserGroup = factory.Manage<IUserGroup>().Get(userGroupId);
                Assert.AreEqual(userGroupName, dataUserGroup.Name);
                factory.Manage<IUserGroup>().Delete(dataUserGroup);
            }
        }

        [Test]
        public void SavesUserGroupOrganizations()
        {
            var dataUserGroup = CreateUserGroup();
            Log.Info(string.Format("dataUserGroup.Id = {0}", dataUserGroup.Id));
            var org = CreateOrganization(TestOrganization.Id, TestHelpers.TestString(), TestHelpers.TestString());
            Log.Info(string.Format("org.Id = {0}", org.Id));
            var session = Business.LogOn(TestUser.UserName, TestUser.Password);
            Assert.IsNotNull(session);
            var userGroup = Business.GetUserGroup(session.SessionId, dataUserGroup.Id);
            Assert.IsNotNull(userGroup);
            var businessOrg = Business.GetOrganization(session.SessionId, org.Id);
            //userGroup.AddOrganization(businessOrg.OrganizationId);
            userGroup.OrganizationIds.Add( businessOrg.OrganizationId );
            Business.SaveUserGroup(session.SessionId, userGroup);
            Business.LogOff(session.SessionId);

            using (var factory = CreateFactory())
            {
                var userGroupXOrgs = from x in factory.Manage<IUserGroupXOrganization>().GetAll()
                    where x.UserGroupId == dataUserGroup.Id && x.OrganizationId == org.Id select x;

                Assert.AreEqual(1, userGroupXOrgs.Count(), "The user group has access to the organization");

                factory.Manage<IUserGroupXOrganization>().Delete(_x => _x.UserGroupId == dataUserGroup.Id &&
                    _x.OrganizationId == org.Id);
            }
            using (var factory = CreateFactory())
            {
                factory.Manage<IUserGroup>().Delete(dataUserGroup.Id);
                factory.Manage<IOrganization>().Delete(org.Id);
            }
        }
    }
}




















