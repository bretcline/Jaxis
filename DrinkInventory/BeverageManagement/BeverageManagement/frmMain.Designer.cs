using BeverageManagement.Properties;

namespace BeverageManagement
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.dlfLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.scStyleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.navBar = new DevExpress.XtraNavBar.NavBarControl();
            this.nbgManagement = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiInventory = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiActivity = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiBranding = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiBarManagement = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiUPCManagement = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiReconcile = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiAdministration = new DevExpress.XtraNavBar.NavBarItem();
            this.nbgReports = new DevExpress.XtraNavBar.NavBarGroup();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.scStyleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // dlfLookAndFeel
            // 
            this.dlfLookAndFeel.LookAndFeel.SkinName = "Black";
            // 
            // scStyleController
            // 
            this.scStyleController.LookAndFeel.SkinName = "Black";
            // 
            // navBar
            // 
            this.navBar.ActiveGroup = this.nbgManagement;
            this.navBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBar.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbgManagement,
            this.nbgReports});
            this.navBar.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nbiBranding,
            this.nbiBarManagement,
            this.nbiInventory,
            this.nbiActivity,
            this.nbiUPCManagement,
            this.nbiAdministration,
            this.nbiReconcile});
            this.navBar.Location = new System.Drawing.Point(0, 0);
            this.navBar.Name = "navBar";
            this.navBar.OptionsNavPane.ExpandedWidth = 140;
            this.navBar.Size = new System.Drawing.Size(140, 730);
            this.navBar.StoreDefaultPaintStyleName = true;
            this.navBar.TabIndex = 0;
            this.navBar.Text = "navBarControl1";
            // 
            // nbgManagement
            // 
            this.nbgManagement.Caption = global::BeverageManagement.Properties.Resources.Management;
            this.nbgManagement.Expanded = true;
            this.nbgManagement.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsText;
            this.nbgManagement.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiInventory),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiActivity),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiBranding),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiBarManagement),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiUPCManagement),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiReconcile),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiAdministration)});
            this.nbgManagement.Name = "nbgManagement";
            // 
            // nbiInventory
            // 
            this.nbiInventory.Caption = global::BeverageManagement.Properties.Resources.Inventory;
            this.nbiInventory.LargeImage = ((System.Drawing.Image)(resources.GetObject("nbiInventory.LargeImage")));
            this.nbiInventory.Name = "nbiInventory";
            this.nbiInventory.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiInventory_LinkClicked);
            // 
            // nbiActivity
            // 
            this.nbiActivity.Caption = global::BeverageManagement.Properties.Resources.Activity;
            this.nbiActivity.LargeImage = ((System.Drawing.Image)(resources.GetObject("nbiActivity.LargeImage")));
            this.nbiActivity.Name = "nbiActivity";
            this.nbiActivity.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiActivity_LinkClicked);
            // 
            // nbiBranding
            // 
            this.nbiBranding.Caption = global::BeverageManagement.Properties.Resources.Branding;
            this.nbiBranding.LargeImage = ((System.Drawing.Image)(resources.GetObject("nbiBranding.LargeImage")));
            this.nbiBranding.Name = "nbiBranding";
            this.nbiBranding.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiBranding_LinkClicked);
            // 
            // nbiBarManagement
            // 
            this.nbiBarManagement.Caption = "Receive and Move";
            this.nbiBarManagement.LargeImage = ((System.Drawing.Image)(resources.GetObject("nbiBarManagement.LargeImage")));
            this.nbiBarManagement.Name = "nbiBarManagement";
            this.nbiBarManagement.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiBarManagement_LinkClicked);
            // 
            // nbiUPCManagement
            // 
            this.nbiUPCManagement.Caption = global::BeverageManagement.Properties.Resources.UPCManagement;
            this.nbiUPCManagement.LargeImage = ((System.Drawing.Image)(resources.GetObject("nbiUPCManagement.LargeImage")));
            this.nbiUPCManagement.Name = "nbiUPCManagement";
            this.nbiUPCManagement.Visible = false;
            this.nbiUPCManagement.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiUPCManagement_LinkClicked);
            // 
            // nbiReconcile
            // 
            this.nbiReconcile.Caption = "POS Reconciliation";
            this.nbiReconcile.LargeImage = global::BeverageManagement.Properties.Resources.cash_register_icon;
            this.nbiReconcile.Name = "nbiReconcile";
            this.nbiReconcile.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiReconcile_LinkClicked);
            // 
            // nbiAdministration
            // 
            this.nbiAdministration.Caption = global::BeverageManagement.Properties.Resources.Administration;
            this.nbiAdministration.LargeImage = global::BeverageManagement.Properties.Resources.Tools_32;
            this.nbiAdministration.Name = "nbiAdministration";
            this.nbiAdministration.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiAdministration_LinkClicked);
            // 
            // nbgReports
            // 
            this.nbgReports.Caption = global::BeverageManagement.Properties.Resources.Reports;
            this.nbgReports.Expanded = true;
            this.nbgReports.Name = "nbgReports";
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtraTabbedMdiManager1.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InTabControlHeader;
            this.xtraTabbedMdiManager1.FloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabbedMdiManager1.FloatOnDrag = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabbedMdiManager1.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Always;
            this.xtraTabbedMdiManager1.MdiParent = this;
            this.xtraTabbedMdiManager1.SelectedPageChanged += new System.EventHandler(this.xtraTabbedMdiManager1_SelectedPageChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.navBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.LookAndFeel.SkinName = "Black";
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.scStyleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel dlfLookAndFeel;
        private DevExpress.XtraEditors.StyleController scStyleController;
        private DevExpress.XtraNavBar.NavBarControl navBar;
        private DevExpress.XtraNavBar.NavBarGroup nbgManagement;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraNavBar.NavBarGroup nbgReports;
        private DevExpress.XtraNavBar.NavBarItem nbiBranding;
        private DevExpress.XtraNavBar.NavBarItem nbiBarManagement;
        private DevExpress.XtraNavBar.NavBarItem nbiInventory;
        private DevExpress.XtraNavBar.NavBarItem nbiUPCManagement;
        private DevExpress.XtraNavBar.NavBarItem nbiActivity;
        private DevExpress.XtraNavBar.NavBarItem nbiAdministration;
        private DevExpress.XtraNavBar.NavBarItem nbiReconcile;
    }
}

