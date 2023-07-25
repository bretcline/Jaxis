using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimetrackerData
{
    public class TimetrackerData
    {
        TimetrackerLinqDataContext m_DataBase = null;

        public TimetrackerData( )
        {
            m_DataBase = new TimetrackerLinqDataContext( );
        }

        public UserSession Login( string _Name, string _Password )
        {
            UserSession rc = null;
            User NewUser = new User( );

            var UserLoggingIn = from c in m_DataBase.Users
                    where c.Login.Equals( _Name ) && c.Password.Equals( _Password )
                    select c;
            if( 1 == UserLoggingIn.Count( ) && UserLoggingIn.First( ).UserVisible == true )
            {
                NewUser = UserLoggingIn.First( );

                if( true == NewUser.Password.Equals( _Password ) )
                {
                    RemoveSessionsForUser( NewUser );

                    rc = new UserSession( );
                    rc.SessionID = Guid.NewGuid( );
                    rc.UserID = NewUser.UserID;


                    m_DataBase.UserSessions.InsertOnSubmit( rc );
                    m_DataBase.SubmitChanges( );
                }
            }

            return rc;
        }

        public void RemoveSessionsForUser( User _TheUser )
        {
            var SessionsToDelete = from c in m_DataBase.UserSessions
                                   where c.UserID == _TheUser.UserID
                                   select c;

            foreach( UserSession US in SessionsToDelete )
            {
                m_DataBase.UserSessions.DeleteOnSubmit( US );
            }
            m_DataBase.SubmitChanges( );
        }

        //public List<vwTimeEntry> GetUserTime( UserSession _Session, DateTime _StartTime, DateTime _EndTime )
        //{
        //    List<vwTimeEntry> Data = new List<vwTimeEntry>( );

        //    var theseTimeEntries = from c in m_DataBase.vwTimeEntries
        //                    where c.StartTime.CompareTo( _StartTime ) >= 0 && c.EndTime.Equals( _EndTime )// < 0
        //                    && c.UserID.Equals( _Session.UserID )
        //                    select c;

        //    foreach( vwTimeEntry entry in theseTimeEntries )
        //    {
        //        Data.Add( entry );
        //    }

        //    return Data;
        //}

        public List<User> GetAllUsers( )
        {
            List<User> ListOfAllUsers = new List<User>( );

            var AllUsers = from u in m_DataBase.Users
                              select u;

            foreach( User u in AllUsers )
            {
                ListOfAllUsers.Add( u );
            }

            return ListOfAllUsers;
        }

        public User GetUserByUserID( int _RecievedUserID )
        {
            User UserToReturn = null;
            var SelectedUsers = from c in m_DataBase.Users
                                where c.UserID == _RecievedUserID
                                select c;

            if( 1 == SelectedUsers.Count( ) )
            {
                UserToReturn = SelectedUsers.First( );
            }

            return UserToReturn;
        }

        //public List<User> GetUsersInProject( Project _TheProject )
        //{
        //    List<User> UserList = new List<User>( );

        //    var TheUsers = from u in m_DataBase.Users
        //                   join uxg in m_DataBase.UserXGroups on u.UserID equals uxg.UserID
        //                   join gxp in m_DataBase.GroupXProjects on uxg.GroupID equals gxp.GroupID
        //                   where gxp.ProjectID == _TheProject.ProjectID && u.UserVisible == true
        //                   select u;

        //    foreach( User AUser in TheUsers )
        //    {
        //        UserList.Add( AUser );
        //    }

        //    return UserList;
        //}

        //public List<User> GetUsersNotInProject( Project _TheProject )
        //{
        //    List<User> UsersInProjectList = new List<User>( );
        //    List<User> AllUsersList = new List<User>( );

        //    var TheUsersInTheProject = from u in m_DataBase.Users
        //                   join uxg in m_DataBase.UserXGroups on u.UserID equals uxg.UserID
        //                   join gxp in m_DataBase.GroupXProjects on uxg.GroupID equals gxp.GroupID
        //                   where gxp.ProjectID == _TheProject.ProjectID && u.UserVisible == true
        //                   select u;

        //    var AllUsers = from u in m_DataBase.Users
        //                   where u.UserVisible == true
        //                   select u;

        //    foreach( User AUser in TheUsersInTheProject )
        //    {
        //        UsersInProjectList.Add( AUser );
        //    }

        //    foreach( User AUser in AllUsers )
        //    {
        //        AllUsersList.Add( AUser );
        //    }
        //    IEnumerable<User> T = AllUsersList.Except( UsersInProjectList );
        //    List<User> ListToReturn = new List<User>( );
        //    ListToReturn.AddRange( T );

        //    return ListToReturn;
        //}

        public List<User> GetUsersInGroup( Group _TheGroup )
        {
            List<User> UserList = new List<User>( );

            var TheUsers = from u in m_DataBase.Users
                           join uxg in m_DataBase.UserXGroups on u.UserID equals uxg.UserID
                           where uxg.GroupID == _TheGroup.GroupID && u.UserVisible == true
                           select u;

            if( null != TheUsers && 0 < TheUsers.Count( ) )
            {
                foreach( User AUser in TheUsers )
                {
                    UserList.Add( AUser );
                }
            }

            return UserList;
        }

        public List<User> GetUsersNotInGroup( Group _TheGroup )
        {
            List<User> UsersInGroupList = new List<User>( );
            List<User> AllUsersList = new List<User>( );

            var TheUsersInTheGroup = from u in m_DataBase.Users
                                       join uxg in m_DataBase.UserXGroups on u.UserID equals uxg.UserID
                                       where uxg.GroupID == _TheGroup.GroupID && u.UserVisible == true
                                       select u;

            var AllUsers = from u in m_DataBase.Users
                           where u.UserVisible == true
                           select u;
            if( null != TheUsersInTheGroup && 0 < TheUsersInTheGroup.Count( ) )
            {
                foreach( User AUser in TheUsersInTheGroup )
                {
                    UsersInGroupList.Add( AUser );
                }
            }

            if( null != AllUsers && 0 < AllUsers.Count( ))
            {
                foreach( User AUser in AllUsers )
                {
                    AllUsersList.Add( AUser );
                }
            }
            IEnumerable<User> T = AllUsersList.Except( UsersInGroupList );
            List<User> ListToReturn = new List<User>( );
            ListToReturn.AddRange( T );

            return ListToReturn;
        }

        public void UpdateUser( User _UserToUpdate )
        {
            User ModifiedUser = null;
            var UserToChange = from c in m_DataBase.Users
                                where c.UserID == _UserToUpdate.UserID
                                select c;

            ModifiedUser = UserToChange.First( );

            if( ModifiedUser.FullName.Equals( _UserToUpdate.FullName ) != true )
            {
                var UsersGroup = from g in m_DataBase.Groups
                                 where g.GroupID == _UserToUpdate.PersonalGroupID
                                 select g;

                if( UsersGroup != null && UsersGroup.Count( ) > 0 )
                {
                    UsersGroup.First( ).GroupName = _UserToUpdate.FullName + " - Group";
                }
            }

            ModifiedUser.Login = _UserToUpdate.Login;
            ModifiedUser.Password = _UserToUpdate.Password;
            ModifiedUser.FirstName = _UserToUpdate.FirstName;
            ModifiedUser.LastName = _UserToUpdate.LastName;
            ModifiedUser.Company = _UserToUpdate.Company;


            m_DataBase.SubmitChanges( );
        }

        public void AddUser( User _UserToAdd )
        {
            _UserToAdd.UserVisible = true;

            Group GroupToAdd = new Group( );
            GroupToAdd.GroupName = _UserToAdd.FullName + " - Group";
            GroupToAdd.Status = 2;
            AddGroup( GroupToAdd );

            _UserToAdd.PersonalGroupID = GroupToAdd.GroupID;
            m_DataBase.Users.InsertOnSubmit( _UserToAdd );
            m_DataBase.SubmitChanges( );
            
            UserXGroup UxGToAdd = new UserXGroup( );
            AddUserToGroup( _UserToAdd.UserID, GroupToAdd.GroupID );
        }

        public void RemoveUser( User _UserToRemove )
        {
            User RemoveThisUser = null;
            var UserToChange = from c in m_DataBase.Users
                               where c.UserID == _UserToRemove.UserID
                               select c;
            if( null != UserToChange && 0 < UserToChange.Count( ) )
            {
                RemoveThisUser = UserToChange.First( );
                RemoveThisUser.UserVisible = false;

                var GroupToRemove = from g in m_DataBase.Groups
                                    join uxg in m_DataBase.UserXGroups on g.GroupID equals uxg.GroupID
                                    where uxg.UserID == RemoveThisUser.UserID
                                    select g;
                
                if( null != GroupToRemove && 0 < GroupToRemove.Count( ) )
                {
                    RemoveGroup( GroupToRemove.First( ) );
                }

                m_DataBase.SubmitChanges( );
            }
        }

        public TimeEntry GetTimeEntryByTEID( int _TimeEntryIDRecieved )
        {
            TimeEntry EntryToReturn = null;

            var FindTimeEntry = from c in m_DataBase.TimeEntries
                                where c.TimeEntryID == _TimeEntryIDRecieved
                                select c;
            if( 1 == FindTimeEntry.Count( ) )
            {
                EntryToReturn = FindTimeEntry.First( );
            }

            return EntryToReturn;
        }

        public List<TimeEntry> GetTERangeByUserID( int _UserID, DateTime _StartTime, DateTime _EndTime )
        {
            List<TimeEntry> TimeEntryList = new List<TimeEntry>( );

            var TEs = from t in m_DataBase.TimeEntries
                      where t.UserID == _UserID && t.StartTime.CompareTo( _StartTime ) >= 0 && t.EndTime.Value.CompareTo( _EndTime ) <= 0
                      select t;

            foreach( TimeEntry Entry in TEs )
            {
                TimeEntryList.Add( Entry );
            }

            return TimeEntryList;
        }

        public List<TimeEntry> GetTERangeByProjectID( int _ProjectID, DateTime _StartTime, DateTime _EndTime )
        {
            List<TimeEntry> TimeEntryList = new List<TimeEntry>( );

            var TEs = from t in m_DataBase.TimeEntries
                      where t.ProjectID == _ProjectID && t.StartTime.CompareTo( _StartTime ) >= 0 && t.EndTime.Value.CompareTo( _EndTime ) <= 0
                      select t;

            foreach( TimeEntry Entry in TEs )
            {
                TimeEntryList.Add( Entry );
            }

            return TimeEntryList;
        }

        //public List<TimeEntry> GetTimeEntriesByUserID( int _UserID )
        //{
        //    List<TimeEntry> TimeEntryList = new List<TimeEntry>( );

        //    var TEs = from t in m_DataBase.TimeEntries
        //              where t.UserID == _UserID
        //              select t;

        //    foreach( TimeEntry Entry in TEs )
        //    {
        //        TimeEntryList.Add( Entry );
        //    }

        //    return TimeEntryList;
        //}

        public void SaveTimeEntry( TimeEntry _ModifiedEntry )
        {
            try
            {
                TimeEntry OldEntry = GetTimeEntryByTEID( _ModifiedEntry.TimeEntryID );

                if( null == OldEntry )
                {
                    if( null == _ModifiedEntry.EndTime )
                    {
                        _ModifiedEntry.EndTime = DateTime.MinValue.AddYears( 1900 );
                    }

                    _ModifiedEntry.StartTime.AddSeconds( _ModifiedEntry.StartTime.Second * -1 );
                    _ModifiedEntry.StartTime.AddMilliseconds( _ModifiedEntry.StartTime.Millisecond * -1 );
                    _ModifiedEntry.EndTime.Value.AddSeconds( _ModifiedEntry.EndTime.Value.Second * -1 );
                    _ModifiedEntry.EndTime.Value.AddMilliseconds( _ModifiedEntry.EndTime.Value.Millisecond * -1 );

                    m_DataBase.TimeEntries.InsertOnSubmit( _ModifiedEntry );
                }
                else
                {
                    OldEntry.Notes = _ModifiedEntry.Notes;
                    OldEntry.StartTime = _ModifiedEntry.StartTime;
                    OldEntry.EndTime = _ModifiedEntry.EndTime;
                    OldEntry.ProjectID = _ModifiedEntry.ProjectID;

                    DateTime RoundedTime = OldEntry.StartTime;
                    RoundedTime = RoundedTime.AddSeconds( RoundedTime.Second * -1 );
                    RoundedTime = RoundedTime.AddMilliseconds( RoundedTime.Millisecond * -1 );
                    OldEntry.StartTime = RoundedTime;

                    RoundedTime = OldEntry.EndTime.Value;
                    RoundedTime = RoundedTime.AddSeconds( RoundedTime.Second * -1 );
                    RoundedTime = RoundedTime.AddMilliseconds( RoundedTime.Millisecond * -1 );
                    OldEntry.EndTime = RoundedTime;

                    //OldEntry.StartTime.AddSeconds( OldEntry.StartTime.Second * -1 );
                    //OldEntry.StartTime.AddMilliseconds( OldEntry.StartTime.Millisecond * -1 );
                    //OldEntry.EndTime.Value.AddSeconds( OldEntry.EndTime.Value.Second * -1 );
                    //OldEntry.EndTime.Value.AddMilliseconds( OldEntry.EndTime.Value.Millisecond * -1 );
                }

                m_DataBase.SubmitChanges( );
            }

            catch( Exception err )
            {
                System.Diagnostics.Debug.WriteLine( err.Message );
            }
        }

        public TimeEntry StartTimeEntry( UserSession CurrentSession, TimeEntry _TimeEntry )
        {
            var SelectedTimeEntries = from c in m_DataBase.TimeEntries
                                      where c.UserID == CurrentSession.User.UserID && c.EndTime == DateTime.MinValue.AddYears( 1900 )
                                      select c;

            foreach( TimeEntry TE in SelectedTimeEntries )
            {
                TE.EndTime = DateTime.Now;
            }

            _TimeEntry.StartTime = DateTime.Now;

            SaveTimeEntry( _TimeEntry );
            
            return _TimeEntry;
        }

        public TimeEntry StopTimeEntry( Guid _SessionID, TimeEntry _TimeEntry )
        {
            _TimeEntry.EndTime = DateTime.Now;

            SaveTimeEntry( _TimeEntry );

            return _TimeEntry;
        }

        public void DeleteTimeEntry( int _IDToDelete )
        {
            TimeEntry EntryToDelete = GetTimeEntryByTEID( _IDToDelete );

            m_DataBase.TimeEntries.DeleteOnSubmit( EntryToDelete );
            m_DataBase.SubmitChanges( );
        }

        public List<TimeEntry> GetActiveTimeEntries( Guid _SessionID, List<int> _ProjectIDs )
        {
            List<TimeEntry> TEList = new List<TimeEntry>( );

            foreach( int ProjectID in _ProjectIDs )
            {
                Project TheProject = GetProjectByProjectID( ProjectID );

                var TEWithProject = from c in m_DataBase.TimeEntries
                                    where c.ProjectID == TheProject.ProjectID
                                    select c;

                TEList.Add( TEWithProject.First( ) );
            }

            return TEList;
        }

        public Project GetProjectByProjectID( int _RecievedProjectID )
        {
            Project ProjectToReturn = null;
            var SelectedProjects = from c in m_DataBase.Projects
                                   where c.ProjectID == _RecievedProjectID
                                select c;

            if( 1 == SelectedProjects.Count( ) )
            {
                ProjectToReturn = SelectedProjects.First( );
            }

            return ProjectToReturn;
        }

        public List<Project> GetAllProjectsForUser( Guid _SessionID, bool _NeedAddNew )
        {
            List<Project> ProjectList = new List<Project>( );
            UserSession CurrentSession = GetSessionBySessionID( _SessionID );

            if( _NeedAddNew == true )
            {
                Project DefaultItem = new Project( );
                DefaultItem.Name = "<<Add New Project>>";
                DefaultItem.ProjectID = -1;

                ProjectList.Add( DefaultItem );
            }

            var TheProjects = from u in m_DataBase.Users
                              join uxg in m_DataBase.UserXGroups on u.UserID equals uxg.UserID
                              join gxp in m_DataBase.GroupXProjects on uxg.GroupID equals gxp.GroupID
                              join p in m_DataBase.Projects on gxp.ProjectID equals p.ProjectID
                              where p.ProjectVisible == true && CurrentSession.UserID == u.UserID
                              select p;

            foreach( Project ProjectToAdd in TheProjects )
            {
                ProjectList.Add( ProjectToAdd );
            }

            return ProjectList;
        }

        public List<Project> GetAllProjects( )
        {
            List<Project> ListOfAllProjects = new List<Project>( );

            var AllProjects = from p in m_DataBase.Projects
                              select p;

            foreach( Project p in AllProjects )
            {
                ListOfAllProjects.Add( p );
            }

            return ListOfAllProjects;
        }

        public List<Project> GetProjectsInGroup( Group _TheGroup )
        {
            List<Project> ProjectList = new List<Project>( );

            var TheProjects = from p in m_DataBase.Projects
                           join gxp in m_DataBase.GroupXProjects on p.ProjectID equals gxp.ProjectID
                           where gxp.GroupID == _TheGroup.GroupID && p.ProjectVisible == true
                           select p;

            if( null != TheProjects && 0 < TheProjects.Count( ) )
            {
                foreach( Project AProject in TheProjects )
                {
                    ProjectList.Add( AProject );
                }
            }

            return ProjectList;
        }

        public List<Project> GetProjectsNotInGroup( Group _TheGroup )
        {
            List<Project> ProjectsInGroupList = new List<Project>( );
            List<Project> AllProjectsList = new List<Project>( );

            var TheProjectsInTheGroup = from p in m_DataBase.Projects
                                        join gxp in m_DataBase.GroupXProjects on p.ProjectID equals gxp.ProjectID
                                        where gxp.GroupID == _TheGroup.GroupID && p.ProjectVisible == true
                                     select p;

            var AllProjects = from p in m_DataBase.Projects
                              where p.ProjectVisible == true
                           select p;
            if( null != TheProjectsInTheGroup && 0 < TheProjectsInTheGroup.Count( ) )
            {
                foreach( Project AProject in TheProjectsInTheGroup )
                {
                    ProjectsInGroupList.Add( AProject );
                }
            }

            if( null != AllProjects && 0 < AllProjects.Count( ) )
            {
                foreach( Project AProject in AllProjects )
                {
                    AllProjectsList.Add( AProject );
                }
            }
            IEnumerable<Project> T = AllProjectsList.Except( ProjectsInGroupList );
            List<Project> ListToReturn = new List<Project>( );
            ListToReturn.AddRange( T );

            return ListToReturn;
        }

        //public List<Project> GetAllProjectsForSession( Guid _SessionID, bool _NeedAddNew )
        //{
        //    List<Project> ProjectList = new List<Project>( );

        //    if( _NeedAddNew == true )
        //    {
        //        Project DefaultItem = new Project( );
        //        DefaultItem.Name = "<<Add New Project>>";
        //        DefaultItem.ProjectID = -1;

        //        ProjectList.Add( DefaultItem );
        //    }

        //    var TheProjects = from u in m_DataBase.Users
        //                      join uxp in m_DataBase.UserXProjects on u.UserID equals uxp.UserID
        //                      join p in m_DataBase.Projects on uxp.ProjectID equals p.ProjectID
        //                      join s in m_DataBase.UserSessions on u.UserID equals s.UserID
        //                      where p.ProjectVisible == true && s.SessionID == _SessionID
        //                      select p;

        //    foreach( Project ProjectToAdd in TheProjects )
        //    {
        //        ProjectList.Add( ProjectToAdd );
        //    }

        //    return ProjectList;
        //}

        public List<Project> GetActiveProjects( Guid _SessionID, DateTime _AfterDate )
        {
            List<int> ProjectIDs = new List<int>( );

            List<Project> ProjectList = new List<Project>( );

            UserSession TheSession = GetSessionBySessionID( _SessionID );

            var TimeEntriesInSession = from c in m_DataBase.TimeEntries
                                       where c.UserID == TheSession.User.UserID && c.StartTime >= _AfterDate
                                       select c;

            foreach( TimeEntry TE in TimeEntriesInSession )
            {
                int ID = TE.ProjectID;

                if( false == ProjectIDs.Contains( ID ) && true == GetProjectByProjectID( ID ).ProjectVisible )
                {
                    ProjectIDs.Add( ID );
                    ProjectList.Add( GetProjectByProjectID( ID ) );
                }
            }

            return ProjectList;
        }

        public void UpdateProject( Project _ProjectToUpdate )
        {
            Project ModifiedProject = null;
            var ProjectToChange = from c in m_DataBase.Projects
                                  where c.ProjectID == _ProjectToUpdate.ProjectID
                               select c;

            ModifiedProject = ProjectToChange.First( );
            ModifiedProject.Name = _ProjectToUpdate.Name;
            ModifiedProject.Description= _ProjectToUpdate.Description;

            m_DataBase.SubmitChanges( );
        }

        public Project AddProject( Project _ProjectToAdd, Guid _SessionID )
        {
            try
            {
                _ProjectToAdd.ProjectVisible = true;
                m_DataBase.Projects.InsertOnSubmit( _ProjectToAdd );
                m_DataBase.SubmitChanges( );

                UserSession Session = GetSessionBySessionID( _SessionID );

                //The following adds the project just created to the group containing the user who created it.

                var TheUxG = from u in m_DataBase.Users
                          join uxg in m_DataBase.UserXGroups on u.UserID equals uxg.UserID
                          join g in m_DataBase.Groups on uxg.GroupID equals g.GroupID
                          where u.PersonalGroupID == g.GroupID
                          select uxg;

                if( TheUxG.First( ) != null )
                {
                    GroupXProject NewGxP = new GroupXProject( );
                    NewGxP.ProjectID = _ProjectToAdd.ProjectID;
                    NewGxP.GroupID = TheUxG.First( ).GroupID;
                    m_DataBase.GroupXProjects.InsertOnSubmit( NewGxP );

                    m_DataBase.SubmitChanges( );
                }
            }
            catch( Exception err )
            {
                string Error = err.Message;
            }
            return _ProjectToAdd;
        }

        public void AddProjectToGroup( int _TheProjectID, int _TheGroupID )
        {
            GroupXProject GxPToAdd = new GroupXProject( );

            GxPToAdd.GroupID = _TheGroupID;
            GxPToAdd.ProjectID = _TheProjectID;

            var GxPsWithSameIDs = from gxp in m_DataBase.GroupXProjects
                                  where gxp.GroupID == _TheGroupID && gxp.ProjectID == _TheProjectID
                                  select gxp;

            if( GxPsWithSameIDs.Count( ) == 0 || GxPsWithSameIDs == null )
            {
                m_DataBase.GroupXProjects.InsertOnSubmit( GxPToAdd );
                m_DataBase.SubmitChanges( );
            }
        }

        public void RemoveProject( Project _ProjectToRemove )
        {
            Project RemoveThisProject = null;
            var ProjectToChange = from c in m_DataBase.Projects
                                  where c.ProjectID == _ProjectToRemove.ProjectID
                               select c;
            RemoveThisProject = ProjectToChange.First( );

            RemoveThisProject.ProjectVisible = false;
            m_DataBase.SubmitChanges( );
        }

        public void RemoveProjectFromGroup( int _TheProjectID, int _TheGroupID )
        {
            var GxPToRemove = from gxp in m_DataBase.GroupXProjects
                              where gxp.ProjectID == _TheProjectID && gxp.GroupID == _TheGroupID
                              select gxp;

            if( GxPToRemove.Count( ) >= 1 )
            {
                m_DataBase.GroupXProjects.DeleteOnSubmit( GxPToRemove.First( ) );
            }

            m_DataBase.SubmitChanges( );
        }

        public UserSession GetSessionBySessionID( Guid _RecievedSessionID )
        {
            UserSession SessionToReturn = null;
            var SelectedSessions = from c in m_DataBase.UserSessions
                                where c.SessionID == _RecievedSessionID
                                select c;

            if( 1 == SelectedSessions.Count( ) )
            {
                SessionToReturn = SelectedSessions.First( );
            }

            return SessionToReturn;
        }

        //public void AddUserToProject( User _UserToAdd, Project _TheProject )
        //{
        //    UserXProject UxPToAdd = new UserXProject( );

        //    UxPToAdd.UserID = _UserToAdd.UserID;
        //    UxPToAdd.ProjectID = _TheProject.ProjectID;

        //    m_DataBase.UserXProjects.InsertOnSubmit( UxPToAdd );
        //    m_DataBase.SubmitChanges( );
        //}

        public void AddUserToGroup( int _UserToAddID, int _GroupID )
        {
            UserXGroup UxGToAdd = new UserXGroup( );

            UxGToAdd.UserID = _UserToAddID;
            UxGToAdd.GroupID = _GroupID;

            m_DataBase.UserXGroups.InsertOnSubmit( UxGToAdd );
            m_DataBase.SubmitChanges( );
        }

        //public void RemoveUserFromProject( User _UserToRemove, Project _TheProject )
        //{
        //    //List<UserXProject> UxPList = new List<UserXProject>( );

        //    var UxPsToRemove = from uxp in m_DataBase.UserXProjects
        //                       where uxp.UserID == _UserToRemove.UserID && uxp.ProjectID == _TheProject.ProjectID
        //                       select uxp;

        //    foreach( UserXProject UxP in UxPsToRemove )
        //    {
        //        m_DataBase.UserXProjects.DeleteOnSubmit( UxP );
        //    }

        //    m_DataBase.SubmitChanges( );
        //}

        public void RemoveUserFromGroup( int _UserToRemoveID, int _GroupID )
        {
            var UxGsToRemove = from uxg in m_DataBase.UserXGroups
                               where uxg.UserID == _UserToRemoveID && uxg.GroupID == _GroupID
                               select uxg;

            if( null != UxGsToRemove && UxGsToRemove.Count( ) > 0 )
            {
                foreach( UserXGroup UxG in UxGsToRemove )
                {
                    m_DataBase.UserXGroups.DeleteOnSubmit( UxG );
                }
            }

            m_DataBase.SubmitChanges( );
        }

        public Group GetGroupByGroupID( int _TheGroupID )
        {
            Group GroupToReturn = new Group( );

            var GroupsWithID = from g in m_DataBase.Groups
                               where g.GroupID == _TheGroupID
                               select g;

            if( null != GroupsWithID && GroupsWithID.Count( ) > 0 )
            {
                GroupToReturn = GroupsWithID.First( );
            }

            return GroupToReturn;
        }

        public List<Group> GetAllGroups( )
        {
            List<Group> GroupList = new List<Group>( );

            var AllGroups = from g in m_DataBase.Groups
                            select g;

            if( null != AllGroups && AllGroups.Count( ) > 0 )
            {
                foreach( Group g in AllGroups )
                {
                    GroupList.Add( g );
                }
            }

            return GroupList;
        }

        public void AddGroup( Group _GroupToAdd )
        {
            m_DataBase.Groups.InsertOnSubmit( _GroupToAdd );

            m_DataBase.SubmitChanges( );
        }

        public void UpdateGroup( Group _GroupToUpdate )
        {
            Group ModifiedGroup = null;
            var GroupToChange = from g in m_DataBase.Groups
                                where g.GroupID == _GroupToUpdate.GroupID
                               select g;

            if( GroupToChange != null && GroupToChange.Count( ) > 0 )
            {
                ModifiedGroup = GroupToChange.First( );
            }

            ModifiedGroup.GroupName = _GroupToUpdate.GroupName;
            ModifiedGroup.GroupDescription = _GroupToUpdate.GroupDescription;
            ModifiedGroup.Status = _GroupToUpdate.Status;

            m_DataBase.SubmitChanges( );
        }

        public void RemoveGroup( Group _GroupToRemove )
        {
            Group RemoveThisGroup = null;
            var GroupToChange = from g in m_DataBase.Groups
                                where g.GroupID == _GroupToRemove.GroupID
                               select g;

            if( GroupToChange != null && GroupToChange.Count( ) > 0 )
            {
                RemoveThisGroup = GroupToChange.First( );
            }

            //var RelationshipsWithGroup = from uxg in m_DataBase.UserXGroups
            //                             where uxg.GroupID == _GroupToRemove.GroupID
            //                             from gxp in m_DataBase.GroupXProjects
            //                             where gxp.GroupID == _GroupToRemove.GroupID
            //                             from gxs in m_DataBase.GroupXSecurities
            //                             where gxs.GroupID == _GroupToRemove.GroupID
            //                             select new { uxg, gxp, gxs };

            var UxGRelations = from uxg in m_DataBase.UserXGroups
                               where uxg.GroupID == _GroupToRemove.GroupID
                               select uxg;

            var GxPRelations = from gxp in m_DataBase.GroupXProjects
                               where gxp.GroupID == _GroupToRemove.GroupID
                               select gxp;

            var GxSRelations = from gxs in m_DataBase.GroupXSecurities
                               where gxs.GroupID == _GroupToRemove.GroupID
                               select gxs;

            if( null != UxGRelations && UxGRelations.Count( ) > 0 )
            {
                foreach( UserXGroup uxg in UxGRelations )
                {
                    m_DataBase.UserXGroups.DeleteOnSubmit( uxg );
                }
            }

            if( null != GxPRelations && GxPRelations.Count( ) > 0 )
            {
                foreach( GroupXProject gxp in GxPRelations )
                {
                    m_DataBase.GroupXProjects.DeleteOnSubmit( gxp );
                }
            }

            if( null != GxSRelations && GxSRelations.Count( ) > 0 )
            {
                foreach( GroupXSecurity gxs in GxSRelations )
                {
                    m_DataBase.GroupXSecurities.DeleteOnSubmit( gxs );
                }
            }

            m_DataBase.Groups.DeleteOnSubmit( RemoveThisGroup );

            m_DataBase.SubmitChanges( );
        }
    }
}
