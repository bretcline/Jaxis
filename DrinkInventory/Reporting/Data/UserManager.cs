using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using Jaxis.DrinkInventory.Reporting.Data.POCO;

namespace Jaxis.DrinkInventory.Reporting.Data
{
    public partial class UserManager
    {
        protected override void OnCreated( IUser _user )
        {
            _user.VisibleWidgetIds = _user.VisibleWidgetIds ?? string.Empty;
        }

        public override void Hydrate( IUser _user, bool _includeRelatedObjects = false )
        {
            if ( _user.UserGroupIds == null )
            {
                if ( _includeRelatedObjects )
                {
                    var userGroups = ( from m in m_Factory.UserGroupMembership.GetAll( ).Where( m => m.UserId == _user.UserId )
                                       join ug in m_Factory.UserGroup.GetAll( ) on m.UserGroupId equals ug.UserGroupId
                                       select ug ).Cast<UserGroup>( ).ToList( );
                    ( ( User ) _user ).NavUserGroups = userGroups;
                    _user.UserGroupIds = userGroups.Select( g => g.UserGroupId ).ToList( );
                }
                else
                {
                    _user.UserGroupIds = m_Factory.UserGroupMembership.GetAll( )
                        .Where( m => m.UserId == _user.UserId )
                        .Select( m => m.UserGroupId )
                        .ToList( );
                }
            }

            if ( _user.Organization == null )
            {
                ( ( User ) _user ).NavOrganization = ( Organization ) m_Factory.Organization.Get( _user.OrganizationId );
            }
        }
    }
}
