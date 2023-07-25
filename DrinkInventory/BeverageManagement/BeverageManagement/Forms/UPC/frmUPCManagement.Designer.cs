using BeverageManagement.Forms;

namespace BeverageManagement
{
    partial class frmUPCManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUPCManagement));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbtnAddUPC = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnUpdateUPC = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnAddCategory = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnAddSubCategory = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.rpInventory = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgInventory = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grdUPCItems = new DevExpress.XtraGrid.GridControl();
            this.gvUPCItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.treeUPC = new DevExpress.XtraTreeList.TreeList();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUPCItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUPCItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeUPC)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonText = null;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.ExpandCollapseItem.Name = "";
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.bbtnAddUPC,
            this.bbtnUpdateUPC,
            this.bbtnAddCategory,
            this.bbtnAddSubCategory,
            this.bbtnRefresh});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 9;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpInventory});
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(616, 96);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // bbtnAddUPC
            // 
            this.bbtnAddUPC.Caption = global::BeverageManagement.Properties.Resources.AddUPC;
            this.bbtnAddUPC.Id = 1;
            this.bbtnAddUPC.LargeGlyph = global::BeverageManagement.Properties.Resources.Addupc_48;
            this.bbtnAddUPC.Name = "bbtnAddUPC";
            this.bbtnAddUPC.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnAddUPC_ItemClick);
            // 
            // bbtnUpdateUPC
            // 
            this.bbtnUpdateUPC.Caption = global::BeverageManagement.Properties.Resources.EditUPC;
            this.bbtnUpdateUPC.Id = 4;
            this.bbtnUpdateUPC.LargeGlyph = global::BeverageManagement.Properties.Resources.EditUpc_48;
            this.bbtnUpdateUPC.Name = "bbtnUpdateUPC";
            this.bbtnUpdateUPC.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnUpdateUPC_ItemClick);
            // 
            // bbtnAddCategory
            // 
            this.bbtnAddCategory.Caption = global::BeverageManagement.Properties.Resources.AddCategory;
            this.bbtnAddCategory.Id = 5;
            this.bbtnAddCategory.LargeGlyph = global::BeverageManagement.Properties.Resources.AddCategory_48;
            this.bbtnAddCategory.Name = "bbtnAddCategory";
            // 
            // bbtnAddSubCategory
            // 
            this.bbtnAddSubCategory.Caption = global::BeverageManagement.Properties.Resources.AddSubCategory;
            this.bbtnAddSubCategory.Id = 6;
            this.bbtnAddSubCategory.LargeGlyph = global::BeverageManagement.Properties.Resources.AddSubCategory_48;
            this.bbtnAddSubCategory.Name = "bbtnAddSubCategory";
            // 
            // bbtnRefresh
            // 
            this.bbtnRefresh.Caption = "Refresh";
            this.bbtnRefresh.Id = 8;
            this.bbtnRefresh.LargeGlyph = global::BeverageManagement.Properties.Resources.Refresh_48;
            this.bbtnRefresh.Name = "bbtnRefresh";
            this.bbtnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnRefresh_ItemClick);
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
            this.rpgInventory.ItemLinks.Add(this.bbtnRefresh);
            this.rpgInventory.ItemLinks.Add(this.bbtnAddCategory);
            this.rpgInventory.ItemLinks.Add(this.bbtnAddSubCategory);
            this.rpgInventory.ItemLinks.Add(this.bbtnAddUPC);
            this.rpgInventory.ItemLinks.Add(this.bbtnUpdateUPC);
            this.rpgInventory.Name = "rpgInventory";
            this.rpgInventory.Text = global::BeverageManagement.Properties.Resources.Inventory;
            // 
            // grdUPCItems
            // 
            this.grdUPCItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUPCItems.Location = new System.Drawing.Point(197, 96);
            this.grdUPCItems.MainView = this.gvUPCItems;
            this.grdUPCItems.Name = "grdUPCItems";
            this.grdUPCItems.Size = new System.Drawing.Size(419, 441);
            this.grdUPCItems.TabIndex = 6;
            this.grdUPCItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUPCItems});
            // 
            // gvUPCItems
            // 
            this.gvUPCItems.GridControl = this.grdUPCItems;
            this.gvUPCItems.Name = "gvUPCItems";
            this.gvUPCItems.OptionsBehavior.Editable = false;
            this.gvUPCItems.OptionsBehavior.ReadOnly = true;
            this.gvUPCItems.OptionsView.ShowAutoFilterRow = true;
            this.gvUPCItems.OptionsView.ShowGroupPanel = false;
            this.gvUPCItems.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(192, 96);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 441);
            this.splitterControl1.TabIndex = 5;
            this.splitterControl1.TabStop = false;
            // 
            // treeUPC
            // 
            this.treeUPC.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colName});
            this.treeUPC.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeUPC.Location = new System.Drawing.Point(0, 96);
            this.treeUPC.Name = "treeUPC";
            this.treeUPC.OptionsBehavior.Editable = false;
            this.treeUPC.OptionsPrint.UsePrintStyles = true;
            this.treeUPC.OptionsView.ShowColumns = false;
            this.treeUPC.OptionsView.ShowHorzLines = false;
            this.treeUPC.OptionsView.ShowIndicator = false;
            this.treeUPC.Size = new System.Drawing.Size(192, 441);
            this.treeUPC.TabIndex = 4;
            // 
            // colName
            // 
            this.colName.Caption = global::BeverageManagement.Properties.Resources.Category;
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // frmUPCManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 537);
            this.Controls.Add(this.grdUPCItems);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.treeUPC);
            this.Controls.Add(this.ribbonControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUPCManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UPC Management";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUPCManagement_FormClosing);
            this.Shown += new System.EventHandler(this.frmUPCManagement_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUPCItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUPCItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeUPC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem bbtnAddUPC;
        private DevExpress.XtraBars.BarButtonItem bbtnUpdateUPC;
        private DevExpress.XtraBars.BarButtonItem bbtnAddCategory;
        private DevExpress.XtraBars.BarButtonItem bbtnAddSubCategory;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpInventory;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgInventory;
        private DevExpress.XtraGrid.GridControl grdUPCItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUPCItems;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraTreeList.TreeList treeUPC;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraBars.BarButtonItem bbtnRefresh;


    }
}