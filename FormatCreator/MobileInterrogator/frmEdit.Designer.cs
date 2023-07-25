namespace MobileInterrogator
{
    partial class frmEdit
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
            this.dlItem = new MobileInterrogator.ctrlDynamicLayout( );
            this.ctrlDynamicLayout1 = new MobileInterrogator.ctrlDynamicLayout( );
            this.sbtnWrite = new LFI.Mobile.Controls.Button.SkinnedButton( );
            this.sbtnCancel = new LFI.Mobile.Controls.Button.SkinnedButton( );
            this.SuspendLayout( );
            // 
            // dlItem
            // 
            this.dlItem.Location = new System.Drawing.Point( 3, 3 );
            this.dlItem.Name = "dlItem";
            this.dlItem.Size = new System.Drawing.Size( 234, 252 );
            this.dlItem.TabIndex = 0;
            // 
            // ctrlDynamicLayout1
            // 
            this.ctrlDynamicLayout1.Location = new System.Drawing.Point( 3, 3 );
            this.ctrlDynamicLayout1.Name = "ctrlDynamicLayout1";
            this.ctrlDynamicLayout1.Size = new System.Drawing.Size( 234, 252 );
            this.ctrlDynamicLayout1.TabIndex = 0;
            // 
            // sbtnWrite
            // 
            this.sbtnWrite.Location = new System.Drawing.Point( 4, 262 );
            this.sbtnWrite.Name = "sbtnWrite";
            this.sbtnWrite.Size = new System.Drawing.Size( 110, 29 );
            this.sbtnWrite.TabIndex = 1;
            this.sbtnWrite.Text = "Write";
            this.sbtnWrite.Click += new System.EventHandler( this.sbtnWrite_Click );
            // 
            // sbtnCancel
            // 
            this.sbtnCancel.Location = new System.Drawing.Point( 127, 261 );
            this.sbtnCancel.Name = "sbtnCancel";
            this.sbtnCancel.Size = new System.Drawing.Size( 110, 29 );
            this.sbtnCancel.TabIndex = 2;
            this.sbtnCancel.Text = "Cancel";
            this.sbtnCancel.Click += new System.EventHandler( this.sbtnCancel_Click );
            // 
            // frmEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 96F, 96F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size( 240, 294 );
            this.Controls.Add( this.sbtnCancel );
            this.Controls.Add( this.sbtnWrite );
            this.Controls.Add( this.dlItem );
            this.Name = "frmEdit";
            this.Text = "Edit Tag Data";
            this.ResumeLayout( false );

        }

        #endregion

        private ctrlDynamicLayout dlItem;
        private ctrlDynamicLayout ctrlDynamicLayout1;
        private LFI.Mobile.Controls.Button.SkinnedButton sbtnWrite;
        private LFI.Mobile.Controls.Button.SkinnedButton sbtnCancel;
    }
}