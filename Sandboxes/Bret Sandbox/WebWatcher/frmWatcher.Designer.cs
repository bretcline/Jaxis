namespace WebWatcher
{
    partial class frmWatcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( frmWatcher ) );
            this.wBrowser = new System.Windows.Forms.WebBrowser( );
            this.tmrRefresh = new System.Windows.Forms.Timer( this.components );
            this.panel1 = new System.Windows.Forms.Panel( );
            this.btnStopWatching = new System.Windows.Forms.Button( );
            this.panel1.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // wBrowser
            // 
            this.wBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wBrowser.Location = new System.Drawing.Point( 0, 32 );
            this.wBrowser.MinimumSize = new System.Drawing.Size( 20, 20 );
            this.wBrowser.Name = "wBrowser";
            this.wBrowser.Size = new System.Drawing.Size( 877, 532 );
            this.wBrowser.TabIndex = 0;
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 5000;
            this.tmrRefresh.Tick += new System.EventHandler( this.tmrRefresh_Tick );
            // 
            // panel1
            // 
            this.panel1.Controls.Add( this.btnStopWatching );
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point( 0, 0 );
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size( 877, 32 );
            this.panel1.TabIndex = 3;
            // 
            // btnStopWatching
            // 
            this.btnStopWatching.Location = new System.Drawing.Point( 3, 3 );
            this.btnStopWatching.Name = "btnStopWatching";
            this.btnStopWatching.Size = new System.Drawing.Size( 104, 23 );
            this.btnStopWatching.TabIndex = 0;
            this.btnStopWatching.Text = "Stop Watching";
            this.btnStopWatching.UseVisualStyleBackColor = true;
            this.btnStopWatching.Click += new System.EventHandler( this.btnStopWatching_Click );
            // 
            // frmWatcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 877, 564 );
            this.Controls.Add( this.wBrowser );
            this.Controls.Add( this.panel1 );
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.Name = "frmWatcher";
            this.Text = "Property Watcher";
            this.Load += new System.EventHandler( this.Form1_Load );
            this.panel1.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.WebBrowser wBrowser;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStopWatching;
    }
}

