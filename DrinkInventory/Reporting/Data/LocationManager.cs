using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.Data
{
    public partial class OrganizationManager
    {
        protected override void OnSaving( IOrganization _organization )
        {
            var path = _organization.Id + "/";

            var parentId = _organization.ParentId;
            while ( parentId.HasValue )
            {
                path = parentId + "/" + path;
                var parent = DataManagerFactory.Get( ).Manage<IOrganization>( ).Get( parentId.Value );
                parentId = parent.ParentId;
            }

            _organization.Path = path.ToUpper( );
        }
    }
}
