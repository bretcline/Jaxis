namespace BeverageManagement.Forms.Reconcile
{
    partial class frmAliasUPC
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grdAliases = new DevExpress.XtraGrid.GridControl();
            this.gvAliases = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdUPCList = new DevExpress.XtraGrid.GridControl();
            this.gvUPCList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.lblUPCName = new DevExpress.XtraEditors.LabelControl();
            this.lblAlias = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAliases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAliases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUPCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUPCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.grdAliases);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.grdUPCList);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(620, 505);
            this.splitContainerControl1.SplitterPosition = 306;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // grdAliases
            // 
            this.grdAliases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAliases.Location = new System.Drawing.Point(0, 0);
            this.grdAliases.MainView = this.gvAliases;
            this.grdAliases.Name = "grdAliases";
            this.grdAliases.Size = new System.Drawing.Size(306, 505);
            this.grdAliases.TabIndex = 0;
            this.grdAliases.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAliases});
            // 
            // gvAliases
            // 
            this.gvAliases.GridControl = this.grdAliases;
            this.gvAliases.Name = "gvAliases";
            this.gvAliases.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvAliases_FocusedRowChanged);
            // 
            // grdUPCList
            // 
            this.grdUPCList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUPCList.Location = new System.Drawing.Point(0, 86);
            this.grdUPCList.MainView = this.gvUPCList;
            this.grdUPCList.Name = "grdUPCList";
            this.grdUPCList.Size = new System.Drawing.Size(308, 419);
            this.grdUPCList.TabIndex = 1;
            this.grdUPCList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUPCList});
            // 
            // gvUPCList
            // 
            this.gvUPCList.GridControl = this.grdUPCList;
            this.gvUPCList.Name = "gvUPCList";
            this.gvUPCList.OptionsBehavior.Editable = false;
            this.gvUPCList.OptionsBehavior.ReadOnly = true;
            this.gvUPCList.OptionsView.ShowAutoFilterRow = true;
            this.gvUPCList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvUPCList_FocusedRowChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.lblUPCName);
            this.panelControl1.Controls.Add(this.lblAlias);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(308, 86);
            this.panelControl1.TabIndex = 2;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(6, 58);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblUPCName
            // 
            this.lblUPCName.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUPCName.Location = new System.Drawing.Point(6, 35);
            this.lblUPCName.Name = "lblUPCName";
            this.lblUPCName.Size = new System.Drawing.Size(110, 19);
            this.lblUPCName.TabIndex = 1;
            this.lblUPCName.Text = "labelControl1";
            // 
            // lblAlias
            // 
            this.lblAlias.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlias.Location = new System.Drawing.Point(6, 6);
            this.lblAlias.Name = "lblAlias";
            this.lblAlias.Size = new System.Drawing.Size(110, 19);
            this.lblAlias.TabIndex = 0;
            this.lblAlias.Text = "labelControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(540, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 505);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(620, 35);
            this.panelControl2.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(459, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmAliasUPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 540);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panelControl2);
            this.Name = "frmAliasUPC";
            this.Text = "frmAliasUPC";
            this.Load += new System.EventHandler(this.frmAliasUPC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAliases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAliases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUPCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUPCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl grdAliases;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAliases;
        private DevExpress.XtraGrid.GridControl grdUPCList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUPCList;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblAlias;
        private DevExpress.XtraEditors.LabelControl lblUPCName;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClear;

    }
}