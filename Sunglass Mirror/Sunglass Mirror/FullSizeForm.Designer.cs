namespace Sunglass_Mirror
{
    partial class FullSizeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sbPicture = new DevExpress.XtraEditors.SimpleButton( );
            this.SuspendLayout( );
            // 
            // sbPicture
            // 
            this.sbPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sbPicture.Location = new System.Drawing.Point( 0, 0 );
            this.sbPicture.Name = "sbPicture";
            this.sbPicture.Size = new System.Drawing.Size( 1366, 768 );
            this.sbPicture.TabIndex = 0;
            this.sbPicture.Click += new System.EventHandler( this.sbPicture_Click );
            // 
            // FullSizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 1366, 768 );
            this.Controls.Add( this.sbPicture );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FullSizeForm";
            this.Text = "FullSizeForm";
            this.ResumeLayout( false );

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton sbPicture;
    }
}