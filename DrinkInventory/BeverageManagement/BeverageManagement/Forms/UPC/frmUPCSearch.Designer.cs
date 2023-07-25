namespace BeverageManagement.Forms.UPC
{
    partial class frmUPCSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUPCSearch));
            this.grdUPCSearch = new DevExpress.XtraGrid.GridControl();
            this.gvUPCSearch = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdUPCSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUPCSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdUPCSearch
            // 
            this.grdUPCSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUPCSearch.Location = new System.Drawing.Point(0, 0);
            this.grdUPCSearch.MainView = this.gvUPCSearch;
            this.grdUPCSearch.Name = "grdUPCSearch";
            this.grdUPCSearch.Size = new System.Drawing.Size(632, 422);
            this.grdUPCSearch.TabIndex = 0;
            this.grdUPCSearch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUPCSearch});
            // 
            // gvUPCSearch
            // 
            this.gvUPCSearch.GridControl = this.grdUPCSearch;
            this.gvUPCSearch.Name = "gvUPCSearch";
            this.gvUPCSearch.OptionsBehavior.Editable = false;
            this.gvUPCSearch.OptionsBehavior.ReadOnly = true;
            this.gvUPCSearch.OptionsView.ShowAutoFilterRow = true;
            this.gvUPCSearch.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvUPCSearch_SelectionChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.btnOK);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 422);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(632, 38);
            this.panelControl1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(552, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(471, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmUPCSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 460);
            this.Controls.Add(this.grdUPCSearch);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUPCSearch";
            this.Text = "UPC Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUPCSearch_FormClosing);
            this.Load += new System.EventHandler(this.frmUPCSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdUPCSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUPCSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdUPCSearch;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUPCSearch;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
    }
}