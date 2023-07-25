using System;
using System.Collections.Generic;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    class FakeDataManagerFactory : IDataManagerFactory
    {
        private static IDictionary<Type, IDataManager> m_managers;

        static FakeDataManagerFactory()
        {
            ClearAll();
        }

        public IDataManager<T> Manage<T>()
        {
            var type = typeof (T);

            if (!m_managers.ContainsKey(type))
            {
                throw new Exception(string.Format("FakeDataManagerFactory does not know how to " +
                    "manage type {0}.", type.FullName));
            }

            return m_managers[type] as IDataManager<T>;
        }

        public void Dispose()
        {
        }

        public static void ClearAll()
        {
            m_managers = new Dictionary<Type, IDataManager>
            {
                {typeof (IUser), new FakeUserDataManager()},
                {typeof (ISession), new FakeSessionDataManager()},
                {typeof (IOrganization), new FakeOrganizationDataManager()},
                {typeof (IArea), new FakeAreaDataManager()},
                {typeof (IAreaMembership), new FakeAreaMembershipDataManager()},
                {typeof (IUserGroup), new FakeUserGroupDataManager()},
                {typeof (IUserGroupMembership), new FakeUserGroupMembershipDataManager()},
                {typeof (IReport), new FakeViewDataManager()},
                {typeof (IUserGroupXOrganization), new FakeUserGroupXOrganizationManager()},
            };
        }

        #region IDataManagerFactory Members


        public int SaveChanges( )
        {
            throw new NotImplementedException( );
        }

        #endregion
    }
}
