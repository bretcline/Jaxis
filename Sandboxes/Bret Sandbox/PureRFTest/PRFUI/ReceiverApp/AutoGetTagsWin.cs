namespace ReceiverApp
{
    using PureRF;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class AutoGetTagsWin : Form
    {
        private Button btnClear;
        private Button btnClose;
        private Button btnExportCSV;
        private Button btnExportXML;
        private Button btnStopStart;
        private Button btnTagsFilter;
        private IContainer components;
        public DisplayType curDisplayType;
        public bool defaultFilterState;
        private SaveFileDialog dlgSaveCSV;
        private SaveFileDialog dlgSaveXML;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private Label label2;
        private ToolStripStatusLabel lblDBStatus;
        private Label lblMsg1;
        private Label lblMsg2;
        private ToolStripStatusLabel lblSpacer;
        private ToolStripStatusLabel lblStatus;
        private ListView listResults;
        public ManualResetEvent mEventStop;
        public ReceiversManager mReceiversManager;
        private TagsOleDB mTagsDB;
        private bool mUseDB;
        public string[] mWorkSet;
        private NumericUpDown numericUpDown1;
        private StatusStrip statusStrip1;
        private TableLayoutPanel tableLayoutPanel1;
        public Dictionary<uint, bool> tagsFilter;
        public Dictionary<uint, string> tagsFirstSeen;
        public List<ListViewItem> tagsList;
        public Dictionary<uint, int> tagsMsgCount;
        private System.Windows.Forms.Timer timer1;

        public AutoGetTagsWin(DisplayType displayType, ReceiversManager _RecieversManager, string[] workset, TagsOleDB tagsDB, bool useDB)
        {
            this.InitializeComponent();
            this.curDisplayType = displayType;
            this.mReceiversManager = _RecieversManager;
            this.mWorkSet = workset;
            this.mTagsDB = tagsDB;
            this.mUseDB = useDB;
            this.mEventStop = new ManualResetEvent(false);
            this.tagsFilter = new Dictionary<uint, bool>();
            this.tagsList = new List<ListViewItem>();
            this.tagsFirstSeen = new Dictionary<uint, string>();
            this.tagsMsgCount = new Dictionary<uint, int>();
            this.defaultFilterState = true;
            switch (this.curDisplayType)
            {
                case DisplayType.MULTIPLE_LINES_PER_TAG:
                    this.lblMsg1.Text = "Number of messages received:";
                    this.lblMsg2.Text = "0";
                    break;

                case DisplayType.SINGLE_LINE_PER_TAG:
                    this.lblMsg1.Text = "Tags count:";
                    this.lblMsg2.Text = "0";
                    break;
            }
            int num = Convert.ToInt32(this.listResults.Font.SizeInPoints);
            if (DisplayType.SINGLE_LINE_PER_TAG != this.curDisplayType)
            {
                this.listResults.Columns.Add("#", (int) (4 * num));
            }
            this.listResults.Columns.Add("Receiver", (int) (12 * num));
            this.listResults.Columns.Add("Loop", (int) (8 * num));
            this.listResults.Columns.Add("Unit ID", (int) (8 * num));
            this.listResults.Columns.Add("RetVal", (int) (20 * num));
            this.listResults.Columns.Add("Tag ID", (int) (20 * num));
            this.listResults.Columns.Add("Transmission Index", (int) (20 * num));
            this.listResults.Columns.Add("Tag Message", (int) (15 * num));
            this.listResults.Columns.Add("Activator Number", (int) (20 * num));
            this.listResults.Columns.Add("RSSI", (int) (6 * num));
            this.listResults.Columns.Add("Timestamp", (int) (0x19 * num));
            if (DisplayType.SINGLE_LINE_PER_TAG == this.curDisplayType)
            {
                this.listResults.Columns.Add("First Seen", (int) (0x19 * num));
                this.listResults.Columns.Add("Last Updated", (int) (0x19 * num));
                this.listResults.Columns.Add("Message Count", (int) (0x19 * num));
            }
            if ((this.mTagsDB == null) || !this.mTagsDB.IsOpened)
            {
                this.mUseDB = false;
                this.lblDBStatus.Text = "No DB connection";
            }
            else if (this.mUseDB)
            {
                this.lblDBStatus.Text = "DB ENABLED";
            }
            else
            {
                this.lblDBStatus.Text = "DB DISABLED";
            }
        }

        private void AddTagToListView(TagID TagID, ListViewItem item)
        {
            if (!this.tagsFilter.ContainsKey(TagID.GetPureRFTagID()))
            {
                this.tagsFilter.Add(TagID.GetPureRFTagID(), this.defaultFilterState);
            }
            if (this.curDisplayType != DisplayType.SINGLE_LINE_PER_TAG)
            {
                this.tagsList.Add(item);
                if (this.tagsFilter[TagID.GetPureRFTagID()])
                {
                    this.listResults.Items.Add(item);
                }
            }
            else if (!this.tagsFirstSeen.ContainsKey(TagID.GetPureRFTagID()))
            {
                this.tagsFirstSeen.Add(TagID.GetPureRFTagID(), DateTime.Now.ToString());
                this.tagsMsgCount.Add(TagID.GetPureRFTagID(), 1);
                item.SubItems[item.SubItems.Count - 3].Text = this.tagsFirstSeen[TagID.GetPureRFTagID()];
                item.SubItems[item.SubItems.Count - 1].Text = this.tagsMsgCount[TagID.GetPureRFTagID()].ToString();
                this.tagsList.Add(item);
                if (this.tagsFilter[TagID.GetPureRFTagID()])
                {
                    this.listResults.Items.Add(item);
                }
            }
            else
            {
                int index;
                Dictionary<uint, int> dictionary;
                uint num3;
                (dictionary = this.tagsMsgCount)[num3 = TagID.GetPureRFTagID()] = dictionary[num3] + 1;
                item.SubItems[item.SubItems.Count - 3].Text = this.tagsFirstSeen[TagID.GetPureRFTagID()];
                item.SubItems[item.SubItems.Count - 1].Text = this.tagsMsgCount[TagID.GetPureRFTagID()].ToString();
                if (this.tagsFilter[TagID.GetPureRFTagID()])
                {
                    index = -1;
                    foreach (ListViewItem item2 in this.listResults.Items)
                    {
                        TagID tag = item2.Tag as TagID;
                        if (tag.GetPureRFTagID() == TagID.GetPureRFTagID())
                        {
                            index = item2.Index;
                            break;
                        }
                    }
                    if (index != -1)
                    {
                        this.listResults.Items[index] = item;
                    }
                }
                index = -1;
                foreach (ListViewItem item3 in this.tagsList)
                {
                    TagID gid2 = item3.Tag as TagID;
                    if (gid2.GetPureRFTagID() == TagID.GetPureRFTagID())
                    {
                        index = item3.Index;
                        break;
                    }
                }
                if (index != -1)
                {
                    this.tagsList[index] = item;
                }
            }
            if (this.listResults.Items.Count > 0)
            {
                this.listResults.EnsureVisible(this.listResults.Items.Count - 1);
            }
            this.lblMsg2.Text = this.listResults.Items.Count.ToString();
        }

        private void AutoGetTagsWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.StopRequesting();
            this.mReceiversManager.WaitForRequestCompletion();
            this.timer1.Enabled = false;
        }

        private void AutoGetTagsWin_Load(object sender, EventArgs e)
        {
            this.dlgSaveCSV.InitialDirectory = Application.ExecutablePath;
            this.dlgSaveXML.InitialDirectory = Application.ExecutablePath;
        }

        private void AutoGetTagsWin_Shown(object sender, EventArgs e)
        {
            this.mReceiversManager.SetEventCallback(new ReceiversManager.EventCallback(this.ReceiversManagerEvent), this);
            this.mReceiversManager.GetAllTags(this.mWorkSet, true);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.tagsList.Clear();
            this.tagsFirstSeen.Clear();
            this.tagsMsgCount.Clear();
            this.listResults.Items.Clear();
            this.lblMsg2.Text = "0";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.StopRequesting();
            this.btnClose.Enabled = false;
            this.btnStopStart.Enabled = false;
        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            Program.ExportListViewToCSV(this.listResults, this.dlgSaveCSV);
        }

        private void btnExportXML_Click(object sender, EventArgs e)
        {
            Program.ExportListViewToXML(this.listResults, this.dlgSaveXML);
        }

        private void btnStopStart_Click(object sender, EventArgs e)
        {
            if (this.btnStopStart.Text == "Stop")
            {
                this.StopRequesting();
            }
            else if (this.btnStopStart.Text == "Start")
            {
                this.mEventStop.Reset();
                this.mReceiversManager.GetAllTags(this.mWorkSet, true);
            }
            this.btnStopStart.Enabled = false;
        }

        private void btnTagsFilter_Click(object sender, EventArgs e)
        {
            TagsFilterWin win = new TagsFilterWin(this);
            bool flag = this.btnStopStart.Text == "Stop";
            if (flag)
            {
                this.StopRequesting();
            }
            win.ShowDialog();
            this.listResults.Items.Clear();
            foreach (ListViewItem item in this.tagsList)
            {
                TagID tag = item.Tag as TagID;
                if (this.tagsFilter[tag.GetPureRFTagID()])
                {
                    this.listResults.Items.Add(item);
                }
            }
            this.lblMsg2.Text = this.listResults.Items.Count.ToString();
            if (this.listResults.Items.Count >= 1)
            {
                this.listResults.EnsureVisible(this.listResults.Items.Count - 1);
            }
            if (flag)
            {
                this.mEventStop.Reset();
                this.mReceiversManager.GetAllTags(this.mWorkSet, true);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.btnClose = new Button();
            this.listResults = new ListView();
            this.statusStrip1 = new StatusStrip();
            this.lblStatus = new ToolStripStatusLabel();
            this.lblSpacer = new ToolStripStatusLabel();
            this.lblDBStatus = new ToolStripStatusLabel();
            this.btnStopStart = new Button();
            this.btnExportCSV = new Button();
            this.dlgSaveCSV = new SaveFileDialog();
            this.btnExportXML = new Button();
            this.dlgSaveXML = new SaveFileDialog();
            this.btnClear = new Button();
            this.lblMsg1 = new Label();
            this.flowLayoutPanel1 = new FlowLayoutPanel();
            this.lblMsg2 = new Label();
            this.label1 = new Label();
            this.numericUpDown1 = new NumericUpDown();
            this.label2 = new Label();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.btnTagsFilter = new Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.numericUpDown1.BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            base.SuspendLayout();
            this.btnClose.DialogResult = DialogResult.OK;
            this.btnClose.Dock = DockStyle.Fill;
            this.btnClose.Location = new Point(0x24c, 0x148);
            this.btnClose.Margin = new Padding(2);
            this.btnClose.MaximumSize = new Size(0x42, 20);
            this.btnClose.MinimumSize = new Size(0x42, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x42, 20);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.tableLayoutPanel1.SetColumnSpan(this.listResults, 6);
            this.listResults.Dock = DockStyle.Fill;
            this.listResults.Location = new Point(2, 2);
            this.listResults.Margin = new Padding(2);
            this.listResults.Name = "listResults";
            this.listResults.Size = new Size(0x28c, 0x12a);
            this.listResults.TabIndex = 2;
            this.listResults.UseCompatibleStateImageBehavior = false;
            this.listResults.View = View.Details;
            this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.lblStatus, this.lblSpacer, this.lblDBStatus });
            this.statusStrip1.Location = new Point(0, 350);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new Size(0x290, 0x16);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(0x39, 0x11);
            this.lblStatus.Text = "Starting...";
            this.lblSpacer.Name = "lblSpacer";
            this.lblSpacer.Size = new Size(0x1ef, 0x11);
            this.lblSpacer.Spring = true;
            this.lblDBStatus.Name = "lblDBStatus";
            this.lblDBStatus.Size = new Size(0x5d, 0x11);
            this.lblDBStatus.Text = "No DB Connection";
            this.btnStopStart.Dock = DockStyle.Fill;
            this.btnStopStart.Location = new Point(0x206, 0x148);
            this.btnStopStart.Margin = new Padding(2);
            this.btnStopStart.MaximumSize = new Size(0x42, 20);
            this.btnStopStart.MinimumSize = new Size(0x42, 20);
            this.btnStopStart.Name = "btnStopStart";
            this.btnStopStart.Size = new Size(0x42, 20);
            this.btnStopStart.TabIndex = 5;
            this.btnStopStart.Text = "Stop";
            this.btnStopStart.UseVisualStyleBackColor = true;
            this.btnStopStart.Click += new EventHandler(this.btnStopStart_Click);
            this.btnExportCSV.Dock = DockStyle.Fill;
            this.btnExportCSV.Location = new Point(0x1a2, 0x148);
            this.btnExportCSV.Margin = new Padding(2);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new Size(0x60, 20);
            this.btnExportCSV.TabIndex = 6;
            this.btnExportCSV.Text = "Export to CSV";
            this.btnExportCSV.UseVisualStyleBackColor = true;
            this.btnExportCSV.Click += new EventHandler(this.btnExportCSV_Click);
            this.dlgSaveCSV.DefaultExt = "csv";
            this.dlgSaveCSV.Filter = "CSV|*.csv";
            this.dlgSaveCSV.RestoreDirectory = true;
            this.dlgSaveCSV.Title = "Export results to CSV";
            this.btnExportXML.Dock = DockStyle.Fill;
            this.btnExportXML.Location = new Point(0x13e, 0x148);
            this.btnExportXML.Margin = new Padding(2);
            this.btnExportXML.Name = "btnExportXML";
            this.btnExportXML.Size = new Size(0x60, 20);
            this.btnExportXML.TabIndex = 7;
            this.btnExportXML.Text = "Export to XML";
            this.btnExportXML.UseVisualStyleBackColor = true;
            this.btnExportXML.Click += new EventHandler(this.btnExportXML_Click);
            this.dlgSaveXML.DefaultExt = "xml";
            this.dlgSaveXML.Filter = "XML|*.xml";
            this.dlgSaveXML.RestoreDirectory = true;
            this.dlgSaveXML.Title = "Export results to XML";
            this.btnClear.Dock = DockStyle.Left;
            this.btnClear.Location = new Point(2, 0x148);
            this.btnClear.Margin = new Padding(2);
            this.btnClear.MaximumSize = new Size(0x42, 20);
            this.btnClear.MinimumSize = new Size(0x42, 20);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(0x42, 20);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.lblMsg1.AutoSize = true;
            this.lblMsg1.Location = new Point(2, 0);
            this.lblMsg1.Margin = new Padding(2, 0, 2, 0);
            this.lblMsg1.Name = "lblMsg1";
            this.lblMsg1.Size = new Size(0x99, 13);
            this.lblMsg1.TabIndex = 9;
            this.lblMsg1.Text = "Number of messages received:";
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 6);
            this.flowLayoutPanel1.Controls.Add(this.lblMsg1);
            this.flowLayoutPanel1.Controls.Add(this.lblMsg2);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.numericUpDown1);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Dock = DockStyle.Fill;
            this.flowLayoutPanel1.Location = new Point(2, 0x130);
            this.flowLayoutPanel1.Margin = new Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new Size(0x28c, 20);
            this.flowLayoutPanel1.TabIndex = 10;
            this.lblMsg2.AutoSize = true;
            this.lblMsg2.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.lblMsg2.Location = new Point(0x9f, 0);
            this.lblMsg2.Margin = new Padding(2, 0, 2, 0);
            this.lblMsg2.Name = "lblMsg2";
            this.lblMsg2.Size = new Size(14, 13);
            this.lblMsg2.TabIndex = 10;
            this.lblMsg2.Text = "0";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0xb2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4a, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Reqest Delay:";
            this.numericUpDown1.Location = new Point(0x102, 3);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new Size(0x38, 20);
            this.numericUpDown1.TabIndex = 12;
            int[] bits = new int[4];
            bits[0] = 1;
            this.numericUpDown1.Value = new decimal(bits);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(320, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1a, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Sec";
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70f));
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listResults, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnStopStart, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnClear, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnExportCSV, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnExportXML, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnTagsFilter, 1, 2);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Margin = new Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
            this.tableLayoutPanel1.Size = new Size(0x290, 350);
            this.tableLayoutPanel1.TabIndex = 11;
            this.btnTagsFilter.Dock = DockStyle.Fill;
            this.btnTagsFilter.Location = new Point(0xda, 0x148);
            this.btnTagsFilter.Margin = new Padding(2);
            this.btnTagsFilter.Name = "btnTagsFilter";
            this.btnTagsFilter.Size = new Size(0x60, 20);
            this.btnTagsFilter.TabIndex = 11;
            this.btnTagsFilter.Text = "Tags Filter";
            this.btnTagsFilter.UseVisualStyleBackColor = true;
            this.btnTagsFilter.Click += new EventHandler(this.btnTagsFilter_Click);
            this.timer1.Interval = 0x3e8;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x290, 0x174);
            base.Controls.Add(this.tableLayoutPanel1);
            base.Controls.Add(this.statusStrip1);
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.Margin = new Padding(2);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            this.MinimumSize = new Size(0x22d, 0x13b);
            base.Name = "AutoGetTagsWin";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "PureRF Receiver - Auto-Get Tags";
            base.Load += new EventHandler(this.AutoGetTagsWin_Load);
            base.Shown += new EventHandler(this.AutoGetTagsWin_Shown);
            base.FormClosing += new FormClosingEventHandler(this.AutoGetTagsWin_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.numericUpDown1.EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void ReceiversManagerEvent(ReceiversManager m, ReceiversManager.ProgressEvent e)
        {
            switch (e.EventID)
            {
                case ReceiversManager.ProgressEvent.ID.STARTED:
                    this.lblStatus.Text = "Started, getting tags from receivers...";
                    this.btnStopStart.Text = "Stop";
                    this.btnStopStart.Enabled = true;
                    return;

                case ReceiversManager.ProgressEvent.ID.COMPLETED:
                    this.RequestCompleted(e);
                    if (!this.mEventStop.WaitOne(0, true))
                    {
                        if (this.numericUpDown1.Value == 0M)
                        {
                            this.mReceiversManager.GetAllTags(this.mWorkSet, true);
                        }
                        else
                        {
                            this.timer1.Interval = (int) (this.numericUpDown1.Value * 1000M);
                            this.timer1.Enabled = true;
                            this.lblStatus.Text = "Tag Reqest dalaed for " + this.numericUpDown1.Value.ToString() + " sec.  Wating....";
                        }
                        return;
                    }
                    this.lblStatus.Text = "Stopped";
                    this.btnStopStart.Text = "Start";
                    this.btnStopStart.Enabled = true;
                    return;
            }
        }

        private void RequestCompleted(ReceiversManager.ProgressEvent e)
        {
            ReceiversManager.ResultSet set;
            ReceiversManager.RetVal allResults = this.mReceiversManager.GetAllResults(out set);
            if (allResults != ReceiversManager.RetVal.SUCCESS)
            {
                MessageBox.Show("Unable to get results: " + allResults.ToString(), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                foreach (ReceiversManager.ManagedReceiver receiver in set.Keys)
                {
                    ReceiversManager.ReceiverResult result = set[receiver];
                    Receiver.Tag[] tagArray = (Receiver.Tag[]) result.Result;
                    if (result.RetVal != ReceiverRetVal.SUCCESS)
                    {
                        if (result.RetVal == ReceiverRetVal.LOOP_COMM_ERROR)
                        {
                            MessageBox.Show("Connection broken. Error: " + result.RetVal.ToString(), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            base.Close();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < tagArray.Length; i++)
                        {
                            string[] items = new string[] { receiver.Name, receiver.Loop.Name, receiver.UnitID.ToString(), result.RetVal.ToString(), tagArray[i].tagID.ToString(), tagArray[i].transmissionIndex.ToString(), tagArray[i].tagMsg.ToString(), tagArray[i].activatorNum.ToString(), tagArray[i].RSSI.ToString(), tagArray[i].ts.ToString() };
                            if (this.mUseDB)
                            {
                                this.mTagsDB.AddTag(receiver, result.RetVal, tagArray[i]);
                            }
                            if (this.curDisplayType == DisplayType.MULTIPLE_LINES_PER_TAG)
                            {
                                string[] array = new string[items.Length + 1];
                                array[0] = (this.listResults.Items.Count + 1).ToString();
                                items.CopyTo(array, 1);
                                items = array;
                            }
                            if (DisplayType.SINGLE_LINE_PER_TAG == this.curDisplayType)
                            {
                                string[] strArray3 = new string[items.Length + 3];
                                items.CopyTo(strArray3, 0);
                                strArray3[items.Length] = "first seen";
                                strArray3[items.Length + 1] = DateTime.Now.ToString();
                                strArray3[items.Length + 2] = "msg count";
                                items = strArray3;
                            }
                            ListViewItem item = new ListViewItem(items);
                            if (((int) tagArray[i].tagMsg) != 0)
                            {
                                item.ForeColor = Color.Red;
                            }
                            item.Tag = tagArray[i].tagID;
                            this.AddTagToListView(tagArray[i].tagID, item);
                        }
                    }
                }
            }
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void StopRequesting()
        {
            this.mEventStop.Set();
            this.mReceiversManager.AbortRequest();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.mReceiversManager.GetAllTags(this.mWorkSet, true);
        }

        public enum DisplayType
        {
            MULTIPLE_LINES_PER_TAG,
            SINGLE_LINE_PER_TAG
        }
    }
}

