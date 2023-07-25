namespace BeverageManagement
{
    partial class frmGridReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGridReport));
            this.ttcTooltips = new DevExpress.Utils.ToolTipController(this.components);
            this.printBarManager1 = new DevExpress.XtraPrinting.Preview.PrintBarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.printPreviewStaticItem1 = new DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.progressBarEditItem1 = new DevExpress.XtraPrinting.Preview.ProgressBarEditItem();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.printPreviewBarItem1 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.printPreviewStaticItem2 = new DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem();
            this.zoomTrackBarEditItem1 = new DevExpress.XtraPrinting.Preview.ZoomTrackBarEditItem();
            this.repositoryItemZoomTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
            this.printPreviewRepositoryItemComboBox1 = new DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox();
            this.barToolbarsListItem1 = new DevExpress.XtraBars.BarToolbarsListItem();
            this.printPreviewSubItem3 = new DevExpress.XtraPrinting.Preview.PrintPreviewSubItem();
            this.grdReportData = new DevExpress.XtraGrid.GridControl();
            this.gvReportData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlPageControls = new DevExpress.XtraEditors.PanelControl();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
            this.layoutParams = new DevExpress.XtraLayout.LayoutControl();
            this.lcgReportParams = new DevExpress.XtraLayout.LayoutControlGroup();
            this.pnlRunReport = new DevExpress.XtraEditors.PanelControl();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.printBarManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.printPreviewRepositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReportData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReportData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPageControls)).BeginInit();
            this.pnlPageControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgReportParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlRunReport)).BeginInit();
            this.pnlRunReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).BeginInit();
            this.SuspendLayout();
            // 
            // printBarManager1
            // 
            this.printBarManager1.DockControls.Add(this.barDockControlTop);
            this.printBarManager1.DockControls.Add(this.barDockControlBottom);
            this.printBarManager1.DockControls.Add(this.barDockControlLeft);
            this.printBarManager1.DockControls.Add(this.barDockControlRight);
            this.printBarManager1.Form = this;
            this.printBarManager1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("printBarManager1.ImageStream")));
            this.printBarManager1.MaxItemId = 0;
            this.printBarManager1.PreviewBar = null;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(644, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 539);
            this.barDockControlBottom.Size = new System.Drawing.Size(644, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 539);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(644, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 539);
            // 
            // printPreviewStaticItem1
            // 
            this.printPreviewStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.printPreviewStaticItem1.Caption = "Nothing";
            this.printPreviewStaticItem1.Id = 0;
            this.printPreviewStaticItem1.LeftIndent = 1;
            this.printPreviewStaticItem1.Name = "printPreviewStaticItem1";
            this.printPreviewStaticItem1.RightIndent = 1;
            this.printPreviewStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            this.printPreviewStaticItem1.Type = "PageOfPages";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStaticItem1.Id = 1;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            this.barStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime;
            // 
            // progressBarEditItem1
            // 
            this.progressBarEditItem1.Edit = this.repositoryItemProgressBar1;
            this.progressBarEditItem1.EditHeight = 12;
            this.progressBarEditItem1.Id = 2;
            this.progressBarEditItem1.Name = "progressBarEditItem1";
            this.progressBarEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.progressBarEditItem1.Width = 150;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            this.repositoryItemProgressBar1.UseParentBackground = true;
            // 
            // printPreviewBarItem1
            // 
            this.printPreviewBarItem1.Caption = global::BeverageManagement.Properties.Resources.Stop;
            this.printPreviewBarItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.StopPageBuilding;
            this.printPreviewBarItem1.Enabled = false;
            this.printPreviewBarItem1.Hint = global::BeverageManagement.Properties.Resources.Stop;
            this.printPreviewBarItem1.Id = 3;
            this.printPreviewBarItem1.Name = "printPreviewBarItem1";
            this.printPreviewBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.barButtonItem1.Enabled = false;
            this.barButtonItem1.Id = 4;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime;
            // 
            // printPreviewStaticItem2
            // 
            this.printPreviewStaticItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.printPreviewStaticItem2.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.printPreviewStaticItem2.Caption = "100%";
            this.printPreviewStaticItem2.Id = 5;
            this.printPreviewStaticItem2.Name = "printPreviewStaticItem2";
            this.printPreviewStaticItem2.TextAlignment = System.Drawing.StringAlignment.Far;
            this.printPreviewStaticItem2.Type = "ZoomFactor";
            this.printPreviewStaticItem2.Width = 40;
            // 
            // zoomTrackBarEditItem1
            // 
            this.zoomTrackBarEditItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.zoomTrackBarEditItem1.Edit = this.repositoryItemZoomTrackBar1;
            this.zoomTrackBarEditItem1.EditValue = 90;
            this.zoomTrackBarEditItem1.Enabled = false;
            this.zoomTrackBarEditItem1.Id = 6;
            this.zoomTrackBarEditItem1.Name = "zoomTrackBarEditItem1";
            this.zoomTrackBarEditItem1.Range = new int[] {
        10,
        500};
            this.zoomTrackBarEditItem1.Width = 140;
            // 
            // repositoryItemZoomTrackBar1
            // 
            this.repositoryItemZoomTrackBar1.Alignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemZoomTrackBar1.AllowFocused = false;
            this.repositoryItemZoomTrackBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.repositoryItemZoomTrackBar1.Maximum = 180;
            this.repositoryItemZoomTrackBar1.Name = "repositoryItemZoomTrackBar1";
            this.repositoryItemZoomTrackBar1.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            this.repositoryItemZoomTrackBar1.UseParentBackground = true;
            // 
            // printPreviewRepositoryItemComboBox1
            // 
            this.printPreviewRepositoryItemComboBox1.AutoComplete = false;
            this.printPreviewRepositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.printPreviewRepositoryItemComboBox1.DropDownRows = 11;
            this.printPreviewRepositoryItemComboBox1.Name = "printPreviewRepositoryItemComboBox1";
            this.printPreviewRepositoryItemComboBox1.UseParentBackground = true;
            // 
            // barToolbarsListItem1
            // 
            this.barToolbarsListItem1.Caption = "Bars";
            this.barToolbarsListItem1.Id = 39;
            this.barToolbarsListItem1.Name = "barToolbarsListItem1";
            // 
            // printPreviewSubItem3
            // 
            this.printPreviewSubItem3.Caption = "&Background";
            this.printPreviewSubItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Background;
            this.printPreviewSubItem3.Id = 35;
            this.printPreviewSubItem3.Name = "printPreviewSubItem3";
            // 
            // grdReportData
            // 
            this.grdReportData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReportData.Location = new System.Drawing.Point(0, 170);
            this.grdReportData.MainView = this.gvReportData;
            this.grdReportData.Name = "grdReportData";
            this.grdReportData.Size = new System.Drawing.Size(644, 336);
            this.grdReportData.TabIndex = 4;
            this.grdReportData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvReportData});
            // 
            // gvReportData
            // 
            this.gvReportData.GridControl = this.grdReportData;
            this.gvReportData.Name = "gvReportData";
            this.gvReportData.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvReportData.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvReportData.OptionsBehavior.Editable = false;
            this.gvReportData.OptionsBehavior.ReadOnly = true;
            this.gvReportData.OptionsNavigation.AutoFocusNewRow = true;
            this.gvReportData.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvReportData.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvReportData.OptionsView.ShowFooter = true;
            this.gvReportData.OptionsView.ShowIndicator = false;
            // 
            // pnlPageControls
            // 
            this.pnlPageControls.Controls.Add(this.btnPrint);
            this.pnlPageControls.Controls.Add(this.btnClose);
            this.pnlPageControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPageControls.Location = new System.Drawing.Point(0, 506);
            this.pnlPageControls.Name = "pnlPageControls";
            this.pnlPageControls.Size = new System.Drawing.Size(644, 33);
            this.pnlPageControls.TabIndex = 5;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(483, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(564, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // sfdExport
            // 
            this.sfdExport.DefaultExt = "xlsx";
            this.sfdExport.Filter = "Excel|*.xlsx|PDF|*.pdf|RTF|*.rtf|HTML|*.html";
            this.sfdExport.Title = global::BeverageManagement.Properties.Resources.ReportExport;
            // 
            // layoutParams
            // 
            this.layoutParams.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutParams.Location = new System.Drawing.Point(0, 0);
            this.layoutParams.Name = "layoutParams";
            this.layoutParams.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2791, 347, 542, 502);
            this.layoutParams.Root = this.lcgReportParams;
            this.layoutParams.Size = new System.Drawing.Size(644, 139);
            this.layoutParams.TabIndex = 0;
            this.layoutParams.Text = "layoutControl1";
            // 
            // lcgReportParams
            // 
            this.lcgReportParams.ContentImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.lcgReportParams.CustomizationFormText = "lcgReportParams";
            this.lcgReportParams.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgReportParams.ExpandButtonVisible = true;
            this.lcgReportParams.ExpandOnDoubleClick = true;
            this.lcgReportParams.Location = new System.Drawing.Point(0, 0);
            this.lcgReportParams.Name = "lcgReportParams";
            this.lcgReportParams.Size = new System.Drawing.Size(644, 139);
            this.lcgReportParams.Spacing = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcgReportParams.Text = global::BeverageManagement.Properties.Resources.ReportParameters;
            this.lcgReportParams.Shown += new System.EventHandler(this.lcgReportParams_Hidden);
            this.lcgReportParams.Hidden += new System.EventHandler(this.lcgReportParams_Hidden);
            // 
            // pnlRunReport
            // 
            this.pnlRunReport.Controls.Add(this.btnQuery);
            this.pnlRunReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRunReport.Location = new System.Drawing.Point(0, 139);
            this.pnlRunReport.Name = "pnlRunReport";
            this.pnlRunReport.Size = new System.Drawing.Size(644, 31);
            this.pnlRunReport.TabIndex = 2;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(522, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(119, 23);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "Run Report";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // frmGridReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 539);
            this.Controls.Add(this.grdReportData);
            this.Controls.Add(this.pnlRunReport);
            this.Controls.Add(this.layoutParams);
            this.Controls.Add(this.pnlPageControls);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmGridReport";
            this.Text = "Bar Activity";
            this.Load += new System.EventHandler(this.frmGridReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.printBarManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.printPreviewRepositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReportData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReportData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPageControls)).EndInit();
            this.pnlPageControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutParams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgReportParams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlRunReport)).EndInit();
            this.pnlRunReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ToolTipController ttcTooltips;
        private DevExpress.XtraPrinting.Preview.PrintBarManager printBarManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl grdReportData;
        private DevExpress.XtraGrid.Views.Grid.GridView gvReportData;
        private DevExpress.XtraEditors.PanelControl pnlPageControls;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem printPreviewStaticItem1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraPrinting.Preview.ProgressBarEditItem progressBarEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem printPreviewStaticItem2;
        private DevExpress.XtraPrinting.Preview.ZoomTrackBarEditItem zoomTrackBarEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar repositoryItemZoomTrackBar1;
        private DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox printPreviewRepositoryItemComboBox1;
        private DevExpress.XtraBars.BarToolbarsListItem barToolbarsListItem1;
        private DevExpress.XtraPrinting.Preview.PrintPreviewSubItem printPreviewSubItem3;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private System.Windows.Forms.SaveFileDialog sfdExport;
        private DevExpress.XtraEditors.PanelControl pnlRunReport;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraLayout.LayoutControl layoutParams;
        private DevExpress.XtraLayout.LayoutControlGroup lcgReportParams;
        private DevExpress.XtraPrinting.PrintingSystem printingSystem1;




    }
}