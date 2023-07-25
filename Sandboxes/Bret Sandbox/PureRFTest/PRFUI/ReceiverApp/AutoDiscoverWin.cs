namespace ReceiverApp
{
    using PureRF;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AutoDiscoverWin : Form
    {
        private Button btnAddReceivers;
        private Button btnClose;
        private Button btnStart;
        private Button btnStop;
        private ColumnHeader columnLoop;
        private ColumnHeader columnLoopAlias;
        private ColumnHeader columnName;
        private ColumnHeader columnPort;
        private ColumnHeader columnUnitID;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private ToolStripStatusLabel lblStatus;
        private ListView listLoops;
        private ListView listReceivers;
        private ReceiversManager mParentReceiversManager;
        private ReceiversManager mReceiversManager;
        private NumericUpDown numEndUnitID;
        private NumericUpDown numStartUnitID;
        private ToolStripProgressBar statusProgBar;
        private StatusStrip statusStrip1;

        public AutoDiscoverWin(ReceiversManager _ReceiversManager)
        {
            this.mParentReceiversManager = _ReceiversManager;
            this.mReceiversManager = new ReceiversManager(300);
            this.mReceiversManager.SetEventCallback(new ReceiversManager.EventCallback(this.ReceiversManagerEvent), this);
            this.InitializeComponent();
            foreach (ReceiversManager.Loop loop in this.mParentReceiversManager.LoopsList)
            {
                this.mReceiversManager.AddLoop(loop, false);
                loop.Conn.SetTimeout(300);
            }
        }

        private void AutoDiscoverWin_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void AutoDiscoverWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.mReceiversManager.AbortRequest();
            this.mReceiversManager.WaitForRequestCompletion();
            foreach (ReceiversManager.Loop loop in this.mParentReceiversManager.LoopsList)
            {
                loop.Conn.SetTimeout(0x5dc);
            }
        }

        private void AutoDiscoverWin_Load(object sender, EventArgs e)
        {
        }

        private void AutoDiscoverWin_Shown(object sender, EventArgs e)
        {
            foreach (ReceiversManager.Loop loop in this.mParentReceiversManager.LoopsList)
            {
                ListViewItem item = new ListViewItem(new string[] { loop.Name, loop.Conn.Name });
                item.Checked = true;
                this.listLoops.Items.Add(item);
            }
        }

        private void btnAddReceivers_Click(object sender, EventArgs e)
        {
            ArrayList list = new ArrayList();
            while (this.listReceivers.CheckedItems.Count > 0)
            {
                ListViewItem.ListViewSubItemCollection subItems = this.listReceivers.CheckedItems[0].SubItems;
                ReceiversManager.RetVal val = this.mParentReceiversManager.AddReceiver(subItems[0].Text, subItems[2].Text, Convert.ToByte(subItems[1].Text));
                if (val != ReceiversManager.RetVal.SUCCESS)
                {
                    MessageBox.Show(string.Format("Unable to add receiver {0}: {1}", subItems[0].Text, val), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    list.Add(this.listReceivers.CheckedItems[0]);
                    this.listReceivers.CheckedItems[0].Checked = false;
                }
                else
                {
                    this.listReceivers.Items.RemoveAt(this.listReceivers.CheckedIndices[0]);
                }
            }
            foreach (ListViewItem item in list)
            {
                item.Checked = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.mReceiversManager.AbortRequest();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (this.listLoops.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one loop", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (this.numStartUnitID.Value > this.numEndUnitID.Value)
            {
                MessageBox.Show("Start unit id needs to be smaller than end unit id", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.mReceiversManager.ClearReceivers();
                this.listReceivers.Items.Clear();
                foreach (ReceiversManager.Loop loop in this.mParentReceiversManager.LoopsList)
                {
                    byte num2 = (byte) this.numStartUnitID.Value;
                    byte num3 = (byte) this.numEndUnitID.Value;
                    for (int j = num2; j <= num3; j++)
                    {
                        string name = loop.Name + "-" + j.ToString();
                        this.mReceiversManager.AddReceiver(name, loop.Name, (byte) j);
                    }
                }
                string[] receiverNames = new string[this.mReceiversManager.ReceiversList.Count];
                for (int i = 0; i < receiverNames.Length; i++)
                {
                    ReceiversManager.ManagedReceiver receiver = this.mReceiversManager.ReceiversList[i] as ReceiversManager.ManagedReceiver;
                    receiverNames[i] = receiver.Name;
                }
                ReceiversManager.RetVal unitInfo = this.mReceiversManager.GetUnitInfo(receiverNames);
                if (unitInfo != ReceiversManager.RetVal.SUCCESS)
                {
                    MessageBox.Show("Error starting request: " + unitInfo.ToString(), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    this.lblStatus.Text = "Starting...";
                    this.btnAddReceivers.Enabled = false;
                    this.btnStart.Enabled = false;
                    this.btnStop.Enabled = true;
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.mReceiversManager.AbortRequest();
            this.btnStop.Enabled = false;
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
            this.btnClose = new Button();
            this.listLoops = new ListView();
            this.columnLoopAlias = new ColumnHeader();
            this.columnPort = new ColumnHeader();
            this.label1 = new Label();
            this.numStartUnitID = new NumericUpDown();
            this.numEndUnitID = new NumericUpDown();
            this.label2 = new Label();
            this.statusStrip1 = new StatusStrip();
            this.statusProgBar = new ToolStripProgressBar();
            this.lblStatus = new ToolStripStatusLabel();
            this.groupBox1 = new GroupBox();
            this.groupBox2 = new GroupBox();
            this.btnAddReceivers = new Button();
            this.listReceivers = new ListView();
            this.columnName = new ColumnHeader();
            this.columnUnitID = new ColumnHeader();
            this.columnLoop = new ColumnHeader();
            this.btnStart = new Button();
            this.btnStop = new Button();
            this.numStartUnitID.BeginInit();
            this.numEndUnitID.BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.btnClose.DialogResult = DialogResult.OK;
            this.btnClose.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnClose.Location = new Point(0x1ef, 220);
            this.btnClose.Margin = new Padding(2, 2, 2, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x56, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.listLoops.CheckBoxes = true;
            this.listLoops.Columns.AddRange(new ColumnHeader[] { this.columnLoopAlias, this.columnPort });
            this.listLoops.Location = new Point(4, 0x11);
            this.listLoops.Margin = new Padding(2, 2, 2, 2);
            this.listLoops.Name = "listLoops";
            this.listLoops.Size = new Size(0xa6, 0x8a);
            this.listLoops.TabIndex = 1;
            this.listLoops.UseCompatibleStateImageBehavior = false;
            this.listLoops.View = View.Details;
            this.columnLoopAlias.Text = "Loop Alias";
            this.columnLoopAlias.Width = 100;
            this.columnPort.Text = "Port";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(4, 0x9d);
            this.label1.Margin = new Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Start Unit ID:";
            this.numStartUnitID.Location = new Point(0x74, 0x9c);
            this.numStartUnitID.Margin = new Padding(2, 2, 2, 2);
            int[] bits = new int[4];
            bits[0] = 0xff;
            this.numStartUnitID.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 1;
            this.numStartUnitID.Minimum = new decimal(numArray2);
            this.numStartUnitID.Name = "numStartUnitID";
            this.numStartUnitID.Size = new Size(0x36, 20);
            this.numStartUnitID.TabIndex = 3;
            int[] numArray3 = new int[4];
            numArray3[0] = 1;
            this.numStartUnitID.Value = new decimal(numArray3);
            this.numEndUnitID.Location = new Point(0x74, 180);
            this.numEndUnitID.Margin = new Padding(2, 2, 2, 2);
            int[] numArray4 = new int[4];
            numArray4[0] = 0xff;
            this.numEndUnitID.Maximum = new decimal(numArray4);
            int[] numArray5 = new int[4];
            numArray5[0] = 1;
            this.numEndUnitID.Minimum = new decimal(numArray5);
            this.numEndUnitID.Name = "numEndUnitID";
            this.numEndUnitID.Size = new Size(0x36, 20);
            this.numEndUnitID.TabIndex = 4;
            int[] numArray6 = new int[4];
            numArray6[0] = 0xff;
            this.numEndUnitID.Value = new decimal(numArray6);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(4, 0xb5);
            this.label2.Margin = new Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "End Unit ID:";
            this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.statusProgBar, this.lblStatus });
            this.statusStrip1.Location = new Point(0, 0xff);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new Size(590, 0x16);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            this.statusProgBar.Margin = new Padding(3, 3, 1, 3);
            this.statusProgBar.Name = "statusProgBar";
            this.statusProgBar.Size = new Size(0x4b, 0x10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(0x2a, 0x11);
            this.lblStatus.Text = "Ready.";
            this.groupBox1.Controls.Add(this.listLoops);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numStartUnitID);
            this.groupBox1.Controls.Add(this.numEndUnitID);
            this.groupBox1.Location = new Point(9, 10);
            this.groupBox1.Margin = new Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(2, 2, 2, 2);
            this.groupBox1.Size = new Size(0xae, 0xce);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auto-discovery parameters";
            this.groupBox2.Controls.Add(this.btnAddReceivers);
            this.groupBox2.Controls.Add(this.listReceivers);
            this.groupBox2.Location = new Point(0xbb, 11);
            this.groupBox2.Margin = new Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new Padding(2, 2, 2, 2);
            this.groupBox2.Size = new Size(0x18a, 0xcd);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Discovered receivers";
            this.btnAddReceivers.Enabled = false;
            this.btnAddReceivers.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnAddReceivers.Location = new Point(0x113, 0xad);
            this.btnAddReceivers.Margin = new Padding(2, 2, 2, 2);
            this.btnAddReceivers.Name = "btnAddReceivers";
            this.btnAddReceivers.Size = new Size(0x73, 0x1c);
            this.btnAddReceivers.TabIndex = 1;
            this.btnAddReceivers.Text = "Add receivers";
            this.btnAddReceivers.UseVisualStyleBackColor = true;
            this.btnAddReceivers.Click += new EventHandler(this.btnAddReceivers_Click);
            this.listReceivers.CheckBoxes = true;
            this.listReceivers.Columns.AddRange(new ColumnHeader[] { this.columnName, this.columnUnitID, this.columnLoop });
            this.listReceivers.LabelEdit = true;
            this.listReceivers.Location = new Point(3, 0x11);
            this.listReceivers.Margin = new Padding(2, 2, 2, 2);
            this.listReceivers.Name = "listReceivers";
            this.listReceivers.Size = new Size(0x183, 0x98);
            this.listReceivers.TabIndex = 0;
            this.listReceivers.UseCompatibleStateImageBehavior = false;
            this.listReceivers.View = View.Details;
            this.listReceivers.AfterLabelEdit += new LabelEditEventHandler(this.listReceivers_AfterLabelEdit);
            this.columnName.Text = "Communication  Name";
            this.columnName.Width = 150;
            this.columnUnitID.Text = "Unit ID";
            this.columnUnitID.Width = 0x4b;
            this.columnLoop.Text = "Communication Loop";
            this.columnLoop.Width = 150;
            this.btnStart.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnStart.Location = new Point(9, 220);
            this.btnStart.Margin = new Padding(2, 2, 2, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new Size(0x56, 30);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new EventHandler(this.btnStart_Click);
            this.btnStop.Enabled = false;
            this.btnStop.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnStop.Location = new Point(100, 220);
            this.btnStop.Margin = new Padding(2, 2, 2, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new Size(0x56, 30);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new EventHandler(this.btnStop_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(590, 0x115);
            base.Controls.Add(this.btnStop);
            base.Controls.Add(this.btnStart);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.statusStrip1);
            base.Controls.Add(this.btnClose);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Margin = new Padding(2, 2, 2, 2);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "AutoDiscoverWin";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "PureRF Receiver";
            base.Load += new EventHandler(this.AutoDiscoverWin_Load);
            base.Shown += new EventHandler(this.AutoDiscoverWin_Shown);
            base.FormClosed += new FormClosedEventHandler(this.AutoDiscoverWin_FormClosed);
            base.FormClosing += new FormClosingEventHandler(this.AutoDiscoverWin_FormClosing);
            this.numStartUnitID.EndInit();
            this.numEndUnitID.EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listReceivers_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string text = this.listReceivers.Items[e.Item].SubItems[0].Text;
            string label = e.Label;
            if (label == null)
            {
                return;
            }
            if (this.mParentReceiversManager.IsReceiverNameAvailable(label))
            {
                for (int i = 0; i < this.listReceivers.Items.Count; i++)
                {
                    if (this.listReceivers.Items[i].SubItems[0].Text == label)
                    {
                        goto Label_0085;
                    }
                }
                return;
            }
        Label_0085:
            MessageBox.Show("New name already in use", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            e.CancelEdit = true;
        }

        private void ReceiversManagerEvent(ReceiversManager m, ReceiversManager.ProgressEvent e)
        {
            switch (e.EventID)
            {
                case ReceiversManager.ProgressEvent.ID.STARTED:
                    this.lblStatus.Text = "Request started.";
                    return;

                case ReceiversManager.ProgressEvent.ID.COMPLETED:
                    this.statusProgBar.Value = 0;
                    this.lblStatus.Text = "Ready.";
                    this.btnStart.Enabled = true;
                    this.btnAddReceivers.Enabled = true;
                    this.btnStop.Enabled = false;
                    return;

                case ReceiversManager.ProgressEvent.ID.PROGRESS_UPDATE:
                    this.lblStatus.Text = e.EventStr;
                    this.statusProgBar.Value = (int) e.EventArgs["PERCENT_COMPLETE"];
                    return;

                case ReceiversManager.ProgressEvent.ID.BEFORE_REQUEST_NOTIFICATION:
                case ReceiversManager.ProgressEvent.ID.AFTER_REQUEST_NOTIFICATION:
                case ReceiversManager.ProgressEvent.ID.LOG_MSG:
                    break;

                case ReceiversManager.ProgressEvent.ID.RESULT_UPDATE:
                {
                    ReceiverRetVal val = (ReceiverRetVal) e.EventArgs["RETVAL"];
                    ReceiversManager.ManagedReceiver receiver = e.EventArgs["RECEIVER"] as ReceiversManager.ManagedReceiver;
                    if (val == ReceiverRetVal.SUCCESS)
                    {
                        if (this.mParentReceiversManager.IsReceiverInLoop(receiver.Loop.Name, receiver.UnitID))
                        {
                            return;
                        }
                        string name = receiver.Loop.Name + "-" + receiver.UnitID.ToString();
                        if (!this.mParentReceiversManager.IsReceiverNameAvailable(name))
                        {
                            int num = 1;
                            do
                            {
                                name = receiver.Loop.Name + "-" + receiver.UnitID.ToString() + "-" + num.ToString();
                            }
                            while (!this.mParentReceiversManager.IsReceiverNameAvailable(name));
                        }
                        ListViewItem item = new ListViewItem(new string[] { name, receiver.UnitID.ToString(), receiver.Loop.Name });
                        item.Checked = true;
                        this.listReceivers.Items.Add(item);
                        break;
                    }
                    return;
                }
                default:
                    return;
            }
        }
    }
}

