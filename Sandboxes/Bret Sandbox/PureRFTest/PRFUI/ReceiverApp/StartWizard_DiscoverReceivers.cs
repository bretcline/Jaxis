namespace ReceiverApp
{
    using PureRF;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class StartWizard_DiscoverReceivers : Form
    {
        private Button btnBack;
        private Button btnCancel;
        private Button btnNext;
        private Button btnStop;
        private ColumnHeader columnLoop;
        private ColumnHeader columnName;
        private ColumnHeader columnUnitID;
        private IContainer components;
        private GroupBox groupBox1;
        private Label lblStatus;
        public ListView listReceivers;
        private PureRF.ReceiversManager m_ReceiversManager;
        public PureRF.ReceiversManager ReceiversManager;
        private ProgressBar statusProgBar;

        public StartWizard_DiscoverReceivers(PureRF.ReceiversManager rm)
        {
            this.InitializeComponent();
            this.m_ReceiversManager = rm;
            this.m_ReceiversManager.SetEventCallback(new PureRF.ReceiversManager.EventCallback(this.ReceiversManagerEvent), this);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.m_ReceiversManager.ClearReceivers();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.m_ReceiversManager.Clear();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bool flag = false;
            ArrayList list = new ArrayList();
            this.ReceiversManager = this.m_ReceiversManager;
            this.m_ReceiversManager.ClearReceivers();
            while (this.listReceivers.CheckedItems.Count > 0)
            {
                ListViewItem.ListViewSubItemCollection subItems = this.listReceivers.CheckedItems[0].SubItems;
                PureRF.ReceiversManager.RetVal val = this.ReceiversManager.AddReceiver(subItems[0].Text, subItems[2].Text, Convert.ToByte(subItems[1].Text));
                if (val != PureRF.ReceiversManager.RetVal.SUCCESS)
                {
                    MessageBox.Show(string.Format("Unable to add receiver {0}: {1}", subItems[0].Text, val), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.listReceivers.CheckedItems[0].Checked = false;
                }
                else
                {
                    this.listReceivers.Items.RemoveAt(this.listReceivers.CheckedIndices[0]);
                }
            }
            foreach (PureRF.ReceiversManager.Loop loop in this.ReceiversManager.LoopsList)
            {
                if (this.ReceiversManager.NumReceiversInLoop(loop.Name) <= 0)
                {
                    if (!flag)
                    {
                        if (MessageBox.Show("Loop with no receivers found.\nWould you like to remove loops with no receivers?", "PureRF Receiver", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            break;
                        }
                        flag = true;
                    }
                    list.Add(loop.Name);
                }
            }
            while (list.Count > 0)
            {
                string name = (string) list[0];
                this.ReceiversManager.RemoveLoop(name);
                list.RemoveAt(0);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.m_ReceiversManager.AbortRequest();
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
            this.btnBack = new Button();
            this.btnCancel = new Button();
            this.btnNext = new Button();
            this.groupBox1 = new GroupBox();
            this.btnStop = new Button();
            this.lblStatus = new Label();
            this.statusProgBar = new ProgressBar();
            this.listReceivers = new ListView();
            this.columnName = new ColumnHeader();
            this.columnUnitID = new ColumnHeader();
            this.columnLoop = new ColumnHeader();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.btnBack.DialogResult = DialogResult.No;
            this.btnBack.Location = new Point(0x170, 0xd9);
            this.btnBack.Margin = new Padding(2, 2, 2, 2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(0x4f, 0x1c);
            this.btnBack.TabIndex = 10;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new EventHandler(this.btnBack_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(10, 0xd9);
            this.btnCancel.Margin = new Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RightToLeft = RightToLeft.Yes;
            this.btnCancel.Size = new Size(0x4f, 0x1d);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnNext.DialogResult = DialogResult.OK;
            this.btnNext.Location = new Point(0x1c3, 0xd9);
            this.btnNext.Margin = new Padding(2, 2, 2, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x4f, 0x1c);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "Next...";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.statusProgBar);
            this.groupBox1.Controls.Add(this.listReceivers);
            this.groupBox1.Location = new Point(10, 11);
            this.groupBox1.Margin = new Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(2, 2, 2, 2);
            this.groupBox1.Size = new Size(0x206, 0xca);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auto-discover Receivers";
            this.btnStop.Location = new Point(0x1b4, 0xa7);
            this.btnStop.Margin = new Padding(2, 2, 2, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new Size(0x4e, 0x1d);
            this.btnStop.TabIndex = 14;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new EventHandler(this.btnStop_Click);
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new Point(5, 0xa7);
            this.lblStatus.Margin = new Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(0x7a, 13);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Scanning for receivers...";
            this.statusProgBar.Location = new Point(4, 0x8f);
            this.statusProgBar.Margin = new Padding(2, 2, 2, 2);
            this.statusProgBar.Name = "statusProgBar";
            this.statusProgBar.Size = new Size(510, 0x13);
            this.statusProgBar.TabIndex = 12;
            this.listReceivers.CheckBoxes = true;
            this.listReceivers.Columns.AddRange(new ColumnHeader[] { this.columnName, this.columnUnitID, this.columnLoop });
            this.listReceivers.LabelEdit = true;
            this.listReceivers.Location = new Point(4, 0x11);
            this.listReceivers.Margin = new Padding(2, 2, 2, 2);
            this.listReceivers.Name = "listReceivers";
            this.listReceivers.Size = new Size(510, 0x7a);
            this.listReceivers.TabIndex = 11;
            this.listReceivers.UseCompatibleStateImageBehavior = false;
            this.listReceivers.View = View.Details;
            this.listReceivers.AfterLabelEdit += new LabelEditEventHandler(this.listReceivers_AfterLabelEdit);
            this.columnName.Text = "Communication  Name";
            this.columnName.Width = 200;
            this.columnUnitID.Text = "Unit ID";
            this.columnUnitID.Width = 0x75;
            this.columnLoop.Text = "Communication Loop";
            this.columnLoop.Width = 0xa2;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x21b, 0x101);
            base.Controls.Add(this.btnBack);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Margin = new Padding(2, 2, 2, 2);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "StartWizard_DiscoverReceivers";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "PureRF Receiver";
            base.Load += new EventHandler(this.StartWizard_DiscoverReceivers_Load);
            base.Shown += new EventHandler(this.StartWizard_DiscoverReceivers_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void listReceivers_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string text = this.listReceivers.Items[e.Item].SubItems[0].Text;
            string label = e.Label;
            if (label != null)
            {
                for (int i = 0; i < this.listReceivers.Items.Count; i++)
                {
                    if (this.listReceivers.Items[i].SubItems[0].Text == label)
                    {
                        MessageBox.Show("New name already in use", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        e.CancelEdit = true;
                        return;
                    }
                }
            }
        }

        private void ReceiversManagerEvent(PureRF.ReceiversManager m, PureRF.ReceiversManager.ProgressEvent e)
        {
            switch (e.EventID)
            {
                case PureRF.ReceiversManager.ProgressEvent.ID.STARTED:
                    this.lblStatus.Text = "Request started.";
                    this.listReceivers.Enabled = false;
                    return;

                case PureRF.ReceiversManager.ProgressEvent.ID.COMPLETED:
                    this.statusProgBar.Value = 0;
                    this.lblStatus.Text = string.Format("Completed. Found {0} receivers", this.listReceivers.Items.Count);
                    this.btnNext.Enabled = true;
                    this.btnBack.Enabled = true;
                    this.btnCancel.Enabled = true;
                    this.btnStop.Enabled = false;
                    this.listReceivers.Enabled = true;
                    base.ActiveControl = this.btnNext;
                    return;

                case PureRF.ReceiversManager.ProgressEvent.ID.PROGRESS_UPDATE:
                    this.lblStatus.Text = e.EventStr;
                    this.statusProgBar.Value = (int) e.EventArgs["PERCENT_COMPLETE"];
                    return;

                case PureRF.ReceiversManager.ProgressEvent.ID.BEFORE_REQUEST_NOTIFICATION:
                case PureRF.ReceiversManager.ProgressEvent.ID.AFTER_REQUEST_NOTIFICATION:
                case PureRF.ReceiversManager.ProgressEvent.ID.LOG_MSG:
                    break;

                case PureRF.ReceiversManager.ProgressEvent.ID.RESULT_UPDATE:
                {
                    ReceiverRetVal val = (ReceiverRetVal) e.EventArgs["RETVAL"];
                    PureRF.ReceiversManager.ManagedReceiver receiver = e.EventArgs["RECEIVER"] as PureRF.ReceiversManager.ManagedReceiver;
                    if (val == ReceiverRetVal.SUCCESS)
                    {
                        string str = receiver.Loop.Name + "-" + receiver.UnitID.ToString();
                        ListViewItem item = new ListViewItem(new string[] { str, receiver.UnitID.ToString(), receiver.Loop.Name });
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

        private void StartDiscovery()
        {
            int index = 0;
            string[] receiverNames = new string[this.m_ReceiversManager.ReceiversList.Count];
            for (index = 0; index < receiverNames.Length; index++)
            {
                PureRF.ReceiversManager.ManagedReceiver receiver = this.m_ReceiversManager.ReceiversList[index] as PureRF.ReceiversManager.ManagedReceiver;
                receiverNames[index] = receiver.Name;
            }
            PureRF.ReceiversManager.RetVal unitInfo = this.m_ReceiversManager.GetUnitInfo(receiverNames);
            if (unitInfo != PureRF.ReceiversManager.RetVal.SUCCESS)
            {
                MessageBox.Show("Error starting request: " + unitInfo.ToString(), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.m_ReceiversManager.Clear();
                base.DialogResult = DialogResult.No;
            }
            else
            {
                this.lblStatus.Text = "Starting...";
                this.btnNext.Enabled = false;
                this.btnBack.Enabled = false;
                this.btnCancel.Enabled = false;
            }
        }

        private void StartWizard_DiscoverReceivers_Load(object sender, EventArgs e)
        {
        }

        private void StartWizard_DiscoverReceivers_Shown(object sender, EventArgs e)
        {
            this.m_ReceiversManager.ClearReceivers();
            foreach (PureRF.ReceiversManager.Loop loop in this.m_ReceiversManager.LoopsList)
            {
                string name = loop.Name;
                for (byte i = 1; i < 0xff; i = (byte) (i + 1))
                {
                    string str2 = name + "-" + i.ToString();
                    this.m_ReceiversManager.AddReceiver(str2, name, i);
                }
            }
            this.StartDiscovery();
        }
    }
}

