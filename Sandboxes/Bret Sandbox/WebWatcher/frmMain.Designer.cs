namespace WebWatcher
{
    partial class frmMain
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
            this.panel1 = new System.Windows.Forms.Panel( );
            this.btnAddWatcher = new System.Windows.Forms.Button( );
            this.panel1.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // panel1
            // 
            this.panel1.Controls.Add( this.btnAddWatcher );
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point( 0, 0 );
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size( 900, 31 );
            this.panel1.TabIndex = 1;
            // 
            // btnAddWatcher
            // 
            this.btnAddWatcher.Location = new System.Drawing.Point( 3, 3 );
            this.btnAddWatcher.Name = "btnAddWatcher";
            this.btnAddWatcher.Size = new System.Drawing.Size( 86, 23 );
            this.btnAddWatcher.TabIndex = 0;
            this.btnAddWatcher.Text = "Add Watcher";
            this.btnAddWatcher.UseVisualStyleBackColor = true;
            this.btnAddWatcher.Click += new System.EventHandler( this.btnAddWatcher_Click );
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 900, 416 );
            this.Controls.Add( this.panel1 );
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.Load += new System.EventHandler( this.frmMain_Load );
            this.panel1.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddWatcher;
    }
}