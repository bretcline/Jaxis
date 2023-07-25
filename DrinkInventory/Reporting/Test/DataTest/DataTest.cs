using System;
using System.Linq;
using Jaxis.DrinkInventory.Reporting.Data;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using NUnit.Framework;

namespace Jaxis.DrinkInventory.Reporting.Test.DataTest
{
    [TestFixture]
    public class DataTest
    {
        [Test]
        public void RecentPoursReportHasCorrectFileName()
        {
            // get rid of this silly test at some point (when it fails)
            using (var factory = DataManagerFactory.Get())
            {
                var view = (from v in factory.Manage<IReport>().GetAll()
                            where v.ShortName == "RecentPours"
                            select v).FirstOrDefault();
                Assert.IsNotNull(view);
                Assert.AreEqual("RecentPours.repx", view.ReportClassName); 
            }
        }

        [Test]
        public void SaveAndDeleteByEntity()
        {
            using (var factory = DataManagerFactory.Get())
            {
                var manager = factory.Manage<IUser>();
                var user = manager.Create();
                user.UserName = Guid.NewGuid().ToString();
                user.Password = Guid.NewGuid().ToString();
                manager.Save(user);
                manager.Delete(user);
                user = manager.Get(user.UserId);
                Assert.IsNull(user);
            }
        }

        [Test]
        public void SaveAndDeleteById()
        {
            using (var factory = DataManagerFactory.Get())
            {
                var manager = factory.Manage<IUser>();
                var user = manager.Create();
                user.UserName = Guid.NewGuid().ToString();
                user.Password = Guid.NewGuid().ToString();
                manager.Save(user);
                manager.Delete(user.UserId);
                user = manager.Get(user.UserId);
                Assert.IsNull(user);
            }
        }

        [Test]
        public void BulkDelete( )
        {
            using ( var factory = DataManagerFactory.Get( ) )
            {
                var manager = factory.Manage<IUser>( );
                var user = manager.Create( );
                user.UserName = "Joey";
                user.Password = "Joe";
                manager.Save( user );

                user = manager.Create( );
                user.UserName = "JoeBob";
                user.Password = "Joe";
                manager.Save( user );

                user = manager.Create( );
                user.UserName = "WonderJoe";
                user.Password = "Joe";
                manager.Save( user );
            }
            using ( var factory = DataManagerFactory.Get( ) )
            {
                var manager = factory.Manage<IUser>( );
                var count = manager.Delete(_u => _u.Password == "Joe");
                Assert.AreEqual( count, 3 );
            }
        }

        [Test]
        public void SessionTest()
        {
            string userName;
            string password;

            using (var factory = DataManagerFactory.Get())
            {
                password = Guid.NewGuid().ToString();
                userName = Guid.NewGuid().ToString();
                var user = factory.Manage<IUser>().Create();
                user.Password = password;
                user.UserName = userName;
                factory.Manage<IUser>().Save(user);
            }

            using (var factory = DataManagerFactory.Get())
            {
                var match = factory.Manage<IUser>().GetAll().Where(
                    _u => _u.UserName == userName && _u.Password == password).FirstOrDefault();
                Assert.IsNotNull(match);
                var session = factory.Manage<ISession>().Create();
                session.ExpirationTime = DateTime.Now + new TimeSpan(1, 0, 0, 0);
                factory.Manage<ISession>().Save(session);
                Assert.IsNotNull(session);
                factory.Manage<ISession>().Delete(session);
            }
        }
    }
}
