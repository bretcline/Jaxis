namespace AccountValidator
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser( );
            this.button2 = new System.Windows.Forms.Button( );
            this.pictureBox1 = new System.Windows.Forms.PictureBox( );
            this.SuspendLayout( );
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point( 4, 4 );
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size( 327, 265 );
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point( 337, 195 );
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size( 140, 70 );
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point( 337, 4 );
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size( 140, 140 );
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 96F, 96F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size( 480, 272 );
            this.ControlBox = false;
            this.Controls.Add( this.pictureBox1 );
            this.Controls.Add( this.button2 );
            this.Controls.Add( this.webBrowser1 );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

