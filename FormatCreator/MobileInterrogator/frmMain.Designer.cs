namespace MobileInterrogator
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
            this.sbtnScan = new LFI.Mobile.Controls.Button.SkinnedButton( );
            this.sbtnViewTag = new LFI.Mobile.Controls.Button.SkinnedButton( );
            this.pnlPickList = new System.Windows.Forms.Panel( );
            this.SuspendLayout( );
            // 
            // sbtnScan
            // 
            this.sbtnScan.Location = new System.Drawing.Point( 3, 5 );
            this.sbtnScan.Name = "sbtnScan";
            this.sbtnScan.Size = new System.Drawing.Size( 110, 29 );
            this.sbtnScan.TabIndex = 1;
            this.sbtnScan.Text = "Scan";
            this.sbtnScan.TextAlignIsRelative = true;
            this.sbtnScan.UseTransparency = true;
            this.sbtnScan.Click += new System.EventHandler( this.sbtnScan_Click );
            // 
            // sbtnViewTag
            // 
            this.sbtnViewTag.Location = new System.Drawing.Point( 126, 5 );
            this.sbtnViewTag.Name = "sbtnViewTag";
            this.sbtnViewTag.Size = new System.Drawing.Size( 110, 29 );
            this.sbtnViewTag.TabIndex = 2;
            this.sbtnViewTag.Text = "View Tag";
            this.sbtnViewTag.TextAlignIsRelative = true;
            this.sbtnViewTag.UseTransparency = true;
            this.sbtnViewTag.Click += new System.EventHandler( this.sbtnViewTag_Click );
            // 
            // pnlPickList
            // 
            this.pnlPickList.Location = new System.Drawing.Point( 3, 40 );
            this.pnlPickList.Name = "pnlPickList";
            this.pnlPickList.Size = new System.Drawing.Size( 233, 253 );
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 96F, 96F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size( 240, 294 );
            this.Controls.Add( this.pnlPickList );
            this.Controls.Add( this.sbtnViewTag );
            this.Controls.Add( this.sbtnScan );
            this.Name = "frmMain";
            this.Text = "Tag List";
            this.Load += new System.EventHandler( this.frmMain_Load );
            this.ResumeLayout( false );

        }

        #endregion

        private LFI.Mobile.Controls.Button.SkinnedButton sbtnScan;
        private LFI.Mobile.Controls.Button.SkinnedButton sbtnViewTag;
        private System.Windows.Forms.Panel pnlPickList;
    }
}

