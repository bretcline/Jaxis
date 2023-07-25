namespace Sunglass_Mirror
{
    partial class SunglassMirror
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sbPicture1 = new DevExpress.XtraEditors.SimpleButton( );
            this.sbPicture2 = new DevExpress.XtraEditors.SimpleButton( );
            this.sbPicture3 = new DevExpress.XtraEditors.SimpleButton( );
            this.sbPicture4 = new DevExpress.XtraEditors.SimpleButton( );
            this.sbPrint = new DevExpress.XtraEditors.SimpleButton( );
            this.sbClear = new DevExpress.XtraEditors.SimpleButton( );
            this.sbFullScreen = new DevExpress.XtraEditors.SimpleButton( );
            this.btnClose = new DevExpress.XtraEditors.SimpleButton( );
            this.SuspendLayout( );
            // 
            // sbPicture1
            // 
            this.sbPicture1.Location = new System.Drawing.Point( 30, 13 );
            this.sbPicture1.Name = "sbPicture1";
            this.sbPicture1.Size = new System.Drawing.Size( 550, 350 );
            this.sbPicture1.TabIndex = 8;
            this.sbPicture1.Click += new System.EventHandler( this.sbPicture1_Click );
            // 
            // sbPicture2
            // 
            this.sbPicture2.Location = new System.Drawing.Point( 636, 13 );
            this.sbPicture2.Name = "sbPicture2";
            this.sbPicture2.Size = new System.Drawing.Size( 550, 350 );
            this.sbPicture2.TabIndex = 9;
            this.sbPicture2.Click += new System.EventHandler( this.sbPicture2_Click );
            // 
            // sbPicture3
            // 
            this.sbPicture3.Location = new System.Drawing.Point( 29, 406 );
            this.sbPicture3.Name = "sbPicture3";
            this.sbPicture3.Size = new System.Drawing.Size( 550, 350 );
            this.sbPicture3.TabIndex = 10;
            this.sbPicture3.Click += new System.EventHandler( this.sbPicture3_Click );
            // 
            // sbPicture4
            // 
            this.sbPicture4.Location = new System.Drawing.Point( 636, 406 );
            this.sbPicture4.Name = "sbPicture4";
            this.sbPicture4.Size = new System.Drawing.Size( 550, 350 );
            this.sbPicture4.TabIndex = 11;
            this.sbPicture4.Click += new System.EventHandler( this.sbPicture4_Click );
            // 
            // sbPrint
            // 
            this.sbPrint.Location = new System.Drawing.Point( 1230, 618 );
            this.sbPrint.Name = "sbPrint";
            this.sbPrint.Size = new System.Drawing.Size( 124, 66 );
            this.sbPrint.TabIndex = 12;
            this.sbPrint.Text = "Print Pictures";
            this.sbPrint.Click += new System.EventHandler( this.sbPrint_Click );
            // 
            // sbClear
            // 
            this.sbClear.Location = new System.Drawing.Point( 1230, 690 );
            this.sbClear.Name = "sbClear";
            this.sbClear.Size = new System.Drawing.Size( 124, 66 );
            this.sbClear.TabIndex = 13;
            this.sbClear.Text = "Clear Pictures";
            this.sbClear.Click += new System.EventHandler( this.sbClear_Click );
            // 
            // sbFullScreen
            // 
            this.sbFullScreen.Location = new System.Drawing.Point( 1230, 546 );
            this.sbFullScreen.Name = "sbFullScreen";
            this.sbFullScreen.Size = new System.Drawing.Size( 124, 66 );
            this.sbFullScreen.TabIndex = 14;
            this.sbFullScreen.Text = "View Full Screen";
            this.sbFullScreen.Click += new System.EventHandler( this.sbFullScreen_Click );
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point( 1230, 13 );
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size( 124, 66 );
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler( this.btnClose_Click );
            // 
            // SunglassMirror
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 1366, 768 );
            this.Controls.Add( this.btnClose );
            this.Controls.Add( this.sbFullScreen );
            this.Controls.Add( this.sbClear );
            this.Controls.Add( this.sbPrint );
            this.Controls.Add( this.sbPicture4 );
            this.Controls.Add( this.sbPicture3 );
            this.Controls.Add( this.sbPicture2 );
            this.Controls.Add( this.sbPicture1 );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SunglassMirror";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sunglass Mirror";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout( false );

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbPicture1;
        private DevExpress.XtraEditors.SimpleButton sbPicture2;
        private DevExpress.XtraEditors.SimpleButton sbPicture3;
        private DevExpress.XtraEditors.SimpleButton sbPicture4;
        private DevExpress.XtraEditors.SimpleButton sbPrint;
        private DevExpress.XtraEditors.SimpleButton sbClear;
        private DevExpress.XtraEditors.SimpleButton sbFullScreen;
        private DevExpress.XtraEditors.SimpleButton btnClose;
    }
}

