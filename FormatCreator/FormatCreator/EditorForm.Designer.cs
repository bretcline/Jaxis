namespace LFI.RFID.Editor
{
    partial class EditorForm
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
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.buttonNew = new DevExpress.XtraBars.BarButtonItem();
            this.buttonDelete = new DevExpress.XtraBars.BarButtonItem();
            this.buttonSave = new DevExpress.XtraBars.BarButtonItem();
            this.buttonRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.buttonCopy = new DevExpress.XtraBars.BarButtonItem();
            this.buttonTagCreate = new DevExpress.XtraBars.BarButtonItem();
            this.buttonTagRead = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageHome = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupFormats = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupData = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupTag = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.dataRowDefEditorHeader = new LFI.RFID.Editor.DataRowDefEditor();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.dataRowDefEditorData = new LFI.RFID.Editor.DataRowDefEditor();
            this.spinEditHistory = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.memoEditDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.listFormats = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditHistory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listFormats)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationIcon = null;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.buttonNew,
            this.buttonDelete,
            this.buttonSave,
            this.buttonRefresh,
            this.buttonCopy,
            this.buttonTagCreate,
            this.buttonTagRead});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 9;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageHome});
            this.ribbonControl1.SelectedPage = this.ribbonPageHome;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(951, 115);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // buttonNew
            // 
            this.buttonNew.Caption = "New";
            this.buttonNew.Id = 0;
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.buttonNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonNew_ItemClick);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Caption = "Delete";
            this.buttonDelete.Id = 1;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.buttonDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonDelete_ItemClick);
            // 
            // buttonSave
            // 
            this.buttonSave.Caption = "Save";
            this.buttonSave.Id = 3;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.buttonSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonSave_ItemClick);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Caption = "Refresh";
            this.buttonRefresh.Id = 4;
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.buttonRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonRefresh_ItemClick);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Caption = "Copy";
            this.buttonCopy.Id = 6;
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.buttonCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonCopy_ItemClick);
            // 
            // buttonTagCreate
            // 
            this.buttonTagCreate.Caption = "Create";
            this.buttonTagCreate.Id = 7;
            this.buttonTagCreate.Name = "buttonTagCreate";
            this.buttonTagCreate.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.buttonTagCreate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonTagCreate_ItemClick);
            // 
            // buttonTagRead
            // 
            this.buttonTagRead.Caption = "Read";
            this.buttonTagRead.Id = 8;
            this.buttonTagRead.Name = "buttonTagRead";
            this.buttonTagRead.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.buttonTagRead.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonTagRead_ItemClick);
            // 
            // ribbonPageHome
            // 
            this.ribbonPageHome.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupFormats,
            this.ribbonPageGroupData,
            this.ribbonPageGroupTag});
            this.ribbonPageHome.Name = "ribbonPageHome";
            this.ribbonPageHome.Text = "Home";
            // 
            // ribbonPageGroupFormats
            // 
            this.ribbonPageGroupFormats.ItemLinks.Add(this.buttonNew);
            this.ribbonPageGroupFormats.ItemLinks.Add(this.buttonCopy);
            this.ribbonPageGroupFormats.ItemLinks.Add(this.buttonDelete);
            this.ribbonPageGroupFormats.Name = "ribbonPageGroupFormats";
            this.ribbonPageGroupFormats.ShowCaptionButton = false;
            this.ribbonPageGroupFormats.Text = "Formats";
            // 
            // ribbonPageGroupData
            // 
            this.ribbonPageGroupData.ItemLinks.Add(this.buttonSave);
            this.ribbonPageGroupData.ItemLinks.Add(this.buttonRefresh);
            this.ribbonPageGroupData.Name = "ribbonPageGroupData";
            this.ribbonPageGroupData.ShowCaptionButton = false;
            this.ribbonPageGroupData.Text = "Data";
            // 
            // ribbonPageGroupTag
            // 
            this.ribbonPageGroupTag.ItemLinks.Add(this.buttonTagCreate);
            this.ribbonPageGroupTag.ItemLinks.Add(this.buttonTagRead);
            this.ribbonPageGroupTag.Name = "ribbonPageGroupTag";
            this.ribbonPageGroupTag.ShowCaptionButton = false;
            this.ribbonPageGroupTag.Text = "Tags";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.xtraTabControl1);
            this.groupControl1.Controls.Add(this.spinEditHistory);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.memoEditDescription);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.textEditName);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(211, 121);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(740, 523);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Format Details";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraTabControl1.Location = new System.Drawing.Point(6, 154);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(729, 364);
            this.xtraTabControl1.TabIndex = 7;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.dataRowDefEditorHeader);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(722, 335);
            this.xtraTabPage1.Text = "Header";
            // 
            // dataRowDefEditorHeader
            // 
            this.dataRowDefEditorHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataRowDefEditorHeader.Location = new System.Drawing.Point(0, 0);
            this.dataRowDefEditorHeader.Name = "dataRowDefEditorHeader";
            this.dataRowDefEditorHeader.Size = new System.Drawing.Size(722, 335);
            this.dataRowDefEditorHeader.TabIndex = 0;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.dataRowDefEditorData);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(722, 335);
            this.xtraTabPage2.Text = "Data";
            // 
            // dataRowDefEditorData
            // 
            this.dataRowDefEditorData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataRowDefEditorData.Location = new System.Drawing.Point(0, 0);
            this.dataRowDefEditorData.Name = "dataRowDefEditorData";
            this.dataRowDefEditorData.Size = new System.Drawing.Size(722, 335);
            this.dataRowDefEditorData.TabIndex = 0;
            // 
            // spinEditHistory
            // 
            this.spinEditHistory.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditHistory.Location = new System.Drawing.Point(77, 114);
            this.spinEditHistory.MenuManager = this.ribbonControl1;
            this.spinEditHistory.Name = "spinEditHistory";
            this.spinEditHistory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditHistory.Properties.IsFloatValue = false;
            this.spinEditHistory.Properties.Mask.EditMask = "N00";
            this.spinEditHistory.Size = new System.Drawing.Size(60, 20);
            this.spinEditHistory.TabIndex = 6;
            this.spinEditHistory.EditValueChanged += new System.EventHandler(this.spinEditHistory_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(6, 117);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(57, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Max History";
            // 
            // memoEditDescription
            // 
            this.memoEditDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.memoEditDescription.Location = new System.Drawing.Point(77, 47);
            this.memoEditDescription.MenuManager = this.ribbonControl1;
            this.memoEditDescription.Name = "memoEditDescription";
            this.memoEditDescription.Size = new System.Drawing.Size(658, 64);
            this.memoEditDescription.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 50);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Description";
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(77, 24);
            this.textEditName.MenuManager = this.ribbonControl1;
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new System.Drawing.Size(214, 20);
            this.textEditName.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(27, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Name";
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControl2.Controls.Add(this.listFormats);
            this.groupControl2.Location = new System.Drawing.Point(7, 121);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(200, 525);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "Available Formats";
            // 
            // listFormats
            // 
            this.listFormats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listFormats.Location = new System.Drawing.Point(2, 22);
            this.listFormats.Name = "listFormats";
            this.listFormats.Size = new System.Drawing.Size(196, 501);
            this.listFormats.TabIndex = 1;
            this.listFormats.SelectedValueChanged += new System.EventHandler(this.listFormats_SelectedValueChanged);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 646);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "EditorForm";
            this.Text = "Format Editor";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinEditHistory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listFormats)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageHome;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupFormats;
        private DevExpress.XtraBars.BarButtonItem buttonNew;
        private DevExpress.XtraBars.BarButtonItem buttonDelete;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.ListBoxControl listFormats;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.MemoEdit memoEditDescription;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit spinEditHistory;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DataRowDefEditor dataRowDefEditorHeader;
        private DataRowDefEditor dataRowDefEditorData;
        private DevExpress.XtraBars.BarButtonItem buttonSave;
        private DevExpress.XtraBars.BarButtonItem buttonRefresh;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupData;
        private DevExpress.XtraBars.BarButtonItem buttonCopy;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupTag;
        private DevExpress.XtraBars.BarButtonItem buttonTagCreate;
        private DevExpress.XtraBars.BarButtonItem buttonTagRead;
    }
}