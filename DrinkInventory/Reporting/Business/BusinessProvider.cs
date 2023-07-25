using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Jaxis.DrinkInventory.Reporting.BusinessInterfaces;
using Jaxis.DrinkInventory.Reporting.Common;
using Jaxis.DrinkInventory.Reporting.Data;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using Jaxis.Util.Log4Net;

namespace Jaxis.DrinkInventory.Reporting.Business
{
    public class BusinessProvider : IBusinessProvider
    {
        #region General ---------------------------------------------------------------------------

        private static Func<IDataManagerFactory> m_createFactory;

        public static void Init(Func<IDataManagerFactory> _createFactory)
        {
            // probably will only need this for unit tests
            m_createFactory = _createFactory;
        }

        private static IDataManagerFactory CreateFactory()
        {
            var create = m_createFactory ?? (m_createFactory = (() => { return DataManagerFactory.Get(); }));
            return create();
        }

        private static DataTable ToTable<T>(IEnumerable<T> _objects) where T : class
        {
            var table = new DataTable("Table");
            var type = typeof(T);
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

            foreach (var property in props)
            {
                table.Columns.Add(property.Name, property.PropertyType);
            }

            foreach (var pour in _objects)
            {
                var row = table.NewRow();

                foreach (var property in props)
                {
                    row[property.Name] = property.GetValue(pour, null);
                }

                table.Rows.Add(row);
            }

            return table;
        }

        private static void RequiredField(string _fieldValue, string _fieldName)
        {
            if (string.IsNullOrWhiteSpace(_fieldValue))
            {
                throw new Exception(string.Format("The {0} field is required", _fieldName));
            }
        }

        #endregion

        #region Organization ----------------------------------------------------------------------

        public IOrganization GetOrganization(Guid _sessionId, Guid _organizationId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                var organization = factory.Organization.Get(_organizationId);
                // TODO: Only return org units the user can see (user logged in to the session provided)
                HydrateOrganization(factory, organization);
                return organization;
            }
        }

