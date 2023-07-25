namespace ContactControls
{
    partial class AddressPanel
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
            this.lblStreet = new System.Windows.Forms.Label( );
            this.txtStreet = new System.Windows.Forms.TextBox( );
            this.lblCity = new System.Windows.Forms.Label( );
            this.txtCity = new System.Windows.Forms.TextBox( );
            this.lblState = new System.Windows.Forms.Label( );
            this.lblZip = new System.Windows.Forms.Label( );
            this.lblCountry = new System.Windows.Forms.Label( );
            this.txtZip = new System.Windows.Forms.MaskedTextBox( );
            this.txtState = new System.Windows.Forms.MaskedTextBox( );
            this.lblType = new System.Windows.Forms.Label( );
            this.cmbAddressType = new DevExpress.XtraEditors.ComboBoxEdit( );
            this.cmbCountry = new DevExpress.XtraEditors.ComboBoxEdit( );
            ( (System.ComponentModel.ISupportInitialize)( this.cmbAddressType.Properties ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.cmbCountry.Properties ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // lblStreet
            // 
            this.lblStreet.AutoSize = true;
            this.lblStreet.Location = new System.Drawing.Point( 6, 28 );
            this.lblStreet.Name = "lblStreet";
            this.lblStreet.Size = new System.Drawing.Size( 35, 13 );
            this.lblStreet.TabIndex = 0;
            this.lblStreet.Text = "Street";
            this.lblStreet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStreet
            // 
            this.txtStreet.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.txtStreet.Location = new System.Drawing.Point( 47, 28 );
            this.txtStreet.Multiline = true;
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size( 224, 64 );
            this.txtStreet.TabIndex = 1;
            // 
            // lblCity
            // 
            this.lblCity.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point( 17, 101 );
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size( 24, 13 );
            this.lblCity.TabIndex = 2;
            this.lblCity.Text = "City";
            this.lblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCity
            // 
            this.txtCity.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.txtCity.Location = new System.Drawing.Point( 47, 98 );
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size( 224, 20 );
            this.txtCity.TabIndex = 3;
            // 
            // lblState
            // 
            this.lblState.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point( 9, 127 );
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size( 32, 13 );
            this.lblState.TabIndex = 5;
            this.lblState.Text = "State";
            this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblZip
            // 
            this.lblZip.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.lblZip.AutoSize = true;
            this.lblZip.Location = new System.Drawing.Point( 82, 127 );
            this.lblZip.Name = "lblZip";
            this.lblZip.Size = new System.Drawing.Size( 22, 13 );
            this.lblZip.TabIndex = 7;
            this.lblZip.Text = "Zip";
            // 
            // lblCountry
            // 
            this.lblCountry.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point( -2, 153 );
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size( 43, 13 );
            this.lblCountry.TabIndex = 9;
            this.lblCountry.Text = "Country";
            this.lblCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtZip
            // 
            this.txtZip.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.txtZip.Location = new System.Drawing.Point( 110, 124 );
            this.txtZip.Mask = "00000-9999";
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size( 73, 20 );
            this.txtZip.TabIndex = 10;
            // 
            // txtState
            // 
            this.txtState.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.txtState.Location = new System.Drawing.Point( 48, 124 );
            this.txtState.Mask = "LL";
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size( 28, 20 );
            this.txtState.TabIndex = 11;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point( 10, 7 );
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size( 31, 13 );
            this.lblType.TabIndex = 13;
            this.lblType.Text = "Type";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbAddressType
            // 
            this.cmbAddressType.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.cmbAddressType.Location = new System.Drawing.Point( 47, 3 );
            this.cmbAddressType.Name = "cmbAddressType";
            this.cmbAddressType.Properties.Buttons.AddRange( new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)} );
            this.cmbAddressType.Size = new System.Drawing.Size( 224, 20 );
            this.cmbAddressType.TabIndex = 14;
            // 
            // cmbCountry
            // 
            this.cmbCountry.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.cmbCountry.Location = new System.Drawing.Point( 48, 150 );
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Properties.Buttons.AddRange( new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)} );
            this.cmbCountry.Size = new System.Drawing.Size( 223, 20 );
            this.cmbCountry.TabIndex = 15;
            // 
            // AddressPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.cmbCountry );
            this.Controls.Add( this.cmbAddressType );
            this.Controls.Add( this.lblType );
            this.Controls.Add( this.txtState );
            this.Controls.Add( this.txtZip );
            this.Controls.Add( this.lblCountry );
            this.Controls.Add( this.lblZip );
            this.Controls.Add( this.lblState );
            this.Controls.Add( this.txtCity );
            this.Controls.Add( this.lblCity );
            this.Controls.Add( this.txtStreet );
            this.Controls.Add( this.lblStreet );
            this.Name = "AddressPanel";
            this.Size = new System.Drawing.Size( 274, 174 );
            ( (System.ComponentModel.ISupportInitialize)( this.cmbAddressType.Properties ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.cmbCountry.Properties ) ).EndInit( );
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.Label lblStreet;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblZip;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.MaskedTextBox txtZip;
        private System.Windows.Forms.MaskedTextBox txtState;
        private System.Windows.Forms.Label lblType;
        private DevExpress.XtraEditors.ComboBoxEdit cmbAddressType;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCountry;
    }
}
