namespace DataImporter
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
            this.btnLoad = new System.Windows.Forms.Button( );
            this.ofdImportFile = new System.Windows.Forms.OpenFileDialog( );
            this.txtPath = new System.Windows.Forms.TextBox( );
            this.btnOpenFile = new System.Windows.Forms.Button( );
            this.panel1 = new System.Windows.Forms.Panel( );
            this.label7 = new System.Windows.Forms.Label( );
            this.txtXLS = new System.Windows.Forms.TextBox( );
            this.btnImport = new System.Windows.Forms.Button( );
            this.btnOpenExcel = new System.Windows.Forms.Button( );
            this.label6 = new System.Windows.Forms.Label( );
            this.panel2 = new System.Windows.Forms.Panel( );
            this.btnAssignSAAS = new System.Windows.Forms.Button( );
            this.label8 = new System.Windows.Forms.Label( );
            this.txtSAASOrg = new System.Windows.Forms.TextBox( );
            this.btnAddMaterial = new System.Windows.Forms.Button( );
            this.btnAddGroup = new System.Windows.Forms.Button( );
            this.btnRemove = new System.Windows.Forms.Button( );
            this.btnSave = new System.Windows.Forms.Button( );
            this.panel3 = new System.Windows.Forms.Panel( );
            this.btnImage = new System.Windows.Forms.Button( );
            this.btnUpdate = new System.Windows.Forms.Button( );
            this.txtVolumn = new System.Windows.Forms.TextBox( );
            this.label3 = new System.Windows.Forms.Label( );
            this.cmbParent = new System.Windows.Forms.ComboBox( );
            this.label5 = new System.Windows.Forms.Label( );
            this.label4 = new System.Windows.Forms.Label( );
            this.label2 = new System.Windows.Forms.Label( );
            this.label1 = new System.Windows.Forms.Label( );
            this.txtUPC = new System.Windows.Forms.TextBox( );
            this.cmbSubType = new System.Windows.Forms.ComboBox( );
            this.txtName = new System.Windows.Forms.TextBox( );
            this.pbImage = new System.Windows.Forms.PictureBox( );
            this.tvMaterial = new System.Windows.Forms.TreeView( );
            this.panel1.SuspendLayout( );
            this.panel2.SuspendLayout( );
            this.panel3.SuspendLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.pbImage ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnLoad.Location = new System.Drawing.Point( 827, 2 );
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size( 75, 23 );
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler( this.btnLoad_Click );
            // 
            // ofdImportFile
            // 
            this.ofdImportFile.FileName = "openFileDialog1";
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.txtPath.Location = new System.Drawing.Point( 47, 4 );
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size( 741, 20 );
            this.txtPath.TabIndex = 1;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnOpenFile.Location = new System.Drawing.Point( 794, 2 );
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size( 27, 23 );
            this.btnOpenFile.TabIndex = 2;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler( this.btnOpenFile_Click );
            // 
            // panel1
            // 
            this.panel1.Controls.Add( this.label7 );
            this.panel1.Controls.Add( this.txtXLS );
            this.panel1.Controls.Add( this.btnImport );
            this.panel1.Controls.Add( this.btnOpenExcel );
            this.panel1.Controls.Add( this.label6 );
            this.panel1.Controls.Add( this.txtPath );
            this.panel1.Controls.Add( this.btnLoad );
            this.panel1.Controls.Add( this.btnOpenFile );
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point( 0, 0 );
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size( 905, 57 );
            this.panel1.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point( 12, 33 );
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size( 27, 13 );
            this.label7.TabIndex = 13;
            this.label7.Text = "XLS";
            // 
            // txtXLS
            // 
            this.txtXLS.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.txtXLS.Location = new System.Drawing.Point( 47, 30 );
            this.txtXLS.Name = "txtXLS";
            this.txtXLS.Size = new System.Drawing.Size( 741, 20 );
            this.txtXLS.TabIndex = 11;
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnImport.Location = new System.Drawing.Point( 827, 28 );
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size( 75, 23 );
            this.btnImport.TabIndex = 10;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler( this.btnImport_Click );
            // 
            // btnOpenExcel
            // 
            this.btnOpenExcel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnOpenExcel.Location = new System.Drawing.Point( 794, 28 );
            this.btnOpenExcel.Name = "btnOpenExcel";
            this.btnOpenExcel.Size = new System.Drawing.Size( 27, 23 );
            this.btnOpenExcel.TabIndex = 12;
            this.btnOpenExcel.Text = "...";
            this.btnOpenExcel.UseVisualStyleBackColor = true;
            this.btnOpenExcel.Click += new System.EventHandler( this.btnOpenExcel_Click );
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point( 12, 7 );
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size( 29, 13 );
            this.label6.TabIndex = 9;
            this.label6.Text = "XML";
            // 
            // panel2
            // 
            this.panel2.Controls.Add( this.btnAssignSAAS );
            this.panel2.Controls.Add( this.label8 );
            this.panel2.Controls.Add( this.txtSAASOrg );
            this.panel2.Controls.Add( this.btnAddMaterial );
            this.panel2.Controls.Add( this.btnAddGroup );
            this.panel2.Controls.Add( this.btnRemove );
            this.panel2.Controls.Add( this.btnSave );
            this.panel2.Controls.Add( this.panel3 );
            this.panel2.Controls.Add( this.tvMaterial );
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point( 0, 57 );
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size( 905, 603 );
            this.panel2.TabIndex = 4;
            // 
            // btnAssignSAAS
            // 
            this.btnAssignSAAS.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnAssignSAAS.Location = new System.Drawing.Point( 521, 9 );
            this.btnAssignSAAS.Name = "btnAssignSAAS";
            this.btnAssignSAAS.Size = new System.Drawing.Size( 68, 23 );
            this.btnAssignSAAS.TabIndex = 14;
            this.btnAssignSAAS.Text = "Assign";
            this.btnAssignSAAS.UseVisualStyleBackColor = true;
            this.btnAssignSAAS.Click += new System.EventHandler( this.btnAssignSAAS_Click );
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point( 12, 12 );
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size( 55, 13 );
            this.label8.TabIndex = 21;
            this.label8.Text = "SAAS Org";
            // 
            // txtSAASOrg
            // 
            this.txtSAASOrg.Location = new System.Drawing.Point( 73, 9 );
            this.txtSAASOrg.Name = "txtSAASOrg";
            this.txtSAASOrg.Size = new System.Drawing.Size( 442, 20 );
            this.txtSAASOrg.TabIndex = 20;
            // 
            // btnAddMaterial
            // 
            this.btnAddMaterial.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnAddMaterial.Location = new System.Drawing.Point( 88, 574 );
            this.btnAddMaterial.Name = "btnAddMaterial";
            this.btnAddMaterial.Size = new System.Drawing.Size( 75, 23 );
            this.btnAddMaterial.TabIndex = 19;
            this.btnAddMaterial.Text = "Add Material";
            this.btnAddMaterial.UseVisualStyleBackColor = true;
            this.btnAddMaterial.Click += new System.EventHandler( this.btnAddMaterial_Click );
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnAddGroup.Location = new System.Drawing.Point( 7, 574 );
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size( 75, 23 );
            this.btnAddGroup.TabIndex = 18;
            this.btnAddGroup.Text = "Add Group";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler( this.btnAddGroup_Click );
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnRemove.Location = new System.Drawing.Point( 219, 574 );
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size( 75, 23 );
            this.btnRemove.TabIndex = 17;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler( this.btnRemove_Click );
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnSave.Location = new System.Drawing.Point( 514, 574 );
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size( 75, 23 );
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler( this.btnSave_Click_1 );
            // 
            // panel3
            // 
            this.panel3.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.panel3.Controls.Add( this.btnImage );
            this.panel3.Controls.Add( this.btnUpdate );
            this.panel3.Controls.Add( this.txtVolumn );
            this.panel3.Controls.Add( this.label3 );
            this.panel3.Controls.Add( this.cmbParent );
            this.panel3.Controls.Add( this.label5 );
            this.panel3.Controls.Add( this.label4 );
            this.panel3.Controls.Add( this.label2 );
            this.panel3.Controls.Add( this.label1 );
            this.panel3.Controls.Add( this.txtUPC );
            this.panel3.Controls.Add( this.cmbSubType );
            this.panel3.Controls.Add( this.txtName );
            this.panel3.Controls.Add( this.pbImage );
            this.panel3.Location = new System.Drawing.Point( 595, 6 );
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size( 307, 594 );
            this.panel3.TabIndex = 8;
            // 
            // btnImage
            // 
            this.btnImage.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnImage.Location = new System.Drawing.Point( 14, 568 );
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size( 75, 23 );
            this.btnImage.TabIndex = 16;
            this.btnImage.Text = "Load Image";
            this.btnImage.UseVisualStyleBackColor = true;
            this.btnImage.Click += new System.EventHandler( this.btnImage_Click );
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnUpdate.Location = new System.Drawing.Point( 223, 568 );
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size( 75, 23 );
            this.btnUpdate.TabIndex = 15;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler( this.btnUpdate_Click );
            // 
            // txtVolumn
            // 
            this.txtVolumn.Location = new System.Drawing.Point( 88, 55 );
            this.txtVolumn.Name = "txtVolumn";
            this.txtVolumn.Size = new System.Drawing.Size( 210, 20 );
            this.txtVolumn.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point( 11, 110 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 38, 13 );
            this.label3.TabIndex = 13;
            this.label3.Text = "Parent";
            // 
            // cmbParent
            // 
            this.cmbParent.FormattingEnabled = true;
            this.cmbParent.Location = new System.Drawing.Point( 88, 107 );
            this.cmbParent.Name = "cmbParent";
            this.cmbParent.Size = new System.Drawing.Size( 210, 21 );
            this.cmbParent.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point( 11, 56 );
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size( 57, 13 );
            this.label5.TabIndex = 11;
            this.label5.Text = "Bottle Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point( 11, 83 );
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size( 50, 13 );
            this.label4.TabIndex = 10;
            this.label4.Text = "SubType";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 11, 10 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 29, 13 );
            this.label2.TabIndex = 8;
            this.label2.Text = "UPC";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 11, 32 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 35, 13 );
            this.label1.TabIndex = 7;
            this.label1.Text = "Name";
            // 
            // txtUPC
            // 
            this.txtUPC.Location = new System.Drawing.Point( 88, 3 );
            this.txtUPC.Name = "txtUPC";
            this.txtUPC.Size = new System.Drawing.Size( 210, 20 );
            this.txtUPC.TabIndex = 0;
            // 
            // cmbSubType
            // 
            this.cmbSubType.FormattingEnabled = true;
            this.cmbSubType.Location = new System.Drawing.Point( 88, 80 );
            this.cmbSubType.Name = "cmbSubType";
            this.cmbSubType.Size = new System.Drawing.Size( 210, 21 );
            this.cmbSubType.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point( 88, 29 );
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size( 210, 20 );
            this.txtName.TabIndex = 5;
            // 
            // pbImage
            // 
            this.pbImage.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.pbImage.Location = new System.Drawing.Point( 14, 134 );
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size( 284, 428 );
            this.pbImage.TabIndex = 4;
            this.pbImage.TabStop = false;
            // 
            // tvMaterial
            // 
            this.tvMaterial.AllowDrop = true;
            this.tvMaterial.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.tvMaterial.Location = new System.Drawing.Point( 7, 38 );
            this.tvMaterial.Name = "tvMaterial";
            this.tvMaterial.Size = new System.Drawing.Size( 582, 530 );
            this.tvMaterial.TabIndex = 7;
            this.tvMaterial.ItemDrag += new System.Windows.Forms.ItemDragEventHandler( this.tvMaterial_ItemDrag );
            this.tvMaterial.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.tvMaterial_AfterSelect );
            this.tvMaterial.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler( this.tvMaterial_NodeMouseClick );
            this.tvMaterial.DragDrop += new System.Windows.Forms.DragEventHandler( this.tvMaterial_DragDrop );
            this.tvMaterial.DragEnter += new System.Windows.Forms.DragEventHandler( this.tvMaterial_DragEnter );
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 905, 660 );
            this.Controls.Add( this.panel2 );
            this.Controls.Add( this.panel1 );
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout( false );
            this.panel1.PerformLayout( );
            this.panel2.ResumeLayout( false );
            this.panel2.PerformLayout( );
            this.panel3.ResumeLayout( false );
            this.panel3.PerformLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.pbImage ) ).EndInit( );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.OpenFileDialog ofdImportFile;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView tvMaterial;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.TextBox txtUPC;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbParent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSubType;
        private System.Windows.Forms.TextBox txtVolumn;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnImage;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAddMaterial;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtXLS;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnOpenExcel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSAASOrg;
        private System.Windows.Forms.Button btnAssignSAAS;

    }
}

