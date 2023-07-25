namespace BeverageManagement.Forms
{
    partial class frmAdjustStockInventory
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lueLocation = new DevExpress.XtraEditors.LookUpEdit();
            this.chkAllowKeyboardEntry = new DevExpress.XtraEditors.CheckEdit();
            this.txtUPCNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.grdNewItems = new DevExpress.XtraGrid.GridControl();
            this.gvNewItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciUPCNumber = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDescription = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllowKeyboardEntry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUPCNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNewItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNewItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciUPCNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lueLocation);
            this.layoutControl1.Controls.Add(this.chkAllowKeyboardEntry);
            this.layoutControl1.Controls.Add(this.txtUPCNumber);
            this.layoutControl1.Controls.Add(this.txtDescription);
            this.layoutControl1.Controls.Add(this.grdNewItems);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(651, 416);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lueLocation
            // 
            this.lueLocation.Location = new System.Drawing.Point(98, 59);
            this.lueLocation.Name = "lueLocation";
            this.lueLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueLocation.Properties.NullText = "<Select a location>";
            this.lueLocation.Size = new System.Drawing.Size(541, 20);
            this.lueLocation.StyleController = this.layoutControl1;
            this.lueLocation.TabIndex = 29;
            this.lueLocation.EditValueChanged += new System.EventHandler(this.lueLocation_EditValueChanged);
            // 
            // chkAllowKeyboardEntry
            // 
            this.chkAllowKeyboardEntry.Location = new System.Drawing.Point(12, 12);
            this.chkAllowKeyboardEntry.Name = "chkAllowKeyboardEntry";
            this.chkAllowKeyboardEntry.Properties.Caption = global::BeverageManagement.Properties.Resources.AllowKeyboardEntry;
            this.chkAllowKeyboardEntry.Size = new System.Drawing.Size(627, 19);
            this.chkAllowKeyboardEntry.StyleController = this.layoutControl1;
            this.chkAllowKeyboardEntry.TabIndex = 24;
            this.chkAllowKeyboardEntry.CheckedChanged += new System.EventHandler(this.chkUPCKeyboard_CheckedChanged);
            // 
            // txtUPCNumber
            // 
            this.txtUPCNumber.Enabled = false;
            this.txtUPCNumber.Location = new System.Drawing.Point(98, 35);
            this.txtUPCNumber.Name = "txtUPCNumber";
            this.txtUPCNumber.Size = new System.Drawing.Size(225, 20);
            this.txtUPCNumber.StyleController = this.layoutControl1;
            this.txtUPCNumber.TabIndex = 27;
            this.txtUPCNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTagID_KeyPress);
            // 
            // txtDescription
            // 
            this.txtDescription.Enabled = false;
            this.txtDescription.Location = new System.Drawing.Point(413, 35);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(226, 20);
            this.txtDescription.StyleController = this.layoutControl1;
            this.txtDescription.TabIndex = 28;
            // 
            // grdNewItems
            // 
            this.grdNewItems.Location = new System.Drawing.Point(12, 83);
            this.grdNewItems.MainView = this.gvNewItems;
            this.grdNewItems.Name = "grdNewItems";
            this.grdNewItems.Size = new System.Drawing.Size(627, 321);
            this.grdNewItems.TabIndex = 15;
            this.grdNewItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNewItems});
            // 
            // gvNewItems
            // 
            this.gvNewItems.GridControl = this.grdNewItems;
            this.gvNewItems.Name = "gvNewItems";
            this.gvNewItems.OptionsView.ShowFooter = true;
            this.gvNewItems.OptionsView.ShowGroupPanel = false;
            this.gvNewItems.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvNewItems_ShowingEditor);
            this.gvNewItems.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvNewItems_FocusedRowChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem1,
            this.lciUPCNumber,
            this.lciDescription,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(651, 416);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chkAllowKeyboardEntry;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(631, 23);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdNewItems;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 71);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(631, 325);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lciUPCNumber
            // 
            this.lciUPCNumber.Control = this.txtUPCNumber;
            this.lciUPCNumber.CustomizationFormText = "Tag ID";
            this.lciUPCNumber.Location = new System.Drawing.Point(0, 23);
            this.lciUPCNumber.Name = "lciUPCNumber";
            this.lciUPCNumber.Size = new System.Drawing.Size(315, 24);
            this.lciUPCNumber.Text = "Tag/UPC Number";
            this.lciUPCNumber.TextSize = new System.Drawing.Size(82, 13);
            // 
            // lciDescription
            // 
            this.lciDescription.Control = this.txtDescription;
            this.lciDescription.CustomizationFormText = "Description";
            this.lciDescription.Location = new System.Drawing.Point(315, 23);
            this.lciDescription.Name = "lciDescription";
            this.lciDescription.Size = new System.Drawing.Size(316, 24);
            this.lciDescription.Text = "Description";
            this.lciDescription.TextSize = new System.Drawing.Size(82, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lueLocation;
            this.layoutControlItem2.CustomizationFormText = "Location";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 47);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(631, 24);
            this.layoutControlItem2.Text = "Location";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(82, 13);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(482, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnRemove);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 416);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(651, 33);
            this.panelControl2.TabIndex = 14;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Location = new System.Drawing.Point(12, 5);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(573, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            // 
            // frmAdjustStockInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 449);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.panelControl2);
            this.Name = "frmAdjustStockInventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modify Stock Items";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAdjustStockInventory_FormClosing);
            this.Load += new System.EventHandler(this.frmAdjustStockInventory_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmAdjustStockInventory_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lueLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllowKeyboardEntry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUPCNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNewItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNewItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciUPCNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit txtUPCNumber;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraLayout.LayoutControlItem lciUPCNumber;
        private DevExpress.XtraLayout.LayoutControlItem lciDescription;
        private DevExpress.XtraEditors.CheckEdit chkAllowKeyboardEntry;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.GridControl grdNewItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNewItems;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LookUpEdit lueLocation;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}