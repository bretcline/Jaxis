namespace BeverageManagement
{
    partial class frmBranding
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBranding));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lueFromLocation = new DevExpress.XtraEditors.LookUpEdit();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbtnRemove = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnClose = new DevExpress.XtraBars.BarButtonItem();
            this.rpInventory = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgInventory = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.chkOnAttach = new DevExpress.XtraEditors.CheckEdit();
            this.lueToLocation = new DevExpress.XtraEditors.LookUpEdit();
            this.btnSummary = new DevExpress.XtraEditors.SimpleButton();
            this.btlFillLevel = new BeverageManagement.Controls.BottleLevel();
            this.txtUPC = new DevExpress.XtraEditors.TextEdit();
            this.btnEditUPC = new DevExpress.XtraEditors.SimpleButton();
            this.rdoInventoryFrom = new DevExpress.XtraEditors.RadioGroup();
            this.txtTagID = new DevExpress.XtraEditors.TextEdit();
            this.chkAllowKeyboardEntry = new DevExpress.XtraEditors.CheckEdit();
            this.grdBottles = new DevExpress.XtraGrid.GridControl();
            this.gvBottles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnUPCSearch = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciBottles = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciUPC = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciTagID = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciFillLevel = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciEvent = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciOnAttach = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueFromLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOnAttach.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueToLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUPC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoInventoryFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTagID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllowKeyboardEntry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBottles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBottles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBottles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciUPC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTagID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciFillLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciEvent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOnAttach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lueFromLocation);
            this.layoutControl1.Controls.Add(this.chkOnAttach);
            this.layoutControl1.Controls.Add(this.lueToLocation);
            this.layoutControl1.Controls.Add(this.btnSummary);
            this.layoutControl1.Controls.Add(this.btlFillLevel);
            this.layoutControl1.Controls.Add(this.txtUPC);
            this.layoutControl1.Controls.Add(this.btnEditUPC);
            this.layoutControl1.Controls.Add(this.rdoInventoryFrom);
            this.layoutControl1.Controls.Add(this.txtTagID);
            this.layoutControl1.Controls.Add(this.chkAllowKeyboardEntry);
            this.layoutControl1.Controls.Add(this.grdBottles);
            this.layoutControl1.Controls.Add(this.btnUPCSearch);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 96);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(764, 510);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lueFromLocation
            // 
            this.lueFromLocation.EditValue = "";
            this.lueFromLocation.EnterMoveNextControl = true;
            this.lueFromLocation.Location = new System.Drawing.Point(83, 12);
            this.lueFromLocation.MenuManager = this.ribbonControl1;
            this.lueFromLocation.Name = "lueFromLocation";
            this.lueFromLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueFromLocation.Size = new System.Drawing.Size(221, 20);
            this.lueFromLocation.StyleController = this.layoutControl1;
            this.lueFromLocation.TabIndex = 28;
            this.lueFromLocation.EditValueChanged += new System.EventHandler(this.lueEvent_EditValueChanged);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonText = null;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.ExpandCollapseItem.Name = "";
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.bbtnRemove,
            this.bbtnSave,
            this.bbtnClose});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 9;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpInventory});
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(764, 96);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // bbtnRemove
            // 
            this.bbtnRemove.Caption = global::BeverageManagement.Properties.Resources.RemoveText;
            this.bbtnRemove.Id = 5;
            this.bbtnRemove.LargeGlyph = global::BeverageManagement.Properties.Resources.bottle_full_remove_32;
            this.bbtnRemove.Name = "bbtnRemove";
            this.bbtnRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnRemove_ItemClick);
            // 
            // bbtnSave
            // 
            this.bbtnSave.Caption = "Save";
            this.bbtnSave.Id = 6;
            this.bbtnSave.LargeGlyph = global::BeverageManagement.Properties.Resources.bottle_full_save_32;
            this.bbtnSave.Name = "bbtnSave";
            this.bbtnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnSave_ItemClick);
            // 
            // bbtnClose
            // 
            this.bbtnClose.Caption = "Close";
            this.bbtnClose.Id = 8;
            this.bbtnClose.Name = "bbtnClose";
            this.bbtnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnClose_ItemClick);
            // 
            // rpInventory
            // 
            this.rpInventory.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgInventory});
            this.rpInventory.Name = "rpInventory";
            this.rpInventory.Text = global::BeverageManagement.Properties.Resources.InventoryManagement;
            // 
            // rpgInventory
            // 
            this.rpgInventory.ItemLinks.Add(this.bbtnRemove);
            this.rpgInventory.ItemLinks.Add(this.bbtnSave);
            this.rpgInventory.ItemLinks.Add(this.bbtnClose);
            this.rpgInventory.Name = "rpgInventory";
            this.rpgInventory.Text = global::BeverageManagement.Properties.Resources.Inventory;
            // 
            // chkOnAttach
            // 
            this.chkOnAttach.Location = new System.Drawing.Point(308, 36);
            this.chkOnAttach.MenuManager = this.ribbonControl1;
            this.chkOnAttach.Name = "chkOnAttach";
            this.chkOnAttach.Properties.Caption = "Add on Attach";
            this.chkOnAttach.Size = new System.Drawing.Size(148, 19);
            this.chkOnAttach.StyleController = this.layoutControl1;
            this.chkOnAttach.TabIndex = 27;
            this.chkOnAttach.Visible = false;
            // 
            // lueToLocation
            // 
            this.lueToLocation.EditValue = "";
            this.lueToLocation.EnterMoveNextControl = true;
            this.lueToLocation.Location = new System.Drawing.Point(83, 36);
            this.lueToLocation.MenuManager = this.ribbonControl1;
            this.lueToLocation.Name = "lueToLocation";
            this.lueToLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueToLocation.Size = new System.Drawing.Size(221, 20);
            this.lueToLocation.StyleController = this.layoutControl1;
            this.lueToLocation.TabIndex = 24;
            this.lueToLocation.EditValueChanged += new System.EventHandler(this.lueEvent_EditValueChanged);
            this.lueToLocation.MouseLeave += new System.EventHandler(this.lueLocation_MouseLeave);
            // 
            // btnSummary
            // 
            this.btnSummary.Location = new System.Drawing.Point(472, 106);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(176, 22);
            this.btnSummary.StyleController = this.layoutControl1;
            this.btnSummary.TabIndex = 29;
            this.btnSummary.Text = "Summary";
            this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
            // 
            // btlFillLevel
            // 
            this.btlFillLevel.BottleNozzle = null;
            this.btlFillLevel.FillLevel = 100;
            this.btlFillLevel.Location = new System.Drawing.Point(652, 12);
            this.btlFillLevel.MaximumSize = new System.Drawing.Size(100, 340);
            this.btlFillLevel.MinimumSize = new System.Drawing.Size(100, 340);
            this.btlFillLevel.Name = "btlFillLevel";
            this.btlFillLevel.NozzleLength = 0D;
            this.btlFillLevel.NozzleShape = Jaxis.Inventory.Data.NozzleShapes.Round;
            this.btlFillLevel.NozzleWidth = 0D;
            this.btlFillLevel.OnFillLevelChanged = null;
            this.btlFillLevel.OnNozzleDiameterChanged = null;
            this.btlFillLevel.Size = new System.Drawing.Size(100, 340);
            this.btlFillLevel.TabIndex = 22;
            // 
            // txtUPC
            // 
            this.txtUPC.EnterMoveNextControl = true;
            this.txtUPC.Location = new System.Drawing.Point(83, 60);
            this.txtUPC.Name = "txtUPC";
            this.txtUPC.Properties.DisplayFormat.FormatString = "#";
            this.txtUPC.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtUPC.Properties.EditFormat.FormatString = "#";
            this.txtUPC.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtUPC.Size = new System.Drawing.Size(318, 20);
            this.txtUPC.StyleController = this.layoutControl1;
            this.txtUPC.TabIndex = 17;
            this.txtUPC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUPC_KeyPress);
            // 
            // btnEditUPC
            // 
            this.btnEditUPC.Location = new System.Drawing.Point(12, 84);
            this.btnEditUPC.Name = "btnEditUPC";
            this.btnEditUPC.Size = new System.Drawing.Size(389, 20);
            this.btnEditUPC.StyleController = this.layoutControl1;
            this.btnEditUPC.TabIndex = 26;
            this.btnEditUPC.Click += new System.EventHandler(this.btnEditUPC_Click);
            // 
            // rdoInventoryFrom
            // 
            this.rdoInventoryFrom.Location = new System.Drawing.Point(472, 12);
            this.rdoInventoryFrom.MenuManager = this.ribbonControl1;
            this.rdoInventoryFrom.Name = "rdoInventoryFrom";
            this.rdoInventoryFrom.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), global::BeverageManagement.Properties.Resources.NewInventory),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), global::BeverageManagement.Properties.Resources.ExistingInventory)});
            this.rdoInventoryFrom.Size = new System.Drawing.Size(176, 90);
            this.rdoInventoryFrom.StyleController = this.layoutControl1;
            this.rdoInventoryFrom.TabIndex = 25;
            // 
            // txtTagID
            // 
            this.txtTagID.EnterMoveNextControl = true;
            this.txtTagID.Location = new System.Drawing.Point(83, 108);
            this.txtTagID.Name = "txtTagID";
            this.txtTagID.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTagID.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTagID.Size = new System.Drawing.Size(373, 20);
            this.txtTagID.StyleController = this.layoutControl1;
            this.txtTagID.TabIndex = 18;
            this.txtTagID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTagID_KeyPress);
            // 
            // chkAllowKeyboardEntry
            // 
            this.chkAllowKeyboardEntry.Location = new System.Drawing.Point(308, 12);
            this.chkAllowKeyboardEntry.MenuManager = this.ribbonControl1;
            this.chkAllowKeyboardEntry.Name = "chkAllowKeyboardEntry";
            this.chkAllowKeyboardEntry.Properties.Caption = global::BeverageManagement.Properties.Resources.AllowKeyboardEntry;
            this.chkAllowKeyboardEntry.Size = new System.Drawing.Size(148, 19);
            this.chkAllowKeyboardEntry.StyleController = this.layoutControl1;
            this.chkAllowKeyboardEntry.TabIndex = 23;
            this.chkAllowKeyboardEntry.CheckedChanged += new System.EventHandler(this.chkUPCKeyboard_CheckedChanged);
            // 
            // grdBottles
            // 
            this.grdBottles.Location = new System.Drawing.Point(12, 148);
            this.grdBottles.MainView = this.gvBottles;
            this.grdBottles.Name = "grdBottles";
            this.grdBottles.Size = new System.Drawing.Size(636, 350);
            this.grdBottles.TabIndex = 15;
            this.grdBottles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBottles});
            // 
            // gvBottles
            // 
            this.gvBottles.GridControl = this.grdBottles;
            this.gvBottles.Name = "gvBottles";
            this.gvBottles.OptionsSelection.MultiSelect = true;
            this.gvBottles.OptionsView.ShowFooter = true;
            this.gvBottles.OptionsView.ShowGroupPanel = false;
            this.gvBottles.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvBottles_SelectionChanged);
            this.gvBottles.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvBottles_ShowingEditor);
            this.gvBottles.HiddenEditor += new System.EventHandler(this.gvBottles_HiddenEditor);
            // 
            // btnUPCSearch
            // 
            this.btnUPCSearch.Image = global::BeverageManagement.Properties.Resources.Zoom;
            this.btnUPCSearch.Location = new System.Drawing.Point(405, 60);
            this.btnUPCSearch.Name = "btnUPCSearch";
            this.btnUPCSearch.Size = new System.Drawing.Size(51, 38);
            this.btnUPCSearch.StyleController = this.layoutControl1;
            this.btnUPCSearch.TabIndex = 30;
            this.btnUPCSearch.Click += new System.EventHandler(this.btnUPCSearch_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciBottles,
            this.lciUPC,
            this.lciTagID,
            this.lciFillLevel,
            this.lciEvent,
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.layoutControlItem4,
            this.lciOnAttach,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(764, 510);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciBottles
            // 
            this.lciBottles.Control = this.grdBottles;
            this.lciBottles.CustomizationFormText = global::BeverageManagement.Properties.Resources.Bottles;
            this.lciBottles.Location = new System.Drawing.Point(0, 120);
            this.lciBottles.Name = "lciBottles";
            this.lciBottles.Size = new System.Drawing.Size(640, 370);
            this.lciBottles.Text = global::BeverageManagement.Properties.Resources.Bottles;
            this.lciBottles.TextLocation = DevExpress.Utils.Locations.Top;
            this.lciBottles.TextSize = new System.Drawing.Size(67, 13);
            // 
            // lciUPC
            // 
            this.lciUPC.Control = this.txtUPC;
            this.lciUPC.CustomizationFormText = global::BeverageManagement.Properties.Resources.UPCLabel;
            this.lciUPC.Location = new System.Drawing.Point(0, 48);
            this.lciUPC.Name = "lciUPC";
            this.lciUPC.Size = new System.Drawing.Size(393, 24);
            this.lciUPC.Text = global::BeverageManagement.Properties.Resources.UPCLabel;
            this.lciUPC.TextSize = new System.Drawing.Size(67, 13);
            // 
            // lciTagID
            // 
            this.lciTagID.Control = this.txtTagID;
            this.lciTagID.CustomizationFormText = global::BeverageManagement.Properties.Resources.TagID;
            this.lciTagID.Location = new System.Drawing.Point(0, 96);
            this.lciTagID.Name = "lciTagID";
            this.lciTagID.Size = new System.Drawing.Size(448, 24);
            this.lciTagID.Text = global::BeverageManagement.Properties.Resources.TagID;
            this.lciTagID.TextSize = new System.Drawing.Size(67, 13);
            // 
            // lciFillLevel
            // 
            this.lciFillLevel.Control = this.btlFillLevel;
            this.lciFillLevel.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lciFillLevel.CustomizationFormText = "lciFillLevel";
            this.lciFillLevel.Location = new System.Drawing.Point(640, 0);
            this.lciFillLevel.Name = "lciFillLevel";
            this.lciFillLevel.Size = new System.Drawing.Size(104, 490);
            this.lciFillLevel.Text = "lciFillLevel";
            this.lciFillLevel.TextSize = new System.Drawing.Size(0, 0);
            this.lciFillLevel.TextToControlDistance = 0;
            this.lciFillLevel.TextVisible = false;
            // 
            // lciEvent
            // 
            this.lciEvent.Control = this.lueToLocation;
            this.lciEvent.CustomizationFormText = "Location";
            this.lciEvent.Location = new System.Drawing.Point(0, 24);
            this.lciEvent.Name = "lciEvent";
            this.lciEvent.Size = new System.Drawing.Size(296, 24);
            this.lciEvent.Text = "To Location";
            this.lciEvent.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.rdoInventoryFrom;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(460, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(180, 94);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chkAllowKeyboardEntry;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(296, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(152, 24);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(448, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(12, 120);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lueFromLocation;
            this.layoutControlItem4.CustomizationFormText = "From Location";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(296, 24);
            this.layoutControlItem4.Text = "From Location";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(67, 13);
            // 
            // lciOnAttach
            // 
            this.lciOnAttach.Control = this.chkOnAttach;
            this.lciOnAttach.CustomizationFormText = "layoutControlItem4";
            this.lciOnAttach.Location = new System.Drawing.Point(296, 24);
            this.lciOnAttach.Name = "lciOnAttach";
            this.lciOnAttach.Size = new System.Drawing.Size(152, 24);
            this.lciOnAttach.Text = "lciOnAttach";
            this.lciOnAttach.TextSize = new System.Drawing.Size(0, 0);
            this.lciOnAttach.TextToControlDistance = 0;
            this.lciOnAttach.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnSummary;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(460, 94);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(180, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnUPCSearch;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(393, 48);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(55, 48);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnEditUPC;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(393, 24);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmBranding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 606);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBranding";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Branding";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBranding_FormClosing);
            this.Load += new System.EventHandler(this.frmBranding_Load);
            this.Shown += new System.EventHandler(this.frmBranding_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmBranding_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lueFromLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOnAttach.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueToLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUPC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoInventoryFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTagID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllowKeyboardEntry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBottles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBottles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBottles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciUPC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTagID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciFillLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciEvent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOnAttach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Controls.BottleLevel btlFillLevel;
        private DevExpress.XtraGrid.GridControl grdBottles;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBottles;
        private DevExpress.XtraEditors.TextEdit txtUPC;
        private DevExpress.XtraEditors.TextEdit txtTagID;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lciBottles;
        private DevExpress.XtraLayout.LayoutControlItem lciUPC;
        private DevExpress.XtraLayout.LayoutControlItem lciTagID;
        private DevExpress.XtraLayout.LayoutControlItem lciFillLevel;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem bbtnRemove;
        private DevExpress.XtraBars.BarButtonItem bbtnSave;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpInventory;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgInventory;
        private DevExpress.XtraEditors.CheckEdit chkAllowKeyboardEntry;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.LookUpEdit lueToLocation;
        private DevExpress.XtraLayout.LayoutControlItem lciEvent;
        private DevExpress.XtraBars.BarButtonItem bbtnClose;
        private DevExpress.XtraEditors.RadioGroup rdoInventoryFrom;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnEditUPC;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.CheckEdit chkOnAttach;
        private DevExpress.XtraLayout.LayoutControlItem lciOnAttach;
        private DevExpress.XtraEditors.LookUpEdit lueFromLocation;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton btnSummary;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton btnUPCSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;


    }
}