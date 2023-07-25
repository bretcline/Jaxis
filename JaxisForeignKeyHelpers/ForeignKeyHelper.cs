using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using SubSonic.Query;

namespace JaxisForeignKeyHelpers
{
    public class ForeignKeyHelper
    {
        Dictionary<string, ForeignKeyRelationship> FKRs = new Dictionary<string, ForeignKeyRelationship>( );
        
        public ForeignKeyHelper( )
        {
            Initialize( );
        }

        private void Initialize( )
        {
            List<ForeignKeyElement> FKs = new List<ForeignKeyElement>( );
            string SQL = @"
            select o.name as Dependent, null as Required, 0 as IsNullable
		    from sys.objects o
		    left join sys.foreign_keys fk on fk.parent_object_id = o.object_id
		    where o.type = 'U'
			    and fk.object_id is null
			    and not o.name = 'sysdiagrams'
	    
            union
		
            select distinct o.name as Dependent, o2.name as Required, c.is_nullable
		    from sys.foreign_keys fk
		    join sys.objects o on fk.parent_object_id = o.object_id
		    join sys.objects o2 on fk.referenced_object_id = o2.object_id
		    join sys.foreign_key_columns fkc on fk.object_id = fkc.constraint_object_id
		    join sys.columns c on fkc.parent_column_id = c.column_id 
			    and fkc.parent_object_id = c.object_id
		    order by 1";

            using ( DbDataReader rdr = new CodingHorror( SQL ).ExecuteReader( ) )
            {
                while ( rdr.Read( ) )
                {
                    // Build FKs, which is simply a list of all the data returned by the query
                    string d = rdr[ "Dependent" ] as string;
                    string r = rdr[ "Required" ] as string;
                    bool i = ( int ) rdr[ "IsNullable" ] == 1;

                    FKs.Add( new ForeignKeyElement( d, r, i ) );

                    // Build a dictionary, which has one entry for each distinct Dependent Table
                    if ( !FKRs.ContainsKey( d ) )
                    {
                        FKRs[ d ] = new ForeignKeyRelationship( d );
                    }
                }
            }

            // Run back through all the data and build up the Required list for each Dependent table.
            foreach ( ForeignKeyElement k in FKs )
            {
                if ( null != k.RequiredTableName )
                {
                    FKRs[ k.DependentTableName ].Required.Add( FKRs[ k.RequiredTableName ] );
                }
            }
        }

        public List<ForeignKeyRelationship> GetRelationshipsInOrder( )
        {
            List<ForeignKeyRelationship> rc = new List<ForeignKeyRelationship>( );
            foreach ( ForeignKeyRelationship f in FKRs.Values.OrderBy( i => i.Level ) )
            {
                rc.Add( f );
            }
            return rc;
        }
    }
}
