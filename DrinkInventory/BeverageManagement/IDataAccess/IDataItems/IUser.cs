using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the secUsers table in the BevMetMobile Database.
    /// </summary>
    public interface IUser : IDataObject<IUser>
    {
        Guid UserID { get; set; }
        string UserName { get; set; }
        string ProperName { get; set; }
        string Password { get; set; }
        string UserData { get; set; }
    }
}