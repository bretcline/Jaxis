namespace ReceiverApp
{
    using PureRF;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class StartWizard_DiscoverLoops : Form
    {
        private Button btnAddIPLoop;
        private Button btnBack;
        private Button btnCancel;
        private Button btnNext;
        private Button btnScanIPLoops;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private IContainer components;
        private GroupBox groupBox1;
        private ListView listLoops;
        private bool m_EnableListviewCheckHandler;
        public ReceiversManager m_ReceiversManager;

        public StartWizard_DiscoverLoops(WinState state)
        {
            List<string> list;
            this.InitializeComponent();
            this.m_ReceiversManager = new ReceiversManager(300);
            this.m_EnableListviewCheckHandler = false;

            list = new List<string>( System.IO.Ports.SerialPort.GetPortNames( ) );

            if (state != null)
            {
                this.m_ReceiversManager = state.ReceiversManager;
                for (int i = 0; i < state.Loops.Length; i++)
                {
                    switch (state.Loops[i].LoopType)
                    {
                        case ReceiverBusConnection.BusType.LOOP_SERIAL:
                            this.listLoops.Items.Add(new ListViewItem(new string[] { state.Loops[i].LoopAlias, "Serial", state.Loops[i].LoopDesc }));
                            this.listLoops.Items[i].Checked = state.Loops[i].Enabled;
                            this.listLoops.Items[i].SubItems[1].Tag = state.Loops[i].LoopType;
                            this.listLoops.Items[i].SubItems[2].Tag = state.Loops[i].LoopPort;
                            break;

                        case ReceiverBusConnection.BusType.LOOP_IP:
                            this.listLoops.Items.Add(new ListViewItem(new string[] { state.Loops[i].LoopAlias, "IP", state.Loops[i].LoopDesc }));
                            this.listLoops.Items[i].Checked = state.Loops[i].Enabled;
                            this.listLoops.Items[i].SubItems[1].Tag = state.Loops[i].LoopType;
                            this.listLoops.Items[i].SubItems[2].Tag = state.Loops[i].LoopPort;
                            break;
                    }
                }
            }
            else
            {
                foreach (string str in list)
                {
                    string str2 = string.Format("{0}", str);
                    ReceiversManager.RetVal val = this.m_ReceiversManager.AddSerialLoop(str, str, 0xe100);
                    this.listLoops.Items.Add(new ListViewItem(new string[] { str, "Serial", str2 }));
                    this.listLoops.Items[this.listLoops.Items.Count - 1].Checked = val == ReceiversManager.RetVal.SUCCESS;
                    this.listLoops.Items[this.listLoops.Items.Count - 1].SubItems[1].Tag = ReceiverBusConnection.BusType.LOOP_SERIAL;
                    this.listLoops.Items[this.listLoops.Items.Count - 1].SubItems[2].Tag = str;
                }
            }
        }

        private void btnAddIPLoop_Click(object sender, EventArgs e)
        {
            IPLoopWin win = new IPLoopWin(this.m_ReceiversManager);
            if (win.ShowDialog() == DialogResult.OK)
            {
                this.m_EnableListviewCheckHandler = false;
                this.listLoops.Items.Add(new ListViewItem(new string[] { win.LoopAlias, "IP", string.Format("IP: {0}:{1}", win.LoopHost, win.LoopPort) }));
                this.listLoops.Items[this.listLoops.Items.Count - 1].Checked = true;
                this.listLoops.Items[this.listLoops.Items.Count - 1].SubItems[1].Tag = ReceiverBusConnection.BusType.LOOP_IP;
                this.listLoops.Items[this.listLoops.Items.Count - 1].SubItems[2].Tag = new object[] { win.LoopHost, win.LoopPort };
                this.m_EnableListviewCheckHandler = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.m_ReceiversManager.Clear();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
        }

        private void btnScanIPLoops_Click(object sender, EventArgs e)
        {
            IPScanWin win = new IPScanWin(true);
            if (win.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < win.SelectedIPs.Count; i++)
                {
                    string str = (string) win.SelectedIPs[i];
                    int num2 = (int) win.SelectedPorts[i];
                    string name = string.Format("IP_{0}:{1}", str, num2);
                    ReceiversManager.RetVal val = this.m_ReceiversManager.AddIPLoop(name, str, num2);
                    this.m_EnableListviewCheckHandler = false;
                    this.listLoops.Items.Add(new ListViewItem(new string[] { name, "IP", string.Format("IP: {0}:{1}", str, num2) }));
                    this.listLoops.Items[this.listLoops.Items.Count - 1].Checked = val == ReceiversManager.RetVal.SUCCESS;
                    this.listLoops.Items[this.listLoops.Items.Count - 1].SubItems[1].Tag = ReceiverBusConnection.BusType.LOOP_IP;
                    this.listLoops.Items[this.listLoops.Items.Count - 1].SubItems[2].Tag = new object[] { str, num2 };
                    this.m_EnableListviewCheckHandler = true;
                    if (val != ReceiversManager.RetVal.SUCCESS)
                    {
                        MessageBox.Show(string.Format("Unable to add loop {0}: {1}", name, val.ToString()), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
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
            this.btnCancel = new Button();
            this.btnNext = new Button();
            this.groupBox1 = new GroupBox();
            this.listLoops = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.btnBack = new Button();
            this.btnScanIPLoops = new Button();
            this.btnAddIPLoop = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(10, 0xce);
            this.btnCancel.Margin = new Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RightToLeft = RightToLeft.Yes;
            this.btnCancel.Size = new Size(0x66, 0x1c);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnNext.DialogResult = DialogResult.OK;
            this.btnNext.Location = new Point(0x196, 0xce);
            this.btnNext.Margin = new Padding(2, 2, 2, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x47, 0x1c);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "Next...";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.groupBox1.Controls.Add(this.listLoops);
            this.groupBox1.Location = new Point(10, 11);
            this.groupBox1.Margin = new Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(2, 2, 2, 2);
            this.groupBox1.Size = new Size(0x1d9, 190);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auto-discover Loops";
            this.listLoops.CheckBoxes = true;
            this.listLoops.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3 });
            this.listLoops.Location = new Point(4, 0x12);
            this.listLoops.Margin = new Padding(2, 2, 2, 2);
            this.listLoops.Name = "listLoops";
            this.listLoops.Size = new Size(0x1cf, 0xa8);
            this.listLoops.TabIndex = 0;
            this.listLoops.UseCompatibleStateImageBehavior = false;
            this.listLoops.View = View.Details;
            this.listLoops.ItemChecked += new ItemCheckedEventHandler(this.listLoops_ItemChecked);
            this.columnHeader1.Text = "Communication Loop Alias";
            this.columnHeader1.Width = 150;
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 70;
            this.columnHeader3.Text = "Port";
            this.columnHeader3.Width = 0xd7;
            this.btnBack.DialogResult = DialogResult.No;
            this.btnBack.Location = new Point(0x148, 0xce);
            this.btnBack.Margin = new Padding(2, 2, 2, 2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(0x4a, 0x1c);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new EventHandler(this.btnBack_Click);
            this.btnScanIPLoops.Location = new Point(0x74, 0xce);
            this.btnScanIPLoops.Margin = new Padding(2, 2, 2, 2);
            this.btnScanIPLoops.Name = "btnScanIPLoops";
            this.btnScanIPLoops.Size = new Size(0x66, 0x1c);
            this.btnScanIPLoops.TabIndex = 7;
            this.btnScanIPLoops.Text = "Scan IP loops";
            this.btnScanIPLoops.UseVisualStyleBackColor = true;
            this.btnScanIPLoops.Click += new EventHandler(this.btnScanIPLoops_Click);
            this.btnAddIPLoop.Location = new Point(0xde, 0xce);
            this.btnAddIPLoop.Margin = new Padding(2, 2, 2, 2);
            this.btnAddIPLoop.Name = "btnAddIPLoop";
            this.btnAddIPLoop.Size = new Size(0x66, 0x1c);
            this.btnAddIPLoop.TabIndex = 0;
            this.btnAddIPLoop.Text = "Add IP loop";
            this.btnAddIPLoop.UseVisualStyleBackColor = true;
            this.btnAddIPLoop.Click += new EventHandler(this.btnAddIPLoop_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1e8, 270);
            base.Controls.Add(this.btnAddIPLoop);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnScanIPLoops);
            base.Controls.Add(this.btnBack);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Margin = new Padding(2, 2, 2, 2);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "StartWizard_DiscoverLoops";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "PureRF Receiver";
            base.Load += new EventHandler(this.StartWizard_DiscoverLoops_Load);
            base.Shown += new EventHandler(this.StartWizard_DiscoverLoops_Shown);
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void listLoops_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ReceiversManager.RetVal eRROR = ReceiversManager.RetVal.ERROR;
            if (this.m_EnableListviewCheckHandler)
            {
                if (!e.Item.Checked)
                {
                    string text = e.Item.SubItems[0].Text;
                    eRROR = this.m_ReceiversManager.RemoveLoop(text);
                    if (eRROR != ReceiversManager.RetVal.SUCCESS)
                    {
                        this.m_EnableListviewCheckHandler = false;
                        e.Item.Checked = true;
                        this.m_EnableListviewCheckHandler = true;
                        MessageBox.Show("Unable to disable loop: " + eRROR.ToString(), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else
                {
                    string name = e.Item.SubItems[0].Text;
                    switch (((ReceiverBusConnection.BusType) e.Item.SubItems[1].Tag))
                    {
                        case ReceiverBusConnection.BusType.LOOP_SERIAL:
                        {
                            string tag = (string) e.Item.SubItems[2].Tag;
                            eRROR = this.m_ReceiversManager.AddSerialLoop(name, tag, 0xe100);
                            break;
                        }
                        case ReceiverBusConnection.BusType.LOOP_IP:
                        {
                            object[] objArray = (object[]) e.Item.SubItems[2].Tag;
                            eRROR = this.m_ReceiversManager.AddIPLoop(name, (string) objArray[0], (int) objArray[1]);
                            break;
                        }
                    }
                    if (eRROR != ReceiversManager.RetVal.SUCCESS)
                    {
                        this.m_EnableListviewCheckHandler = false;
                        e.Item.Checked = false;
                        this.m_EnableListviewCheckHandler = true;
                        MessageBox.Show("Unable to enable loop: " + eRROR.ToString(), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
        }

        private void StartWizard_DiscoverLoops_Load(object sender, EventArgs e)
        {
        }

        private void StartWizard_DiscoverLoops_Shown(object sender, EventArgs e)
        {
            this.m_EnableListviewCheckHandler = true;
            base.ActiveControl = this.btnNext;
        }

        public WinState State
        {
            get
            {
                WinState state = new WinState();
                state.Loops = new WinState.Loop[this.listLoops.Items.Count];
                state.ReceiversManager = this.m_ReceiversManager;
                for (int i = 0; i < state.Loops.Length; i++)
                {
                    state.Loops[i] = new WinState.Loop();
                    state.Loops[i].LoopAlias = this.listLoops.Items[i].SubItems[0].Text;
                    state.Loops[i].LoopType = (ReceiverBusConnection.BusType) this.listLoops.Items[i].SubItems[1].Tag;
                    state.Loops[i].Enabled = this.listLoops.Items[i].Checked;
                    state.Loops[i].LoopDesc = this.listLoops.Items[i].SubItems[2].Text;
                    state.Loops[i].LoopPort = this.listLoops.Items[i].SubItems[2].Tag;
                }
                return state;
            }
        }

        public class WinState
        {
            public Loop[] Loops;
            public PureRF.ReceiversManager ReceiversManager;

            public class Loop
            {
                public bool Enabled;
                public string LoopAlias;
                public string LoopDesc;
                public object LoopPort;
                public ReceiverBusConnection.BusType LoopType;
            }
        }
    }
}

