using System;
using System.Collections.Generic;
using System.Linq;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using Jaxis.Util.Log4Net;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeDataProvider
    {
        private readonly IDictionary<Guid, IUser> m_users;
        private readonly IDictionary<Guid, ISession> m_sessions;
        private readonly IDictionary<Guid, IArea> m_areas;
        private readonly IDictionary<Guid, IAreaMembership> m_areaMemberships;
        private readonly IDictionary<Guid, IUserGroup> m_userGroups;
        private readonly IDictionary<Guid, IUserGroupMembership> m_userGroupMemberships;
        private readonly IDictionary<Guid, IReport> m_views;
        private readonly IDictionary<Guid, IOrganization> m_organizations;
        private readonly IDictionary<Guid, IParameter> m_parameters;
        private readonly IDictionary<Guid, IPour> m_pours;

        public FakeDataProvider()
        {
            m_users = new Dictionary<Guid, IUser>();
            m_sessions = new Dictionary<Guid, ISession>();
            m_areas = new Dictionary<Guid, IArea>();
            m_areaMemberships = new Dictionary<Guid, IAreaMembership>();
            m_userGroups = new Dictionary<Guid, IUserGroup>();
            m_userGroupMemberships = new Dictionary<Guid, IUserGroupMembership>();
            m_views = new Dictionary<Guid, IReport>();
            m_organizations = new Dictionary<Guid, IOrganization>();
            m_parameters = new Dictionary<Guid, IParameter>();
            m_pours = new Dictionary<Guid, IPour>();
        }

        public IUser GetUser(string _userName)
        {
            return (from u in m_users.Values where string.Compare(u.UserName, _userName, true) == 0 select u).FirstOrDefault();
        }

        public IUser GetUser(Guid _userId)
        {
            return !m_users.ContainsKey(_userId) ? null : m_users[_userId];
        }

        public IQueryable<IUser> GetUsers()
        {
            return m_users.Values.AsQueryable();
        }

        public ISession NewSession(IUser _user)
        {
            var session = new FakeSession {UserId = _user.UserId};
            return session;
        }

        public ISession GetSession(Guid _sessionId)
        {
            return m_sessions[_sessionId];
        }

        public void Save(ISession _session)
        {
            m_sessions[_session.SessionId] = _session;
        }

        public void Delete(ISession _session)
        {
            m_sessions.Remove(_session.SessionId);
        }

        public IArea NewArea()

        {
            var area = new FakeArea();
            return area;
        }

        public void Save(IArea _area)
        {
            m_areas[_area.AreaId] = _area;
        }

        public IReport NewView()
        {
            var view = new FakeView();
            return view;
        }

        public void Save(IReport _view)
        {
            m_views[_view.ReportId] = _view;
        }

        public IUserGroupMembership NewUserGroupMembership()
        {
            var userGroupMembership = new FakeUserGroupMembership();
            return userGroupMembership;
        }

        public void Save(IUserGroupMembership _userGroupMembership)
        {
            m_userGroupMemberships[_userGroupMembership.UserGroupMembershipId] = _userGroupMembership;
        }

        public IAreaMembership NewAreaMembership()
        {
            var areaMembership = new FakeAreaMembership();
            return areaMembership;
        }

        public void Save(IAreaMembership _areaMembership)
        {
            m_areaMemberships[_areaMembership.AreaMembershipId] = _areaMembership;
        }

        public IUserGroup NewUserGroup()
        {
            return new FakeUserGroup();
        }

        public void Save(IUserGroup _userGroup)
        {
            m_userGroups[_userGroup.UserGroupId] = _userGroup;
        }

        public IOrganization NewOrganization()
        {
            var organization = new FakeOrganization();
            return organization;
        }

        public IOrganization GetOrganization(Guid _organizationId)
        {
            return !m_organizations.ContainsKey(_organizationId) ? null : m_organizations[_organizationId];
        }

        public IQueryable<IOrganization> GetOrganizations()
        {
            return m_organizations.Values.AsQueryable();
        }

        public IArea GetArea(Guid _areaId)
        {
            return m_areas[_areaId];
        }

        public IQueryable<IParameter> GetParameters()
        {
            return m_parameters.Values.AsQueryable();
        }

        public IQueryable<IPour> GetPours()
        {
            return m_pours.Values.AsQueryable();
        }

        public void Save(IOrganization _organization)
        {
            Log.Info( string.Format( "FakeDataProvider saving org unit id = {0}", _organization.OrganizationId ) );
            m_organizations[ _organization.OrganizationId ] = _organization;
        }

        public IUser NewUser()
        {
            var user = new FakeUser();
            return user;
        }

        public void Save(IUser _user)
        {
            m_users[_user.UserId] = _user;
        }

        public IQueryable<IReport> GetViews()
        {
            return m_views.Values.AsQueryable();
        }

        protected IQueryable<IUserGroup> GetUserGroups()
        {
            return m_userGroups.Values.AsQueryable();
        }

        protected IQueryable<IAreaMembership> GetAreaMemberships()
        {
            return m_areaMemberships.Values.AsQueryable();
        }

        protected IQueryable<IArea> GetAreas()
        {
            return m_areas.Values.AsQueryable();
        }

        public void Delete(IUserGroup _userGroup)
        {
            m_userGroups.Remove(_userGroup.UserGroupId);
        }

        public void Delete(IUser _user)
        {
            m_users.Remove(_user.UserId);
        }

        public void Delete(IAreaMembership _areaMembership)
        {
            m_areaMemberships.Remove(_areaMembership.AreaMembershipId);
        }

        public void Delete(IUserGroupMembership _userGroupMembership)
        {
            m_userGroupMemberships.Remove(_userGroupMembership.UserGroupMembershipId);
        }

        public void Delete(IArea _area)
        {
            m_areas.Remove(_area.AreaId);
        }

        public void Delete(IReport _view)
        {
            m_views.Remove(_view.ReportId);
        }

        public void Delete(IOrganization _organization)
        {
            m_organizations.Remove( _organization.OrganizationId );
        }

        #region IDataProvider Members


        public List<IArea> GetAreas( ISession _session )
        {
            throw new NotImplementedException( );
        }

        public List<IUser> GetUsersByName( string _userName )
        {
            throw new NotImplementedException( );
        }

        public List<IReport> GetViews( Guid _sectionId )
        {
            throw new NotImplementedException( );
        }

        public List<IOrganization> GetOrganizations( Guid _parentId )
        {
            throw new NotImplementedException( );
        }

        public List<IParameter> GetParameters( Guid _viewId )
        {
            throw new NotImplementedException( );
        }

        #endregion
    }
}
