namespace CustomDraw
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
            this.components = new System.ComponentModel.Container( );
            this.tmrDrawSpeed = new System.Windows.Forms.Timer( this.components );
            this.btnFill = new System.Windows.Forms.Button( );
            this.pnlPad = new System.Windows.Forms.Panel( );
            this.pbImage = new System.Windows.Forms.PictureBox( );
            this.tbarFill = new System.Windows.Forms.TrackBar( );
            this.btnShot = new System.Windows.Forms.Button( );
            this.btnWine = new System.Windows.Forms.Button( );
            this.btnBeer = new System.Windows.Forms.Button( );
            this.pnlPad.SuspendLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.pbImage ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.tbarFill ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // tmrDrawSpeed
            // 
            this.tmrDrawSpeed.Interval = 50;
            this.tmrDrawSpeed.Tick += new System.EventHandler( this.tmrDrawSpeed_Tick );
            // 
            // btnFill
            // 
            this.btnFill.Location = new System.Drawing.Point( 12, 12 );
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size( 75, 23 );
            this.btnFill.TabIndex = 0;
            this.btnFill.Text = "Fill";
            this.btnFill.UseVisualStyleBackColor = true;
            this.btnFill.Click += new System.EventHandler( this.btnFill_Click );
            // 
            // pnlPad
            // 
            this.pnlPad.Controls.Add( this.pbImage );
            this.pnlPad.Location = new System.Drawing.Point( 93, 63 );
            this.pnlPad.Name = "pnlPad";
            this.pnlPad.Size = new System.Drawing.Size( 634, 481 );
            this.pnlPad.TabIndex = 1;
            // 
            // pbImage
            // 
            this.pbImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbImage.Location = new System.Drawing.Point( 3, 3 );
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size( 628, 475 );
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            // 
            // tbarFill
            // 
            this.tbarFill.Location = new System.Drawing.Point( 93, 12 );
            this.tbarFill.Maximum = 100;
            this.tbarFill.Name = "tbarFill";
            this.tbarFill.Size = new System.Drawing.Size( 634, 45 );
            this.tbarFill.TabIndex = 0;
            // 
            // btnShot
            // 
            this.btnShot.Location = new System.Drawing.Point( 12, 121 );
            this.btnShot.Name = "btnShot";
            this.btnShot.Size = new System.Drawing.Size( 75, 23 );
            this.btnShot.TabIndex = 2;
            this.btnShot.Text = "Shot";
            this.btnShot.UseVisualStyleBackColor = true;
            this.btnShot.Click += new System.EventHandler( this.btnShot_Click );
            // 
            // btnWine
            // 
            this.btnWine.Location = new System.Drawing.Point( 12, 92 );
            this.btnWine.Name = "btnWine";
            this.btnWine.Size = new System.Drawing.Size( 75, 23 );
            this.btnWine.TabIndex = 3;
            this.btnWine.Text = "Wine";
            this.btnWine.UseVisualStyleBackColor = true;
            this.btnWine.Click += new System.EventHandler( this.btnWine_Click );
            // 
            // btnBeer
            // 
            this.btnBeer.Location = new System.Drawing.Point( 12, 63 );
            this.btnBeer.Name = "btnBeer";
            this.btnBeer.Size = new System.Drawing.Size( 75, 23 );
            this.btnBeer.TabIndex = 4;
            this.btnBeer.Text = "Beer";
            this.btnBeer.UseVisualStyleBackColor = true;
            this.btnBeer.Click += new System.EventHandler( this.btnBeer_Click );
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 739, 556 );
            this.Controls.Add( this.btnBeer );
            this.Controls.Add( this.btnWine );
            this.Controls.Add( this.btnShot );
            this.Controls.Add( this.tbarFill );
            this.Controls.Add( this.pnlPad );
            this.Controls.Add( this.btnFill );
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler( this.Form1_Paint );
            this.pnlPad.ResumeLayout( false );
            ( (System.ComponentModel.ISupportInitialize)( this.pbImage ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.tbarFill ) ).EndInit( );
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.Timer tmrDrawSpeed;
        private System.Windows.Forms.Button btnFill;
        private System.Windows.Forms.Panel pnlPad;
        private System.Windows.Forms.TrackBar tbarFill;
        private System.Windows.Forms.Button btnShot;
        private System.Windows.Forms.Button btnWine;
        private System.Windows.Forms.Button btnBeer;
        private System.Windows.Forms.PictureBox pbImage;
    }
}

