using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the secUserSessions table in the BevMetMobile Database.
    /// </summary>
    public interface IUserSession : IDataObject<IUserSession>
    {
        Guid UserSessionID { get; set; }
        Guid SessionID { get; set; }
        Guid UserID { get; set; }
    }

    public interface IUsersXGroup : IDataObject<IUsersXGroup>
    {
        Guid UserID { get; set; }
        Guid GroupID { get; set; }
    }

    public interface IUsersXOrganization : IDataObject<IUsersXOrganization>
    {
        Guid UxOID { get; set; }
        Guid UserID { get; set; }
        Guid OrganizationID { get; set; }
    }


}