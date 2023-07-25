namespace WebWatcher
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( frmSettings ) );
            this.txtLoginURL = new System.Windows.Forms.TextBox( );
            this.numInterval = new System.Windows.Forms.NumericUpDown( );
            this.btnOK = new System.Windows.Forms.Button( );
            this.btnCancel = new System.Windows.Forms.Button( );
            this.label1 = new System.Windows.Forms.Label( );
            this.label2 = new System.Windows.Forms.Label( );
            this.numWatchers = new System.Windows.Forms.NumericUpDown( );
            ( (System.ComponentModel.ISupportInitialize)( this.numInterval ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.numWatchers ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // txtLoginURL
            // 
            this.txtLoginURL.Location = new System.Drawing.Point( 96, 12 );
            this.txtLoginURL.Name = "txtLoginURL";
            this.txtLoginURL.Size = new System.Drawing.Size( 293, 20 );
            this.txtLoginURL.TabIndex = 0;
            this.txtLoginURL.Text = "http://irisval.com/RWC-web/login.jsf";
            // 
            // numInterval
            // 
            this.numInterval.Location = new System.Drawing.Point( 96, 40 );
            this.numInterval.Name = "numInterval";
            this.numInterval.Size = new System.Drawing.Size( 46, 20 );
            this.numInterval.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point( 233, 37 );
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size( 75, 23 );
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler( this.btnOK_Click );
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point( 314, 37 );
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size( 75, 23 );
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 36, 15 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 54, 13 );
            this.label1.TabIndex = 4;
            this.label1.Text = "Web Site:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 5, 42 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 85, 13 );
            this.label2.TabIndex = 5;
            this.label2.Text = "Refresh Interval:";
            // 
            // numWatchers
            // 
            this.numWatchers.Location = new System.Drawing.Point( 170, 40 );
            this.numWatchers.Name = "numWatchers";
            this.numWatchers.Size = new System.Drawing.Size( 46, 20 );
            this.numWatchers.TabIndex = 6;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 401, 69 );
            this.Controls.Add( this.numWatchers );
            this.Controls.Add( this.label2 );
            this.Controls.Add( this.label1 );
            this.Controls.Add( this.btnCancel );
            this.Controls.Add( this.btnOK );
            this.Controls.Add( this.numInterval );
            this.Controls.Add( this.txtLoginURL );
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.Name = "frmSettings";
            this.Text = "frmSettings";
            ( (System.ComponentModel.ISupportInitialize)( this.numInterval ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.numWatchers ) ).EndInit( );
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.TextBox txtLoginURL;
        private System.Windows.Forms.NumericUpDown numInterval;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numWatchers;
    }
}