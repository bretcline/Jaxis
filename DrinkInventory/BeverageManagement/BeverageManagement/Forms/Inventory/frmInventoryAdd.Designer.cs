namespace BeverageManagement.Forms
{
    partial class frmInventoryAdd
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
            this.grdUPCItems = new DevExpress.XtraGrid.GridControl();
            this.gvUPCItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.treeUPC = new DevExpress.XtraTreeList.TreeList();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeLocation = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.grdNewItems = new DevExpress.XtraGrid.GridControl();
            this.gvNewItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.lcCost = new DevExpress.XtraEditors.LabelControl();
            this.lcQuantity = new DevExpress.XtraEditors.LabelControl();
            this.sbSubtractCost = new DevExpress.XtraEditors.SimpleButton();
            this.sbAddCost = new DevExpress.XtraEditors.SimpleButton();
            this.txtCost = new DevExpress.XtraEditors.TextEdit();
            this.btnSubtract = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.txtQuantity = new DevExpress.XtraEditors.TextEdit();
            this.btnAddItem = new DevExpress.XtraEditors.SimpleButton();
            this.splitterControl3 = new DevExpress.XtraEditors.SplitterControl();
            this.xtabUPCSelection = new DevExpress.XtraTab.XtraTabControl();
            this.tabScanUPC = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtUPC = new DevExpress.XtraEditors.TextEdit();
            this.chkUPCKeyboard = new DevExpress.XtraEditors.CheckEdit();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.txtManufacturer = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciUPC = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciManufacturer = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDescription = new DevExpress.XtraLayout.LayoutControlItem();
            this.tabSelectUPC = new DevExpress.XtraTab.XtraTabPage();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.grdUPCItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUPCItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeUPC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNewItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNewItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtabUPCSelection)).BeginInit();
            this.xtabUPCSelection.SuspendLayout();
            this.tabScanUPC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUPC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUPCKeyboard.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManufacturer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciUPC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciManufacturer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDescription)).BeginInit();
            this.tabSelectUPC.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdUPCItems
            // 
            this.grdUPCItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUPCItems.Location = new System.Drawing.Point(186, 0);
            this.grdUPCItems.MainView = this.gvUPCItems;
            this.grdUPCItems.Name = "grdUPCItems";
            this.grdUPCItems.Size = new System.Drawing.Size(383, 319);
            this.grdUPCItems.TabIndex = 9;
            this.grdUPCItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUPCItems});
            this.grdUPCItems.Click += new System.EventHandler(this.grdUPCItems_Click);
            // 
            // gvUPCItems
            // 
            this.gvUPCItems.GridControl = this.grdUPCItems;
            this.gvUPCItems.Name = "gvUPCItems";
            this.gvUPCItems.OptionsBehavior.Editable = false;
            // 
            // treeUPC
            // 
            this.treeUPC.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colName});
            this.treeUPC.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeUPC.Location = new System.Drawing.Point(0, 0);
            this.treeUPC.Name = "treeUPC";
            this.treeUPC.OptionsBehavior.Editable = false;
            this.treeUPC.OptionsPrint.UsePrintStyles = true;
            this.treeUPC.OptionsView.ShowColumns = false;
            this.treeUPC.OptionsView.ShowHorzLines = false;
            this.treeUPC.OptionsView.ShowIndicator = false;
            this.treeUPC.Size = new System.Drawing.Size(186, 319);
            this.treeUPC.TabIndex = 7;
            this.treeUPC.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeUPC_FocusedNodeChanged);
            // 
            // colName
            // 
            this.colName.Caption = global::BeverageManagement.Properties.Resources.Category;
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // treeLocation
            // 
            this.treeLocation.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.treeLocation.Dock = System.Windows.Forms.DockStyle.Right;
            this.treeLocation.Location = new System.Drawing.Point(580, 0);
            this.treeLocation.Name = "treeLocation";
            this.treeLocation.OptionsBehavior.Editable = false;
            this.treeLocation.OptionsPrint.UsePrintStyles = true;
            this.treeLocation.OptionsView.ShowColumns = false;
            this.treeLocation.OptionsView.ShowHorzLines = false;
            this.treeLocation.OptionsView.ShowIndicator = false;
            this.treeLocation.Size = new System.Drawing.Size(171, 347);
            this.treeLocation.TabIndex = 13;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = global::BeverageManagement.Properties.Resources.Category;
            this.treeListColumn2.FieldName = "Name";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // splitterControl2
            // 
            this.splitterControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl2.Location = new System.Drawing.Point(575, 0);
            this.splitterControl2.Name = "splitterControl2";
            this.splitterControl2.Size = new System.Drawing.Size(5, 347);
            this.splitterControl2.TabIndex = 14;
            this.splitterControl2.TabStop = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.grdNewItems);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 352);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(751, 276);
            this.panelControl1.TabIndex = 15;
            // 
            // grdNewItems
            // 
            this.grdNewItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNewItems.Location = new System.Drawing.Point(2, 34);
            this.grdNewItems.MainView = this.gvNewItems;
            this.grdNewItems.Name = "grdNewItems";
            this.grdNewItems.Size = new System.Drawing.Size(747, 207);
            this.grdNewItems.TabIndex = 13;
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
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(2, 241);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(747, 33);
            this.panelControl2.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(578, 5);
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
            this.btnCancel.Location = new System.Drawing.Point(669, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.lcCost);
            this.panelControl3.Controls.Add(this.lcQuantity);
            this.panelControl3.Controls.Add(this.sbSubtractCost);
            this.panelControl3.Controls.Add(this.sbAddCost);
            this.panelControl3.Controls.Add(this.txtCost);
            this.panelControl3.Controls.Add(this.btnSubtract);
            this.panelControl3.Controls.Add(this.btnAdd);
            this.panelControl3.Controls.Add(this.txtQuantity);
            this.panelControl3.Controls.Add(this.btnAddItem);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(2, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(747, 32);
            this.panelControl3.TabIndex = 1;
            // 
            // lcCost
            // 
            this.lcCost.Location = new System.Drawing.Point(198, 10);
            this.lcCost.Name = "lcCost";
            this.lcCost.Size = new System.Drawing.Size(22, 13);
            this.lcCost.TabIndex = 9;
            this.lcCost.Text = "Cost";
            // 
            // lcQuantity
            // 
            this.lcQuantity.Location = new System.Drawing.Point(426, 11);
            this.lcQuantity.Name = "lcQuantity";
            this.lcQuantity.Size = new System.Drawing.Size(42, 13);
            this.lcQuantity.TabIndex = 8;
            this.lcQuantity.Text = "Quantity";
            // 
            // sbSubtractCost
            // 
            this.sbSubtractCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbSubtractCost.Image = global::BeverageManagement.Properties.Resources.Remove;
            this.sbSubtractCost.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.sbSubtractCost.Location = new System.Drawing.Point(328, 4);
            this.sbSubtractCost.Name = "sbSubtractCost";
            this.sbSubtractCost.Size = new System.Drawing.Size(26, 26);
            this.sbSubtractCost.TabIndex = 7;
            this.sbSubtractCost.Text = "-";
            this.sbSubtractCost.Click += new System.EventHandler(this.sbSubtractCost_Click);
            // 
            // sbAddCost
            // 
            this.sbAddCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbAddCost.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbAddCost.Image = global::BeverageManagement.Properties.Resources.Add;
            this.sbAddCost.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.sbAddCost.Location = new System.Drawing.Point(227, 4);
            this.sbAddCost.Name = "sbAddCost";
            this.sbAddCost.Size = new System.Drawing.Size(26, 26);
            this.sbAddCost.TabIndex = 6;
            this.sbAddCost.Text = "+";
            this.sbAddCost.Click += new System.EventHandler(this.sbAddCost_Click);
            // 
            // txtCost
            // 
            this.txtCost.EditValue = "0.00";
            this.txtCost.Location = new System.Drawing.Point(256, 7);
            this.txtCost.Name = "txtCost";
            this.txtCost.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCost.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCost.Properties.Mask.EditMask = "n0";
            this.txtCost.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCost.Size = new System.Drawing.Size(66, 20);
            this.txtCost.TabIndex = 5;
            // 
            // btnSubtract
            // 
            this.btnSubtract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubtract.Image = global::BeverageManagement.Properties.Resources.Remove;
            this.btnSubtract.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSubtract.Location = new System.Drawing.Point(576, 4);
            this.btnSubtract.Name = "btnSubtract";
            this.btnSubtract.Size = new System.Drawing.Size(26, 26);
            this.btnSubtract.TabIndex = 4;
            this.btnSubtract.Text = "-";
            this.btnSubtract.Click += new System.EventHandler(this.btnSubtract_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Image = global::BeverageManagement.Properties.Resources.Add;
            this.btnAdd.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAdd.Location = new System.Drawing.Point(475, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(26, 26);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "+";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtQuantity
            // 
            this.txtQuantity.EditValue = "1";
            this.txtQuantity.Location = new System.Drawing.Point(504, 7);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Properties.Appearance.Options.UseTextOptions = true;
            this.txtQuantity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtQuantity.Properties.Mask.EditMask = "n0";
            this.txtQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtQuantity.Size = new System.Drawing.Size(66, 20);
            this.txtQuantity.TabIndex = 2;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddItem.Location = new System.Drawing.Point(617, 5);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(125, 23);
            this.btnAddItem.TabIndex = 0;
            this.btnAddItem.Text = "Add Inventory";
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // splitterControl3
            // 
            this.splitterControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterControl3.Location = new System.Drawing.Point(0, 347);
            this.splitterControl3.Name = "splitterControl3";
            this.splitterControl3.Size = new System.Drawing.Size(751, 5);
            this.splitterControl3.TabIndex = 16;
            this.splitterControl3.TabStop = false;
            // 
            // xtabUPCSelection
            // 
            this.xtabUPCSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtabUPCSelection.Location = new System.Drawing.Point(0, 0);
            this.xtabUPCSelection.Name = "xtabUPCSelection";
            this.xtabUPCSelection.SelectedTabPage = this.tabScanUPC;
            this.xtabUPCSelection.Size = new System.Drawing.Size(575, 347);
            this.xtabUPCSelection.TabIndex = 17;
            this.xtabUPCSelection.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabScanUPC,
            this.tabSelectUPC});
            // 
            // tabScanUPC
            // 
            this.tabScanUPC.Controls.Add(this.layoutControl1);
            this.tabScanUPC.Name = "tabScanUPC";
            this.tabScanUPC.Size = new System.Drawing.Size(569, 319);
            this.tabScanUPC.Text = "Scan UPC";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtUPC);
            this.layoutControl1.Controls.Add(this.chkUPCKeyboard);
            this.layoutControl1.Controls.Add(this.txtDescription);
            this.layoutControl1.Controls.Add(this.txtManufacturer);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2988, 181, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(569, 319);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtUPC
            // 
            this.txtUPC.Location = new System.Drawing.Point(81, 12);
            this.txtUPC.Name = "txtUPC";
            this.txtUPC.Size = new System.Drawing.Size(201, 20);
            this.txtUPC.StyleController = this.layoutControl1;
            this.txtUPC.TabIndex = 4;
            this.txtUPC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUPC_KeyPress);
            // 
            // chkUPCKeyboard
            // 
            this.chkUPCKeyboard.Location = new System.Drawing.Point(286, 12);
            this.chkUPCKeyboard.Name = "chkUPCKeyboard";
            this.chkUPCKeyboard.Properties.Caption = global::BeverageManagement.Properties.Resources.AllowKeyboardEntry;
            this.chkUPCKeyboard.Size = new System.Drawing.Size(271, 19);
            this.chkUPCKeyboard.StyleController = this.layoutControl1;
            this.chkUPCKeyboard.TabIndex = 24;
            this.chkUPCKeyboard.CheckedChanged += new System.EventHandler(this.chkUPCKeyboard_CheckedChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Enabled = false;
            this.txtDescription.Location = new System.Drawing.Point(81, 36);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(476, 20);
            this.txtDescription.StyleController = this.layoutControl1;
            this.txtDescription.TabIndex = 25;
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.Enabled = false;
            this.txtManufacturer.Location = new System.Drawing.Point(81, 60);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Size = new System.Drawing.Size(476, 20);
            this.txtManufacturer.StyleController = this.layoutControl1;
            this.txtManufacturer.TabIndex = 26;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciUPC,
            this.layoutControlItem1,
            this.lciManufacturer,
            this.lciDescription});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(569, 319);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciUPC
            // 
            this.lciUPC.Control = this.txtUPC;
            this.lciUPC.CustomizationFormText = "UPC";
            this.lciUPC.Location = new System.Drawing.Point(0, 0);
            this.lciUPC.Name = "lciUPC";
            this.lciUPC.Size = new System.Drawing.Size(274, 24);
            this.lciUPC.Text = "UPC";
            this.lciUPC.TextSize = new System.Drawing.Size(65, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chkUPCKeyboard;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(274, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(275, 24);
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
            this.lciManufacturer.Size = new System.Drawing.Size(549, 251);
            this.lciManufacturer.Text = "Manufacturer";
            this.lciManufacturer.TextSize = new System.Drawing.Size(65, 13);
            // 
            // lciDescription
            // 
            this.lciDescription.Control = this.txtDescription;
            this.lciDescription.CustomizationFormText = "Description";
            this.lciDescription.Location = new System.Drawing.Point(0, 24);
            this.lciDescription.Name = "lciDescription";
            this.lciDescription.Size = new System.Drawing.Size(549, 24);
            this.lciDescription.Text = "Description";
            this.lciDescription.TextSize = new System.Drawing.Size(65, 13);
            // 
            // tabSelectUPC
            // 
            this.tabSelectUPC.Controls.Add(this.splitterControl1);
            this.tabSelectUPC.Controls.Add(this.grdUPCItems);
            this.tabSelectUPC.Controls.Add(this.treeUPC);
            this.tabSelectUPC.Name = "tabSelectUPC";
            this.tabSelectUPC.Size = new System.Drawing.Size(569, 319);
            this.tabSelectUPC.Text = "Select UPC";
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(186, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 319);
            this.splitterControl1.TabIndex = 9;
            this.splitterControl1.TabStop = false;
            // 
            // frmInventoryAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 628);
            this.Controls.Add(this.xtabUPCSelection);
            this.Controls.Add(this.splitterControl2);
            this.Controls.Add(this.treeLocation);
            this.Controls.Add(this.splitterControl3);
            this.Controls.Add(this.panelControl1);
            this.KeyPreview = true;
            this.Name = "frmInventoryAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Inventory";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInventoryAdd_FormClosing);
            this.Load += new System.EventHandler(this.frmInventoryAdd_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmInventoryAdd_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.grdUPCItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUPCItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeUPC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdNewItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNewItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtabUPCSelection)).EndInit();
            this.xtabUPCSelection.ResumeLayout(false);
            this.tabScanUPC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtUPC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUPCKeyboard.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManufacturer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciUPC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciManufacturer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDescription)).EndInit();
            this.tabSelectUPC.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdUPCItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUPCItems;
        private DevExpress.XtraTreeList.TreeList treeUPC;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraTreeList.TreeList treeLocation;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.SplitterControl splitterControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl grdNewItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNewItems;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnAddItem;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SplitterControl splitterControl3;
        private DevExpress.XtraTab.XtraTabControl xtabUPCSelection;
        private DevExpress.XtraTab.XtraTabPage tabSelectUPC;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraTab.XtraTabPage tabScanUPC;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit txtUPC;
        private DevExpress.XtraLayout.LayoutControlItem lciUPC;
        private DevExpress.XtraEditors.CheckEdit chkUPCKeyboard;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraLayout.LayoutControlItem lciDescription;
        private DevExpress.XtraEditors.TextEdit txtManufacturer;
        private DevExpress.XtraLayout.LayoutControlItem lciManufacturer;
        private DevExpress.XtraEditors.SimpleButton btnSubtract;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.TextEdit txtQuantity;
        private DevExpress.XtraEditors.LabelControl lcCost;
        private DevExpress.XtraEditors.LabelControl lcQuantity;
        private DevExpress.XtraEditors.SimpleButton sbSubtractCost;
        private DevExpress.XtraEditors.SimpleButton sbAddCost;
        private DevExpress.XtraEditors.TextEdit txtCost;
    }
}