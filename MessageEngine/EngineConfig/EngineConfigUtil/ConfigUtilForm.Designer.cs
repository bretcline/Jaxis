namespace EngineConfigUtil
{
    partial class ConfigUtilForm
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tEngine = new DevExpress.XtraTab.XtraTabPage();
            this.sbGetEngineState = new DevExpress.XtraEditors.SimpleButton();
            this.sbGetEngineVersion = new DevExpress.XtraEditors.SimpleButton();
            this.sbStartEngine = new DevExpress.XtraEditors.SimpleButton();
            this.sbStopEngine = new DevExpress.XtraEditors.SimpleButton();
            this.tEngineState = new DevExpress.XtraEditors.TextEdit();
            this.tEngineVersion = new DevExpress.XtraEditors.TextEdit();
            this.tDevices = new DevExpress.XtraTab.XtraTabPage();
            this.sbDeviceAddFilter = new DevExpress.XtraEditors.SimpleButton();
            this.abAddDevice = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmbState = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtProducerMessageType = new DevExpress.XtraEditors.TextEdit();
            this.gridControlDeviceFilterOptions = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtAssemblyName = new DevExpress.XtraEditors.TextEdit();
            this.gridControlOptions = new DevExpress.XtraGrid.GridControl();
            this.gridViewOptions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtAssemblyType = new DevExpress.XtraEditors.TextEdit();
            this.txtAssemblyVersion = new DevExpress.XtraEditors.TextEdit();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.txtType = new DevExpress.XtraEditors.TextEdit();
            this.lbFilters = new DevExpress.XtraEditors.ListBoxControl();
            this.txtConsumerMessageType = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbUpdateOptions = new DevExpress.XtraEditors.SimpleButton();
            this.lbDevices = new DevExpress.XtraEditors.ListBoxControl();
            this.sbGetDevices = new DevExpress.XtraEditors.SimpleButton();
            this.tFilters = new DevExpress.XtraTab.XtraTabPage();
            this.sbAddFilter = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlFilterOptions = new DevExpress.XtraGrid.GridControl();
            this.gridViewFilterOptions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sbUpdateFilterOptions = new DevExpress.XtraEditors.SimpleButton();
            this.tFilterVersion = new DevExpress.XtraEditors.TextEdit();
            this.lcFilterVersion = new DevExpress.XtraEditors.LabelControl();
            this.tFilterType = new DevExpress.XtraEditors.TextEdit();
            this.tFilterName = new DevExpress.XtraEditors.TextEdit();
            this.lcFilterType = new DevExpress.XtraEditors.LabelControl();
            this.lcFilterName = new DevExpress.XtraEditors.LabelControl();
            this.tFilterAssemblyType = new DevExpress.XtraEditors.TextEdit();
            this.tFilterAssemblyName = new DevExpress.XtraEditors.TextEdit();
            this.lcFilterAssemblyType = new DevExpress.XtraEditors.LabelControl();
            this.lcFilterAssemblyName = new DevExpress.XtraEditors.LabelControl();
            this.lbFiltersFilters = new DevExpress.XtraEditors.ListBoxControl();
            this.lcFiltersFilterOptions = new DevExpress.XtraEditors.LabelControl();
            this.lcFiltersFilters = new DevExpress.XtraEditors.LabelControl();
            this.lcDevices = new DevExpress.XtraEditors.LabelControl();
            this.lbFilterDevices = new DevExpress.XtraEditors.ListBoxControl();
            this.tPlugIns = new DevExpress.XtraTab.XtraTabPage();
            this.sbRemove = new DevExpress.XtraEditors.SimpleButton();
            this.lbDependents = new DevExpress.XtraEditors.ListBoxControl();
            this.lcDependents = new DevExpress.XtraEditors.LabelControl();
            this.sbDependSelectFile = new DevExpress.XtraEditors.SimpleButton();
            this.lcPlugInFile = new DevExpress.XtraEditors.LabelControl();
            this.sbSelectFile = new DevExpress.XtraEditors.SimpleButton();
            this.tPlugIn = new DevExpress.XtraEditors.TextEdit();
            this.sbPushPlugIn = new DevExpress.XtraEditors.SimpleButton();
            this.tLog = new DevExpress.XtraTab.XtraTabPage();
            this.mLog = new DevExpress.XtraEditors.MemoEdit();
            this.sbGetLog = new DevExpress.XtraEditors.SimpleButton();
            this.deviceConfigBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tbError = new System.Windows.Forms.TextBox();
            this.lLastError = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tEngine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tEngineState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tEngineVersion.Properties)).BeginInit();
            this.tDevices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProducerMessageType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDeviceFilterOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssemblyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssemblyType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssemblyVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbFilters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsumerMessageType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbDevices)).BeginInit();
            this.tFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFilterOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFilterOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tFilterVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tFilterType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tFilterName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tFilterAssemblyType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tFilterAssemblyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbFiltersFilters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbFilterDevices)).BeginInit();
            this.tPlugIns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbDependents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tPlugIn.Properties)).BeginInit();
            this.tLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deviceConfigBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraTabControl1.Location = new System.Drawing.Point(10, 9);
            this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tEngine;
            this.xtraTabControl1.Size = new System.Drawing.Size(688, 591);
            this.xtraTabControl1.TabIndex = 2;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tEngine,
            this.tDevices,
            this.tFilters,
            this.tPlugIns,
            this.tLog});
            // 
            // tEngine
            // 
            this.tEngine.Controls.Add(this.sbGetEngineState);
            this.tEngine.Controls.Add(this.sbGetEngineVersion);
            this.tEngine.Controls.Add(this.sbStartEngine);
            this.tEngine.Controls.Add(this.sbStopEngine);
            this.tEngine.Controls.Add(this.tEngineState);
            this.tEngine.Controls.Add(this.tEngineVersion);
            this.tEngine.Margin = new System.Windows.Forms.Padding(2);
            this.tEngine.Name = "tEngine";
            this.tEngine.Size = new System.Drawing.Size(682, 565);
            this.tEngine.Text = "Engine";
            // 
            // sbGetEngineState
            // 
            this.sbGetEngineState.Location = new System.Drawing.Point(3, 28);
            this.sbGetEngineState.Margin = new System.Windows.Forms.Padding(2);
            this.sbGetEngineState.Name = "sbGetEngineState";
            this.sbGetEngineState.Size = new System.Drawing.Size(114, 19);
            this.sbGetEngineState.TabIndex = 1;
            this.sbGetEngineState.Text = "Get Engine State";
            this.sbGetEngineState.Click += new System.EventHandler(this.sbGetEngineState_Click);
            // 
            // sbGetEngineVersion
            // 
            this.sbGetEngineVersion.Location = new System.Drawing.Point(3, 5);
            this.sbGetEngineVersion.Margin = new System.Windows.Forms.Padding(2);
            this.sbGetEngineVersion.Name = "sbGetEngineVersion";
            this.sbGetEngineVersion.Size = new System.Drawing.Size(114, 19);
            this.sbGetEngineVersion.TabIndex = 0;
            this.sbGetEngineVersion.Text = "Get Engine Version";
            this.sbGetEngineVersion.Click += new System.EventHandler(this.sbGetEngineVersion_Click);
            // 
            // sbStartEngine
            // 
            this.sbStartEngine.Location = new System.Drawing.Point(183, 52);
            this.sbStartEngine.Margin = new System.Windows.Forms.Padding(2);
            this.sbStartEngine.Name = "sbStartEngine";
            this.sbStartEngine.Size = new System.Drawing.Size(56, 19);
            this.sbStartEngine.TabIndex = 3;
            this.sbStartEngine.Text = "Start";
            this.sbStartEngine.Click += new System.EventHandler(this.sbStartEngine_Click);
            // 
            // sbStopEngine
            // 
            this.sbStopEngine.Location = new System.Drawing.Point(243, 52);
            this.sbStopEngine.Margin = new System.Windows.Forms.Padding(2);
            this.sbStopEngine.Name = "sbStopEngine";
            this.sbStopEngine.Size = new System.Drawing.Size(56, 19);
            this.sbStopEngine.TabIndex = 2;
            this.sbStopEngine.Text = "Stop";
            this.sbStopEngine.Click += new System.EventHandler(this.sbStopEngine_Click);
            // 
            // tEngineState
            // 
            this.tEngineState.Enabled = false;
            this.tEngineState.Location = new System.Drawing.Point(121, 27);
            this.tEngineState.Margin = new System.Windows.Forms.Padding(2);
            this.tEngineState.Name = "tEngineState";
            this.tEngineState.Size = new System.Drawing.Size(178, 20);
            this.tEngineState.TabIndex = 3;
            // 
            // tEngineVersion
            // 
            this.tEngineVersion.Enabled = false;
            this.tEngineVersion.Location = new System.Drawing.Point(121, 4);
            this.tEngineVersion.Margin = new System.Windows.Forms.Padding(2);
            this.tEngineVersion.Name = "tEngineVersion";
            this.tEngineVersion.Size = new System.Drawing.Size(178, 20);
            this.tEngineVersion.TabIndex = 1;
            // 
            // tDevices
            // 
            this.tDevices.Controls.Add(this.sbDeviceAddFilter);
            this.tDevices.Controls.Add(this.abAddDevice);
            this.tDevices.Controls.Add(this.layoutControl1);
            this.tDevices.Controls.Add(this.sbUpdateOptions);
            this.tDevices.Controls.Add(this.lbDevices);
            this.tDevices.Controls.Add(this.sbGetDevices);
            this.tDevices.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tDevices.Name = "tDevices";
            this.tDevices.Size = new System.Drawing.Size(682, 565);
            this.tDevices.Text = "Devices";
            // 
            // sbDeviceAddFilter
            // 
            this.sbDeviceAddFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sbDeviceAddFilter.Location = new System.Drawing.Point(91, 541);
            this.sbDeviceAddFilter.Name = "sbDeviceAddFilter";
            this.sbDeviceAddFilter.Size = new System.Drawing.Size(80, 18);
            this.sbDeviceAddFilter.TabIndex = 38;
            this.sbDeviceAddFilter.Text = "Add Filter";
            this.sbDeviceAddFilter.Click += new System.EventHandler(this.sbDeviceAddFilter_Click);
            // 
            // abAddDevice
            // 
            this.abAddDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.abAddDevice.Location = new System.Drawing.Point(5, 541);
            this.abAddDevice.Name = "abAddDevice";
            this.abAddDevice.Size = new System.Drawing.Size(80, 18);
            this.abAddDevice.TabIndex = 37;
            this.abAddDevice.Text = "Add Device";
            this.abAddDevice.Click += new System.EventHandler(this.abAddDevice_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Controls.Add(this.cmbState);
            this.layoutControl1.Controls.Add(this.txtProducerMessageType);
            this.layoutControl1.Controls.Add(this.gridControlDeviceFilterOptions);
            this.layoutControl1.Controls.Add(this.txtAssemblyName);
            this.layoutControl1.Controls.Add(this.gridControlOptions);
            this.layoutControl1.Controls.Add(this.txtAssemblyType);
            this.layoutControl1.Controls.Add(this.txtAssemblyVersion);
            this.layoutControl1.Controls.Add(this.txtID);
            this.layoutControl1.Controls.Add(this.txtType);
            this.layoutControl1.Controls.Add(this.lbFilters);
            this.layoutControl1.Controls.Add(this.txtConsumerMessageType);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Location = new System.Drawing.Point(176, 10);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(505, 531);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmbState
            // 
            this.cmbState.Location = new System.Drawing.Point(90, 147);
            this.cmbState.Name = "cmbState";
            this.cmbState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbState.Properties.Items.AddRange(new object[] {
            "Started",
            "Stopped"});
            this.cmbState.Size = new System.Drawing.Size(412, 20);
            this.cmbState.StyleController = this.layoutControl1;
            this.cmbState.TabIndex = 43;
            // 
            // txtProducerMessageType
            // 
            this.txtProducerMessageType.Location = new System.Drawing.Point(341, 123);
            this.txtProducerMessageType.Name = "txtProducerMessageType";
            this.txtProducerMessageType.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtProducerMessageType.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtProducerMessageType.Size = new System.Drawing.Size(161, 20);
            this.txtProducerMessageType.StyleController = this.layoutControl1;
            this.txtProducerMessageType.TabIndex = 42;
            // 
            // gridControlDeviceFilterOptions
            // 
            this.gridControlDeviceFilterOptions.Location = new System.Drawing.Point(175, 394);
            this.gridControlDeviceFilterOptions.MainView = this.gridView1;
            this.gridControlDeviceFilterOptions.Margin = new System.Windows.Forms.Padding(2);
            this.gridControlDeviceFilterOptions.Name = "gridControlDeviceFilterOptions";
            this.gridControlDeviceFilterOptions.Size = new System.Drawing.Size(327, 134);
            this.gridControlDeviceFilterOptions.TabIndex = 41;
            this.gridControlDeviceFilterOptions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlDeviceFilterOptions;
            this.gridView1.Name = "gridView1";
            // 
            // txtAssemblyName
            // 
            this.txtAssemblyName.Enabled = false;
            this.txtAssemblyName.Location = new System.Drawing.Point(90, 3);
            this.txtAssemblyName.Margin = new System.Windows.Forms.Padding(2);
            this.txtAssemblyName.Name = "txtAssemblyName";
            this.txtAssemblyName.Size = new System.Drawing.Size(412, 20);
            this.txtAssemblyName.StyleController = this.layoutControl1;
            this.txtAssemblyName.TabIndex = 10;
            // 
            // gridControlOptions
            // 
            this.gridControlOptions.Location = new System.Drawing.Point(3, 187);
            this.gridControlOptions.MainView = this.gridViewOptions;
            this.gridControlOptions.Margin = new System.Windows.Forms.Padding(2);
            this.gridControlOptions.Name = "gridControlOptions";
            this.gridControlOptions.Size = new System.Drawing.Size(499, 187);
            this.gridControlOptions.TabIndex = 38;
            this.gridControlOptions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOptions});
            // 
            // gridViewOptions
            // 
            this.gridViewOptions.GridControl = this.gridControlOptions;
            this.gridViewOptions.Name = "gridViewOptions";
            // 
            // txtAssemblyType
            // 
            this.txtAssemblyType.Enabled = false;
            this.txtAssemblyType.Location = new System.Drawing.Point(90, 27);
            this.txtAssemblyType.Margin = new System.Windows.Forms.Padding(2);
            this.txtAssemblyType.Name = "txtAssemblyType";
            this.txtAssemblyType.Size = new System.Drawing.Size(412, 20);
            this.txtAssemblyType.StyleController = this.layoutControl1;
            this.txtAssemblyType.TabIndex = 11;
            // 
            // txtAssemblyVersion
            // 
            this.txtAssemblyVersion.Enabled = false;
            this.txtAssemblyVersion.Location = new System.Drawing.Point(90, 51);
            this.txtAssemblyVersion.Margin = new System.Windows.Forms.Padding(2);
            this.txtAssemblyVersion.Name = "txtAssemblyVersion";
            this.txtAssemblyVersion.Size = new System.Drawing.Size(412, 20);
            this.txtAssemblyVersion.StyleController = this.layoutControl1;
            this.txtAssemblyVersion.TabIndex = 31;
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(90, 99);
            this.txtID.Margin = new System.Windows.Forms.Padding(2);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(160, 20);
            this.txtID.StyleController = this.layoutControl1;
            this.txtID.TabIndex = 12;
            // 
            // txtType
            // 
            this.txtType.Enabled = false;
            this.txtType.Location = new System.Drawing.Point(90, 75);
            this.txtType.Margin = new System.Windows.Forms.Padding(2);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(412, 20);
            this.txtType.StyleController = this.layoutControl1;
            this.txtType.TabIndex = 14;
            // 
            // lbFilters
            // 
            this.lbFilters.Location = new System.Drawing.Point(3, 378);
            this.lbFilters.Margin = new System.Windows.Forms.Padding(2);
            this.lbFilters.Name = "lbFilters";
            this.lbFilters.Size = new System.Drawing.Size(168, 150);
            this.lbFilters.StyleController = this.layoutControl1;
            this.lbFilters.TabIndex = 35;
            this.lbFilters.SelectedIndexChanged += new System.EventHandler(this.lbFilters_SelectedIndexChanged);
            // 
            // txtConsumerMessageType
            // 
            this.txtConsumerMessageType.Location = new System.Drawing.Point(90, 123);
            this.txtConsumerMessageType.Margin = new System.Windows.Forms.Padding(2);
            this.txtConsumerMessageType.Name = "txtConsumerMessageType";
            this.txtConsumerMessageType.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtConsumerMessageType.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtConsumerMessageType.Size = new System.Drawing.Size(160, 20);
            this.txtConsumerMessageType.StyleController = this.layoutControl1;
            this.txtConsumerMessageType.TabIndex = 17;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(341, 99);
            this.txtName.Margin = new System.Windows.Forms.Padding(2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(161, 20);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 13;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem7,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem12,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem8});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup1.Size = new System.Drawing.Size(505, 531);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtAssemblyType;
            this.layoutControlItem2.CustomizationFormText = "Assembly Type";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(503, 24);
            this.layoutControlItem2.Text = "Assembly Type";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtAssemblyName;
            this.layoutControlItem1.CustomizationFormText = "Assembly Name";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(503, 24);
            this.layoutControlItem1.Text = "Assembly Name";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtAssemblyVersion;
            this.layoutControlItem3.CustomizationFormText = "Assembly Version";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(503, 24);
            this.layoutControlItem3.Text = "Assembly Version";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtID;
            this.layoutControlItem4.CustomizationFormText = "ID";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(251, 24);
            this.layoutControlItem4.Text = "ID";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtConsumerMessageType;
            this.layoutControlItem7.CustomizationFormText = "Consume Type";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(251, 24);
            this.layoutControlItem7.Text = "Consume Type";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.lbFilters;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 375);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(172, 154);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.gridControlDeviceFilterOptions;
            this.layoutControlItem10.CustomizationFormText = "Filter Options";
            this.layoutControlItem10.Location = new System.Drawing.Point(172, 375);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(331, 154);
            this.layoutControlItem10.Text = "Filter Options";
            this.layoutControlItem10.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.gridControlOptions;
            this.layoutControlItem11.CustomizationFormText = "Options";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 168);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(503, 207);
            this.layoutControlItem11.Text = "Options";
            this.layoutControlItem11.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.txtProducerMessageType;
            this.layoutControlItem12.CustomizationFormText = "Produce Type";
            this.layoutControlItem12.Location = new System.Drawing.Point(251, 120);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(252, 24);
            this.layoutControlItem12.Text = "Produce Type";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtName;
            this.layoutControlItem5.CustomizationFormText = "Name";
            this.layoutControlItem5.Location = new System.Drawing.Point(251, 96);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(252, 24);
            this.layoutControlItem5.Text = "Name";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtType;
            this.layoutControlItem6.CustomizationFormText = "Type";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(503, 24);
            this.layoutControlItem6.Text = "Type";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.cmbState;
            this.layoutControlItem8.CustomizationFormText = "Device State";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(503, 24);
            this.layoutControlItem8.Text = "Device State";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(83, 13);
            // 
            // sbUpdateOptions
            // 
            this.sbUpdateOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbUpdateOptions.Location = new System.Drawing.Point(582, 543);
            this.sbUpdateOptions.Margin = new System.Windows.Forms.Padding(2);
            this.sbUpdateOptions.Name = "sbUpdateOptions";
            this.sbUpdateOptions.Size = new System.Drawing.Size(95, 19);
            this.sbUpdateOptions.TabIndex = 36;
            this.sbUpdateOptions.Text = "Update Device";
            this.sbUpdateOptions.Click += new System.EventHandler(this.sbUpdateOptions_Click);
            // 
            // lbDevices
            // 
            this.lbDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDevices.Location = new System.Drawing.Point(5, 10);
            this.lbDevices.Margin = new System.Windows.Forms.Padding(2);
            this.lbDevices.Name = "lbDevices";
            this.lbDevices.Size = new System.Drawing.Size(166, 496);
            this.lbDevices.TabIndex = 1;
            this.lbDevices.SelectedIndexChanged += new System.EventHandler(this.lbDevices_SelectedIndexChanged);
            // 
            // sbGetDevices
            // 
            this.sbGetDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sbGetDevices.Location = new System.Drawing.Point(5, 514);
            this.sbGetDevices.Margin = new System.Windows.Forms.Padding(2);
            this.sbGetDevices.Name = "sbGetDevices";
            this.sbGetDevices.Size = new System.Drawing.Size(166, 19);
            this.sbGetDevices.TabIndex = 0;
            this.sbGetDevices.Text = "Refresh Devices";
            this.sbGetDevices.Click += new System.EventHandler(this.sbGetDevices_Click);
            // 
            // tFilters
            // 
            this.tFilters.Controls.Add(this.sbAddFilter);
            this.tFilters.Controls.Add(this.gridControlFilterOptions);
            this.tFilters.Controls.Add(this.sbUpdateFilterOptions);
            this.tFilters.Controls.Add(this.tFilterVersion);
            this.tFilters.Controls.Add(this.lcFilterVersion);
            this.tFilters.Controls.Add(this.tFilterType);
            this.tFilters.Controls.Add(this.tFilterName);
            this.tFilters.Controls.Add(this.lcFilterType);
            this.tFilters.Controls.Add(this.lcFilterName);
            this.tFilters.Controls.Add(this.tFilterAssemblyType);
            this.tFilters.Controls.Add(this.tFilterAssemblyName);
            this.tFilters.Controls.Add(this.lcFilterAssemblyType);
            this.tFilters.Controls.Add(this.lcFilterAssemblyName);
            this.tFilters.Controls.Add(this.lbFiltersFilters);
            this.tFilters.Controls.Add(this.lcFiltersFilterOptions);
            this.tFilters.Controls.Add(this.lcFiltersFilters);
            this.tFilters.Controls.Add(this.lcDevices);
            this.tFilters.Controls.Add(this.lbFilterDevices);
            this.tFilters.Margin = new System.Windows.Forms.Padding(2);
            this.tFilters.Name = "tFilters";
            this.tFilters.PageVisible = false;
            this.tFilters.Size = new System.Drawing.Size(682, 565);
            this.tFilters.Text = "Filters";
            // 
            // sbAddFilter
            // 
            this.sbAddFilter.Location = new System.Drawing.Point(5, 375);
            this.sbAddFilter.Margin = new System.Windows.Forms.Padding(2);
            this.sbAddFilter.Name = "sbAddFilter";
            this.sbAddFilter.Size = new System.Drawing.Size(100, 19);
            this.sbAddFilter.TabIndex = 52;
            this.sbAddFilter.Text = "Add Filter";
            this.sbAddFilter.Click += new System.EventHandler(this.sbAddFilter_Click);
            // 
            // gridControlFilterOptions
            // 
            this.gridControlFilterOptions.Location = new System.Drawing.Point(245, 225);
            this.gridControlFilterOptions.MainView = this.gridViewFilterOptions;
            this.gridControlFilterOptions.Margin = new System.Windows.Forms.Padding(2);
            this.gridControlFilterOptions.Name = "gridControlFilterOptions";
            this.gridControlFilterOptions.Size = new System.Drawing.Size(406, 136);
            this.gridControlFilterOptions.TabIndex = 51;
            this.gridControlFilterOptions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFilterOptions});
            // 
            // gridViewFilterOptions
            // 
            this.gridViewFilterOptions.GridControl = this.gridControlFilterOptions;
            this.gridViewFilterOptions.Name = "gridViewFilterOptions";
            // 
            // sbUpdateFilterOptions
            // 
            this.sbUpdateFilterOptions.Location = new System.Drawing.Point(540, 391);
            this.sbUpdateFilterOptions.Margin = new System.Windows.Forms.Padding(2);
            this.sbUpdateFilterOptions.Name = "sbUpdateFilterOptions";
            this.sbUpdateFilterOptions.Size = new System.Drawing.Size(111, 19);
            this.sbUpdateFilterOptions.TabIndex = 50;
            this.sbUpdateFilterOptions.Text = "Update Filter";
            this.sbUpdateFilterOptions.Click += new System.EventHandler(this.sbUpdateFilterOptions_Click);
            // 
            // tFilterVersion
            // 
            this.tFilterVersion.Enabled = false;
            this.tFilterVersion.Location = new System.Drawing.Point(245, 199);
            this.tFilterVersion.Margin = new System.Windows.Forms.Padding(2);
            this.tFilterVersion.Name = "tFilterVersion";
            this.tFilterVersion.Size = new System.Drawing.Size(406, 20);
            this.tFilterVersion.TabIndex = 49;
            // 
            // lcFilterVersion
            // 
            this.lcFilterVersion.Location = new System.Drawing.Point(179, 204);
            this.lcFilterVersion.Margin = new System.Windows.Forms.Padding(2);
            this.lcFilterVersion.Name = "lcFilterVersion";
            this.lcFilterVersion.Size = new System.Drawing.Size(62, 13);
            this.lcFilterVersion.TabIndex = 48;
            this.lcFilterVersion.Text = "Filter Version";
            // 
            // tFilterType
            // 
            this.tFilterType.Enabled = false;
            this.tFilterType.Location = new System.Drawing.Point(245, 176);
            this.tFilterType.Margin = new System.Windows.Forms.Padding(2);
            this.tFilterType.Name = "tFilterType";
            this.tFilterType.Size = new System.Drawing.Size(406, 20);
            this.tFilterType.TabIndex = 47;
            // 
            // tFilterName
            // 
            this.tFilterName.Enabled = false;
            this.tFilterName.Location = new System.Drawing.Point(245, 152);
            this.tFilterName.Margin = new System.Windows.Forms.Padding(2);
            this.tFilterName.Name = "tFilterName";
            this.tFilterName.Size = new System.Drawing.Size(406, 20);
            this.tFilterName.TabIndex = 46;
            // 
            // lcFilterType
            // 
            this.lcFilterType.Location = new System.Drawing.Point(190, 179);
            this.lcFilterType.Margin = new System.Windows.Forms.Padding(2);
            this.lcFilterType.Name = "lcFilterType";
            this.lcFilterType.Size = new System.Drawing.Size(51, 13);
            this.lcFilterType.TabIndex = 45;
            this.lcFilterType.Text = "Filter Type";
            // 
            // lcFilterName
            // 
            this.lcFilterName.Location = new System.Drawing.Point(187, 155);
            this.lcFilterName.Margin = new System.Windows.Forms.Padding(2);
            this.lcFilterName.Name = "lcFilterName";
            this.lcFilterName.Size = new System.Drawing.Size(54, 13);
            this.lcFilterName.TabIndex = 44;
            this.lcFilterName.Text = "Filter Name";
            // 
            // tFilterAssemblyType
            // 
            this.tFilterAssemblyType.Enabled = false;
            this.tFilterAssemblyType.Location = new System.Drawing.Point(245, 129);
            this.tFilterAssemblyType.Margin = new System.Windows.Forms.Padding(2);
            this.tFilterAssemblyType.Name = "tFilterAssemblyType";
            this.tFilterAssemblyType.Size = new System.Drawing.Size(406, 20);
            this.tFilterAssemblyType.TabIndex = 43;
            // 
            // tFilterAssemblyName
            // 
            this.tFilterAssemblyName.Enabled = false;
            this.tFilterAssemblyName.Location = new System.Drawing.Point(245, 105);
            this.tFilterAssemblyName.Margin = new System.Windows.Forms.Padding(2);
            this.tFilterAssemblyName.Name = "tFilterAssemblyName";
            this.tFilterAssemblyName.Size = new System.Drawing.Size(406, 20);
            this.tFilterAssemblyName.TabIndex = 42;
            // 
            // lcFilterAssemblyType
            // 
            this.lcFilterAssemblyType.Location = new System.Drawing.Point(145, 132);
            this.lcFilterAssemblyType.Margin = new System.Windows.Forms.Padding(2);
            this.lcFilterAssemblyType.Name = "lcFilterAssemblyType";
            this.lcFilterAssemblyType.Size = new System.Drawing.Size(96, 13);
            this.lcFilterAssemblyType.TabIndex = 41;
            this.lcFilterAssemblyType.Text = "Filter AssemblyType";
            // 
            // lcFilterAssemblyName
            // 
            this.lcFilterAssemblyName.Location = new System.Drawing.Point(142, 108);
            this.lcFilterAssemblyName.Margin = new System.Windows.Forms.Padding(2);
            this.lcFilterAssemblyName.Name = "lcFilterAssemblyName";
            this.lcFilterAssemblyName.Size = new System.Drawing.Size(99, 13);
            this.lcFilterAssemblyName.TabIndex = 40;
            this.lcFilterAssemblyName.Text = "Filter AssemblyName";
            // 
            // lbFiltersFilters
            // 
            this.lbFiltersFilters.Location = new System.Drawing.Point(245, 37);
            this.lbFiltersFilters.Margin = new System.Windows.Forms.Padding(2);
            this.lbFiltersFilters.Name = "lbFiltersFilters";
            this.lbFiltersFilters.Size = new System.Drawing.Size(406, 58);
            this.lbFiltersFilters.TabIndex = 1;
            this.lbFiltersFilters.SelectedIndexChanged += new System.EventHandler(this.lbFiltersFilters_SelectedIndexChanged);
            // 
            // lcFiltersFilterOptions
            // 
            this.lcFiltersFilterOptions.Location = new System.Drawing.Point(177, 225);
            this.lcFiltersFilterOptions.Margin = new System.Windows.Forms.Padding(2);
            this.lcFiltersFilterOptions.Name = "lcFiltersFilterOptions";
            this.lcFiltersFilterOptions.Size = new System.Drawing.Size(64, 13);
            this.lcFiltersFilterOptions.TabIndex = 37;
            this.lcFiltersFilterOptions.Text = "Filter Options";
            // 
            // lcFiltersFilters
            // 
            this.lcFiltersFilters.Location = new System.Drawing.Point(212, 37);
            this.lcFiltersFilters.Margin = new System.Windows.Forms.Padding(2);
            this.lcFiltersFilters.Name = "lcFiltersFilters";
            this.lcFiltersFilters.Size = new System.Drawing.Size(29, 13);
            this.lcFiltersFilters.TabIndex = 36;
            this.lcFiltersFilters.Text = "Filters";
            // 
            // lcDevices
            // 
            this.lcDevices.Location = new System.Drawing.Point(5, 2);
            this.lcDevices.Margin = new System.Windows.Forms.Padding(2);
            this.lcDevices.Name = "lcDevices";
            this.lcDevices.Size = new System.Drawing.Size(37, 13);
            this.lcDevices.TabIndex = 3;
            this.lcDevices.Text = "Devices";
            // 
            // lbFilterDevices
            // 
            this.lbFilterDevices.Location = new System.Drawing.Point(2, 19);
            this.lbFilterDevices.Margin = new System.Windows.Forms.Padding(2);
            this.lbFilterDevices.Name = "lbFilterDevices";
            this.lbFilterDevices.Size = new System.Drawing.Size(131, 329);
            this.lbFilterDevices.TabIndex = 0;
            this.lbFilterDevices.SelectedIndexChanged += new System.EventHandler(this.lbFilterDevices_SelectedIndexChanged);
            // 
            // tPlugIns
            // 
            this.tPlugIns.Controls.Add(this.sbRemove);
            this.tPlugIns.Controls.Add(this.lbDependents);
            this.tPlugIns.Controls.Add(this.lcDependents);
            this.tPlugIns.Controls.Add(this.sbDependSelectFile);
            this.tPlugIns.Controls.Add(this.lcPlugInFile);
            this.tPlugIns.Controls.Add(this.sbSelectFile);
            this.tPlugIns.Controls.Add(this.tPlugIn);
            this.tPlugIns.Controls.Add(this.sbPushPlugIn);
            this.tPlugIns.Margin = new System.Windows.Forms.Padding(2);
            this.tPlugIns.Name = "tPlugIns";
            this.tPlugIns.Size = new System.Drawing.Size(682, 565);
            this.tPlugIns.Text = "Push dll";
            // 
            // sbRemove
            // 
            this.sbRemove.Location = new System.Drawing.Point(597, 111);
            this.sbRemove.Margin = new System.Windows.Forms.Padding(2);
            this.sbRemove.Name = "sbRemove";
            this.sbRemove.Size = new System.Drawing.Size(56, 19);
            this.sbRemove.TabIndex = 99;
            this.sbRemove.Text = "Remove";
            this.sbRemove.Click += new System.EventHandler(this.sbRemove_Click);
            // 
            // lbDependents
            // 
            this.lbDependents.Location = new System.Drawing.Point(17, 89);
            this.lbDependents.Margin = new System.Windows.Forms.Padding(2);
            this.lbDependents.Name = "lbDependents";
            this.lbDependents.Size = new System.Drawing.Size(575, 182);
            this.lbDependents.TabIndex = 98;
            // 
            // lcDependents
            // 
            this.lcDependents.Location = new System.Drawing.Point(16, 73);
            this.lcDependents.Margin = new System.Windows.Forms.Padding(2);
            this.lcDependents.Name = "lcDependents";
            this.lcDependents.Size = new System.Drawing.Size(67, 13);
            this.lcDependents.TabIndex = 97;
            this.lcDependents.Text = "Dependencies";
            // 
            // sbDependSelectFile
            // 
            this.sbDependSelectFile.Location = new System.Drawing.Point(597, 89);
            this.sbDependSelectFile.Margin = new System.Windows.Forms.Padding(2);
            this.sbDependSelectFile.Name = "sbDependSelectFile";
            this.sbDependSelectFile.Size = new System.Drawing.Size(56, 19);
            this.sbDependSelectFile.TabIndex = 96;
            this.sbDependSelectFile.Text = "Add";
            this.sbDependSelectFile.Click += new System.EventHandler(this.sbDependSelectFile_Click);
            // 
            // lcPlugInFile
            // 
            this.lcPlugInFile.Location = new System.Drawing.Point(16, 28);
            this.lcPlugInFile.Margin = new System.Windows.Forms.Padding(2);
            this.lcPlugInFile.Name = "lcPlugInFile";
            this.lcPlugInFile.Size = new System.Drawing.Size(49, 13);
            this.lcPlugInFile.TabIndex = 95;
            this.lcPlugInFile.Text = "PlugIn File";
            // 
            // sbSelectFile
            // 
            this.sbSelectFile.Location = new System.Drawing.Point(597, 44);
            this.sbSelectFile.Margin = new System.Windows.Forms.Padding(2);
            this.sbSelectFile.Name = "sbSelectFile";
            this.sbSelectFile.Size = new System.Drawing.Size(56, 19);
            this.sbSelectFile.TabIndex = 93;
            this.sbSelectFile.Text = "Select File";
            this.sbSelectFile.Click += new System.EventHandler(this.sbSelectFile_Click);
            // 
            // tPlugIn
            // 
            this.tPlugIn.Enabled = false;
            this.tPlugIn.Location = new System.Drawing.Point(17, 45);
            this.tPlugIn.Margin = new System.Windows.Forms.Padding(2);
            this.tPlugIn.Name = "tPlugIn";
            this.tPlugIn.Size = new System.Drawing.Size(575, 20);
            this.tPlugIn.TabIndex = 92;
            // 
            // sbPushPlugIn
            // 
            this.sbPushPlugIn.Location = new System.Drawing.Point(16, 329);
            this.sbPushPlugIn.Margin = new System.Windows.Forms.Padding(2);
            this.sbPushPlugIn.Name = "sbPushPlugIn";
            this.sbPushPlugIn.Size = new System.Drawing.Size(117, 19);
            this.sbPushPlugIn.TabIndex = 94;
            this.sbPushPlugIn.Text = "Push PlugIn";
            this.sbPushPlugIn.Click += new System.EventHandler(this.sbPushPlugIn_Click);
            // 
            // tLog
            // 
            this.tLog.Controls.Add(this.mLog);
            this.tLog.Controls.Add(this.sbGetLog);
            this.tLog.Margin = new System.Windows.Forms.Padding(2);
            this.tLog.Name = "tLog";
            this.tLog.Size = new System.Drawing.Size(682, 565);
            this.tLog.Text = "Log";
            // 
            // mLog
            // 
            this.mLog.Location = new System.Drawing.Point(3, 25);
            this.mLog.Margin = new System.Windows.Forms.Padding(2);
            this.mLog.Name = "mLog";
            this.mLog.Size = new System.Drawing.Size(662, 323);
            this.mLog.TabIndex = 1;
            // 
            // sbGetLog
            // 
            this.sbGetLog.Location = new System.Drawing.Point(3, 2);
            this.sbGetLog.Margin = new System.Windows.Forms.Padding(2);
            this.sbGetLog.Name = "sbGetLog";
            this.sbGetLog.Size = new System.Drawing.Size(56, 19);
            this.sbGetLog.TabIndex = 0;
            this.sbGetLog.Text = "Get Log";
            this.sbGetLog.Click += new System.EventHandler(this.sbGetLog_Click);
            // 
            // deviceConfigBindingSource
            // 
            this.deviceConfigBindingSource.DataSource = typeof(Jaxis.Engine.Base.Device.DeviceConfig);
            // 
            // tbError
            // 
            this.tbError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbError.Location = new System.Drawing.Point(7, 620);
            this.tbError.Margin = new System.Windows.Forms.Padding(2);
            this.tbError.Name = "tbError";
            this.tbError.Size = new System.Drawing.Size(686, 20);
            this.tbError.TabIndex = 1;
            // 
            // lLastError
            // 
            this.lLastError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lLastError.AutoSize = true;
            this.lLastError.Location = new System.Drawing.Point(7, 602);
            this.lLastError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lLastError.Name = "lLastError";
            this.lLastError.Size = new System.Drawing.Size(52, 13);
            this.lLastError.TabIndex = 2;
            this.lLastError.Text = "Last Error";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Title = "Select PlugIn File";
            // 
            // ConfigUtilForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 644);
            this.Controls.Add(this.lLastError);
            this.Controls.Add(this.tbError);
            this.Controls.Add(this.xtraTabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ConfigUtilForm";
            this.Text = "Jaxis Engine Config Util";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tEngine.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tEngineState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tEngineVersion.Properties)).EndInit();
            this.tDevices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProducerMessageType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDeviceFilterOptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssemblyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssemblyType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssemblyVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbFilters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsumerMessageType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbDevices)).EndInit();
            this.tFilters.ResumeLayout(false);
            this.tFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFilterOptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFilterOptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tFilterVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tFilterType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tFilterName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tFilterAssemblyType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tFilterAssemblyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbFiltersFilters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbFilterDevices)).EndInit();
            this.tPlugIns.ResumeLayout(false);
            this.tPlugIns.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbDependents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tPlugIn.Properties)).EndInit();
            this.tLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deviceConfigBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Windows Form Designer generated code

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tDevices;
        private DevExpress.XtraTab.XtraTabPage tLog;
        private DevExpress.XtraEditors.ListBoxControl lbDevices;
        private DevExpress.XtraEditors.SimpleButton sbGetDevices;
        private System.Windows.Forms.TextBox tbError;
        private DevExpress.XtraEditors.MemoEdit mLog;
        private DevExpress.XtraEditors.SimpleButton sbGetLog;
        private DevExpress.XtraEditors.TextEdit txtType;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraEditors.TextEdit txtAssemblyType;
        private DevExpress.XtraEditors.TextEdit txtAssemblyName;
        private DevExpress.XtraEditors.TextEdit txtConsumerMessageType;
        private System.Windows.Forms.Label lLastError;
        private DevExpress.XtraTab.XtraTabPage tPlugIns;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraTab.XtraTabPage tEngine;
        private DevExpress.XtraEditors.SimpleButton sbStartEngine;
        private DevExpress.XtraEditors.SimpleButton sbStopEngine;
        private DevExpress.XtraEditors.TextEdit tEngineState;
        private DevExpress.XtraEditors.TextEdit tEngineVersion;
        private System.Windows.Forms.BindingSource deviceConfigBindingSource;
        private DevExpress.XtraEditors.SimpleButton sbGetEngineState;
        private DevExpress.XtraEditors.SimpleButton sbGetEngineVersion;
        private DevExpress.XtraEditors.TextEdit txtAssemblyVersion;
        private DevExpress.XtraTab.XtraTabPage tFilters;
        private DevExpress.XtraEditors.ListBoxControl lbFilters;
        private DevExpress.XtraEditors.ListBoxControl lbFiltersFilters;
        private DevExpress.XtraEditors.LabelControl lcFiltersFilterOptions;
        private DevExpress.XtraEditors.LabelControl lcFiltersFilters;
        private DevExpress.XtraEditors.LabelControl lcDevices;
        private DevExpress.XtraEditors.ListBoxControl lbFilterDevices;
        private DevExpress.XtraEditors.TextEdit tFilterType;
        private DevExpress.XtraEditors.TextEdit tFilterName;
        private DevExpress.XtraEditors.LabelControl lcFilterType;
        private DevExpress.XtraEditors.LabelControl lcFilterName;
        private DevExpress.XtraEditors.TextEdit tFilterAssemblyType;
        private DevExpress.XtraEditors.TextEdit tFilterAssemblyName;
        private DevExpress.XtraEditors.LabelControl lcFilterAssemblyType;
        private DevExpress.XtraEditors.LabelControl lcFilterAssemblyName;
        private DevExpress.XtraEditors.TextEdit tFilterVersion;
        private DevExpress.XtraEditors.LabelControl lcFilterVersion;
        private DevExpress.XtraEditors.SimpleButton sbUpdateFilterOptions;
        private DevExpress.XtraEditors.SimpleButton sbUpdateOptions;
        private DevExpress.XtraGrid.GridControl gridControlOptions;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOptions;
        private DevExpress.XtraGrid.GridControl gridControlFilterOptions;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFilterOptions;
        private DevExpress.XtraGrid.GridControl gridControlDeviceFilterOptions;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.TextEdit txtProducerMessageType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraEditors.ComboBoxEdit cmbState;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.SimpleButton sbRemove;
        private DevExpress.XtraEditors.ListBoxControl lbDependents;
        private DevExpress.XtraEditors.LabelControl lcDependents;
        private DevExpress.XtraEditors.SimpleButton sbDependSelectFile;
        private DevExpress.XtraEditors.LabelControl lcPlugInFile;
        private DevExpress.XtraEditors.SimpleButton sbSelectFile;
        private DevExpress.XtraEditors.TextEdit tPlugIn;
        private DevExpress.XtraEditors.SimpleButton sbPushPlugIn;
        private DevExpress.XtraEditors.SimpleButton abAddDevice;
        private DevExpress.XtraEditors.SimpleButton sbAddFilter;
        private DevExpress.XtraEditors.SimpleButton sbDeviceAddFilter;
    }
}