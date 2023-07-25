using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    /// <summary>
    /// A Business Logic interface which represents the UserSessions table in the BevMetMobile Database.
    /// </summary>
    public interface IBLUserSession : IUserSession
    {
        Guid UserSessionID { get; set; }
        Guid UserID { get; set; }
        Guid SessionID { get; set; }
        DateTime StartTime { get; set; }
        DateTime? EndTime { get; set; }
    }
}