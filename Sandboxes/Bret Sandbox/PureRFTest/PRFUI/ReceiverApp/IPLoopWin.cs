namespace ReceiverApp
{
    using PureRF;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class IPLoopWin : Form
    {
        private Button btnAddRing;
        private Button btnCancel;
        private Button btnScan;
        private CheckBox checkAutoName;
        private IContainer components;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        public string LoopAlias;
        public string LoopHost;
        public int LoopPort;
        private ReceiversManager mReceiversManager;
        private NumericUpDown numLoopPort;
        private TextBox txtLoopAlias;
        private TextBox txtLoopHost;

        public IPLoopWin(ReceiversManager ReceiversManager)
        {
            this.InitializeComponent();
            this.mReceiversManager = ReceiversManager;
        }

        private void btnAddRing_Click(object sender, EventArgs e)
        {
            string text = "";
            if (this.txtLoopAlias.Text == "")
            {
                text = "Please provide a loop alias";
            }
            else if (this.txtLoopHost.Text == "")
            {
                text = "Please provide a host";
            }
            else if (this.numLoopPort.Value == 0M)
            {
                text = "Please provide a port";
            }
            else if (!this.mReceiversManager.IsLoopNameAvailable(this.txtLoopAlias.Text))
            {
                text = "Loop name already in use!";
            }
            else
            {
                this.btnAddRing.Text = "Adding...";
                this.btnAddRing.Enabled = false;
                this.btnCancel.Enabled = false;
                ReceiversManager.RetVal val = this.mReceiversManager.AddIPLoop(this.txtLoopAlias.Text, this.txtLoopHost.Text, (int) this.numLoopPort.Value);
                this.btnAddRing.Enabled = true;
                this.btnCancel.Enabled = true;
                this.btnAddRing.Text = "Add";
                if (val != ReceiversManager.RetVal.SUCCESS)
                {
                    string str2 = "";
                    if (val == ReceiversManager.RetVal.PORT_NOT_AVAILABLE)
                    {
                        str2 = "Unable to connect to remote host";
                    }
                    else
                    {
                        str2 = val.ToString();
                    }
                    MessageBox.Show("Error adding loop: " + str2, "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                this.LoopAlias = this.txtLoopAlias.Text;
                this.LoopHost = this.txtLoopHost.Text;
                this.LoopPort = (int) this.numLoopPort.Value;
                base.DialogResult = DialogResult.OK;
                return;
            }
            MessageBox.Show(text, "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            IPScanWin win = new IPScanWin(false);
            if (win.ShowDialog() == DialogResult.OK)
            {
                this.txtLoopHost.Text = (string) win.SelectedIPs[0];
                this.numLoopPort.Value = (int) win.SelectedPorts[0];
            }
        }

        private void checkAutoName_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkAutoName.Checked)
            {
                this.txtLoopAlias.Enabled = false;
                this.txtLoopAlias.Text = "IP_" + this.txtLoopHost.Text + ":" + this.numLoopPort.Value.ToString();
            }
            else
            {
                this.txtLoopAlias.Enabled = true;
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
            this.txtLoopAlias = new TextBox();
            this.groupBox1 = new GroupBox();
            this.label3 = new Label();
            this.numLoopPort = new NumericUpDown();
            this.txtLoopHost = new TextBox();
            this.checkAutoName = new CheckBox();
            this.label2 = new Label();
            this.label1 = new Label();
            this.btnAddRing = new Button();
            this.btnCancel = new Button();
            this.btnScan = new Button();
            this.groupBox1.SuspendLayout();
            this.numLoopPort.BeginInit();
            base.SuspendLayout();
            this.txtLoopAlias.Enabled = false;
            this.txtLoopAlias.Location = new Point(0x58, 0x16);
            this.txtLoopAlias.Name = "txtLoopAlias";
            this.txtLoopAlias.Size = new Size(0xd8, 0x16);
            this.txtLoopAlias.TabIndex = 1;
            this.groupBox1.Controls.Add(this.btnScan);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numLoopPort);
            this.groupBox1.Controls.Add(this.txtLoopHost);
            this.groupBox1.Controls.Add(this.checkAutoName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtLoopAlias);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x137, 0x6d);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add loop";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.label3.Location = new Point(0xdb, 0x35);
            this.label3.Name = "label3";
            this.label3.Size = new Size(13, 0x11);
            this.label3.TabIndex = 8;
            this.label3.Text = ":";
            this.numLoopPort.Location = new Point(0xed, 0x33);
            int[] bits = new int[4];
            bits[0] = 0xffff;
            this.numLoopPort.Maximum = new decimal(bits);
            this.numLoopPort.Name = "numLoopPort";
            this.numLoopPort.Size = new Size(0x42, 0x16);
            this.numLoopPort.TabIndex = 7;
            int[] numArray2 = new int[4];
            numArray2[0] = 0x1f57;
            this.numLoopPort.Value = new decimal(numArray2);
            this.numLoopPort.ValueChanged += new EventHandler(this.numLoopPort_ValueChanged);
            this.txtLoopHost.Location = new Point(0x58, 0x33);
            this.txtLoopHost.Name = "txtLoopHost";
            this.txtLoopHost.Size = new Size(0x7d, 0x16);
            this.txtLoopHost.TabIndex = 6;
            this.txtLoopHost.TextChanged += new EventHandler(this.loopHost_TextChanged);
            this.checkAutoName.AutoSize = true;
            this.checkAutoName.Checked = true;
            this.checkAutoName.CheckState = CheckState.Checked;
            this.checkAutoName.Location = new Point(6, 80);
            this.checkAutoName.Name = "checkAutoName";
            this.checkAutoName.Size = new Size(0xb9, 0x15);
            this.checkAutoName.TabIndex = 5;
            this.checkAutoName.Text = "Set loop alias to ring port";
            this.checkAutoName.UseVisualStyleBackColor = true;
            this.checkAutoName.CheckedChanged += new EventHandler(this.checkAutoName_CheckedChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(7, 0x35);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x36, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "IP/Port:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 0x16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loop alias:";
            this.btnAddRing.Location = new Point(0xf9, 0x7f);
            this.btnAddRing.Name = "btnAddRing";
            this.btnAddRing.Size = new Size(0x4b, 0x17);
            this.btnAddRing.TabIndex = 6;
            this.btnAddRing.Text = "Add";
            this.btnAddRing.UseVisualStyleBackColor = true;
            this.btnAddRing.Click += new EventHandler(this.btnAddRing_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xa8, 0x7f);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnScan.Location = new Point(230, 80);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new Size(0x4b, 0x17);
            this.btnScan.TabIndex = 9;
            this.btnScan.Text = "Scan...";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new EventHandler(this.btnScan_Click);
            base.AutoScaleDimensions = new SizeF(8f, 16f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x14d, 0x9c);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnAddRing);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "IPLoopWin";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "PureRF Receiver - Add IP Loop";
            base.Load += new EventHandler(this.IPLoopWin_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.numLoopPort.EndInit();
            base.ResumeLayout(false);
        }

        private void IPLoopWin_Load(object sender, EventArgs e)
        {
        }

        private void loopHost_TextChanged(object sender, EventArgs e)
        {
            if (this.checkAutoName.Checked)
            {
                this.txtLoopAlias.Text = "IP_" + this.txtLoopHost.Text + ":" + this.numLoopPort.Value.ToString();
            }
        }

        private void numLoopPort_ValueChanged(object sender, EventArgs e)
        {
            if (this.checkAutoName.Checked)
            {
                this.txtLoopAlias.Text = "IP_" + this.txtLoopHost.Text + ":" + this.numLoopPort.Value.ToString();
            }
        }
    }
}

