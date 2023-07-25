namespace BeverageManagement.Forms
{
    partial class frmInventoryMove
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
            this.xtabUPCSelection = new DevExpress.XtraTab.XtraTabControl();
            this.tabScanUPC = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtTagID = new DevExpress.XtraEditors.TextEdit();
            this.chkKeyboard = new DevExpress.XtraEditors.CheckEdit();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.txtManufacturer = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciTagID = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciManufacturer = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDescription = new DevExpress.XtraLayout.LayoutControlItem();
            this.tabSelectUPC = new DevExpress.XtraTab.XtraTabPage();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.grdInventory = new DevExpress.XtraGrid.GridControl();
            this.gvInventory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.treeLocation = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeNewLocation = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.grdNewItems = new DevExpress.XtraGrid.GridControl();
            this.gvNewItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnMove = new DevExpress.XtraEditors.SimpleButton();
            this.splitterControl3 = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtabUPCSelection)).BeginInit();
            this.xtabUPCSelection.SuspendLayout();
            this.tabScanUPC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTagID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKeyboard.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManufacturer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTagID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciManufacturer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDescription)).BeginInit();
            this.tabSelectUPC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeNewLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNewItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNewItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtabUPCSelection
            // 
            this.xtabUPCSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtabUPCSelection.Location = new System.Drawing.Point(2, 2);
            this.xtabUPCSelection.Name = "xtabUPCSelection";
            this.xtabUPCSelection.SelectedTabPage = this.tabScanUPC;
            this.xtabUPCSelection.Size = new System.Drawing.Size(588, 307);
            this.xtabUPCSelection.TabIndex = 22;
            this.xtabUPCSelection.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabScanUPC,
            this.tabSelectUPC});
            this.xtabUPCSelection.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtabUPCSelection_SelectedPageChanged);
            // 
            // tabScanUPC
            // 
            this.tabScanUPC.Controls.Add(this.layoutControl1);
            this.tabScanUPC.Name = "tabScanUPC";
            this.tabScanUPC.Size = new System.Drawing.Size(582, 279);
            this.tabScanUPC.Text = "Scan UPC";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtTagID);
            this.layoutControl1.Controls.Add(this.chkKeyboard);
            this.layoutControl1.Controls.Add(this.txtDescription);
            this.layoutControl1.Controls.Add(this.txtManufacturer);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2988, 181, 250, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(582, 279);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtTagID
            // 
            this.txtTagID.Location = new System.Drawing.Point(81, 12);
            this.txtTagID.Name = "txtTagID";
            this.txtTagID.Size = new System.Drawing.Size(207, 20);
            this.txtTagID.StyleController = this.layoutControl1;
            this.txtTagID.TabIndex = 4;
            this.txtTagID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTagID_KeyPress);
            // 
            // chkKeyboard
            // 
            this.chkKeyboard.Location = new System.Drawing.Point(292, 12);
            this.chkKeyboard.Name = "chkKeyboard";
            this.chkKeyboard.Properties.Caption = global::BeverageManagement.Properties.Resources.AllowKeyboardEntry;
            this.chkKeyboard.Size = new System.Drawing.Size(278, 19);
            this.chkKeyboard.StyleController = this.layoutControl1;
            this.chkKeyboard.TabIndex = 24;
            this.chkKeyboard.Click += new System.EventHandler(this.chkUPCKeyboard_CheckedChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Enabled = false;
            this.txtDescription.Location = new System.Drawing.Point(81, 36);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(489, 20);
            this.txtDescription.StyleController = this.layoutControl1;
            this.txtDescription.TabIndex = 25;
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.Enabled = false;
            this.txtManufacturer.Location = new System.Drawing.Point(81, 60);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Size = new System.Drawing.Size(489, 20);
            this.txtManufacturer.StyleController = this.layoutControl1;
            this.txtManufacturer.TabIndex = 26;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciTagID,
            this.layoutControlItem1,
            this.lciManufacturer,
            this.lciDescription});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(582, 279);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciTagID
            // 
            this.lciTagID.Control = this.txtTagID;
            this.lciTagID.CustomizationFormText = "Tag ID";
            this.lciTagID.Location = new System.Drawing.Point(0, 0);
            this.lciTagID.Name = "lciTagID";
            this.lciTagID.Size = new System.Drawing.Size(280, 24);
            this.lciTagID.Text = "Tag ID";
            this.lciTagID.TextSize = new System.Drawing.Size(65, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chkKeyboard;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(280, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(282, 24);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lciManufacturer
            // 
            this.lciManufacturer.Control = this.txtManufacturer;
            this.lciManufacturer.CustomizationFormText = "Manufacturer";
            this.lciManufacturer.Location = new System.Drawing.Point(0, 48);
            this.lciManufacturer.Name = "lciManufacturer";
            this.lciManufacturer.Size = new System.Drawing.Size(562, 211);
            this.lciManufacturer.Text = "Manufacturer";
            this.lciManufacturer.TextSize = new System.Drawing.Size(65, 13);
            // 
            // lciDescription
            // 
            this.lciDescription.Control = this.txtDescription;
            this.lciDescription.CustomizationFormText = "Description";
            this.lciDescription.Location = new System.Drawing.Point(0, 24);
            this.lciDescription.Name = "lciDescription";
            this.lciDescription.Size = new System.Drawing.Size(562, 24);
            this.lciDescription.Text = "Description";
            this.lciDescription.TextSize = new System.Drawing.Size(65, 13);
            // 
            // tabSelectUPC
            // 
            this.tabSelectUPC.Controls.Add(this.splitterControl1);
            this.tabSelectUPC.Controls.Add(this.grdInventory);
            this.tabSelectUPC.Controls.Add(this.treeLocation);
            this.tabSelectUPC.Name = "tabSelectUPC";
            this.tabSelectUPC.Size = new System.Drawing.Size(582, 279);
            this.tabSelectUPC.Text = "Select UPC";
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(158, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 279);
            this.splitterControl1.TabIndex = 9;
            this.splitterControl1.TabStop = false;
            // 
            // grdInventory
            // 
            this.grdInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInventory.Location = new System.Drawing.Point(158, 0);
            this.grdInventory.MainView = this.gvInventory;
            this.grdInventory.Name = "grdInventory";
            this.grdInventory.Size = new System.Drawing.Size(424, 279);
            this.grdInventory.TabIndex = 9;
            this.grdInventory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInventory});
            // 
            // gvInventory
            // 
            this.gvInventory.GridControl = this.grdInventory;
            this.gvInventory.Name = "gvInventory";
            // 
            // treeLocation
            // 
            this.treeLocation.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeLocation.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeLocation.Location = new System.Drawing.Point(0, 0);
            this.treeLocation.Name = "treeLocation";
            this.treeLocation.OptionsBehavior.Editable = false;
            this.treeLocation.OptionsPrint.UsePrintStyles = true;
            this.treeLocation.OptionsView.ShowColumns = false;
            this.treeLocation.OptionsView.ShowHorzLines = false;
            this.treeLocation.OptionsView.ShowIndicator = false;
            this.treeLocation.Size = new System.Drawing.Size(158, 279);
            this.treeLocation.TabIndex = 21;
            this.treeLocation.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeUPC_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = global::BeverageManagement.Properties.Resources.Category;
            this.treeListColumn1.FieldName = "Name";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // treeNewLocation
            // 
            this.treeNewLocation.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.treeNewLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeNewLocation.Location = new System.Drawing.Point(2, 2);
            this.treeNewLocation.Name = "treeNewLocation";
            this.treeNewLocation.OptionsBehavior.Editable = false;
            this.treeNewLocation.OptionsPrint.UsePrintStyles = true;
            this.treeNewLocation.OptionsView.ShowColumns = false;
            this.treeNewLocation.OptionsView.ShowHorzLines = false;
            this.treeNewLocation.OptionsView.ShowIndicator = false;
            this.treeNewLocation.Size = new System.Drawing.Size(158, 303);
            this.treeNewLocation.TabIndex = 20;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = global::BeverageManagement.Properties.Resources.Category;
            this.treeListColumn2.FieldName = "Name";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 519);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(754, 40);
            this.panelControl2.TabIndex = 23;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(585, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(676, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            // 
            // grdNewItems
            // 
            this.grdNewItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNewItems.Location = new System.Drawing.Point(0, 311);
            this.grdNewItems.MainView = this.gvNewItems;
            this.grdNewItems.Name = "grdNewItems";
            this.grdNewItems.Size = new System.Drawing.Size(754, 208);
            this.grdNewItems.TabIndex = 24;
            this.grdNewItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNewItems});
            // 
            // gvNewItems
            // 
            this.gvNewItems.GridControl = this.grdNewItems;
            this.gvNewItems.Name = "gvNewItems";
            this.gvNewItems.OptionsBehavior.Editable = false;
            this.gvNewItems.OptionsBehavior.ReadOnly = true;
            this.gvNewItems.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.splitterControl2);
            this.panelControl1.Controls.Add(this.xtabUPCSelection);
            this.panelControl1.Controls.Add(this.panelControl4);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(754, 311);
            this.panelControl1.TabIndex = 25;
            // 
            // splitterControl2
            // 
            this.splitterControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl2.Location = new System.Drawing.Point(585, 2);
            this.splitterControl2.Name = "splitterControl2";
            this.splitterControl2.Size = new System.Drawing.Size(5, 307);
            this.splitterControl2.TabIndex = 23;
            this.splitterControl2.TabStop = false;
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.panelControl3);
            this.panelControl4.Controls.Add(this.treeNewLocation);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl4.Location = new System.Drawing.Point(590, 2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(162, 307);
            this.panelControl4.TabIndex = 10;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnMove);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(2, 277);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(158, 28);
            this.panelControl3.TabIndex = 26;
            // 
            // btnMove
            // 
            this.btnMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMove.Location = new System.Drawing.Point(80, 3);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(75, 23);
            this.btnMove.TabIndex = 0;
            this.btnMove.Text = "Move";
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // splitterControl3
            // 
            this.splitterControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl3.Location = new System.Drawing.Point(0, 311);
            this.splitterControl3.Name = "splitterControl3";
            this.splitterControl3.Size = new System.Drawing.Size(754, 5);
            this.splitterControl3.TabIndex = 26;
            this.splitterControl3.TabStop = false;
            // 
            // frmInventoryMove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 559);
            this.Controls.Add(this.splitterControl3);
            this.Controls.Add(this.grdNewItems);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl2);
            this.Name = "frmInventoryMove";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Move Inventory";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInventoryMove_FormClosing);
            this.Load += new System.EventHandler(this.frmInventoryAdd_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmInventoryMove_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.xtabUPCSelection)).EndInit();
            this.xtabUPCSelection.ResumeLayout(false);
            this.tabScanUPC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTagID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKeyboard.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManufacturer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTagID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciManufacturer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDescription)).EndInit();
            this.tabSelectUPC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeNewLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdNewItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNewItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtabUPCSelection;
        private DevExpress.XtraTab.XtraTabPage tabScanUPC;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtTagID;
        private DevExpress.XtraEditors.CheckEdit chkKeyboard;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraEditors.TextEdit txtManufacturer;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lciTagID;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem lciManufacturer;
        private DevExpress.XtraLayout.LayoutControlItem lciDescription;
        private DevExpress.XtraTab.XtraTabPage tabSelectUPC;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraGrid.GridControl grdInventory;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInventory;
        private DevExpress.XtraTreeList.TreeList treeNewLocation;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraGrid.GridControl grdNewItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNewItems;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitterControl splitterControl2;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnMove;
        private DevExpress.XtraEditors.SplitterControl splitterControl3;
        private DevExpress.XtraTreeList.TreeList treeLocation;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
    }
}