namespace ContactControls
{
    partial class PhoneNumberPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblExtension = new System.Windows.Forms.Label( );
            this.cmbType = new DevExpress.XtraEditors.ComboBoxEdit( );
            this.mtxtExtension = new DevExpress.XtraEditors.TextEdit( );
            this.txtPhoneNumber = new DevExpress.XtraEditors.TextEdit( );
            ( (System.ComponentModel.ISupportInitialize)( this.cmbType.Properties ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.mtxtExtension.Properties ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.txtPhoneNumber.Properties ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // lblExtension
            // 
            this.lblExtension.AutoSize = true;
            this.lblExtension.Location = new System.Drawing.Point( 84, 30 );
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size( 25, 13 );
            this.lblExtension.TabIndex = 5;
            this.lblExtension.Text = "Ext.";
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.cmbType.Location = new System.Drawing.Point( 0, 1 );
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Buttons.AddRange( new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)} );
            this.cmbType.Size = new System.Drawing.Size( 174, 20 );
            this.cmbType.TabIndex = 11;
            // 
            // mtxtExtension
            // 
            this.mtxtExtension.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.mtxtExtension.Location = new System.Drawing.Point( 109, 27 );
            this.mtxtExtension.Name = "mtxtExtension";
            this.mtxtExtension.Size = new System.Drawing.Size( 65, 20 );
            this.mtxtExtension.TabIndex = 12;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point( 0, 27 );
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Properties.Mask.EditMask = "000-000-0000";
            this.txtPhoneNumber.Size = new System.Drawing.Size( 86, 20 );
            this.txtPhoneNumber.TabIndex = 13;
            // 
            // PhoneNumberPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.txtPhoneNumber );
            this.Controls.Add( this.mtxtExtension );
            this.Controls.Add( this.cmbType );
            this.Controls.Add( this.lblExtension );
            this.Name = "PhoneNumberPanel";
            this.Size = new System.Drawing.Size( 174, 50 );
            ( (System.ComponentModel.ISupportInitialize)( this.cmbType.Properties ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.mtxtExtension.Properties ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.txtPhoneNumber.Properties ) ).EndInit( );
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.Label lblExtension;
        private DevExpress.XtraEditors.ComboBoxEdit cmbType;
        private DevExpress.XtraEditors.TextEdit mtxtExtension;
        private DevExpress.XtraEditors.TextEdit txtPhoneNumber;

    }
}
