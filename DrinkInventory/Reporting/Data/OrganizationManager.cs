using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.DrinkInventory.Reporting.Data.POCO;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Data
{
    public partial class OrganizationManager
    {
        protected override void OnSaving( IOrganization _organization )
        {
            var path = _organization.OrganizationId + "/";
            var parentId = _organization.ParentId;
            while ( parentId.HasValue )
            {
                path = parentId + "/" + path;
                var parent = Get( parentId.Value );
                parentId = parent.ParentId;
            }

            _organization.Path = path.ToUpper( );
        }

        public override void Hydrate( IOrganization _item, bool _includeRelatedObjects = false )
        {
            if ( _item.UserGroupIds == null )
            {
                if ( _includeRelatedObjects )
                {
                    var userGroups =
                        ( from x in m_Factory.UserGroupXOrganization.GetAll( )
                          join g in m_Factory.UserGroup.GetAll( ) on x.UserGroupId equals g.UserGroupId
                          where x.OrganizationId == _item.OrganizationId
                          select g )
                          .ToList( ).Cast<UserGroup>( ).ToList( );
                    ( ( Organization ) _item ).NavUserGroups = userGroups;
                    _item.UserGroupIds = userGroups.Select( ug => ug.UserGroupId ).ToList( );
                }
                else
                {
                    _item.UserGroupIds = m_Factory.UserGroupXOrganization.GetAll( )
                        .Where( x => x.OrganizationId == _item.OrganizationId )
                        .Select( u => u.UserGroupId )
                        .ToList( );
                }
            }
        }
    }
}
