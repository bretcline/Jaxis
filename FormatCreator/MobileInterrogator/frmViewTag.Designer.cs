namespace MobileInterrogator
{
    partial class frmViewTag
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
            this.pnlPickList = new System.Windows.Forms.Panel( );
            this.sbtnEditValue = new LFI.Mobile.Controls.Button.SkinnedButton( );
            this.SuspendLayout( );
            // 
            // pnlPickList
            // 
            this.pnlPickList.Location = new System.Drawing.Point( 4, 38 );
            this.pnlPickList.Name = "pnlPickList";
            this.pnlPickList.Size = new System.Drawing.Size( 233, 253 );
            // 
            // sbtnEditValue
            // 
            this.sbtnEditValue.Location = new System.Drawing.Point( 127, 3 );
            this.sbtnEditValue.Name = "sbtnEditValue";
            this.sbtnEditValue.Size = new System.Drawing.Size( 110, 29 );
            this.sbtnEditValue.TabIndex = 4;
            this.sbtnEditValue.Text = "Edit Value";
            this.sbtnEditValue.TextAlignIsRelative = true;
            this.sbtnEditValue.UseTransparency = true;
            // 
            // frmViewTag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 96F, 96F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size( 240, 294 );
            this.Controls.Add( this.pnlPickList );
            this.Controls.Add( this.sbtnEditValue );
            this.Name = "frmViewTag";
            this.Text = "View Tag";
            this.Load += new System.EventHandler( this.frmViewTag_Load );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Panel pnlPickList;
        private LFI.Mobile.Controls.Button.SkinnedButton sbtnEditValue;
    }
}