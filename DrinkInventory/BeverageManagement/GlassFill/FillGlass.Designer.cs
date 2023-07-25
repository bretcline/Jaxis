namespace Jaxis.Controls.GlassFill
{
    partial class FillGlass
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.components = new System.ComponentModel.Container( );
            this.timer1 = new System.Windows.Forms.Timer( this.components );
            this.picEmpty = new System.Windows.Forms.PictureBox( );
            this.picType = new System.Windows.Forms.PictureBox( );
            ( (System.ComponentModel.ISupportInitialize)( this.picEmpty ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.picType ) ).BeginInit( );
            this.SuspendLayout( );
            //
            // timer1
            //
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler( this.timer1_Tick );
            //
            // picEmpty
            //
            this.picEmpty.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.picEmpty.BackgroundImage = global::Jaxis.Controls.GlassFill.Properties.Resources.All;
            this.picEmpty.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picEmpty.Location = new System.Drawing.Point( 0, 0 );
            this.picEmpty.Margin = new System.Windows.Forms.Padding( 0 );
            this.picEmpty.Name = "picEmpty";
            this.picEmpty.Size = new System.Drawing.Size( 360, 326 );
            this.picEmpty.TabIndex = 0;
            this.picEmpty.TabStop = false;
            //
            // picType
            //
            this.picType.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.picType.BackgroundImage = global::Jaxis.Controls.GlassFill.Properties.Resources.All;
            this.picType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picType.Location = new System.Drawing.Point( 0, 0 );
            this.picType.Margin = new System.Windows.Forms.Padding( 0 );
            this.picType.Name = "picType";
            this.picType.Size = new System.Drawing.Size( 360, 326 );
            this.picType.TabIndex = 1;
            this.picType.TabStop = false;
            //
            // FillGlass
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add( this.picType );
            this.Controls.Add( this.picEmpty );
            this.Name = "FillGlass";
            this.Size = new System.Drawing.Size( 360, 326 );
            ( (System.ComponentModel.ISupportInitialize)( this.picEmpty ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.picType ) ).EndInit( );
            this.ResumeLayout( false );
        }

        #endregion Component Designer generated code

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox picEmpty;
        private System.Windows.Forms.PictureBox picType;
    }
}