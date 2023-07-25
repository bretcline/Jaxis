namespace PassivePickup
{
    partial class Form1
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
            this.btnInvalid = new System.Windows.Forms.Button( );
            this.pnlAddress = new System.Windows.Forms.Panel( );
            this.btnSubmit = new System.Windows.Forms.Button( );
            this.txtTagID = new System.Windows.Forms.TextBox( );
            this.lblAddress = new System.Windows.Forms.Label( );
            this.pbPickup = new System.Windows.Forms.PictureBox( );
            this.pnlAddress.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // btnInvalid
            // 
            this.btnInvalid.Font = new System.Drawing.Font( "Tahoma", 13F, System.Drawing.FontStyle.Bold );
            this.btnInvalid.Location = new System.Drawing.Point( 184, 158 );
            this.btnInvalid.Name = "btnInvalid";
            this.btnInvalid.Size = new System.Drawing.Size( 156, 89 );
            this.btnInvalid.TabIndex = 0;
            this.btnInvalid.Text = "Invalid Address";
            this.btnInvalid.Click += new System.EventHandler( this.btnInvalid_Click );
            // 
            // pnlAddress
            // 
            this.pnlAddress.BackColor = System.Drawing.Color.White;
            this.pnlAddress.Controls.Add( this.btnSubmit );
            this.pnlAddress.Controls.Add( this.txtTagID );
            this.pnlAddress.Controls.Add( this.lblAddress );
            this.pnlAddress.Location = new System.Drawing.Point( 3, 3 );
            this.pnlAddress.Name = "pnlAddress";
            this.pnlAddress.Size = new System.Drawing.Size( 180, 241 );
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point( 149, 215 );
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size( 26, 23 );
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "+";
            this.btnSubmit.Click += new System.EventHandler( this.btnSubmit_Click );
            // 
            // txtTagID
            // 
            this.txtTagID.Location = new System.Drawing.Point( 4, 215 );
            this.txtTagID.Name = "txtTagID";
            this.txtTagID.Size = new System.Drawing.Size( 139, 23 );
            this.txtTagID.TabIndex = 1;
            // 
            // lblAddress
            // 
            this.lblAddress.Font = new System.Drawing.Font( "Tahoma", 12F, System.Drawing.FontStyle.Bold );
            this.lblAddress.Location = new System.Drawing.Point( 3, 9 );
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size( 172, 203 );
            this.lblAddress.Text = "label1";
            // 
            // pbPickup
            // 
            this.pbPickup.Location = new System.Drawing.Point( 184, 6 );
            this.pbPickup.Name = "pbPickup";
            this.pbPickup.Size = new System.Drawing.Size( 156, 146 );
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 96F, 96F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size( 340, 247 );
            this.Controls.Add( this.pbPickup );
            this.Controls.Add( this.pnlAddress );
            this.Controls.Add( this.btnInvalid );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler( this.Form1_Load );
            this.pnlAddress.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Button btnInvalid;
        private System.Windows.Forms.Panel pnlAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.PictureBox pbPickup;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtTagID;
    }
}

