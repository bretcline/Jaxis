using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using System.Linq.Expressions;

namespace Jaxis.DrinkInventory.Reporting.Data
{
    public partial class UserGroupManager
    {
        public override void Hydrate( IUserGroup _userGroup, bool _includeRelatedObjects = false )
        {
            var oMgr = m_Factory.Organization;
            if ( _userGroup.Organization == null )
            {
                ( ( UserGroup ) _userGroup ).NavOrganization = ( Organization ) oMgr.Get( _userGroup.OrganizationId );
            }

            if ( _userGroup.AreaIds == null )
            {
                if ( _includeRelatedObjects )
                {
                    var areas = ( from m in m_Factory.AreaMembership.GetAll( )
                                  join a in m_Factory.Area.GetAll( ) on m.AreaId equals a.AreaId
                                  where m.UserGroupId == _userGroup.UserGroupId
                                  select a ).Cast<Area>( ).ToList( );
                    ( ( UserGroup ) _userGroup ).NavAreas = areas;
                    _userGroup.AreaIds = areas.Select( a => a.AreaId ).ToList( );
                }
                else
                {
                    _userGroup.AreaIds = m_Factory.AreaMembership.GetAll( )
                        .Where( a => a.UserGroupId == _userGroup.UserGroupId )
                        .Select( a => a.AreaId )
                        .ToList( );
                }
            }

            if ( _userGroup.UserIds == null )
            {
                if ( _includeRelatedObjects )
                {
                    var users = ( from m in m_Factory.UserGroupMembership.GetAll( )
                                  join user in m_Factory.User.GetAll( ) on m.UserId equals user.UserId
                                  where m.UserGroupId == _userGroup.UserGroupId
                                  select user ).Cast<User>( ).ToList( );
                    ( ( UserGroup ) _userGroup ).NavUsers = users;
                    _userGroup.UserIds = users.Select( u => u.UserId ).ToList( );
                }
                else
                {
                    _userGroup.UserIds = m_Factory.UserGroupMembership.GetAll( )
                        .Where( u => u.UserGroupId == _userGroup.UserGroupId )
                        .Select( u => u.UserId )
                        .ToList( );
                }
            }

            if ( _userGroup.OrganizationIds == null )
            {
                if ( _includeRelatedObjects )
                {
                    var organizations = ( from x in m_Factory.UserGroupXOrganization.GetAll( )
                                          join o in oMgr.GetAll( ) on x.OrganizationId equals o.OrganizationId
                                          where x.UserGroupId == _userGroup.UserGroupId
                                          select o ).Cast<Organization>( ).ToList( );
                    ( ( UserGroup ) _userGroup ).NavOrganizations = organizations;
                    _userGroup.OrganizationIds = organizations.Select( o => o.OrganizationId ).ToList( );
                }
                else
                {
                    _userGroup.OrganizationIds = m_Factory.UserGroupXOrganization.GetAll( )
                        .Where( x => x.UserGroupId == _userGroup.UserGroupId )
                        .Select( x => x.OrganizationId )
                        .ToList( );
                }
            }
        }
    }
}
