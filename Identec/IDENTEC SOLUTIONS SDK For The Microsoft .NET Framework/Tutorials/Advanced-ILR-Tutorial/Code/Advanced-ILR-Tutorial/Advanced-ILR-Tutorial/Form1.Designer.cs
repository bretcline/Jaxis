namespace Advanced_ILR_Tutorial
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageTags = new System.Windows.Forms.TabPage();
            this.dataGridViewTags = new System.Windows.Forms.DataGridView();
            this.serialNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.signalStrengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.longRangeEnabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.batteryConsumedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataTableTagsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.myTagsDataset = new Advanced_ILR_Tutorial.DataSetTags();
            this.tabPageOptions = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxRxBoost = new System.Windows.Forms.CheckBox();
            this.textBoxCurrentOutput = new System.Windows.Forms.TextBox();
            this.trackBarOutputPower = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownExpectedTags = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownScanRepeats = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxScanBlink = new System.Windows.Forms.CheckBox();
            this.tabPageMessages = new System.Windows.Forms.TabPage();
            this.listBoxMessages = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarWorking = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelCardInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCardSerialNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelTagCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonStartScan = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonMultiBlink = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBoxBlinkCount = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tabControl1.SuspendLayout();
            this.tabPageTags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableTagsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myTagsDataset)).BeginInit();
            this.tabPageOptions.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOutputPower)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExpectedTags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScanRepeats)).BeginInit();
            this.tabPageMessages.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageTags);
            this.tabControl1.Controls.Add(this.tabPageOptions);
            this.tabControl1.Controls.Add(this.tabPageMessages);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(710, 400);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageTags
            // 
            this.tabPageTags.Controls.Add(this.dataGridViewTags);
            this.tabPageTags.Location = new System.Drawing.Point(4, 22);
            this.tabPageTags.Name = "tabPageTags";
            this.tabPageTags.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTags.Size = new System.Drawing.Size(702, 374);
            this.tabPageTags.TabIndex = 0;
            this.tabPageTags.Text = "Tags";
            this.tabPageTags.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTags
            // 
            this.dataGridViewTags.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Blue;
            this.dataGridViewTags.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTags.AutoGenerateColumns = false;
            this.dataGridViewTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTags.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serialNumberDataGridViewTextBoxColumn,
            this.signalStrengthDataGridViewTextBoxColumn,
            this.modelDataGridViewTextBoxColumn,
            this.longRangeEnabledDataGridViewCheckBoxColumn,
            this.batteryConsumedDataGridViewTextBoxColumn});
            this.dataGridViewTags.DataSource = this.dataTableTagsBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTags.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTags.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewTags.Name = "dataGridViewTags";
            this.dataGridViewTags.Size = new System.Drawing.Size(696, 368);
            this.dataGridViewTags.TabIndex = 0;
            this.dataGridViewTags.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTags_CellEndEdit);
            // 
            // serialNumberDataGridViewTextBoxColumn
            // 
            this.serialNumberDataGridViewTextBoxColumn.DataPropertyName = "SerialNumber";
            this.serialNumberDataGridViewTextBoxColumn.HeaderText = "Tag";
            this.serialNumberDataGridViewTextBoxColumn.Name = "serialNumberDataGridViewTextBoxColumn";
            // 
            // signalStrengthDataGridViewTextBoxColumn
            // 
            this.signalStrengthDataGridViewTextBoxColumn.DataPropertyName = "SignalStrength";
            this.signalStrengthDataGridViewTextBoxColumn.HeaderText = "Signal";
            this.signalStrengthDataGridViewTextBoxColumn.Name = "signalStrengthDataGridViewTextBoxColumn";
            // 
            // modelDataGridViewTextBoxColumn
            // 
            this.modelDataGridViewTextBoxColumn.DataPropertyName = "Model";
            this.modelDataGridViewTextBoxColumn.HeaderText = "Model";
            this.modelDataGridViewTextBoxColumn.Name = "modelDataGridViewTextBoxColumn";
            // 
            // longRangeEnabledDataGridViewCheckBoxColumn
            // 
            this.longRangeEnabledDataGridViewCheckBoxColumn.DataPropertyName = "LongRangeEnabled";
            this.longRangeEnabledDataGridViewCheckBoxColumn.HeaderText = "Long Range";
            this.longRangeEnabledDataGridViewCheckBoxColumn.Name = "longRangeEnabledDataGridViewCheckBoxColumn";
            this.longRangeEnabledDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.longRangeEnabledDataGridViewCheckBoxColumn.ThreeState = true;
            // 
            // batteryConsumedDataGridViewTextBoxColumn
            // 
            this.batteryConsumedDataGridViewTextBoxColumn.DataPropertyName = "BatteryConsumed";
            this.batteryConsumedDataGridViewTextBoxColumn.HeaderText = "Battery Consumed";
            this.batteryConsumedDataGridViewTextBoxColumn.Name = "batteryConsumedDataGridViewTextBoxColumn";
            // 
            // dataTableTagsBindingSource
            // 
            this.dataTableTagsBindingSource.DataMember = "DataTableTags";
            this.dataTableTagsBindingSource.DataSource = this.myTagsDataset;
            // 
            // myTagsDataset
            // 
            this.myTagsDataset.DataSetName = "DataSetTags";
            this.myTagsDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabPageOptions
            // 
            this.tabPageOptions.Controls.Add(this.groupBox2);
            this.tabPageOptions.Controls.Add(this.groupBox1);
            this.tabPageOptions.Location = new System.Drawing.Point(4, 22);
            this.tabPageOptions.Name = "tabPageOptions";
            this.tabPageOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOptions.Size = new System.Drawing.Size(702, 374);
            this.tabPageOptions.TabIndex = 1;
            this.tabPageOptions.Text = "Options";
            this.tabPageOptions.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxRxBoost);
            this.groupBox2.Controls.Add(this.textBoxCurrentOutput);
            this.groupBox2.Controls.Add(this.trackBarOutputPower);
            this.groupBox2.Location = new System.Drawing.Point(245, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 116);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RF Settings";
            // 
            // checkBoxRxBoost
            // 
            this.checkBoxRxBoost.AutoSize = true;
            this.checkBoxRxBoost.Location = new System.Drawing.Point(53, 93);
            this.checkBoxRxBoost.Name = "checkBoxRxBoost";
            this.checkBoxRxBoost.Size = new System.Drawing.Size(69, 17);
            this.checkBoxRxBoost.TabIndex = 2;
            this.checkBoxRxBoost.Text = "Rx Boost";
            this.checkBoxRxBoost.UseVisualStyleBackColor = true;
            this.checkBoxRxBoost.CheckedChanged += new System.EventHandler(this.checkBoxRxBoost_CheckedChanged);
            // 
            // textBoxCurrentOutput
            // 
            this.textBoxCurrentOutput.Location = new System.Drawing.Point(42, 67);
            this.textBoxCurrentOutput.Name = "textBoxCurrentOutput";
            this.textBoxCurrentOutput.ReadOnly = true;
            this.textBoxCurrentOutput.Size = new System.Drawing.Size(100, 20);
            this.textBoxCurrentOutput.TabIndex = 1;
            // 
            // trackBarOutputPower
            // 
            this.trackBarOutputPower.Location = new System.Drawing.Point(6, 19);
            this.trackBarOutputPower.Maximum = 6;
            this.trackBarOutputPower.Minimum = -5;
            this.trackBarOutputPower.Name = "trackBarOutputPower";
            this.trackBarOutputPower.Size = new System.Drawing.Size(188, 42);
            this.trackBarOutputPower.TabIndex = 0;
            this.trackBarOutputPower.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarOutputPower.ValueChanged += new System.EventHandler(this.trackBarOutputPower_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownExpectedTags);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDownScanRepeats);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBoxScanBlink);
            this.groupBox1.Location = new System.Drawing.Point(8, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 116);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scanning Options";
            // 
            // numericUpDownExpectedTags
            // 
            this.numericUpDownExpectedTags.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownExpectedTags.Location = new System.Drawing.Point(98, 73);
            this.numericUpDownExpectedTags.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.numericUpDownExpectedTags.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownExpectedTags.Name = "numericUpDownExpectedTags";
            this.numericUpDownExpectedTags.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownExpectedTags.TabIndex = 4;
            this.numericUpDownExpectedTags.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Expected Tags:";
            // 
            // numericUpDownScanRepeats
            // 
            this.numericUpDownScanRepeats.Location = new System.Drawing.Point(19, 73);
            this.numericUpDownScanRepeats.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownScanRepeats.Name = "numericUpDownScanRepeats";
            this.numericUpDownScanRepeats.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownScanRepeats.TabIndex = 2;
            this.numericUpDownScanRepeats.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Repeats:";
            // 
            // checkBoxScanBlink
            // 
            this.checkBoxScanBlink.AutoSize = true;
            this.checkBoxScanBlink.Location = new System.Drawing.Point(16, 29);
            this.checkBoxScanBlink.Name = "checkBoxScanBlink";
            this.checkBoxScanBlink.Size = new System.Drawing.Size(49, 17);
            this.checkBoxScanBlink.TabIndex = 0;
            this.checkBoxScanBlink.Text = "Blink";
            this.checkBoxScanBlink.UseVisualStyleBackColor = true;
            // 
            // tabPageMessages
            // 
            this.tabPageMessages.Controls.Add(this.listBoxMessages);
            this.tabPageMessages.Location = new System.Drawing.Point(4, 22);
            this.tabPageMessages.Name = "tabPageMessages";
            this.tabPageMessages.Size = new System.Drawing.Size(702, 374);
            this.tabPageMessages.TabIndex = 2;
            this.tabPageMessages.Text = "Messages";
            this.tabPageMessages.UseVisualStyleBackColor = true;
            // 
            // listBoxMessages
            // 
            this.listBoxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMessages.FormattingEnabled = true;
            this.listBoxMessages.Location = new System.Drawing.Point(0, 0);
            this.listBoxMessages.Name = "listBoxMessages";
            this.listBoxMessages.Size = new System.Drawing.Size(702, 368);
            this.listBoxMessages.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelAction,
            this.toolStripProgressBarWorking,
            this.toolStripStatusLabelCardInfo,
            this.toolStripStatusLabelCardSerialNumber,
            this.toolStripStatusLabelTagCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(710, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelAction
            // 
            this.toolStripStatusLabelAction.Name = "toolStripStatusLabelAction";
            this.toolStripStatusLabelAction.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.toolStripStatusLabelAction.Size = new System.Drawing.Size(48, 17);
            this.toolStripStatusLabelAction.Text = "Ready";
            // 
            // toolStripProgressBarWorking
            // 
            this.toolStripProgressBarWorking.Name = "toolStripProgressBarWorking";
            this.toolStripProgressBarWorking.Size = new System.Drawing.Size(120, 16);
            // 
            // toolStripStatusLabelCardInfo
            // 
            this.toolStripStatusLabelCardInfo.Name = "toolStripStatusLabelCardInfo";
            this.toolStripStatusLabelCardInfo.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.toolStripStatusLabelCardInfo.Size = new System.Drawing.Size(94, 17);
            this.toolStripStatusLabelCardInfo.Text = "Card Information";
            // 
            // toolStripStatusLabelCardSerialNumber
            // 
            this.toolStripStatusLabelCardSerialNumber.Name = "toolStripStatusLabelCardSerialNumber";
            this.toolStripStatusLabelCardSerialNumber.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.toolStripStatusLabelCardSerialNumber.Size = new System.Drawing.Size(84, 17);
            this.toolStripStatusLabelCardSerialNumber.Text = "Card Firmware";
            // 
            // toolStripStatusLabelTagCount
            // 
            this.toolStripStatusLabelTagCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelTagCount.Name = "toolStripStatusLabelTagCount";
            this.toolStripStatusLabelTagCount.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.toolStripStatusLabelTagCount.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabelTagCount.Text = "0 Tags";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tabControl1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(710, 400);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(710, 471);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(710, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearTagsToolStripMenuItem,
            this.clearMessagesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // clearTagsToolStripMenuItem
            // 
            this.clearTagsToolStripMenuItem.Name = "clearTagsToolStripMenuItem";
            this.clearTagsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearTagsToolStripMenuItem.Text = "&Clear Tags";
            this.clearTagsToolStripMenuItem.Click += new System.EventHandler(this.clearTagsToolStripMenuItem_Click);
            // 
            // clearMessagesToolStripMenuItem
            // 
            this.clearMessagesToolStripMenuItem.Name = "clearMessagesToolStripMenuItem";
            this.clearMessagesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearMessagesToolStripMenuItem.Text = "Clear &Messages";
            this.clearMessagesToolStripMenuItem.Click += new System.EventHandler(this.clearMessagesToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonStartScan,
            this.toolStripSeparator1,
            this.toolStripButtonMultiBlink,
            this.toolStripTextBoxBlinkCount,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(194, 25);
            this.toolStrip1.TabIndex = 1;
            // 
            // toolStripButtonStartScan
            // 
            this.toolStripButtonStartScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonStartScan.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStartScan.Image")));
            this.toolStripButtonStartScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStartScan.Name = "toolStripButtonStartScan";
            this.toolStripButtonStartScan.Size = new System.Drawing.Size(39, 22);
            this.toolStripButtonStartScan.Text = "Scan!";
            this.toolStripButtonStartScan.Click += new System.EventHandler(this.toolStripButtonStartScan_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonMultiBlink
            // 
            this.toolStripButtonMultiBlink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonMultiBlink.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMultiBlink.Image")));
            this.toolStripButtonMultiBlink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMultiBlink.Name = "toolStripButtonMultiBlink";
            this.toolStripButtonMultiBlink.Size = new System.Drawing.Size(56, 22);
            this.toolStripButtonMultiBlink.Text = "Blink Tag";
            this.toolStripButtonMultiBlink.Click += new System.EventHandler(this.toolStripButtonMultiBlink_Click);
            // 
            // toolStripTextBoxBlinkCount
            // 
            this.toolStripTextBoxBlinkCount.Name = "toolStripTextBoxBlinkCount";
            this.toolStripTextBoxBlinkCount.Size = new System.Drawing.Size(50, 25);
            this.toolStripTextBoxBlinkCount.Text = "10";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(31, 22);
            this.toolStripLabel1.Text = "times";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 471);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "MainForm";
            this.Text = "Advanced ILR Tutorial";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageTags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableTagsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myTagsDataset)).EndInit();
            this.tabPageOptions.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOutputPower)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExpectedTags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScanRepeats)).EndInit();
            this.tabPageMessages.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageTags;
        private System.Windows.Forms.TabPage tabPageOptions;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonStartScan;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAction;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCardSerialNumber;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCardInfo;
        private System.Windows.Forms.DataGridView dataGridViewTags;
        private System.Windows.Forms.BindingSource dataTableTagsBindingSource;
        private DataSetTags myTagsDataset;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearTagsToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxScanBlink;
        private System.Windows.Forms.NumericUpDown numericUpDownExpectedTags;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownScanRepeats;
        private System.Windows.Forms.TabPage tabPageMessages;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarWorking;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTagCount;
        private System.Windows.Forms.ListBox listBoxMessages;
        private System.Windows.Forms.ToolStripMenuItem clearMessagesToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar trackBarOutputPower;
        private System.Windows.Forms.TextBox textBoxCurrentOutput;
        private System.Windows.Forms.CheckBox checkBoxRxBoost;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn signalStrengthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn longRangeEnabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn batteryConsumedDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonMultiBlink;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxBlinkCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;


    }
}

