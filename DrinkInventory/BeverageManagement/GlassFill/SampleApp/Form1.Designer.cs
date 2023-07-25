namespace SampleApp
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
            this.fillGlass1 = new Jaxis.Controls.GlassFill.FillGlass( );
            this.button1 = new System.Windows.Forms.Button( );
            this.trackBar1 = new System.Windows.Forms.TrackBar( );
            this.rdoBeer = new System.Windows.Forms.RadioButton( );
            this.rdoWine = new System.Windows.Forms.RadioButton( );
            this.rdoLiquor = new System.Windows.Forms.RadioButton( );
            ( (System.ComponentModel.ISupportInitialize)( this.trackBar1 ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // fillGlass1
            // 
            this.fillGlass1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fillGlass1.BackColor = System.Drawing.Color.White;
            this.fillGlass1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.fillGlass1.FillLevel = 100;
            this.fillGlass1.GlassType = Jaxis.Controls.GlassFill.GlassTypes.Beer;
            this.fillGlass1.Location = new System.Drawing.Point( 12, 81 );
            this.fillGlass1.Name = "fillGlass1";
            this.fillGlass1.Size = new System.Drawing.Size( 360, 326 );
            this.fillGlass1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point( 12, 52 );
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size( 75, 23 );
            this.button1.TabIndex = 1;
            this.button1.Text = "Fill";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler( this.button1_Click );
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point( 12, 1 );
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size( 360, 45 );
            this.trackBar1.TabIndex = 2;
            this.trackBar1.Value = 50;
            // 
            // rdoBeer
            // 
            this.rdoBeer.AutoSize = true;
            this.rdoBeer.Location = new System.Drawing.Point( 104, 52 );
            this.rdoBeer.Name = "rdoBeer";
            this.rdoBeer.Size = new System.Drawing.Size( 47, 17 );
            this.rdoBeer.TabIndex = 3;
            this.rdoBeer.TabStop = true;
            this.rdoBeer.Text = "Beer";
            this.rdoBeer.UseVisualStyleBackColor = true;
            // 
            // rdoWine
            // 
            this.rdoWine.AutoSize = true;
            this.rdoWine.Location = new System.Drawing.Point( 195, 52 );
            this.rdoWine.Name = "rdoWine";
            this.rdoWine.Size = new System.Drawing.Size( 50, 17 );
            this.rdoWine.TabIndex = 4;
            this.rdoWine.TabStop = true;
            this.rdoWine.Text = "Wine";
            this.rdoWine.UseVisualStyleBackColor = true;
            // 
            // rdoLiquor
            // 
            this.rdoLiquor.AutoSize = true;
            this.rdoLiquor.Location = new System.Drawing.Point( 287, 52 );
            this.rdoLiquor.Name = "rdoLiquor";
            this.rdoLiquor.Size = new System.Drawing.Size( 54, 17 );
            this.rdoLiquor.TabIndex = 5;
            this.rdoLiquor.TabStop = true;
            this.rdoLiquor.Text = "Liquor";
            this.rdoLiquor.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 393, 413 );
            this.Controls.Add( this.rdoLiquor );
            this.Controls.Add( this.rdoWine );
            this.Controls.Add( this.rdoBeer );
            this.Controls.Add( this.trackBar1 );
            this.Controls.Add( this.button1 );
            this.Controls.Add( this.fillGlass1 );
            this.Name = "Form1";
            this.Text = "Form1";
            ( (System.ComponentModel.ISupportInitialize)( this.trackBar1 ) ).EndInit( );
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private Jaxis.Controls.GlassFill.FillGlass fillGlass1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.RadioButton rdoBeer;
        private System.Windows.Forms.RadioButton rdoWine;
        private System.Windows.Forms.RadioButton rdoLiquor;
    }
}

