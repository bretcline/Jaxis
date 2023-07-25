namespace BeverageManagement.Forms.Reconcile
{
    partial class frmReconcile
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
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.rpgInventory = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.bbtnAliases = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnRecipes = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.rpInventory = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.gvTickets = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdTickets = new DevExpress.XtraGrid.GridControl();
            this.grdPours = new DevExpress.XtraGrid.GridControl();
            this.gvPours = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTickets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTickets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPours)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitterControl1
            // 
            this.splitterControl1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterControl1.Location = new System.Drawing.Point(0, 409);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(818, 5);
            this.splitterControl1.TabIndex = 9;
            this.splitterControl1.TabStop = false;
            // 
            // rpgInventory
            // 
            this.rpgInventory.ItemLinks.Add(this.bbtnAliases);
            this.rpgInventory.ItemLinks.Add(this.bbtnRecipes);
            this.rpgInventory.Name = "rpgInventory";
            this.rpgInventory.ShowCaptionButton = false;
            this.rpgInventory.Text = "Manage";
            // 
            // bbtnAliases
            // 
            this.bbtnAliases.Caption = "Ticket Items";
            this.bbtnAliases.Id = 9;
            this.bbtnAliases.LargeGlyph = global::BeverageManagement.Properties.Resources.alias;
            this.bbtnAliases.Name = "bbtnAliases";
            this.bbtnAliases.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BbtnAliasesItemClick);
            // 
            // bbtnRecipes
            // 
            this.bbtnRecipes.Caption = "Drink Recipes";
            this.bbtnRecipes.Id = 5;
            this.bbtnRecipes.LargeGlyph = global::BeverageManagement.Properties.Resources.flask_icon;
            this.bbtnRecipes.Name = "bbtnRecipes";
            this.bbtnRecipes.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BbtnRecipesItemClick);
            // 
            // bbtnRefresh
            // 
            this.bbtnRefresh.Caption = "Refresh";
            this.bbtnRefresh.Id = 8;
            this.bbtnRefresh.LargeGlyph = global::BeverageManagement.Properties.Resources.Refresh_48;
            this.bbtnRefresh.Name = "bbtnRefresh";
            this.bbtnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BbtnRefreshItemClick);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonText = null;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.ExpandCollapseItem.Name = "";
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.bbtnRecipes,
            this.bbtnRefresh,
            this.bbtnAliases,
            this.barStaticItem1,
            this.barEditItem1,
            this.barEditItem2});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 16;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpInventory});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1,
            this.repositoryItemCheckEdit1});
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(818, 96);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "View past:";
            this.barStaticItem1.Id = 13;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "View Past:";
            this.barEditItem1.Edit = this.repositoryItemComboBox1;
            this.barEditItem1.Id = 14;
            this.barEditItem1.Name = "barEditItem1";
            this.barEditItem1.Width = 75;
            this.barEditItem1.EditValueChanged += new System.EventHandler(this.BarEditItem1EditValueChanged);
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // barEditItem2
            // 
            this.barEditItem2.Caption = "Reconciled Items";
            this.barEditItem2.CaptionAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.barEditItem2.Edit = this.repositoryItemCheckEdit1;
            this.barEditItem2.Id = 15;
            this.barEditItem2.Name = "barEditItem2";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // rpInventory
            // 
            this.rpInventory.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.rpgInventory});
            this.rpInventory.Name = "rpInventory";
            this.rpInventory.Text = global::BeverageManagement.Properties.Resources.InventoryManagement;
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barEditItem1);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbtnRefresh);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Recent Activity";
            // 
            // gvTickets
            // 
            this.gvTickets.GridControl = this.grdTickets;
            this.gvTickets.Name = "gvTickets";
            this.gvTickets.OptionsBehavior.Editable = false;
            this.gvTickets.OptionsBehavior.ReadOnly = true;
            this.gvTickets.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gvTickets.OptionsView.ShowGroupPanel = false;
            this.gvTickets.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.GvTicketsRowStyle);
            this.gvTickets.MasterRowExpanded += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.GvTicketsMasterRowExpanded);
            this.gvTickets.MasterRowCollapsed += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.GvTicketsMasterRowCollapsed);
            // 
            // grdTickets
            // 
            this.grdTickets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTickets.Location = new System.Drawing.Point(0, 32);
            this.grdTickets.MainView = this.gvTickets;
            this.grdTickets.Name = "grdTickets";
            this.grdTickets.Size = new System.Drawing.Size(818, 280);
            this.grdTickets.TabIndex = 10;
            this.grdTickets.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTickets});
            // 
            // grdPours
            // 
            this.grdPours.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPours.Location = new System.Drawing.Point(0, 32);
            this.grdPours.MainView = this.gvPours;
            this.grdPours.Name = "grdPours";
            this.grdPours.Size = new System.Drawing.Size(818, 138);
            this.grdPours.TabIndex = 10;
            this.grdPours.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPours});
            // 
            // gvPours
            // 
            this.gvPours.GridControl = this.grdPours;
            this.gvPours.Name = "gvPours";
            this.gvPours.OptionsBehavior.Editable = false;
            this.gvPours.OptionsBehavior.ReadOnly = true;
            this.gvPours.OptionsView.ShowGroupPanel = false;
            this.gvPours.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.GvTicketsRowStyle);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.grdTickets);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 96);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 318);
            this.panel1.TabIndex = 12;
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
            this.labelControl2.Size = new System.Drawing.Size(818, 23);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "POS Tickets";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelControl1);
            this.panel2.Controls.Add(this.grdPours);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 414);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(818, 170);
            this.panel2.TabIndex = 13;
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
            this.labelControl1.Size = new System.Drawing.Size(818, 23);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "Pours";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Drink Recipes";
            this.barButtonItem1.Id = 5;
            this.barButtonItem1.LargeGlyph = global::BeverageManagement.Properties.Resources.flask_icon;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // frmReconcile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 584);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ribbonControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReconcile";
            this.Text = "POS Reconciliation";
            this.Load += new System.EventHandler(this.FrmReconcileLoad);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTickets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTickets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPours)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgInventory;
        private DevExpress.XtraBars.BarButtonItem bbtnRefresh;
        private DevExpress.XtraBars.BarButtonItem bbtnRecipes;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpInventory;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTickets;
        private DevExpress.XtraGrid.GridControl grdTickets;
        private DevExpress.XtraGrid.GridControl grdPours;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPours;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraBars.BarButtonItem bbtnAliases;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox ricbViewPast;
        private DevExpress.XtraBars.BarEditItem barEditItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
    }
}