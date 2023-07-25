namespace PriceUpdater
{
    partial class frnUPCPrices
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
            this.grdUPCs = new DevExpress.XtraGrid.GridControl();
            this.gvUPCs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdateDB = new DevExpress.XtraEditors.SimpleButton();
            this.txtFileName = new DevExpress.XtraEditors.TextEdit();
            this.btnFile = new DevExpress.XtraEditors.SimpleButton();
            this.ofdDataFile = new System.Windows.Forms.OpenFileDialog();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoadFromDB = new DevExpress.XtraEditors.SimpleButton();
            this.cmbDatabase = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUPCs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUPCs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDatabase.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grdUPCs
            // 
            this.grdUPCs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdUPCs.Location = new System.Drawing.Point(12, 39);
            this.grdUPCs.MainView = this.gvUPCs;
            this.grdUPCs.Name = "grdUPCs";
            this.grdUPCs.Size = new System.Drawing.Size(891, 563);
            this.grdUPCs.TabIndex = 0;
            this.grdUPCs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUPCs});
            // 
            // gvUPCs
            // 
            this.gvUPCs.GridControl = this.grdUPCs;
            this.gvUPCs.Name = "gvUPCs";
            this.gvUPCs.OptionsView.ShowAutoFilterRow = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(828, 608);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(747, 608);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpdateDB
            // 
            this.btnUpdateDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateDB.Location = new System.Drawing.Point(245, 608);
            this.btnUpdateDB.Name = "btnUpdateDB";
            this.btnUpdateDB.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateDB.TabIndex = 3;
            this.btnUpdateDB.Text = "Update DB";
            this.btnUpdateDB.Click += new System.EventHandler(this.btnUpdateDB_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(12, 13);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(844, 20);
            this.txtFileName.TabIndex = 4;
            // 
            // btnFile
            // 
            this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFile.Location = new System.Drawing.Point(862, 10);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(41, 23);
            this.btnFile.TabIndex = 5;
            this.btnFile.Text = "...";
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // ofdDataFile
            // 
            this.ofdDataFile.FileName = "openFileDialog1";
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(666, 608);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 6;
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnLoadFromDB
            // 
            this.btnLoadFromDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadFromDB.Location = new System.Drawing.Point(326, 608);
            this.btnLoadFromDB.Name = "btnLoadFromDB";
            this.btnLoadFromDB.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFromDB.TabIndex = 7;
            this.btnLoadFromDB.Text = "Load From DB";
            this.btnLoadFromDB.Click += new System.EventHandler(this.btnLoadFromDB_Click);
            // 
            // cmbDatabase
            // 
            this.cmbDatabase.Location = new System.Drawing.Point(12, 610);
            this.cmbDatabase.Name = "cmbDatabase";
            this.cmbDatabase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDatabase.Size = new System.Drawing.Size(226, 20);
            this.cmbDatabase.TabIndex = 8;
            // 
            // frnUPCPrices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 643);
            this.Controls.Add(this.cmbDatabase);
            this.Controls.Add(this.btnLoadFromDB);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnUpdateDB);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grdUPCs);
            this.Name = "frnUPCPrices";
            this.Text = "UPC Prices";
            this.Load += new System.EventHandler(this.frnUPCPrices_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdUPCs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUPCs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDatabase.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdUPCs;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUPCs;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnUpdateDB;
        private DevExpress.XtraEditors.TextEdit txtFileName;
        private DevExpress.XtraEditors.SimpleButton btnFile;
        private System.Windows.Forms.OpenFileDialog ofdDataFile;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
        private DevExpress.XtraEditors.SimpleButton btnLoadFromDB;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDatabase;
    }
}

