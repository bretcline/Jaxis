namespace WindowsTracker
{
    partial class Notes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( Notes ) );
            this.rchtxtPreviousNotes = new System.Windows.Forms.RichTextBox( );
            this.lblProject = new System.Windows.Forms.Label( );
            this.txtProject = new System.Windows.Forms.TextBox( );
            this.lblStartTime = new System.Windows.Forms.Label( );
            this.txtStartTime = new System.Windows.Forms.TextBox( );
            this.lblEndTime = new System.Windows.Forms.Label( );
            this.txtEndTime = new System.Windows.Forms.TextBox( );
            this.lblPreviousNotes = new System.Windows.Forms.Label( );
            this.btnOK = new System.Windows.Forms.Button( );
            this.btnCancel = new System.Windows.Forms.Button( );
            this.lblNewNotes = new System.Windows.Forms.Label( );
            this.rchtxtNewNotes = new System.Windows.Forms.RichTextBox( );
            this.SuspendLayout( );
            // 
            // rchtxtPreviousNotes
            // 
            this.rchtxtPreviousNotes.Location = new System.Drawing.Point( 15, 181 );
            this.rchtxtPreviousNotes.Name = "rchtxtPreviousNotes";
            this.rchtxtPreviousNotes.ReadOnly = true;
            this.rchtxtPreviousNotes.Size = new System.Drawing.Size( 598, 199 );
            this.rchtxtPreviousNotes.TabIndex = 10;
            this.rchtxtPreviousNotes.Text = "";
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.Location = new System.Drawing.Point( 30, 8 );
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size( 40, 13 );
            this.lblProject.TabIndex = 1;
            this.lblProject.Text = "Project";
            // 
            // txtProject
            // 
            this.txtProject.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
            this.txtProject.Location = new System.Drawing.Point( 75, 5 );
            this.txtProject.Name = "txtProject";
            this.txtProject.ReadOnly = true;
            this.txtProject.Size = new System.Drawing.Size( 120, 20 );
            this.txtProject.TabIndex = 4;
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point( 202, 8 );
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size( 55, 13 );
            this.lblStartTime.TabIndex = 3;
            this.lblStartTime.Text = "Start Time";
            // 
            // txtStartTime
            // 
            this.txtStartTime.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
            this.txtStartTime.Location = new System.Drawing.Point( 263, 5 );
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.ReadOnly = true;
            this.txtStartTime.Size = new System.Drawing.Size( 129, 20 );
            this.txtStartTime.TabIndex = 5;
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point( 398, 8 );
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size( 52, 13 );
            this.lblEndTime.TabIndex = 5;
            this.lblEndTime.Text = "End Time";
            // 
            // txtEndTime
            // 
            this.txtEndTime.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
            this.txtEndTime.Location = new System.Drawing.Point( 456, 5 );
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.ReadOnly = true;
            this.txtEndTime.Size = new System.Drawing.Size( 129, 20 );
            this.txtEndTime.TabIndex = 6;
            // 
            // lblPreviousNotes
            // 
            this.lblPreviousNotes.AutoSize = true;
            this.lblPreviousNotes.Location = new System.Drawing.Point( 12, 165 );
            this.lblPreviousNotes.Name = "lblPreviousNotes";
            this.lblPreviousNotes.Size = new System.Drawing.Size( 79, 13 );
            this.lblPreviousNotes.TabIndex = 7;
            this.lblPreviousNotes.Text = "Previous Notes";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point( 457, 386 );
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size( 75, 23 );
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler( this.btnOK_Click );
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point( 538, 386 );
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size( 75, 23 );
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler( this.btnCancel_Click );
            // 
            // lblNewNotes
            // 
            this.lblNewNotes.AutoSize = true;
            this.lblNewNotes.Location = new System.Drawing.Point( 12, 32 );
            this.lblNewNotes.Name = "lblNewNotes";
            this.lblNewNotes.Size = new System.Drawing.Size( 60, 13 );
            this.lblNewNotes.TabIndex = 9;
            this.lblNewNotes.Text = "New Notes";
            // 
            // rchtxtNewNotes
            // 
            this.rchtxtNewNotes.Location = new System.Drawing.Point( 15, 48 );
            this.rchtxtNewNotes.Name = "rchtxtNewNotes";
            this.rchtxtNewNotes.Size = new System.Drawing.Size( 598, 114 );
            this.rchtxtNewNotes.TabIndex = 1;
            this.rchtxtNewNotes.Text = "";
            // 
            // Notes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 625, 416 );
            this.Controls.Add( this.rchtxtNewNotes );
            this.Controls.Add( this.lblNewNotes );
            this.Controls.Add( this.btnCancel );
            this.Controls.Add( this.btnOK );
            this.Controls.Add( this.lblPreviousNotes );
            this.Controls.Add( this.txtEndTime );
            this.Controls.Add( this.lblEndTime );
            this.Controls.Add( this.txtStartTime );
            this.Controls.Add( this.lblStartTime );
            this.Controls.Add( this.txtProject );
            this.Controls.Add( this.lblProject );
            this.Controls.Add( this.rchtxtPreviousNotes );
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.Name = "Notes";
            this.Text = "Notes";
            this.Load += new System.EventHandler( this.Notes_Load );
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.RichTextBox rchtxtPreviousNotes;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.TextBox txtProject;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.TextBox txtEndTime;
        private System.Windows.Forms.Label lblPreviousNotes;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblNewNotes;
        private System.Windows.Forms.RichTextBox rchtxtNewNotes;
    }
}