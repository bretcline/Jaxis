using BeverageManagement.Forms;

namespace BeverageManagement
{
    partial class frmInventory
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInventory));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbtnAddInventory = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnAddLocation = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnMoveInventory = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnBranding = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnUnBrandBottle = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnEditUPC = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnModifyStockItems = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnReceiveMove = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnReport = new DevExpress.XtraBars.BarButtonItem();
            this.rpInventory = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rbgSave = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgMove = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgInventory = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgBranding = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgReporting = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.grdInventoryItems = new DevExpress.XtraGrid.GridControl();
            this.gvInventoryItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.treeInventory = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInventoryItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInventoryItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonText = null;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.ExpandCollapseItem.Name = "";
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.bbtnAddInventory,
            this.bbtnAddLocation,
            this.bbtnMoveInventory,
            this.bbtnBranding,
            this.bbtnUnBrandBottle,
            this.bbtnRefresh,
            this.bbtnEditUPC,
            this.bbtnSave,
            this.bbtnModifyStockItems,
            this.bbtnReceiveMove,
            this.bbtnReport});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 23;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpInventory});
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(866, 96);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // bbtnAddInventory
            // 
            this.bbtnAddInventory.Caption = global::BeverageManagement.Properties.Resources.AddInventory;
            this.bbtnAddInventory.Id = 1;
            this.bbtnAddInventory.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbtnAddInventory.LargeGlyph")));
            this.bbtnAddInventory.Name = "bbtnAddInventory";
            this.bbtnAddInventory.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbtnAddInventory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnAddInventory_ItemClick);
            // 
            // bbtnAddLocation
            // 
            this.bbtnAddLocation.Caption = "Edit Location";
            this.bbtnAddLocation.Id = 8;
            this.bbtnAddLocation.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbtnAddLocation.LargeGlyph")));
            this.bbtnAddLocation.Name = "bbtnAddLocation";
            this.bbtnAddLocation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnAddLocation_ItemClick);
            // 
            // bbtnMoveInventory
            // 
            this.bbtnMoveInventory.Caption = global::BeverageManagement.Properties.Resources.MoveInventory;
            this.bbtnMoveInventory.Id = 13;
            this.bbtnMoveInventory.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbtnMoveInventory.LargeGlyph")));
            this.bbtnMoveInventory.Name = "bbtnMoveInventory";
            this.bbtnMoveInventory.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbtnMoveInventory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnMoveInventory_ItemClick);
            // 
            // bbtnBranding
            // 
            this.bbtnBranding.Caption = global::BeverageManagement.Properties.Resources.BrandBottle;
            this.bbtnBranding.Id = 14;
            this.bbtnBranding.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbtnBranding.LargeGlyph")));
            this.bbtnBranding.Name = "bbtnBranding";
            this.bbtnBranding.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnBranding_ItemClick);
            // 
            // bbtnUnBrandBottle
            // 
            this.bbtnUnBrandBottle.Caption = global::BeverageManagement.Properties.Resources.UnbrandBottle;
            this.bbtnUnBrandBottle.Id = 15;
            this.bbtnUnBrandBottle.LargeGlyph = global::BeverageManagement.Properties.Resources.RemoveBranding_48;
            this.bbtnUnBrandBottle.Name = "bbtnUnBrandBottle";
            this.bbtnUnBrandBottle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnUnBrandBottle_ItemClick);
            // 
            // bbtnRefresh
            // 
            this.bbtnRefresh.Caption = global::BeverageManagement.Properties.Resources.Refresh;
            this.bbtnRefresh.Id = 16;
            this.bbtnRefresh.LargeGlyph = global::BeverageManagement.Properties.Resources.Refresh_48;
            this.bbtnRefresh.Name = "bbtnRefresh";
            this.bbtnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnRefresh_ItemClick);
            // 
            // bbtnEditUPC
            // 
            this.bbtnEditUPC.Caption = "Edit UPC";
            this.bbtnEditUPC.Id = 17;
            this.bbtnEditUPC.LargeGlyph = global::BeverageManagement.Properties.Resources.EditUpc_48;
            this.bbtnEditUPC.Name = "bbtnEditUPC";
            this.bbtnEditUPC.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnEditUPC_ItemClick);
            // 
            // bbtnSave
            // 
            this.bbtnSave.Caption = "Save";
            this.bbtnSave.Enabled = false;
            this.bbtnSave.Id = 18;
            this.bbtnSave.LargeGlyph = global::BeverageManagement.Properties.Resources.SaveBarManagement_48;
            this.bbtnSave.Name = "bbtnSave";
            this.bbtnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnSave_ItemClick);
            // 
            // bbtnModifyStockItems
            // 
            this.bbtnModifyStockItems.Caption = "Unbrand/Reduce Inventory";
            this.bbtnModifyStockItems.Id = 19;
            this.bbtnModifyStockItems.LargeGlyph = global::BeverageManagement.Properties.Resources.IncreaseInventory_48;
            this.bbtnModifyStockItems.Name = "bbtnModifyStockItems";
            this.bbtnModifyStockItems.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnModifyStockItems_ItemClick);
            // 
            // bbtnReceiveMove
            // 
            this.bbtnReceiveMove.Caption = "Receive and Move";
            this.bbtnReceiveMove.Id = 20;
            this.bbtnReceiveMove.LargeGlyph = global::BeverageManagement.Properties.Resources.BarManagement_48;
            this.bbtnReceiveMove.Name = "bbtnReceiveMove";
            this.bbtnReceiveMove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnReceiveMove_ItemClick);
            // 
            // bbtnReport
            // 
            this.bbtnReport.Caption = "Export Inventory";
            this.bbtnReport.Id = 22;
            this.bbtnReport.Name = "bbtnReport";
            this.bbtnReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnReport_ItemClick);
            // 
            // rpInventory
            // 
            this.rpInventory.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rbgSave,
            this.rpgMove,
            this.rpgInventory,
            this.rpgBranding,
            this.rpgReporting});
            this.rpInventory.Name = "rpInventory";
            this.rpInventory.Text = global::BeverageManagement.Properties.Resources.InventoryManagement;
            // 
            // rbgSave
            // 
            this.rbgSave.ItemLinks.Add(this.bbtnSave);
            this.rbgSave.ItemLinks.Add(this.bbtnRefresh);
            this.rbgSave.Name = "rbgSave";
            this.rbgSave.Text = "Save";
            // 
            // rpgMove
            // 
            this.rpgMove.ItemLinks.Add(this.bbtnAddLocation);
            this.rpgMove.ItemLinks.Add(this.bbtnMoveInventory);
            this.rpgMove.Name = "rpgMove";
            this.rpgMove.Text = "Location";
            // 
            // rpgInventory
            // 
            this.rpgInventory.ItemLinks.Add(this.bbtnAddInventory);
            this.rpgInventory.ItemLinks.Add(this.bbtnReceiveMove);
            this.rpgInventory.Name = "rpgInventory";
            this.rpgInventory.Text = "Inventory";
            // 
            // rpgBranding
            // 
            this.rpgBranding.ItemLinks.Add(this.bbtnBranding);
            this.rpgBranding.ItemLinks.Add(this.bbtnModifyStockItems);
            this.rpgBranding.ItemLinks.Add(this.bbtnEditUPC);
            this.rpgBranding.Name = "rpgBranding";
            this.rpgBranding.Text = "Branding";
            // 
            // rpgReporting
            // 
            this.rpgReporting.ItemLinks.Add(this.bbtnReport);
            this.rpgReporting.Name = "rpgReporting";
            this.rpgReporting.Text = "Inventory Reporting";
            // 
            // colName
            // 
            this.colName.Caption = global::BeverageManagement.Properties.Resources.Location;
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // grdInventoryItems
            // 
            this.grdInventoryItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInventoryItems.Location = new System.Drawing.Point(197, 96);
            this.grdInventoryItems.MainView = this.gvInventoryItems;
            this.grdInventoryItems.Name = "grdInventoryItems";
            this.grdInventoryItems.Size = new System.Drawing.Size(669, 630);
            this.grdInventoryItems.TabIndex = 7;
            this.grdInventoryItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInventoryItems});
            // 
            // gvInventoryItems
            // 
            this.gvInventoryItems.GridControl = this.grdInventoryItems;
            this.gvInventoryItems.Name = "gvInventoryItems";
            this.gvInventoryItems.OptionsView.ShowAutoFilterRow = true;
            this.gvInventoryItems.OptionsView.ShowFooter = true;
            this.gvInventoryItems.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvInventoryItems_RowStyle);
            this.gvInventoryItems.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvInventoryItems_ShowingEditor);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(192, 96);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 630);
            this.splitterControl1.TabIndex = 9;
            this.splitterControl1.TabStop = false;
            // 
            // treeInventory
            // 
            this.treeInventory.Appearance.SelectedRow.BackColor = System.Drawing.Color.Silver;
            this.treeInventory.Appearance.SelectedRow.Options.UseBackColor = true;
            this.treeInventory.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.treeInventory.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeInventory.Location = new System.Drawing.Point(0, 96);
            this.treeInventory.Name = "treeInventory";
            this.treeInventory.OptionsBehavior.Editable = false;
            this.treeInventory.OptionsPrint.UsePrintStyles = true;
            this.treeInventory.OptionsView.ShowColumns = false;
            this.treeInventory.OptionsView.ShowHorzLines = false;
            this.treeInventory.OptionsView.ShowIndicator = false;
            this.treeInventory.Size = new System.Drawing.Size(192, 630);
            this.treeInventory.TabIndex = 11;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = global::BeverageManagement.Properties.Resources.Category;
            this.treeListColumn2.FieldName = "Name";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // frmInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 726);
            this.Controls.Add(this.grdInventoryItems);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.treeInventory);
            this.Controls.Add(this.ribbonControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventory";
            this.Load += new System.EventHandler(this.frmInventory_Load);
            this.Shown += new System.EventHandler(this.frmInventory_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInventoryItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInventoryItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion Windows Form Designer generated code

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem bbtnAddInventory;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpInventory;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgInventory;
        private DevExpress.XtraBars.BarButtonItem bbtnAddLocation;
        private DevExpress.XtraBars.BarButtonItem bbtnMoveInventory;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgMove;
        private DevExpress.XtraBars.BarButtonItem bbtnBranding;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgBranding;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraGrid.GridControl grdInventoryItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInventoryItems;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraBars.BarButtonItem bbtnUnBrandBottle;
        private DevExpress.XtraTreeList.TreeList treeInventory;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraBars.BarButtonItem bbtnRefresh;
        private DevExpress.XtraBars.BarButtonItem bbtnEditUPC;
        private DevExpress.XtraBars.BarButtonItem bbtnSave;
        private DevExpress.XtraBars.BarButtonItem bbtnModifyStockItems;
        private DevExpress.XtraBars.BarButtonItem bbtnReceiveMove;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rbgSave;
        private DevExpress.XtraBars.BarButtonItem bbtnReport;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgReporting;
        private DevExpress.XtraPrinting.PrintingSystem printingSystem1;
    }
}