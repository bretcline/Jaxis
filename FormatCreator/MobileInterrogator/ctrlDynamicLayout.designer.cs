namespace MobileInterrogator
{
    partial class ctrlDynamicLayout
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
            this.pnlTagInfo = new System.Windows.Forms.Panel( );
            this.SuspendLayout( );
            // 
            // pnlTagInfo
            // 
            this.pnlTagInfo.AutoScroll = true;
            this.pnlTagInfo.BackColor = System.Drawing.Color.Transparent;
            this.pnlTagInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTagInfo.Location = new System.Drawing.Point( 0, 0 );
            this.pnlTagInfo.Name = "pnlTagInfo";
            this.pnlTagInfo.Size = new System.Drawing.Size( 200, 180 );
            // 
            // ctrlDynamicLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 96F, 96F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add( this.pnlTagInfo );
            this.Name = "ctrlDynamicLayout";
            this.Size = new System.Drawing.Size( 200, 180 );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Panel pnlTagInfo;
    }
}
