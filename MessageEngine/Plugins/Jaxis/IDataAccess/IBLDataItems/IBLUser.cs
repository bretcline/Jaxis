using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    /// <summary>
    /// A Business Logic interface which represents the Users table in the BevMetMobile Database.
    /// </summary>
    public interface IBLUser : IUser
    {
        Guid UserID { get; set; }
        string UserName { get; set; }
        string ProperName { get; set; }
        string Password { get; set; }
        string UserData { get; set; }
    }
}