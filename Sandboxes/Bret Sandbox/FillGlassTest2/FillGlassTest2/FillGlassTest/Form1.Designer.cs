namespace FillGlassTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( Form1 ) );
            this.trackBar1 = new System.Windows.Forms.TrackBar( );
            this.btnFill = new System.Windows.Forms.Button( );
            this.timer1 = new System.Windows.Forms.Timer( this.components );
            this.btnWine = new System.Windows.Forms.Button( );
            this.btnBear = new System.Windows.Forms.Button( );
            this.btnShot = new System.Windows.Forms.Button( );
            this.btnReset = new System.Windows.Forms.Button( );
            this.fillGlass1 = new Jaxis.Controls.GlassFill.FillGlass( );
            ( (System.ComponentModel.ISupportInitialize)( this.trackBar1 ) ).BeginInit( );
            this.SuspendLayout( );
            //
            // trackBar1
            //
            this.trackBar1.Location = new System.Drawing.Point( 13, 13 );
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size( 481, 45 );
            this.trackBar1.TabIndex = 1;
            //
            // btnFill
            //
            this.btnFill.Location = new System.Drawing.Point( 13, 70 );
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size( 75, 23 );
            this.btnFill.TabIndex = 2;
            this.btnFill.Text = "Fill";
            this.btnFill.UseVisualStyleBackColor = true;
            this.btnFill.Click += new System.EventHandler( this.btnFill_Click );
            //
            // btnWine
            //
            this.btnWine.Location = new System.Drawing.Point( 12, 99 );
            this.btnWine.Name = "btnWine";
            this.btnWine.Size = new System.Drawing.Size( 75, 23 );
            this.btnWine.TabIndex = 2;
            this.btnWine.Text = "Wine";
            this.btnWine.UseVisualStyleBackColor = true;
            this.btnWine.Click += new System.EventHandler( this.btnWine_Click );
            //
            // btnBear
            //
            this.btnBear.Location = new System.Drawing.Point( 13, 128 );
            this.btnBear.Name = "btnBear";
            this.btnBear.Size = new System.Drawing.Size( 75, 23 );
            this.btnBear.TabIndex = 2;
            this.btnBear.Text = "Beer";
            this.btnBear.UseVisualStyleBackColor = true;
            this.btnBear.Click += new System.EventHandler( this.btnBeer_Click );
            //
            // btnShot
            //
            this.btnShot.Location = new System.Drawing.Point( 13, 157 );
            this.btnShot.Name = "btnShot";
            this.btnShot.Size = new System.Drawing.Size( 75, 23 );
            this.btnShot.TabIndex = 2;
            this.btnShot.Text = "Shot";
            this.btnShot.UseVisualStyleBackColor = true;
            this.btnShot.Click += new System.EventHandler( this.btnShot_Click );
            //
            // btnReset
            //
            this.btnReset.Location = new System.Drawing.Point( 13, 221 );
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size( 75, 23 );
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler( this.btnReset_Click );
            //
            // fillGlass1
            //
            this.fillGlass1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.fillGlass1.BackColor = System.Drawing.Color.White;
            this.fillGlass1.BackgroundImage = ( (System.Drawing.Image)( resources.GetObject( "fillGlass1.BackgroundImage" ) ) );
            this.fillGlass1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.fillGlass1.FillLevel = 100;
            this.fillGlass1.GlassType = Jaxis.Controls.GlassFill.GlassTypes.Beer;
            this.fillGlass1.Location = new System.Drawing.Point( 112, 70 );
            this.fillGlass1.Name = "fillGlass1";
            this.fillGlass1.Size = new System.Drawing.Size( 413, 503 );
            this.fillGlass1.TabIndex = 4;
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 537, 585 );
            this.Controls.Add( this.fillGlass1 );
            this.Controls.Add( this.btnReset );
            this.Controls.Add( this.btnShot );
            this.Controls.Add( this.btnBear );
            this.Controls.Add( this.btnWine );
            this.Controls.Add( this.btnFill );
            this.Controls.Add( this.trackBar1 );
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            ( (System.ComponentModel.ISupportInitialize)( this.trackBar1 ) ).EndInit( );
            this.ResumeLayout( false );
            this.PerformLayout( );
        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button btnFill;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnWine;
        private System.Windows.Forms.Button btnBear;
        private System.Windows.Forms.Button btnShot;
        private Jaxis.Controls.GlassFill.FillGlass fillGlass1;
        private System.Windows.Forms.Button btnReset;
    }
}