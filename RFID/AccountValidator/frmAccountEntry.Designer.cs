using Jaxis.Util.Log4Net;
namespace AccountValidator
{
    partial class frmAccountEntry
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
            Log.Wrap<int>("frmAccountEntry.Designer::Dispose", LogType.Debug, true, () =>
            {
                if( disposing && ( components != null ) )
                {
                    components.Dispose( );
                }
                base.Dispose( disposing );
                return 1;
            });

        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.txtAccountKey = new DevExpress.XtraEditors.TextEdit( );
            this.btnSubmit = new DevExpress.XtraEditors.SimpleButton( );
            ( (System.ComponentModel.ISupportInitialize)( this.txtAccountKey.Properties ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // txtAccountKey
            // 
            this.txtAccountKey.Location = new System.Drawing.Point( 3, 12 );
            this.txtAccountKey.Name = "txtAccountKey";
            this.txtAccountKey.Size = new System.Drawing.Size( 195, 20 );
            this.txtAccountKey.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point( 204, 10 );
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size( 75, 23 );
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.Click += new System.EventHandler( this.btnSubmit_Click );
            // 
            // frmAccountEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 284, 38 );
            this.Controls.Add( this.txtAccountKey );
            this.Controls.Add( this.btnSubmit );
            this.Name = "frmAccountEntry";
            this.Text = "frmAccountEntry";
            ( (System.ComponentModel.ISupportInitialize)( this.txtAccountKey.Properties ) ).EndInit( );
            this.ResumeLayout( false );

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtAccountKey;
        private DevExpress.XtraEditors.SimpleButton btnSubmit;
    }
}