namespace WindowsTracker
{
    partial class Tracker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.components = new System.ComponentModel.Container( );
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( Tracker ) );
            this.ddlProjects = new System.Windows.Forms.ComboBox( );
            this.projectsBindingSource = new System.Windows.Forms.BindingSource( this.components );
            this.timetrackerDataSet = new WindowsTracker.TimetrackerDataSet( );
            this.btnNotes = new System.Windows.Forms.Button( );
            this.btnStart = new System.Windows.Forms.Button( );
            this.ssProjectAndTime = new System.Windows.Forms.StatusStrip( );
            this.ttlblProject = new System.Windows.Forms.ToolStripStatusLabel( );
            this.ttlblElapesedTime = new System.Windows.Forms.ToolStripStatusLabel( );
            this.tmrElapsedTime = new System.Windows.Forms.Timer( this.components );
            this.projectsTableAdapter = new WindowsTracker.TimetrackerDataSetTableAdapters.ProjectsTableAdapter( );
            ( (System.ComponentModel.ISupportInitialize)( this.projectsBindingSource ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.timetrackerDataSet ) ).BeginInit( );
            this.ssProjectAndTime.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // ddlProjects
            // 
            this.ddlProjects.DataBindings.Add( new System.Windows.Forms.Binding( "SelectedValue", this.projectsBindingSource, "ProjectID", true ) );
            this.ddlProjects.FormattingEnabled = true;
            this.ddlProjects.Location = new System.Drawing.Point( 6, 3 );
            this.ddlProjects.Name = "ddlProjects";
            this.ddlProjects.Size = new System.Drawing.Size( 199, 21 );
            this.ddlProjects.TabIndex = 0;
            this.ddlProjects.SelectedIndexChanged += new System.EventHandler( this.ddlProjects_SelectedIndexChanged );
            // 
            // projectsBindingSource
            // 
            this.projectsBindingSource.DataMember = "Projects";
            this.projectsBindingSource.DataSource = this.timetrackerDataSet;
            this.projectsBindingSource.Filter = "";
            // 
            // timetrackerDataSet
            // 
            this.timetrackerDataSet.DataSetName = "TimetrackerDataSet";
            this.timetrackerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnNotes
            // 
            this.btnNotes.Location = new System.Drawing.Point( 6, 30 );
            this.btnNotes.Name = "btnNotes";
            this.btnNotes.Size = new System.Drawing.Size( 75, 23 );
            this.btnNotes.TabIndex = 1;
            this.btnNotes.Text = "Notes";
            this.btnNotes.UseVisualStyleBackColor = true;
            this.btnNotes.Click += new System.EventHandler( this.btnNotes_Click );
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point( 130, 30 );
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size( 75, 23 );
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler( this.btnStart_Click );
            // 
            // ssProjectAndTime
            // 
            this.ssProjectAndTime.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.ttlblProject,
            this.ttlblElapesedTime} );
            this.ssProjectAndTime.Location = new System.Drawing.Point( 0, 56 );
            this.ssProjectAndTime.Name = "ssProjectAndTime";
            this.ssProjectAndTime.Size = new System.Drawing.Size( 213, 22 );
            this.ssProjectAndTime.TabIndex = 5;
            this.ssProjectAndTime.Text = "statusStrip1";
            // 
            // ttlblProject
            // 
            this.ttlblProject.Name = "ttlblProject";
            this.ttlblProject.Size = new System.Drawing.Size( 41, 17 );
            this.ttlblProject.Text = "Project";
            // 
            // ttlblElapesedTime
            // 
            this.ttlblElapesedTime.Name = "ttlblElapesedTime";
            this.ttlblElapesedTime.Size = new System.Drawing.Size( 29, 17 );
            this.ttlblElapesedTime.Text = "Time";
            // 
            // tmrElapsedTime
            // 
            this.tmrElapsedTime.Interval = 1000;
            this.tmrElapsedTime.Tick += new System.EventHandler( this.tmrElapsedTime_Tick );
            // 
            // projectsTableAdapter
            // 
            this.projectsTableAdapter.ClearBeforeFill = true;
            // 
            // Tracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 213, 78 );
            this.Controls.Add( this.ssProjectAndTime );
            this.Controls.Add( this.btnStart );
            this.Controls.Add( this.btnNotes );
            this.Controls.Add( this.ddlProjects );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.Name = "Tracker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows Tracker";
            this.Load += new System.EventHandler( this.frmWindowsTracker_Load );
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.frmWindowsTracker_FormClosing );
            ( (System.ComponentModel.ISupportInitialize)( this.projectsBindingSource ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.timetrackerDataSet ) ).EndInit( );
            this.ssProjectAndTime.ResumeLayout( false );
            this.ssProjectAndTime.PerformLayout( );
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.Button btnNotes;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.StatusStrip ssProjectAndTime;
        private System.Windows.Forms.ToolStripStatusLabel ttlblProject;
        private System.Windows.Forms.ToolStripStatusLabel ttlblElapesedTime;
        private System.Windows.Forms.Timer tmrElapsedTime;
        private TimetrackerDataSet timetrackerDataSet;
        private System.Windows.Forms.BindingSource projectsBindingSource;
        private WindowsTracker.TimetrackerDataSetTableAdapters.ProjectsTableAdapter projectsTableAdapter;
        private System.Windows.Forms.ComboBox ddlProjects;
    }
}

