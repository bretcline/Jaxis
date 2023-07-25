namespace BeverageManagement.Controls
{
    partial class PourControl
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
            this.pbEmpty = new System.Windows.Forms.PictureBox( );
            this.pbType = new System.Windows.Forms.PictureBox( );
            this.timer1 = new System.Windows.Forms.Timer( this.components );
            ( (System.ComponentModel.ISupportInitialize)( this.pbEmpty ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.pbType ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // pbEmpty
            // 
            this.pbEmpty.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.pbEmpty.BackgroundImage = global::BeverageManagement.Properties.Resources.All;
            this.pbEmpty.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbEmpty.InitialImage = global::BeverageManagement.Properties.Resources.All;
            this.pbEmpty.Location = new System.Drawing.Point( 3, 3 );
            this.pbEmpty.Name = "pbEmpty";
            this.pbEmpty.Size = new System.Drawing.Size( 360, 326 );
            this.pbEmpty.TabIndex = 0;
            this.pbEmpty.TabStop = false;
            // 
            // pbType
            // 
            this.pbType.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.pbType.BackgroundImage = global::BeverageManagement.Properties.Resources.Mixed_full;
            this.pbType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbType.Location = new System.Drawing.Point( 3, 3 );
            this.pbType.Name = "pbType";
            this.pbType.Size = new System.Drawing.Size( 360, 326 );
            this.pbType.TabIndex = 2;
            this.pbType.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler( this.timer1_Tick );
            // 
            // PourControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.pbEmpty );
            this.Controls.Add( this.pbType );
            this.Name = "PourControl";
            this.Size = new System.Drawing.Size( 366, 332 );
            ( (System.ComponentModel.ISupportInitialize)( this.pbEmpty ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.pbType ) ).EndInit( );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.PictureBox pbEmpty;
        private System.Windows.Forms.PictureBox pbType;
        private System.Windows.Forms.Timer timer1;
    }
}
