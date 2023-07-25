namespace ReceiverApp
{
    using PureRF;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ManageLoopsWin : Form
    {
        private Button btnAddRing;
        private Button btnClose;
        private Button btnRemoveSelected;
        private CheckBox checkAutoName;
        private ColumnHeader columnLoopAlias;
        private ColumnHeader columnNumReceivers;
        private ColumnHeader columnPort;
        public ComboBox comboLoopPort;
        private IContainer components;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private ListView listLoops;
        private ArrayList mPorts;
        public ReceiversManager mReceiversManager;
        private TextBox txtLoopAlias;

        public ManageLoopsWin(ReceiversManager _ReceiversManager)
        {
            SortedList<string, string> list;
            this.mReceiversManager = _ReceiversManager;
            this.mPorts = new ArrayList();
            this.InitializeComponent();
            if (!PortsEnumerator.EnumeratePorts(out list))
            {
                MessageBox.Show("Unable to enumerate serial ports!", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                base.DialogResult = DialogResult.Cancel;
            }
            else
            {
                foreach (string str in list.Keys)
                {
                    this.comboLoopPort.Items.Add(string.Format("{0} ({1})", str, list[str]));
                    this.mPorts.Add(str);
                }
                this.comboLoopPort.Items.Add("IP...");
            }
        }

        private void btnAddLoop_Click(object sender, EventArgs e)
        {
            ListViewItem item;
            if (this.comboLoopPort.SelectedIndex == (this.comboLoopPort.Items.Count - 1))
            {
                IPLoopWin win = new IPLoopWin(this.mReceiversManager);
                if (win.ShowDialog() != DialogResult.Cancel)
                {
                    item = new ListViewItem(new string[] { win.LoopAlias, string.Format("{0}:{1}", win.LoopHost, win.LoopPort), "0" });
                    this.listLoops.Items.Add(item);
                }
            }
            else if ((this.txtLoopAlias.Text == "") || (this.comboLoopPort.SelectedIndex == -1))
            {
                MessageBox.Show("Please fill in both fields!", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!this.mReceiversManager.IsLoopNameAvailable(this.txtLoopAlias.Text))
            {
                MessageBox.Show("Loop name already in use!", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ReceiversManager.RetVal val = this.mReceiversManager.AddSerialLoop(this.txtLoopAlias.Text, (string) this.mPorts[this.comboLoopPort.SelectedIndex], 0xe100);
                if (val != ReceiversManager.RetVal.SUCCESS)
                {
                    MessageBox.Show("Error adding loop: " + val.ToString(), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    item = new ListViewItem(new string[] { this.txtLoopAlias.Text, (string) this.mPorts[this.comboLoopPort.SelectedIndex], "0" });
                    this.listLoops.Items.Add(item);
                }
            }
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            bool flag = false;
            for (int i = 0; i < this.listLoops.CheckedItems.Count; i++)
            {
                if (this.mReceiversManager.IsLoopInUse(this.listLoops.CheckedItems[i].SubItems[0].Text))
                {
                    flag = true;
                    break;
                }
            }
            if (!flag || (MessageBox.Show("At least one of the selected loops contains receivers." + Environment.NewLine + "Do you wish to proceed?", "PureRF Receiver", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No))
            {
                while (this.listLoops.CheckedItems.Count > 0)
                {
                    this.mReceiversManager.RemoveLoop(this.listLoops.CheckedItems[0].SubItems[0].Text);
                    this.listLoops.Items.RemoveAt(this.listLoops.CheckedIndices[0]);
                }
            }
        }

        private void checkAutoName_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkAutoName.Checked)
            {
                if (this.comboLoopPort.SelectedIndex != -1)
                {
                    this.txtLoopAlias.Text = this.comboLoopPort.SelectedItem.ToString();
                }
                this.txtLoopAlias.Enabled = false;
            }
            else
            {
                this.txtLoopAlias.Enabled = true;
            }
        }

        private void comboPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboLoopPort.SelectedIndex == (this.comboLoopPort.Items.Count - 1))
            {
                this.checkAutoName.Enabled = false;
                this.txtLoopAlias.Enabled = false;
            }
            else
            {
                this.checkAutoName.Enabled = true;
                if (this.checkAutoName.Checked)
                {
                    this.txtLoopAlias.Text = (string) this.mPorts[this.comboLoopPort.SelectedIndex];
                }
                else
                {
                    this.txtLoopAlias.Enabled = true;
                }
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
            this.listLoops = new ListView();
            this.columnLoopAlias = new ColumnHeader();
            this.columnPort = new ColumnHeader();
            this.columnNumReceivers = new ColumnHeader();
            this.groupBox1 = new GroupBox();
            this.btnAddRing = new Button();
            this.checkAutoName = new CheckBox();
            this.comboLoopPort = new ComboBox();
            this.label2 = new Label();
            this.txtLoopAlias = new TextBox();
            this.label1 = new Label();
            this.btnRemoveSelected = new Button();
            this.btnClose = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.listLoops.CheckBoxes = true;
            this.listLoops.Columns.AddRange(new ColumnHeader[] { this.columnLoopAlias, this.columnPort, this.columnNumReceivers });
            this.listLoops.Location = new Point(10, 11);
            this.listLoops.Margin = new Padding(2, 2, 2, 2);
            this.listLoops.Name = "listLoops";
            this.listLoops.Size = new Size(0x16e, 0x93);
            this.listLoops.TabIndex = 0;
            this.listLoops.UseCompatibleStateImageBehavior = false;
            this.listLoops.View = View.Details;
            this.columnLoopAlias.Text = "Communication Loop Alias";
            this.columnLoopAlias.Width = 0x8d;
            this.columnPort.Text = "Port";
            this.columnPort.Width = 0x3f;
            this.columnNumReceivers.Text = "Receivers on loop";
            this.columnNumReceivers.Width = 0x92;
            this.groupBox1.Controls.Add(this.btnAddRing);
            this.groupBox1.Controls.Add(this.checkAutoName);
            this.groupBox1.Controls.Add(this.comboLoopPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtLoopAlias);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(380, 11);
            this.groupBox1.Margin = new Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(2, 2, 2, 2);
            this.groupBox1.Size = new Size(0xd5, 0x72);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add loop";
            this.btnAddRing.Location = new Point(0x98, 0x55);
            this.btnAddRing.Margin = new Padding(2, 2, 2, 2);
            this.btnAddRing.Name = "btnAddRing";
            this.btnAddRing.Size = new Size(0x38, 0x18);
            this.btnAddRing.TabIndex = 6;
            this.btnAddRing.Text = "Add";
            this.btnAddRing.UseVisualStyleBackColor = true;
            this.btnAddRing.Click += new EventHandler(this.btnAddLoop_Click);
            this.checkAutoName.AutoSize = true;
            this.checkAutoName.Checked = true;
            this.checkAutoName.CheckState = CheckState.Checked;
            this.checkAutoName.Location = new Point(4, 0x41);
            this.checkAutoName.Margin = new Padding(2, 2, 2, 2);
            this.checkAutoName.Name = "checkAutoName";
            this.checkAutoName.Size = new Size(0x8e, 0x11);
            this.checkAutoName.TabIndex = 5;
            this.checkAutoName.Text = "Set loop alias to ring port";
            this.checkAutoName.UseVisualStyleBackColor = true;
            this.checkAutoName.CheckedChanged += new EventHandler(this.checkAutoName_CheckedChanged);
            this.comboLoopPort.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboLoopPort.FormattingEnabled = true;
            this.comboLoopPort.Location = new Point(0x42, 0x29);
            this.comboLoopPort.Margin = new Padding(2, 2, 2, 2);
            this.comboLoopPort.Name = "comboLoopPort";
            this.comboLoopPort.Size = new Size(0x90, 0x15);
            this.comboLoopPort.TabIndex = 4;
            this.comboLoopPort.SelectedIndexChanged += new EventHandler(this.comboPort_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(5, 0x2b);
            this.label2.Margin = new Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Loop port:";
            this.txtLoopAlias.Enabled = false;
            this.txtLoopAlias.Location = new Point(0x42, 0x12);
            this.txtLoopAlias.Margin = new Padding(2, 2, 2, 2);
            this.txtLoopAlias.Name = "txtLoopAlias";
            this.txtLoopAlias.Size = new Size(0x90, 20);
            this.txtLoopAlias.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(5, 0x12);
            this.label1.Margin = new Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3a, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loop alias:";
            this.btnRemoveSelected.Location = new Point(380, 0x81);
            this.btnRemoveSelected.Margin = new Padding(2, 2, 2, 2);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new Size(0x70, 0x1d);
            this.btnRemoveSelected.TabIndex = 2;
            this.btnRemoveSelected.Text = "Remove selected";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new EventHandler(this.btnRemoveSelected_Click);
            this.btnClose.DialogResult = DialogResult.OK;
            this.btnClose.Location = new Point(0x1f0, 0x81);
            this.btnClose.Margin = new Padding(2, 2, 2, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x61, 0x1d);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x256, 0xa4);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnRemoveSelected);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.listLoops);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Margin = new Padding(2, 2, 2, 2);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ManageLoopsWin";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "PureRF Receiver";
            base.Load += new EventHandler(this.ManageLoopsWin_Load);
            base.Shown += new EventHandler(this.ManageLoopsWin_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void ManageLoopsWin_Load(object sender, EventArgs e)
        {
        }

        private void ManageLoopsWin_Shown(object sender, EventArgs e)
        {
            foreach (ReceiversManager.Loop loop in this.mReceiversManager.LoopsList)
            {
                ListViewItem item = new ListViewItem(new string[] { loop.Name, loop.Conn.Name, this.mReceiversManager.NumReceiversInLoop(loop.Name).ToString() });
                this.listLoops.Items.Add(item);
            }
        }
    }
}

