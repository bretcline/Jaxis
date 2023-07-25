using System;
using System.Collections.Generic;
using System.Web.Services;
using TimetrackerData;

/// <summary>
/// Summary description for TimeEntry
/// </summary>
[WebService( Namespace = "http://tempuri.org/" )]
[WebServiceBinding( ConformsTo = WsiProfiles.BasicProfile1_1 )]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class TimeEntryService : System.Web.Services.WebService
{

    public TimeEntryService( )
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public Guid Login( String _UID, String _PWD )
    {
        Guid rc = Guid.Empty;
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession MySession = Data.Login( _UID, _PWD );

        if( null != MySession )
        {
            rc = MySession.SessionID;
        }
        return rc;
    }

    [WebMethod]
    public void Logout( Guid _SessionID )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );
        if( null != Current )
        {
            Data.RemoveSessionsForUser( Current.User );
        }
    }

    [WebMethod]
    public TimeEntry Start( Guid _SessionID, TimeEntry _TimeEntry )
    {
        TimeEntry TE = new TimeEntry( );
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );
        if( null != Current )
        {
            _TimeEntry.UserID = Current.UserID;
            TE = Data.StartTimeEntry( Current, _TimeEntry );
        }
        return TE;
    }

    [WebMethod]
    public TimeEntry Stop( Guid _SessionID, TimeEntry _TimeEntry )
    {
        TimeEntry TE = new TimeEntry( );
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );
        if( null != Current )
        {
            _TimeEntry.UserID = Current.UserID;

            TE = Data.StopTimeEntry( _SessionID, _TimeEntry );
        }

        return TE;
    }

    [WebMethod]
    public List<Project> GetProjects( Guid _SessionID, bool _NeedAddNew )
    {
        List<Project> MyList = null;

        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );
        if( null != Current )
        {
            MyList = Data.GetAllProjectsForUser( _SessionID, _NeedAddNew );
        }

        return MyList;
    }

    [WebMethod]
    public List<Project> GetProjectsInGroup( Guid _SessionID, Group _RecievedGroup )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        List<Project> ListToReturn = new List<Project>( );

        if( null != Current )
        {
            ListToReturn = Data.GetProjectsInGroup( _RecievedGroup );
        }

        return ListToReturn;
    }

    [WebMethod]
    public List<Project> GetProjectsNotInGroup( Guid _SessionID, Group _RecievedGroup )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        List<Project> ListToReturn = new List<Project>( );

        if( null != Current )
        {
            ListToReturn = Data.GetProjectsNotInGroup( _RecievedGroup );
        }

        return ListToReturn;
    }

    [WebMethod]
    public Project GetProjectByProjectID( Guid _SessionID, int _ProjectID )
    {
        Project TheProject = new Project( );

        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );
        if( null != Current )
        {
            TheProject = Data.GetProjectByProjectID( _ProjectID );
        }

        return TheProject;
    }

    [WebMethod]
    public void RemoveAProject( Guid _SessionID, Project _ProjectToRemove )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );
        if( null != Current )
        {
            Data.RemoveProject( _ProjectToRemove );
        }
    }

    [WebMethod]
    public List<Project> GetActiveProjects( Guid _SessionID, DateTime _AfterDate )
    {
        List<Project> MyList = null;

        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );
        if( null != Current )
        {
            MyList = Data.GetActiveProjects( _SessionID, _AfterDate );
        }

        return MyList;
    }

    [WebMethod]
    public List<TimeEntry> GetActiveTimeEntries( Guid _SessionID, List<int> _ProjectIDs )
    {
        List<TimeEntry> MyList = null;

        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );
        if( null != Current )
        {
            MyList = Data.GetActiveTimeEntries( _SessionID, _ProjectIDs );
        }

        return MyList;
    }

    [WebMethod]
    public TimeEntry SaveTimeEntry( Guid _SessionID, TimeEntry _TheTE )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );
        if( null != Current )
        {
            _TheTE.Notes = DateTime.Now + System.Environment.NewLine + _TheTE.Notes;
            Data.SaveTimeEntry( _TheTE );
        }

        return _TheTE;
    }

    [WebMethod]
    public Project AddOrEditProject( Guid _SessionID, Project _RecievedProject, bool _IsNew )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        if( null != Current )
        {
            if( true == _IsNew )
            {
                _RecievedProject = Data.AddProject( _RecievedProject, _SessionID );
            }
            else
            {
                Data.UpdateProject( _RecievedProject );
            }
        }

        return _RecievedProject;
    }

    [WebMethod]
    public void AddProjectToGroup( Guid _SessionID, int _ProjectToAddID, int _GroupID )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        if( null != Current )
        {
            Data.AddProjectToGroup( _ProjectToAddID, _GroupID );
        }
    }

    [WebMethod]
    public void RemoveProjectFromGroup( Guid _SessionID, int _ProjectToRemoveID, int _GroupID )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        if( null != Current )
        {
            Data.RemoveProjectFromGroup( _ProjectToRemoveID, _GroupID );
        }
    }

    [WebMethod]
    public UserSession GetSession( Guid _SessionID )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        return Current;
    }

    [WebMethod]
    public TimeEntry GetTimeEntryByTEID( Guid _SessionID, int _TEID )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        TimeEntry TheTimeEntry = new TimeEntry( );

        if( null != Current )
        {
            TheTimeEntry = Data.GetTimeEntryByTEID( _TEID );
        }

        return TheTimeEntry;
    }

    //[WebMethod]
    //public List<TimeEntry> GetTimeEntriesByUserID( Guid _SessionID, int _UserID )
    //{
    //    TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

    //    UserSession Current = Data.GetSessionBySessionID( _SessionID );

    //    List<TimeEntry> TheTimeEntries = new List<TimeEntry>( );

    //    if( null != Current )
    //    {
    //        TheTimeEntries = Data.GetTimeEntriesByUserID( _UserID );
    //    }

    //    return TheTimeEntries;
    //}

    [WebMethod]
    public List<TimeEntry> GetTERangeByUserID( Guid _SessionID, int _UserID, DateTime _StartTime, DateTime _EndTime )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        List<TimeEntry> TheTimeEntries = new List<TimeEntry>( );

        if( null != Current )
        {
            TheTimeEntries = Data.GetTERangeByUserID( _UserID, _StartTime, _EndTime );
        }

        return TheTimeEntries;
    }

    [WebMethod]
    public List<TimeEntry> GetTERangeByProjectID( Guid _SessionID, int _ProjectID, DateTime _StartTime, DateTime _EndTime )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        List<TimeEntry> TheTimeEntries = new List<TimeEntry>( );

        if( null != Current )
        {
            TheTimeEntries = Data.GetTERangeByProjectID( _ProjectID, _StartTime, _EndTime );
        }

        return TheTimeEntries;
    }

    [WebMethod]
    public User GetUserByUserID( Guid _SessionID, int _UserID )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        User TheUser = new User( );

        if( null != Current )
        {
            TheUser = Data.GetUserByUserID( _UserID );
        }

        return TheUser;
    }

    [WebMethod]
    public void AddOrEditUser( Guid _SessionID, User _RecievedUser, bool _IsNew )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        if( null != Current )
        {
            if( true == _IsNew )
            {
                Data.AddUser( _RecievedUser );
            }
            else
            {
                Data.UpdateUser( _RecievedUser );
            }
        }
    }

    [WebMethod]
    public void AddUserToGroup( Guid _SessionID, int _UserToAddID, int _GroupID )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        if( null != Current )
        {
            Data.AddUserToGroup( _UserToAddID, _GroupID );
        }
    }

    [WebMethod]
    public void RemoveUser( Guid _SessionID, User _RecievedUser )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        if( null != Current )
        {
            Data.RemoveUser( _RecievedUser );
        }
    }

    [WebMethod]
    public void RemoveUserFromGroup( Guid _SessionID, int _UserToRemoveID, int _GroupID )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        if( null != Current )
        {
            Data.RemoveUserFromGroup( _UserToRemoveID, _GroupID );
        }
    }

    //[WebMethod]
    //public List<User> GetUsersInProject( Guid _SessionID, Project _RecievedProject )
    //{
    //    TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

    //    UserSession Current = Data.GetSessionBySessionID( _SessionID );

    //    List<User> ListToReturn = new List<User>( );

    //    if( null != Current )
    //    {
    //        ListToReturn = Data.GetUsersInProject( _RecievedProject );
    //    }

    //    return ListToReturn;
    //}

    [WebMethod]
    public List<User> GetUsersInGroup( Guid _SessionID, Group _RecievedGroup )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        List<User> ListToReturn = new List<User>( );

        if( null != Current )
        {
            ListToReturn = Data.GetUsersInGroup( _RecievedGroup );
        }

        return ListToReturn;
    }

    //[WebMethod]
    //public List<User> GetUsersNotInProject( Guid _SessionID, Project _RecievedProject )
    //{
    //    TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

    //    UserSession Current = Data.GetSessionBySessionID( _SessionID );

    //    List<User> ListToReturn = new List<User>( );

    //    if( null != Current )
    //    {
    //        ListToReturn = Data.GetUsersNotInProject( _RecievedProject );
    //    }

    //    return ListToReturn;
    //}

    [WebMethod]
    public List<User> GetUsersNotInGroup( Guid _SessionID, Group _RecievedGroup )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        List<User> ListToReturn = new List<User>( );

        if( null != Current )
        {
            ListToReturn = Data.GetUsersNotInGroup( _RecievedGroup );
        }

        return ListToReturn;
    }

    //[WebMethod]
    //public void AddUserToProject( Guid _SessionID, User _UserToAdd, Project _TheProject )
    //{
    //    TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

    //    UserSession Current = Data.GetSessionBySessionID( _SessionID );

    //    if( null != Current )
    //    {
    //        Data.AddUserToProject( _UserToAdd, _TheProject );
    //    }
    //}

    [WebMethod]
    public List<Group> GetAllGroups( Guid _SessionID )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        List<Group> ListToReturn = new List<Group>( );

        if( null != Current )
        {
            ListToReturn = Data.GetAllGroups( );
        }

        return ListToReturn;
    }

    //[WebMethod]
    //public void RemoveUserFromProject( Guid _SessionID, User _UserToRemove, Project _TheProject )
    //{
    //    TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

    //    UserSession Current = Data.GetSessionBySessionID( _SessionID );

    //    if( null != Current )
    //    {
    //        Data.RemoveUserFromProject( _UserToRemove, _TheProject );
    //    }
    //}

    [WebMethod]
    public List<Project> GetAllProjects( Guid _SessionID )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        List<Project> ListOfAllProjects = new List<Project>( );

        if( null != Current )
        {
            ListOfAllProjects = Data.GetAllProjects( );
        }

        return ListOfAllProjects;
    }

    [WebMethod]
    public List<User> GetAllUsers( Guid _SessionID )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        List<User> ListOfAllUsers = new List<User>( );

        if( null != Current )
        {
            ListOfAllUsers = Data.GetAllUsers( );
        }

        return ListOfAllUsers;
    }

    [WebMethod]
    public Group GetGroupByGroupID( Guid _SessionID, int _TheGroupID )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        Group GroupToReturn = new Group( );

        if( null != Current )
        {
            GroupToReturn = Data.GetGroupByGroupID( _TheGroupID );
        }

        return GroupToReturn;
    }

    [WebMethod]
    public void AddOrEditGroup( Guid _SessionID, Group _RecievedGroup, bool _IsNew )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        if( null != Current )
        {
            if( true == _IsNew )
            {
                Data.AddGroup( _RecievedGroup );
            }
            else
            {
                Data.UpdateGroup( _RecievedGroup );
            }
        }
    }

    [WebMethod]
    public void RemoveGroup( Guid _SessionID, Group _RecievedGroup )
    {
        TimetrackerData.TimetrackerData Data = new TimetrackerData.TimetrackerData( );

        UserSession Current = Data.GetSessionBySessionID( _SessionID );

        if( null != Current )
        {
            Data.RemoveGroup( _RecievedGroup );
        }
    }
}

