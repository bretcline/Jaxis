namespace ContactControls
{
    partial class CompanyPanel
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
            this.lblCompanyName = new System.Windows.Forms.Label( );
            this.txtCompanyName = new System.Windows.Forms.TextBox( );
            this.lblRelationshipType = new System.Windows.Forms.Label( );
            this.grdContacts = new DevExpress.XtraGrid.GridControl( );
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView( );
            this.pnlPhoneNumbers = new System.Windows.Forms.Panel( );
            this.cmbRelationshipType = new System.Windows.Forms.ComboBox( );
            this.tabPage1 = new System.Windows.Forms.TabPage( );
            this.addressPanel1 = new ContactControls.AddressPanel( );
            this.tabAddresses = new System.Windows.Forms.TabControl( );
            ( (System.ComponentModel.ISupportInitialize)( this.grdContacts ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.gridView1 ) ).BeginInit( );
            this.tabPage1.SuspendLayout( );
            this.tabAddresses.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Location = new System.Drawing.Point( 5, 7 );
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size( 82, 13 );
            this.lblCompanyName.TabIndex = 0;
            this.lblCompanyName.Text = "Company Name";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.txtCompanyName.Location = new System.Drawing.Point( 103, 4 );
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size( 209, 20 );
            this.txtCompanyName.TabIndex = 1;
            // 
            // lblRelationshipType
            // 
            this.lblRelationshipType.AutoSize = true;
            this.lblRelationshipType.Location = new System.Drawing.Point( 5, 33 );
            this.lblRelationshipType.Name = "lblRelationshipType";
            this.lblRelationshipType.Size = new System.Drawing.Size( 92, 13 );
            this.lblRelationshipType.TabIndex = 3;
            this.lblRelationshipType.Text = "Relationship Type";
            // 
            // grdContacts
            // 
            this.grdContacts.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.grdContacts.Location = new System.Drawing.Point( 8, 269 );
            this.grdContacts.MainView = this.gridView1;
            this.grdContacts.Name = "grdContacts";
            this.grdContacts.Size = new System.Drawing.Size( 304, 200 );
            this.grdContacts.TabIndex = 5;
            this.grdContacts.ViewCollection.AddRange( new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1} );
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdContacts;
            this.gridView1.Name = "gridView1";
            // 
            // pnlPhoneNumbers
            // 
            this.pnlPhoneNumbers.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.pnlPhoneNumbers.AutoScroll = true;
            this.pnlPhoneNumbers.Location = new System.Drawing.Point( 4, 476 );
            this.pnlPhoneNumbers.Name = "pnlPhoneNumbers";
            this.pnlPhoneNumbers.Size = new System.Drawing.Size( 308, 70 );
            this.pnlPhoneNumbers.TabIndex = 6;
            // 
            // cmbRelationshipType
            // 
            this.cmbRelationshipType.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.cmbRelationshipType.FormattingEnabled = true;
            this.cmbRelationshipType.Location = new System.Drawing.Point( 104, 31 );
            this.cmbRelationshipType.Name = "cmbRelationshipType";
            this.cmbRelationshipType.Size = new System.Drawing.Size( 208, 21 );
            this.cmbRelationshipType.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add( this.addressPanel1 );
            this.tabPage1.Location = new System.Drawing.Point( 4, 22 );
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage1.Size = new System.Drawing.Size( 296, 186 );
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // addressPanel1
            // 
            this.addressPanel1.Location = new System.Drawing.Point( 3, 3 );
            this.addressPanel1.Name = "addressPanel1";
            this.addressPanel1.Size = new System.Drawing.Size( 287, 155 );
            this.addressPanel1.TabIndex = 0;
            // 
            // tabAddresses
            // 
            this.tabAddresses.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.tabAddresses.Controls.Add( this.tabPage1 );
            this.tabAddresses.Location = new System.Drawing.Point( 8, 50 );
            this.tabAddresses.Name = "tabAddresses";
            this.tabAddresses.SelectedIndex = 0;
            this.tabAddresses.Size = new System.Drawing.Size( 304, 212 );
            this.tabAddresses.TabIndex = 4;
            // 
            // CompanyPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.cmbRelationshipType );
            this.Controls.Add( this.pnlPhoneNumbers );
            this.Controls.Add( this.grdContacts );
            this.Controls.Add( this.tabAddresses );
            this.Controls.Add( this.lblRelationshipType );
            this.Controls.Add( this.txtCompanyName );
            this.Controls.Add( this.lblCompanyName );
            this.Name = "CompanyPanel";
            this.Size = new System.Drawing.Size( 318, 561 );
            ( (System.ComponentModel.ISupportInitialize)( this.grdContacts ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.gridView1 ) ).EndInit( );
            this.tabPage1.ResumeLayout( false );
            this.tabAddresses.ResumeLayout( false );
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label lblRelationshipType;
        private DevExpress.XtraGrid.GridControl grdContacts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel pnlPhoneNumbers;
        private System.Windows.Forms.ComboBox cmbRelationshipType;
        private System.Windows.Forms.TabPage tabPage1;
        private AddressPanel addressPanel1;
        private System.Windows.Forms.TabControl tabAddresses;
    }
}
