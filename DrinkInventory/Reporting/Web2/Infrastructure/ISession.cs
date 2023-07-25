namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public interface ISession
    {
        void AttemptRestoreFromCookie();
        object this[string _key] { get; set; }
    }
}
