namespace BeverageManagement.Forms.Reconcile
{
    partial class frmTicketItemAliases
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
            this.gcAliases = new DevExpress.XtraGrid.GridControl();
            this.gvAliases = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbOk = new DevExpress.XtraEditors.SimpleButton();
            this.lbcUnassigned = new DevExpress.XtraEditors.ListBoxControl();
            this.pcUnknownItems = new DevExpress.XtraEditors.PanelControl();
            this.sbAssign = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.pcAssigned = new DevExpress.XtraEditors.PanelControl();
            this.sbDeleteAssignment = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pcContent = new DevExpress.XtraEditors.PanelControl();
            this.scContent = new DevExpress.XtraEditors.SplitterControl();
            this.btnAlias = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcAliases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAliases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbcUnassigned)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcUnknownItems)).BeginInit();
            this.pcUnknownItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcAssigned)).BeginInit();
            this.pcAssigned.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcContent)).BeginInit();
            this.pcContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcAliases
            // 
            this.gcAliases.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcAliases.Location = new System.Drawing.Point(11, 32);
            this.gcAliases.MainView = this.gvAliases;
            this.gcAliases.Name = "gcAliases";
            this.gcAliases.Size = new System.Drawing.Size(491, 351);
            this.gcAliases.TabIndex = 0;
            this.gcAliases.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAliases});
            // 
            // gvAliases
            // 
            this.gvAliases.GridControl = this.gcAliases;
            this.gvAliases.Name = "gvAliases";
            this.gvAliases.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvAliases.OptionsView.ShowGroupPanel = false;
            this.gvAliases.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.GvAliasesRowUpdated);
            // 
            // sbCancel
            // 
            this.sbCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbCancel.Location = new System.Drawing.Point(629, 439);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(75, 23);
            this.sbCancel.TabIndex = 1;
            this.sbCancel.Text = "Cancel";
            // 
            // sbOk
            // 
            this.sbOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sbOk.Location = new System.Drawing.Point(548, 439);
            this.sbOk.Name = "sbOk";
            this.sbOk.Size = new System.Drawing.Size(75, 23);
            this.sbOk.TabIndex = 1;
            this.sbOk.Text = "OK";
            this.sbOk.Click += new System.EventHandler(this.SbOkClick);
            // 
            // lbcUnassigned
            // 
            this.lbcUnassigned.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbcUnassigned.Location = new System.Drawing.Point(5, 32);
            this.lbcUnassigned.Name = "lbcUnassigned";
            this.lbcUnassigned.Size = new System.Drawing.Size(170, 351);
            this.lbcUnassigned.TabIndex = 2;
            this.lbcUnassigned.DoubleClick += new System.EventHandler(this.LbcUnassignedDoubleClick);
            // 
            // pcUnknownItems
            // 
            this.pcUnknownItems.Controls.Add(this.sbAssign);
            this.pcUnknownItems.Controls.Add(this.labelControl2);
            this.pcUnknownItems.Controls.Add(this.lbcUnassigned);
            this.pcUnknownItems.Dock = System.Windows.Forms.DockStyle.Left;
            this.pcUnknownItems.Location = new System.Drawing.Point(2, 2);
            this.pcUnknownItems.Name = "pcUnknownItems";
            this.pcUnknownItems.Size = new System.Drawing.Size(181, 417);
            this.pcUnknownItems.TabIndex = 3;
            // 
            // sbAssign
            // 
            this.sbAssign.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.sbAssign.Location = new System.Drawing.Point(39, 389);
            this.sbAssign.Name = "sbAssign";
            this.sbAssign.Size = new System.Drawing.Size(102, 23);
            this.sbAssign.TabIndex = 4;
            this.sbAssign.Text = "Assign >";
            this.sbAssign.Click += new System.EventHandler(this.SbAssignClick);
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Location = new System.Drawing.Point(0, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(181, 23);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Unassigned Items";
            // 
            // pcAssigned
            // 
            this.pcAssigned.Controls.Add(this.sbDeleteAssignment);
            this.pcAssigned.Controls.Add(this.labelControl1);
            this.pcAssigned.Controls.Add(this.gcAliases);
            this.pcAssigned.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcAssigned.Location = new System.Drawing.Point(183, 2);
            this.pcAssigned.Name = "pcAssigned";
            this.pcAssigned.Size = new System.Drawing.Size(507, 417);
            this.pcAssigned.TabIndex = 5;
            // 
            // sbDeleteAssignment
            // 
            this.sbDeleteAssignment.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.sbDeleteAssignment.Location = new System.Drawing.Point(191, 389);
            this.sbDeleteAssignment.Name = "sbDeleteAssignment";
            this.sbDeleteAssignment.Size = new System.Drawing.Size(125, 23);
            this.sbDeleteAssignment.TabIndex = 5;
            this.sbDeleteAssignment.Text = "Delete Assignment";
            this.sbDeleteAssignment.Click += new System.EventHandler(this.SbDeleteAssignmentClick);
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(0, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(507, 23);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Assigned Ticket Items";
            // 
            // pcContent
            // 
            this.pcContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pcContent.Controls.Add(this.scContent);
            this.pcContent.Controls.Add(this.pcAssigned);
            this.pcContent.Controls.Add(this.pcUnknownItems);
            this.pcContent.Location = new System.Drawing.Point(12, 12);
            this.pcContent.Name = "pcContent";
            this.pcContent.Size = new System.Drawing.Size(692, 421);
            this.pcContent.TabIndex = 6;
            // 
            // scContent
            // 
            this.scContent.Location = new System.Drawing.Point(183, 2);
            this.scContent.Name = "scContent";
            this.scContent.Size = new System.Drawing.Size(5, 417);
            this.scContent.TabIndex = 6;
            this.scContent.TabStop = false;
            // 
            // btnAlias
            // 
            this.btnAlias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAlias.Location = new System.Drawing.Point(14, 439);
            this.btnAlias.Name = "btnAlias";
            this.btnAlias.Size = new System.Drawing.Size(75, 23);
            this.btnAlias.TabIndex = 7;
            this.btnAlias.Text = "Alias UPC\'s";
            this.btnAlias.Click += new System.EventHandler(this.btnAlias_Click);
            // 
            // frmTicketItemAliases
            // 
            this.AcceptButton = this.sbOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.sbCancel;
            this.ClientSize = new System.Drawing.Size(716, 474);
            this.Controls.Add(this.btnAlias);
            this.Controls.Add(this.pcContent);
            this.Controls.Add(this.sbOk);
            this.Controls.Add(this.sbCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTicketItemAliases";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ticket Items";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTicketItemAliasesFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gcAliases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAliases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbcUnassigned)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcUnknownItems)).EndInit();
            this.pcUnknownItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcAssigned)).EndInit();
            this.pcAssigned.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcContent)).EndInit();
            this.pcContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcAliases;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAliases;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraEditors.SimpleButton sbOk;
        private DevExpress.XtraEditors.ListBoxControl lbcUnassigned;
        private DevExpress.XtraEditors.PanelControl pcUnknownItems;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton sbAssign;
        private DevExpress.XtraEditors.PanelControl pcAssigned;
        private DevExpress.XtraEditors.SimpleButton sbDeleteAssignment;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl pcContent;
        private DevExpress.XtraEditors.SplitterControl scContent;
        private DevExpress.XtraEditors.SimpleButton btnAlias;
        //private DevExpress.XtraVerticalGrid.Blending.XtraVertGridBlending xtraVertGridBlending1;

    }
}