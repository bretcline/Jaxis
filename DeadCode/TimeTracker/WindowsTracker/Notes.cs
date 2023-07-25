using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsTracker.TimeTracker;

namespace WindowsTracker
{
    public partial class Notes : Form
    {
        TimeTracker.TimeEntryServiceSoapClient m_Service = null;
        TimeEntry m_CurrentTE = null;
        Guid m_SessionID;

        public TimeEntry CurrentTimeEntry
        {
            get { return m_CurrentTE; }
            set { m_CurrentTE = value; }
        }

        public Notes( TimeEntry _RecievedTE, Guid _RecievedSessionID )
        {
            m_Service = new TimeTracker.TimeEntryServiceSoapClient( );

            m_CurrentTE = _RecievedTE;
            m_SessionID = _RecievedSessionID;

            InitializeComponent( );
        }

        private void Notes_Load( object sender, EventArgs e )
        {            
            txtProject.Text = m_CurrentTE.Project.Name;
            txtStartTime.Text = Convert.ToString( m_CurrentTE.StartTime );
            if( true == m_CurrentTE.EndTime.Equals( DateTime.MinValue.AddYears( 1900 ) ) )
            {
                txtEndTime.Text = "Currently running";
            }
            else
            {
                txtEndTime.Text = Convert.ToString( m_CurrentTE.EndTime );
            }

            rchtxtPreviousNotes.Text = m_CurrentTE.Notes;
        }

        private void btnCancel_Click( object sender, EventArgs e )
        {
            this.Close( );
        }

        private void btnOK_Click( object sender, EventArgs e )
        {
            m_CurrentTE.Notes = rchtxtNewNotes.Text + System.Environment.NewLine + 
                System.Environment.NewLine + rchtxtPreviousNotes.Text.Trim( );
            m_CurrentTE = m_Service.SaveTimeEntry( m_SessionID, m_CurrentTE );
            this.Close( );
        }
    }
}
