namespace ReceiverApp
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Net;
    using System.Windows.Forms;

    public class IPScanWin : Form
    {
        private Button btnCancel;
        private Button btnOK;
        private Button btnStartStop;
        private ColumnHeader colIP;
        private ColumnHeader colPort;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private ToolStripStatusLabel lblStatus;
        private ListView listResults;
        private PortScanner m_PortScanner;
        private int m_ScannedIPs;
        private int m_TotalIPs;
        private NumericUpDown numPort;
        private ToolStripProgressBar progbarStatus;
        public ArrayList SelectedIPs;
        public ArrayList SelectedPorts;
        private StatusStrip statusStrip1;
        private TextBox txtEndIP;
        private TextBox txtStartIP;

        public IPScanWin(bool MultipleLoops)
        {
            this.InitializeComponent();
            this.SelectedIPs = new ArrayList();
            this.SelectedPorts = new ArrayList();
            this.m_PortScanner = new PortScanner();
            this.m_PortScanner.SetCallback(new PortScanner.EventCallback(this.PortScannerCallback), this);
            if (MultipleLoops)
            {
                this.listResults.CheckBoxes = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!this.m_PortScanner.IsReady)
            {
                this.btnCancel.Enabled = false;
                this.btnStartStop.Enabled = false;
                this.lblStatus.Text = "Cancelling...";
                this.m_PortScanner.Stop();
                this.m_PortScanner.WaitForScanCompletion();
            }
            base.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!this.listResults.CheckBoxes)
            {
                ListViewItem item = this.listResults.SelectedItems[0];
                this.SelectedIPs.Add(item.SubItems[0].Text);
                this.SelectedPorts.Add(Convert.ToInt32(item.SubItems[1].Text));
            }
            else
            {
                foreach (ListViewItem item2 in this.listResults.CheckedItems)
                {
                    this.SelectedIPs.Add(item2.SubItems[0].Text);
                    this.SelectedPorts.Add(Convert.ToInt32(item2.SubItems[1].Text));
                }
            }
            base.DialogResult = DialogResult.OK;
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (this.m_PortScanner.IsReady)
            {
                this.btnStartStop.Enabled = false;
                this.lblStatus.Text = "Starting...";
                this.m_ScannedIPs = 0;
                this.listResults.Items.Clear();
                try
                {
                    this.m_PortScanner.SetCallback(new PortScanner.EventCallback(this.PortScannerCallback), this);
                    this.m_TotalIPs = this.m_PortScanner.Start(this.txtStartIP.Text, this.txtEndIP.Text, (int) this.numPort.Value, 10, 0x2710);
                }
                catch (PortScannerInvalidIPRangeException)
                {
                    MessageBox.Show("Invalid IP port range supplied", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.btnStartStop.Enabled = true;
                    this.lblStatus.Text = "Scan not started";
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Unable to start scan: " + exception.GetType().ToString(), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.btnStartStop.Enabled = true;
                    this.lblStatus.Text = "Scan not started";
                }
            }
            else
            {
                this.btnStartStop.Enabled = false;
                this.btnCancel.Enabled = false;
                this.m_PortScanner.Stop();
                this.lblStatus.Text = "Stopping...";
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
            this.groupBox1 = new GroupBox();
            this.txtEndIP = new TextBox();
            this.label2 = new Label();
            this.txtStartIP = new TextBox();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.numPort = new NumericUpDown();
            this.label3 = new Label();
            this.listResults = new ListView();
            this.colIP = new ColumnHeader();
            this.colPort = new ColumnHeader();
            this.btnStartStop = new Button();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.statusStrip1 = new StatusStrip();
            this.lblStatus = new ToolStripStatusLabel();
            this.progbarStatus = new ToolStripProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.numPort.BeginInit();
            this.statusStrip1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtEndIP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtStartIP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(9, 10);
            this.groupBox1.Margin = new Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(2, 2, 2, 2);
            this.groupBox1.Size = new Size(0xcc, 0x43);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IP Range";
            this.txtEndIP.Location = new Point(0x36, 0x29);
            this.txtEndIP.Margin = new Padding(2, 2, 2, 2);
            this.txtEndIP.Name = "txtEndIP";
            this.txtEndIP.Size = new Size(0x92, 20);
            this.txtEndIP.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(4, 0x2b);
            this.label2.Margin = new Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x2a, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "End IP:";
            this.txtStartIP.Location = new Point(0x36, 0x12);
            this.txtStartIP.Margin = new Padding(2, 2, 2, 2);
            this.txtStartIP.Name = "txtStartIP";
            this.txtStartIP.Size = new Size(0x92, 20);
            this.txtStartIP.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(4, 20);
            this.label1.Margin = new Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2d, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start IP:";
            this.groupBox2.Controls.Add(this.numPort);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new Point(9, 0x51);
            this.groupBox2.Margin = new Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new Padding(2, 2, 2, 2);
            this.groupBox2.Size = new Size(0xcc, 40);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Port";
            this.numPort.Location = new Point(0x36, 14);
            this.numPort.Margin = new Padding(2, 2, 2, 2);
            int[] bits = new int[4];
            bits[0] = 0xffff;
            this.numPort.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 1;
            this.numPort.Minimum = new decimal(numArray2);
            this.numPort.Name = "numPort";
            this.numPort.Size = new Size(0x92, 20);
            this.numPort.TabIndex = 1;
            int[] numArray3 = new int[4];
            numArray3[0] = 0x1f57;
            this.numPort.Value = new decimal(numArray3);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(7, 0x12);
            this.label3.Margin = new Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Port:";
            this.listResults.Columns.AddRange(new ColumnHeader[] { this.colIP, this.colPort });
            this.listResults.Location = new Point(0xda, 15);
            this.listResults.Margin = new Padding(2, 2, 2, 2);
            this.listResults.Name = "listResults";
            this.listResults.Size = new Size(0x10b, 0x8b);
            this.listResults.TabIndex = 2;
            this.listResults.UseCompatibleStateImageBehavior = false;
            this.listResults.View = View.Details;
            this.listResults.ItemChecked += new ItemCheckedEventHandler(this.listResults_ItemChecked);
            this.listResults.SelectedIndexChanged += new EventHandler(this.listResults_SelectedIndexChanged);
            this.colIP.Text = "IP";
            this.colIP.Width = 160;
            this.colPort.Text = "Port";
            this.colPort.Width = 0x55;
            this.btnStartStop.Location = new Point(10, 0x7f);
            this.btnStartStop.Margin = new Padding(2, 2, 2, 2);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new Size(0x38, 0x1b);
            this.btnStartStop.TabIndex = 3;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new EventHandler(this.btnStartStop_Click);
            this.btnCancel.Location = new Point(0x9c, 0x7e);
            this.btnCancel.Margin = new Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x1c);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(0x5f, 0x7f);
            this.btnOK.Margin = new Padding(2, 2, 2, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x1b);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.lblStatus, this.progbarStatus });
            this.statusStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new Point(0, 0xa3);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new Size(0x1ef, 0x16);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(0x57, 0x11);
            this.lblStatus.Text = "Scan not started";
            this.progbarStatus.Alignment = ToolStripItemAlignment.Right;
            this.progbarStatus.Name = "progbarStatus";
            this.progbarStatus.Size = new Size(0x4b, 0x10);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1ef, 0xb9);
            base.Controls.Add(this.statusStrip1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnStartStop);
            base.Controls.Add(this.listResults);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Margin = new Padding(2, 2, 2, 2);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "IPScanWin";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "PureRF Receiver - IP Scan";
            base.Load += new EventHandler(this.IPScanWin_Load);
            base.FormClosing += new FormClosingEventHandler(this.IPScanWin_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.numPort.EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void IPScanWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.m_PortScanner.Stop();
                this.m_PortScanner.WaitForScanCompletion();
            }
            catch
            {
            }
        }

        private void IPScanWin_Load(object sender, EventArgs e)
        {
        }

        private void listResults_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (this.m_PortScanner.IsReady)
            {
                this.btnOK.Enabled = this.listResults.CheckedItems.Count > 0;
            }
        }

        private void listResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.listResults.CheckBoxes && this.m_PortScanner.IsReady)
            {
                this.btnOK.Enabled = true;
            }
        }

        private void PortScannerCallback(PortScanner Instance, PortScanner.EventID eID, object Arg)
        {
            switch (eID)
            {
                case PortScanner.EventID.STARTED:
                    this.lblStatus.Text = string.Format("Scanning... (0/{0} (0%) IPs scanned)", this.m_TotalIPs);
                    this.btnStartStop.Text = "Stop";
                    this.btnStartStop.Enabled = true;
                    this.listResults.Enabled = false;
                    return;

                case PortScanner.EventID.FINISHED:
                    Instance.WaitForScanCompletion();
                    this.lblStatus.Text = "Scan finished.";
                    this.btnStartStop.Text = "Start";
                    this.btnStartStop.Enabled = true;
                    this.progbarStatus.Value = 0;
                    this.btnCancel.Enabled = true;
                    this.listResults.Enabled = true;
                    return;

                case PortScanner.EventID.PORT_OPENED:
                {
                    IPEndPoint point = Arg as IPEndPoint;
                    this.m_ScannedIPs++;
                    this.progbarStatus.Value = (this.m_ScannedIPs * 100) / this.m_TotalIPs;
                    this.lblStatus.Text = string.Format("Scanning... ({0}/{1} ({2}%) IPs scanned)", this.m_ScannedIPs, this.m_TotalIPs, this.progbarStatus.Value);
                    ListViewItem item = new ListViewItem(new string[] { point.Address.ToString(), point.Port.ToString() });
                    this.listResults.Items.Add(item);
                    return;
                }
                case PortScanner.EventID.PORT_CLOSED:
                    this.m_ScannedIPs++;
                    this.progbarStatus.Value = (this.m_ScannedIPs * 100) / this.m_TotalIPs;
                    this.lblStatus.Text = string.Format("Scanning... ({0}/{1} ({2}%) IPs scanned)", this.m_ScannedIPs, this.m_TotalIPs, this.progbarStatus.Value);
                    return;
            }
        }
    }
}

