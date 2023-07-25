namespace Jaxis.Inventory.Data
{
    public interface IDataManagerFactory
    {
        IDataManager< T > Manage< T >( );
    }
}