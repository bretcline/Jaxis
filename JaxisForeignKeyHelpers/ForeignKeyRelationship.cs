using System.Collections.Generic;

namespace JaxisForeignKeyHelpers
{
    public class ForeignKeyRelationship
    {
        public string TableName { get; set; }
        public List<ForeignKeyRelationship> Required { get; set; }
        public int Level
        {
            get
            {
                List<string> workList = new List<string> { TableName };
                GetLevel( Required, workList );
                return workList.Count;
            }
        }

        public ForeignKeyRelationship( string _t )
        {
            TableName = _t;
            Required = new List<ForeignKeyRelationship>( );
        }

        public override string ToString( )
        {
            return TableName;
        }

        /// GetLevel recursively processes this table, building up a distinct list (including itself) of tables in the Required 
        /// list(s).
        protected void GetLevel( List<ForeignKeyRelationship> _required, List<string> _visited )
        {
            foreach ( ForeignKeyRelationship fkr in _required )
            {
                if ( !_visited.Contains( fkr.TableName ) )
                {
                    _visited.Add( fkr.TableName );
                    GetLevel( fkr.Required, _visited );
                }
            }
        }
    }
}
