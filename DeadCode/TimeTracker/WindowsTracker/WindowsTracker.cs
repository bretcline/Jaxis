using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsTracker.TimeTracker;

namespace WindowsTracker
{
    public partial class Tracker : Form
    {
        TimeTracker.TimeEntryServiceSoapClient m_Service = null;
        Project m_SelectedProject = new Project( );
        Guid m_Session = new Guid( );
        TimeEntry m_CurrentTE = null;
        int m_PreviousSelectedIndex = -1;
        List<Project> m_ComboBoxList = new List<Project>( );


        public Tracker( )
        {
            m_Service = new TimeTracker.TimeEntryServiceSoapClient( );

            InitializeComponent( );
        }

        private void tmrElapsedTime_Tick( object sender, EventArgs e )
        {
            DateTime? Stop = ( m_CurrentTE.EndTime.Equals( DateTime.MinValue.AddYears( 1900 ) ) ) ? DateTime.Now : m_CurrentTE.EndTime;
            
            TimeSpan? Elapsed = Stop - m_CurrentTE.StartTime;

            ttlblElapesedTime.Text = Elapsed.Value.Hours + ":" + Elapsed.Value.Minutes;
        }

        private void frmWindowsTracker_Load( object sender, EventArgs e )
        {
            m_Service = new TimeTracker.TimeEntryServiceSoapClient( );
            Login LoginForm = new Login( );
            LoginForm.UserName = "Other";

            if( DialogResult.OK == LoginForm.ShowDialog( ) )
            {
                bool StopTrying = false;
                m_Session = m_Service.Login( LoginForm.UserName, LoginForm.Password );

                while( m_Session.Equals( Guid.Empty ) && false == StopTrying )
                {
                    MessageBox.Show( "Incorrect username or password." );

                    if( DialogResult.OK == LoginForm.ShowDialog( ) )
                    {
                        m_Session = m_Service.Login( LoginForm.UserName, LoginForm.Password );
                    }
                    else
                    {
                        this.Close( );
                        StopTrying = true;
                    }
                }

                UpdateDDL( );

                //List<int> AllProjectIDs = new List<int>( );

                //foreach( Project ThisProject in ComboBoxList )
                //{
                //    AllProjectIDs.Add( ThisProject.ProjectID );
                //}
                //ArrayOfInt Values = AllProjectIDs as ArrayOfInt;
                //List<TimeEntry> ActiveTimeEntries = new List<TimeEntry>( );
                //ActiveTimeEntries.AddRange( m_Service.GetActiveTimeEntries( m_Session, Values ) );
            }
            else
            {
                this.Close( );
            }
        }

        private void btnNotes_Click( object sender, EventArgs e )
        {
            if( null != m_CurrentTE )
            {
                Notes NotesForm = new Notes( m_CurrentTE, m_Session );

                NotesForm.ShowDialog( );

                m_CurrentTE = NotesForm.CurrentTimeEntry;
            }
            else
            {
                MessageBox.Show( "No current time entry." );
            }
        }

        private void btnStart_Click( object sender, EventArgs e )
        {
            if( btnStart.Text.Equals( "Start") )
            {
                StopTime( );

                m_CurrentTE = new TimeEntry( );
                m_CurrentTE.ProjectID = Convert.ToInt32( ddlProjects.SelectedValue );
                m_CurrentTE = m_Service.Start( m_Session, m_CurrentTE );
                btnStart.Text = "Stop";
                tmrElapsedTime.Enabled = true;
                ttlblElapesedTime.ForeColor = Color.Black;
                ttlblProject.Text = ddlProjects.Text + ": ";
            }
            else if( btnStart.Text.Equals( "Stop" ) )
            {
                StopTime( );
            }
            else //btnStart.Text.Equals("Add")
            {
                StopTime( );

                NewProject NPForm = new NewProject( m_Session, ref m_ComboBoxList );
                if( DialogResult.OK == NPForm.ShowDialog( ) )
                {
                    UpdateDDL( );

                    int i = 0;
                    for( i = 0; i < ddlProjects.Items.Count; i++ )
                    {
                        Project P = ddlProjects.Items[i] as Project;
                        if( null != P && NPForm.ProjectAdded.Name.Equals( P.Name ) )
                        {
                            break;
                        }
                    }

                    if( i < ddlProjects.Items.Count )
                    {
                        ddlProjects.SelectedIndex = i;
                    }
                    //int Index = ddlProjects.Items.IndexOf( NPForm.ProjectAdded );
                    //ddlProjects.SelectedIndex = Index;
                }
            }
        }

        private void ddlProjects_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( m_PreviousSelectedIndex != ddlProjects.SelectedIndex )
            {
                if( true == ddlProjects.SelectedValue.Equals( -1) )
                {
                    btnStart.Text = "Add";
                }
                else
                {
                    btnStart.Text = "Start";
                }
            }
        }

        private void frmWindowsTracker_FormClosing( object sender, FormClosingEventArgs e )
        {
            StopTime( );
        }

        public void StopTime( )
        {
            if( m_CurrentTE != null )
            {
                Notes NotesForm = new Notes( m_CurrentTE, m_Session );
                while( m_CurrentTE.Notes == null || m_CurrentTE.Notes.Length == 0 )
                {
                    NotesForm.ShowDialog( );
                }

                m_CurrentTE = NotesForm.CurrentTimeEntry;

                if( true == tmrElapsedTime.Enabled )
                {
                    tmrElapsedTime.Enabled = false;
                }

                if( btnStart.Text == "Start" )
                {
                    btnStart.Text = "Stop";
                }
                else
                {
                    btnStart.Text = "Start";
                }

                ttlblElapesedTime.ForeColor = Color.Red;

                //ttlblElapesedTime.Text = ttlblElapesedTime.Text + " Time stopped";

                m_CurrentTE = m_Service.Stop( m_Session, m_CurrentTE );
            }
        }

        public void UpdateDDL( )
        {
            m_ComboBoxList.Clear( );
            Project[] Items = m_Service.GetProjects( m_Session, true );
            m_ComboBoxList.AddRange( Items );

            ddlProjects.DataSource = null;
            ddlProjects.DisplayMember = "Name";
            ddlProjects.ValueMember = "ProjectID";
            ddlProjects.DataSource = m_ComboBoxList;
        }
    }
}