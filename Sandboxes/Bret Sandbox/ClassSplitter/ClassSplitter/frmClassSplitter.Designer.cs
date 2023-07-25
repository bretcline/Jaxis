namespace ClassSplitter
{
    partial class frmClassSplitter
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
            this.ofdClassList = new System.Windows.Forms.OpenFileDialog( );
            this.btnFile = new System.Windows.Forms.Button( );
            this.txtFile = new System.Windows.Forms.TextBox( );
            this.btnSplit = new System.Windows.Forms.Button( );
            this.txtClassOne = new System.Windows.Forms.TextBox( );
            this.txtClassTwo = new System.Windows.Forms.TextBox( );
            this.SuspendLayout( );
            // 
            // ofdClassList
            // 
            this.ofdClassList.FileName = "openFileDialog1";
            // 
            // btnFile
            // 
            this.btnFile.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnFile.Location = new System.Drawing.Point( 543, 7 );
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size( 24, 23 );
            this.btnFile.TabIndex = 2;
            this.btnFile.Text = "...";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler( this.btnFile_Click );
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.txtFile.Location = new System.Drawing.Point( 13, 9 );
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size( 524, 20 );
            this.txtFile.TabIndex = 3;
            // 
            // btnSplit
            // 
            this.btnSplit.Location = new System.Drawing.Point( 13, 35 );
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size( 75, 23 );
            this.btnSplit.TabIndex = 4;
            this.btnSplit.Text = "Split";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler( this.btnSplit_Click );
            // 
            // txtClassOne
            // 
            this.txtClassOne.Location = new System.Drawing.Point( 13, 65 );
            this.txtClassOne.Multiline = true;
            this.txtClassOne.Name = "txtClassOne";
            this.txtClassOne.Size = new System.Drawing.Size( 270, 405 );
            this.txtClassOne.TabIndex = 5;
            // 
            // txtClassTwo
            // 
            this.txtClassTwo.Location = new System.Drawing.Point( 297, 65 );
            this.txtClassTwo.Multiline = true;
            this.txtClassTwo.Name = "txtClassTwo";
            this.txtClassTwo.Size = new System.Drawing.Size( 270, 405 );
            this.txtClassTwo.TabIndex = 6;
            // 
            // frmClassSplitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 577, 482 );
            this.Controls.Add( this.txtClassTwo );
            this.Controls.Add( this.txtClassOne );
            this.Controls.Add( this.btnSplit );
            this.Controls.Add( this.txtFile );
            this.Controls.Add( this.btnFile );
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmClassSplitter";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdClassList;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnSplit;
        private System.Windows.Forms.TextBox txtClassOne;
        private System.Windows.Forms.TextBox txtClassTwo;
    }
}

