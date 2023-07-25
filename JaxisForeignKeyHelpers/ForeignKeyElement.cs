
namespace JaxisForeignKeyHelpers
{
    public class ForeignKeyElement
    {
        public string DependentTableName;
        public string RequiredTableName;
        public bool IsNullable;

        public ForeignKeyElement( string _dependent, string _required, bool _isNullable )
        {
            DependentTableName = _dependent;
            RequiredTableName = _required;
            IsNullable = _isNullable;
        }
    }
}
