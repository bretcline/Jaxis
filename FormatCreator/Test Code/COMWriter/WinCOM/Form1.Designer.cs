namespace WinCOM
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
            this.btnConnect = new System.Windows.Forms.Button( );
            this.btnClose = new System.Windows.Forms.Button( );
            this.cmbCommands = new System.Windows.Forms.ComboBox( );
            this.btnExecute = new System.Windows.Forms.Button( );
            this.lstResults = new System.Windows.Forms.ListBox( );
            this.btnClear = new System.Windows.Forms.Button( );
            this.txtOptions = new System.Windows.Forms.TextBox( );
            this.txtData = new System.Windows.Forms.TextBox( );
            this.SuspendLayout( );
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point( 12, 12 );
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size( 75, 23 );
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler( this.btnConnect_Click );
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point( 197, 12 );
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size( 75, 23 );
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler( this.btnClose_Click );
            // 
            // cmbCommands
            // 
            this.cmbCommands.FormattingEnabled = true;
            this.cmbCommands.Location = new System.Drawing.Point( 12, 41 );
            this.cmbCommands.Name = "cmbCommands";
            this.cmbCommands.Size = new System.Drawing.Size( 208, 21 );
            this.cmbCommands.TabIndex = 2;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point( 197, 144 );
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size( 75, 23 );
            this.btnExecute.TabIndex = 3;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler( this.btnExecute_Click );
            // 
            // lstResults
            // 
            this.lstResults.FormattingEnabled = true;
            this.lstResults.Location = new System.Drawing.Point( 12, 173 );
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size( 260, 264 );
            this.lstResults.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point( 12, 144 );
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size( 75, 23 );
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler( this.btnClear_Click );
            // 
            // txtOptions
            // 
            this.txtOptions.Location = new System.Drawing.Point( 226, 41 );
            this.txtOptions.Name = "txtOptions";
            this.txtOptions.Size = new System.Drawing.Size( 46, 20 );
            this.txtOptions.TabIndex = 6;
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point( 12, 68 );
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size( 260, 70 );
            this.txtData.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 284, 450 );
            this.Controls.Add( this.txtData );
            this.Controls.Add( this.txtOptions );
            this.Controls.Add( this.btnClear );
            this.Controls.Add( this.lstResults );
            this.Controls.Add( this.btnExecute );
            this.Controls.Add( this.cmbCommands );
            this.Controls.Add( this.btnClose );
            this.Controls.Add( this.btnConnect );
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbCommands;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtOptions;
        private System.Windows.Forms.TextBox txtData;
    }
}

