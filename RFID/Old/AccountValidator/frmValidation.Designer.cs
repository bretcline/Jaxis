namespace AccountValidator
{
    partial class frmAccountValidator
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser( );
            this.btnReport = new DevExpress.XtraEditors.SimpleButton( );
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl( );
            this.lblReadTime = new DevExpress.XtraEditors.LabelControl( );
            this.btnColor = new DevExpress.XtraEditors.SimpleButton( );
            this.SuspendLayout( );
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point( 5, 5 );
            this.webBrowser1.MinimumSize = new System.Drawing.Size( 20, 20 );
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size( 337, 252 );
            this.webBrowser1.TabIndex = 0;
            // 
            // btnReport
            // 
            this.btnReport.Appearance.Font = new System.Drawing.Font( "Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
            this.btnReport.Appearance.Options.UseFont = true;
            this.btnReport.Appearance.Options.UseTextOptions = true;
            this.btnReport.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnReport.Location = new System.Drawing.Point( 348, 193 );
            this.btnReport.Name = "btnReport";
            this.btnReport.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnReport.Size = new System.Drawing.Size( 124, 64 );
            this.btnReport.TabIndex = 1;
            this.btnReport.Text = "Report a Problem";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point( 349, 143 );
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size( 26, 13 );
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Time:";
            // 
            // lblReadTime
            // 
            this.lblReadTime.Location = new System.Drawing.Point( 382, 143 );
            this.lblReadTime.Name = "lblReadTime";
            this.lblReadTime.Size = new System.Drawing.Size( 0, 13 );
            this.lblReadTime.TabIndex = 4;
            // 
            // btnColor
            // 
            this.btnColor.Appearance.Options.UseImage = true;
            this.btnColor.Image = global::AccountValidator.Properties.Resources.Red_Circle;
            this.btnColor.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnColor.Location = new System.Drawing.Point( 348, 12 );
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size( 124, 124 );
            this.btnColor.TabIndex = 2;
            // 
            // frmAccountValidator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 484, 262 );
            this.Controls.Add( this.lblReadTime );
            this.Controls.Add( this.labelControl1 );
            this.Controls.Add( this.btnColor );
            this.Controls.Add( this.btnReport );
            this.Controls.Add( this.webBrowser1 );
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAccountValidator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Account Validator";
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private DevExpress.XtraEditors.SimpleButton btnReport;
        private DevExpress.XtraEditors.SimpleButton btnColor;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblReadTime;


    }
}