        public IOrganization NewOrganization(Guid _sessionId, Guid _parentId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                var organization = factory.Organization.Create();
                organization.ParentId = _parentId;
                organization.Name = "New Organization";
                organization.ShortName = "NEW";
                organization.UserGroupIds = new List<Guid>();
                return organization;
            }
        }

        public IOrganization SaveOrganization(Guid _sessionId, IOrganization _organization)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                factory.Organization.Save(_organization);

                UpdateOrganizationUserGroups(factory, _organization,
                    () => _organization.UserGroupIds != null,
                    () => _organization.UserGroupIds);
                factory.SaveChanges();

                return _organization;
            }
        }

        // Helper method to update only relationships that have changed.
        private void UpdateOrganizationUserGroups(IDataManagerFactory _factory,
            IOrganization _organization,
            Func<bool> _testForUpdate,
            Func<List<Guid>> _getNewIds)
        {
            // Organization may have some UserGroup relationships that have changed.
            if (_testForUpdate())
            {
                // Get the ones it currently has.
                var ugoMgr = _factory.UserGroupXOrganization;
                var oldOnes = ugoMgr.GetAll()
                    .Where(u => u.OrganizationId == _organization.OrganizationId)
                    .ToList();
                var oldIds = oldOnes.Select(u => u.UserGroupId).ToList();
                var newIds = _getNewIds();
                // Are there new ones?
                foreach (Guid id in newIds.Except(oldIds))
                {
                    var ugm = ugoMgr.Create();
                    ugm.OrganizationId = _organization.OrganizationId;
                    ugm.UserGroupId = id;
                    ugoMgr.Save(ugm);
                }
                // Any to be deleted?
                foreach (Guid id in oldIds.Except(newIds))
                {
                    ugoMgr.Delete(oldOnes.First(o => o.UserGroupId == id));
                }
            }
        }

        private IEnumerable<IOrganization> GetOrganizations(IDataManagerFactory _factory, Guid _sessionId, Guid _parentId)
        {
            IQueryable<IOrganization> organizations;

            if (_parentId == Guid.Empty)
            {
                organizations =
                    from org in _factory.Organization.GetAll( )
                    join sv in _factory.SecurityView.GetAll( ) on org.OrganizationId equals sv.OrganizationId
                    join svp in _factory.SecurityView.GetAll( ) on org.ParentId equals svp.OrganizationId into joined
                    from svp in joined.DefaultIfEmpty()
                    where sv.SessionId == _sessionId && svp == null
                    select org;
            }
            else
            {
                organizations =
                    from ou in _factory.Organization.GetAll( )
                    join sv in _factory.SecurityView.GetAll( ) on ou.OrganizationId equals sv.OrganizationId
                    where sv.SessionId == _sessionId && ou.ParentId == _parentId
                    select ou;
            }

            return organizations.ToList().OrderBy(o => o.Name);
        }

        public IEnumerable<IOrganization> GetOrganizations(Guid _sessionId, Guid _parentId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);

                return GetOrganizations(factory, _sessionId, _parentId);
            }
        }

        public void DeleteOrganization(Guid _sessionId, Guid _organizationId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                factory.Organization.Delete(_organizationId);
                factory.SaveChanges();
            }
        }

        private static void HydrateOrganization(IDataManagerFactory _factory, IOrganization _organization)
        {
            HydrateOrganizations(_factory, new List<IOrganization> { _organization });
        }

        private static void HydrateOrganizations(IDataManagerFactory _factory, List<IOrganization> _organizations)
        {
            var oMgr = _factory.Organization;
            _organizations.ForEach(s => oMgr.Hydrate(s));
        }

        #endregion

        #region Session ---------------------------------------------------------------------------

        public ISession LogOn(string _userName, string _password)
        {
            using (var factory = CreateFactory())
            {
                ISession session = null;
                var match = factory.User.GetAll( ).Where(_u => _u.UserName == _userName &&
                    _u.Password == _password).FirstOrDefault();

                if (match != null)
                {
                    session = factory.Session.Create();
                    session.UserId = match.UserId;
                    session.ExpirationTime = Services.Clock.Now + Settings.Default.SessionLifetime;
                    factory.Session.Save(session);
                    HydrateSession( factory, session );

                    //session = new Session(factory, dataSession);
                }
                else
                {
                    Log.Info(string.Format("BusinessProvider.LogOn did not find user {0}", _userName));
                }

                factory.SaveChanges( );
                return session;
            }
        }

        public void LogOff(Guid _sessionId)
        {
            using (var factory = CreateFactory())
            {
                var session = ValidateSession(factory, _sessionId);
                Log.Info(string.Format("Deleting session {0}", session.SessionId));
                factory.Session.Delete(session.SessionId);
                factory.SaveChanges();
            }
        }

        public void SessionActivity(Guid _sessionId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
            }
        }

        public bool SessionIsValid(Guid _sessionId)
        {
            if ( _sessionId == Guid.Empty )
                return false;
            using ( var factory = CreateFactory( ) )
            {
                try
                {
                    ValidateSession(factory, _sessionId);
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
        }

        // Usually, ValidateSession does not need to pull in the User object.
        private static ISession ValidateSession( IDataManagerFactory _factory, Guid _sessionId, bool _needsUser = false )
        {
            var session = _factory.Session.Get( _sessionId );
            return ValidateSession( _factory, session, _needsUser );
        }

        private static ISession ValidateSession(IDataManagerFactory _factory, ISession _session, bool _needsUser)
        {
            if (_session == null)
                throw new Exception("Session not found.");

            if (Services.Clock.Now >= _session.ExpirationTime)
            {
                Log.Info("Session time has expired");
                throw new SessionExpiredException();
            }

            var newExpirationTime = Services.Clock.Now + Settings.Default.SessionLifetime;
            if (newExpirationTime - _session.ExpirationTime > new TimeSpan(Convert.ToInt64(Settings.Default.SessionLifetime.Ticks * 0.1)))
            {
                _session.ExpirationTime = Services.Clock.Now + Settings.Default.SessionLifetime;
                _factory.Session.Save(_session);
                _factory.SaveChanges();
            }

            if (_needsUser)
            {
                HydrateSession(_factory, _session);
            }
            return _session;
        }

        public ISession GetSession(Guid _sessionId)
        {
            using (var factory = CreateFactory())
            {
                var session = ValidateSession(factory, _sessionId);
                return session;
            }
        }

        private static void HydrateSession(IDataManagerFactory _factory, ISession _session)
        {
            HydrateSessions(_factory, new List<ISession> { _session });
        }

        private static void HydrateSessions(IDataManagerFactory _factory, List<ISession> _sessions)
        {
            var sMgr = _factory.Session;
            _sessions.ForEach(s => sMgr.Hydrate(s));
        }

        #endregion

        #region Area ------------------------------------------------------------------------------

        public IEnumerable<IArea> GetAreas(Guid _sessionId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);

                var areas =
                    from a in factory.Area.GetAll( )
                    join am in factory.AreaMembership.GetAll( ) on a.AreaId equals am.AreaId
                    join ug in factory.UserGroup.GetAll( ) on am.UserGroupId equals ug.UserGroupId
                    join ugm in factory.UserGroupMembership.GetAll( ) on ug.UserGroupId equals ugm.UserGroupId
                    join u in factory.User.GetAll( ) on ugm.UserId equals u.UserId
                    join s in factory.Session.GetAll( ) on u.UserId equals s.UserId
                    where s.SessionId == _sessionId
                    select a;
                
                // Linq-to-Entities has to execute (the first ToList) before we apply the Cast.  Otherwise, the error is
                // "Linq to Entities only supports casting Entity Data Model primitive types."
                //return areas.ToList( ).Cast<IArea>( ).ToList( );
                return areas.Distinct().OrderBy(_a => _a.Order).ToList();
            }
        }

        public IArea GetArea(Guid _sessionId, Guid _areaId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);

                return factory.Area.Get(_areaId);
            }
        }

        #endregion

        #region Report ----------------------------------------------------------------------------

        public IEnumerable<IReport> GetReports(Guid _sessionId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession( factory, _sessionId );

                var reports =
                    from r in factory.Report.GetAll( )
                    orderby r.Order
                    select r;

                return reports.ToList();
            }
        }

        public IReport GetReport(Guid _sessionId, Guid _reportId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                var report = factory.Report.Get(_reportId);
                return report;
            }
        }

        public IEnumerable<IParameter> GetParameters(Guid _sessionId, Guid _reportId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);

                var parameters =
                    from p in factory.Parameter.GetAll( )
                    where p.ReportId == _reportId
                    orderby p.Order
                    select p;

                return parameters.ToList();
            }
        }

        #endregion

        #region User Group ------------------------------------------------------------------------
        
        public IUserGroup SaveUserGroup(Guid _sessionId, IUserGroup _userGroup)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                factory.UserGroup.Save(_userGroup);
                UpdateUserGroupOrganizations(factory, _userGroup, () => _userGroup.OrganizationIds != null, () => _userGroup.OrganizationIds);
                UpdateUserGroupAreas(factory, _userGroup, () => _userGroup.AreaIds != null, () => _userGroup.AreaIds);
                UpdateUserGroupUsers(factory, _userGroup, () => _userGroup.UserIds != null, () => _userGroup.UserIds);
                factory.SaveChanges();
                return _userGroup;
            }
        }

        public IEnumerable<IUserGroup> GetUserGroups(Guid _sessionId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);

                var userGroups =
                    from g in factory.UserGroup.GetAll( )
                    join sv in factory.SecurityView.GetAll( ) on g.OrganizationId equals sv.OrganizationId
                    orderby g.Name
                    where sv.SessionId == _sessionId
                    select g;

                return userGroups.ToList();
            }
        }

        public IUserGroup NewUserGroup(Guid _sessionId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                var group = factory.UserGroup.Create();
                group.Name = "New User Group";
                HydrateUserGroup(factory, group);
                return group;
            }
        }

        public IUserGroup GetUserGroup(Guid _sessionId, Guid _userGroupId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                var userGroup = factory.UserGroup.Get( _userGroupId );
                HydrateUserGroup(factory, userGroup);
                return userGroup;
            }
        }

        private void UpdateUserGroupAreas(IDataManagerFactory _factory, IUserGroup _userGroup, Func<bool> _testForUpdate, Func<List<Guid>> _getNewIds)
        {
            // UserGroup may have some Area relationships that have changed.
            if (_testForUpdate())
            {
                var amMgr = _factory.AreaMembership;
                // Get the ones it currently has.
                var oldOnes = amMgr.GetAll()
                    .Where(i => i.UserGroupId == _userGroup.UserGroupId)
                    .ToList();
                var oldIds = oldOnes.Select(o => o.AreaId).ToList();
                var newIds = _getNewIds();
                // Any new ones?
                foreach (Guid id in newIds.Except(oldIds))
                {
                    var am = amMgr.Create();
                    am.UserGroupId = _userGroup.UserGroupId;
                    am.AreaId = id;
                    amMgr.Save(am);
                }
                // Any to be deleted?
                foreach (Guid id in oldIds.Except(newIds))
                {
                    amMgr.Delete(oldOnes.First(o => o.AreaId == id));
                }
            }
        }

        private void UpdateUserGroupUsers(IDataManagerFactory _factory, IUserGroup _userGroup, Func<bool> _testForUpdate, Func<List<Guid>> _getNewIds)
        {
            // UserGroup may have some User relationships that have changed.
            if (_testForUpdate())
            {
                var ugmMgr = _factory.UserGroupMembership;
                // Get the ones it currently has.
                var oldOnes = ugmMgr.GetAll()
                    .Where(i => i.UserGroupId == _userGroup.UserGroupId)
                    .ToList();
                var oldIds = oldOnes.Select(o => o.UserId).ToList();
                var newIds = _getNewIds();
                // Any new ones?
                foreach (Guid id in newIds.Except(oldIds))
                {
                    var ugm = ugmMgr.Create();
                    ugm.UserGroupId = _userGroup.UserGroupId;
                    ugm.UserId = id;
                    ugmMgr.Save(ugm);
                }
                // Any to be deleted?
                foreach (Guid id in oldIds.Except(newIds))
                {
                    ugmMgr.Delete(oldOnes.First(o => o.UserId == id));
                }
            }
        }

        public void DeleteUserGroup(Guid _sessionId, Guid _userGroupId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                factory.UserGroupMembership.Delete(_u => _u.UserGroupId == _userGroupId);
                factory.UserGroup.Delete(_userGroupId);
                factory.SaveChanges();
            }
        }

        private void UpdateUserGroupOrganizations(IDataManagerFactory _factory, IUserGroup _userGroup, Func<bool> _testForUpdate, Func<List<Guid>> _getNewIds)
        {
            // UserGroup may have some Organization relationships that have changed.
            if (_testForUpdate())
            {
                var ugoMgr = _factory.UserGroupXOrganization;
                // Get the ones it currently has.
                var oldOnes = ugoMgr.GetAll()
                    .Where(i => i.UserGroupId == _userGroup.UserGroupId)
                    .ToList();
                var oldIds = oldOnes.Select(o => o.OrganizationId).ToList();
                var newIds = _getNewIds();
                // Any new ones?
                foreach (Guid id in newIds.Except(oldIds))
                {
                    var ugo = ugoMgr.Create();
                    ugo.UserGroupId = _userGroup.UserGroupId;
                    ugo.OrganizationId = id;
                    ugoMgr.Save(ugo);
                }
                // Any to be deleted?
                foreach (Guid id in oldIds.Except(newIds))
                {
                    ugoMgr.Delete(oldOnes.First(o => o.OrganizationId == id));
                }
            }
        }

        public IEnumerable<IOrganization> GetOrganizationsForUserGroup(Guid _sessionId, Guid _userGroupId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);

                var organization =
                    from o in factory.Organization.GetAll( )
                    join x in factory.UserGroupXOrganization.GetAll( ) on o.OrganizationId equals x.OrganizationId
                    where x.UserGroupId == _userGroupId
                    select o;

                return organization.ToList().Cast<IOrganization>();
            }
        }

        private static void HydrateUserGroup(IDataManagerFactory _factory, IUserGroup _userGroup)
        {
            HydrateUserGroups(_factory, new List<IUserGroup> { _userGroup });
        }

        private static void HydrateUserGroups(IDataManagerFactory _factory, List<IUserGroup> _userGroups)
        {
            var ugMgr = _factory.UserGroup;
            _userGroups.ForEach(u => ugMgr.Hydrate(u));
        }

        #endregion

        #region User ------------------------------------------------------------------------------

        public IEnumerable<IUser> GetUsers(Guid _sessionId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);

                var users = 
                    ( from u in factory.User.GetAll( )
                    join sv in factory.SecurityView.GetAll( ) on u.OrganizationId equals sv.OrganizationId
                    where sv.SessionId == _sessionId
                    orderby u.UserName
                    select u ).ToList( );

                HydrateUsers( factory, users );
                return users;
            }
        }

        public IUser GetUser(Guid _sessionId, Guid _userId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                var user = factory.User.Get(_userId);
                HydrateUser( factory, user );               
                return user;
            }
        }

        public IUser NewUser(Guid _sessionId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                var organization = GetOrganizations(factory, _sessionId, Guid.Empty).FirstOrDefault();
                var user = factory.User.Create();
                user.UserName = "New User";
                user.OrganizationId = organization.OrganizationId;
                HydrateUser(factory, user);
                return user;
            }
        }

        public void DeleteUser(Guid _sessionId, Guid _userId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                factory.User.Delete(_userId);
                factory.SaveChanges( );
            }
        }

        public IUser SaveUser(Guid _sessionId, IUser _user)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                RequiredField(_user.UserName, "User Name");
                RequiredField(_user.Password, "Password");

                if (_user.VisibleWidgetIds == null)
                    _user.VisibleWidgetIds = string.Empty;

                factory.User.Save( _user );

                if ( _user.UserGroupIds != null )
                {
                    // Get the ones he currently has.
                    var ugmMgr = factory.UserGroupMembership;
                    var oldOnes = ugmMgr.GetAll( )
                        .Where( u => u.UserId == _user.UserId ).ToList( );
                    var oldIds = oldOnes.Select( o => o.UserGroupId );
                    var newIds = _user.UserGroupIds;
                    // Are there new ones?
                    foreach ( Guid id in oldIds.Except( newIds ) )
                    {
                        var ugm = ugmMgr.Create( );
                        ugm.UserId = _user.UserId;
                        ugm.UserGroupId = id;
                        ugmMgr.Save( ugm );
                    }
                    // Any that should be deleted?
                    foreach ( Guid id in newIds.Except( oldIds ) )
                    {
                        ugmMgr.Delete( oldOnes.First( o => o.UserGroupId == id ) );
                    }
                }
                factory.SaveChanges( );
                return _user;
            }
        }

        public IEnumerable<Guid> GetVisibleWidgetIds(Guid _sessionId)
        {
            using (var factory = CreateFactory())
            {
                var session = ValidateSession(factory, _sessionId, true);
                return (string.IsNullOrWhiteSpace(session.User.VisibleWidgetIds) ? new List<Guid>() :
                    from id in session.User.VisibleWidgetIds.Split(',') select Guid.Parse(id)).ToList();
            }
        }

        public void SetVisibleWidgetIds(Guid _sessionId, IEnumerable<Guid> _widgetIds)
        {
            using (var factory = CreateFactory())
            {
                var session = ValidateSession(factory, _sessionId, true);
                var builder = new SeparatedStringBuilder(",");

                foreach (var widgetId in _widgetIds)
                {
                    builder.Append(widgetId);
                }

                session.User.VisibleWidgetIds = builder.ToString();
                factory.User.Save(session.User);
                factory.SaveChanges();
            }
        }

        private static void HydrateUser(IDataManagerFactory _factory, IUser _user)
        {
            HydrateUsers(_factory, new List<IUser> { _user });
        }

        private static void HydrateUsers(IDataManagerFactory _factory, List<IUser> _users)
        {
            var uMgr = _factory.User;
            _users.ForEach(u => uMgr.Hydrate(u));
        }

        #endregion

        #region Pour ------------------------------------------------------------------------------
        
        public DataTable GetRecentPours(Guid _sessionId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);

                var pours =
                    (
                        from p in factory.Pour.GetAll( )
                        join ou in factory.Organization.GetAll( ) on p.OrganizationId equals ou.OrganizationId
                        orderby p.PourTime descending
                        select new {Time = p.PourTime, UPC = p.ItemNumber, Organization = ou.Name, p.Volume}
                    ).Take(8);

                // todo: only get the pours they should be able to see and add params for the the organization (organization) they have selected to view.
                // the organization selection could be kept in the session object
                return ToTable(pours);
            }
        }

        #endregion

        #region UPC -------------------------------------------------------------------------------
        
        public IUPC GetUpc(string _itemNumber)
        {
            using (var factory = CreateFactory())
            {
                return factory.UPC.GetAll( ).Where(_u => _u.ItemNumber == _itemNumber).FirstOrDefault();
            }
        }

        public IUPC GetUpc(Guid _sessionId, Guid _upcId)
        {
            using (var factory = CreateFactory())
            {
                var upc = factory.UPC.GetAll( ).Where(_u => _u.UPCId == _upcId).FirstOrDefault();
                return upc;
            }
        }

        public IEnumerable<IUPC> GetUpcs(Guid _sessionId, string _filter, bool _showValidated)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                
                var upcs =
                    from u in factory.UPC.GetAll( )
                    where (string.IsNullOrEmpty(_filter) || u.Name.Contains(_filter) || u.ItemNumber.Contains(_filter)) &&
                        (_showValidated || !u.Validated)
                    orderby u.Name
                    select u;

                return upcs.ToList();
            }
        }

        public IUPC NewUpc(Guid _sessionId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                var upc = factory.UPC.Create();
                upc.ItemNumber = string.Empty;
                upc.ChildItemNumber = string.Empty;
                upc.BottleCount = 1;
                upc.CategoryName = string.Empty;
                upc.ManufacturerName = string.Empty;
                upc.Name = "New UPC";
                upc.RootCategoryName = string.Empty;
                upc.Size = 0;
                upc.SizeLabel = string.Empty;
                upc.Validated = false;
                return upc;
            }
        }

        public void DeleteUpc(Guid _sessionId, Guid _upcId)
        {
            using (var factory = CreateFactory())
            {
                ValidateSession(factory, _sessionId);
                factory.UPC.Delete(_upcId);
                factory.SaveChanges( );
            }
        }

        public IUPC SaveUpc(Guid _sessionId, IUPC _upc)
        {
            try
            {
                using (var factory = CreateFactory())
                {
                    ValidateSession(factory, _sessionId);
                    var manager = factory.UPC;

                    RequiredField(_upc.Name, "Name");
                    RequiredField(_upc.ItemNumber, "Item Number");
                    RequiredField(_upc.SizeLabel, "Size Label");
                    RequiredField(_upc.CategoryName, "Category Name");
                    RequiredField(_upc.RootCategoryName, "Root Category Name");
                    RequiredField(_upc.ManufacturerName, "Manufacturer Name");

                    manager.Save(_upc);
                    factory.SaveChanges( );
                    return _upc;
                }
            }
            catch (DataException ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException is SqlException &&
                    ex.InnerException.Message.Contains("Violation of PRIMARY KEY constraint"))
                {
                    throw new Exception("The Item Number field must be unique");
                }

                throw;
            }
        }

        #endregion
    }
}
