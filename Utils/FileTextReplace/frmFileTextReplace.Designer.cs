namespace FileTextReplace
{
    partial class frmFileTextReplace
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
            this.ofdFile = new System.Windows.Forms.OpenFileDialog( );
            this.txtFileName = new System.Windows.Forms.TextBox( );
            this.btnFilePicker = new System.Windows.Forms.Button( );
            this.label1 = new System.Windows.Forms.Label( );
            this.txtFind = new System.Windows.Forms.TextBox( );
            this.btnCancel = new System.Windows.Forms.Button( );
            this.btnOk = new System.Windows.Forms.Button( );
            this.txtReplace = new System.Windows.Forms.TextBox( );
            this.label2 = new System.Windows.Forms.Label( );
            this.label3 = new System.Windows.Forms.Label( );
            this.SuspendLayout( );
            // 
            // ofdFile
            // 
            this.ofdFile.FileName = "openFileDialog1";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point( 69, 12 );
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size( 287, 20 );
            this.txtFileName.TabIndex = 0;
            // 
            // btnFilePicker
            // 
            this.btnFilePicker.Location = new System.Drawing.Point( 362, 12 );
            this.btnFilePicker.Name = "btnFilePicker";
            this.btnFilePicker.Size = new System.Drawing.Size( 24, 20 );
            this.btnFilePicker.TabIndex = 1;
            this.btnFilePicker.Text = "...";
            this.btnFilePicker.UseVisualStyleBackColor = true;
            this.btnFilePicker.Click += new System.EventHandler( this.btnFilePicker_Click );
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 12, 15 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 57, 13 );
            this.label1.TabIndex = 2;
            this.label1.Text = "File Name:";
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point( 69, 39 );
            this.txtFind.Multiline = true;
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size( 317, 56 );
            this.txtFind.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point( 311, 170 );
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size( 75, 23 );
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point( 230, 170 );
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size( 75, 23 );
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler( this.btnOk_Click );
            // 
            // txtReplace
            // 
            this.txtReplace.Location = new System.Drawing.Point( 69, 101 );
            this.txtReplace.Multiline = true;
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size( 317, 63 );
            this.txtReplace.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 33, 39 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 30, 13 );
            this.label2.TabIndex = 7;
            this.label2.Text = "Find:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point( 13, 101 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 50, 13 );
            this.label3.TabIndex = 8;
            this.label3.Text = "Replace:";
            // 
            // frmFileTextReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 393, 199 );
            this.Controls.Add( this.label3 );
            this.Controls.Add( this.label2 );
            this.Controls.Add( this.txtReplace );
            this.Controls.Add( this.btnOk );
            this.Controls.Add( this.btnCancel );
            this.Controls.Add( this.txtFind );
            this.Controls.Add( this.label1 );
            this.Controls.Add( this.btnFilePicker );
            this.Controls.Add( this.txtFileName );
            this.Name = "frmFileTextReplace";
            this.Text = "File Text Replace";
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnFilePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

