namespace PureRF
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class PortWin : Form
    {
        private Button btnCancel;
        private Button btnOK;
        private ComboBox comboBaudrate;
        private ComboBox comboPort;
        private IContainer components;
        private Label label1;
        private Label label2;
        public ArrayList Ports;
        public SortedList<string, string> PortsMap;
        public string selectedBaudrate;
        public string selectedPort;

        public PortWin(string defaultPort, int defaultBaudrate)
        {
            this.InitializeComponent();
            this.Ports = new ArrayList();
            if (defaultBaudrate == -1)
            {
                this.label2.Visible = false;
                this.comboBaudrate.Visible = false;
                defaultBaudrate = 0x1c200;
            }
            for (int i = 0; i < this.comboBaudrate.Items.Count; i++)
            {
                if (this.comboBaudrate.Items[i].ToString() == Convert.ToString(defaultBaudrate))
                {
                    this.comboBaudrate.SelectedIndex = i;
                    break;
                }
            }
            if (!PortsEnumerator.EnumeratePorts(out this.PortsMap))
            {
                MessageBox.Show("Unable to enumerate serial ports!");
                base.DialogResult = DialogResult.Cancel;
            }
            else
            {
                foreach (string str in this.PortsMap.Keys)
                {
                    this.comboPort.Items.Add(string.Format("{0} ({1})", str, this.PortsMap[str]));
                    this.Ports.Add(str);
                    if (defaultPort == str)
                    {
                        this.comboPort.SelectedIndex = this.comboPort.Items.Count - 1;
                    }
                    if ((this.comboPort.SelectedIndex == -1) && (this.comboPort.Items.Count > 0))
                    {
                        this.comboPort.SelectedIndex = 0;
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.selectedPort = this.Ports[this.comboPort.SelectedIndex].ToString();
            this.selectedBaudrate = this.comboBaudrate.SelectedItem.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public int GetSelectedBaudrate()
        {
            return Convert.ToInt32(this.comboBaudrate.SelectedItem.ToString());
        }

        public string GetSelectedPort()
        {
            return this.Ports[this.comboPort.SelectedIndex].ToString();
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.comboPort = new ComboBox();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.label2 = new Label();
            this.comboBaudrate = new ComboBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 11);
            this.label1.Margin = new Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "COM Port:";
            this.comboPort.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboPort.DropDownWidth = 0xca;
            this.comboPort.FormattingEnabled = true;
            this.comboPort.Location = new Point(0x45, 8);
            this.comboPort.Margin = new Padding(2);
            this.comboPort.Name = "comboPort";
            this.comboPort.Size = new Size(0xe7, 0x15);
            this.comboPort.TabIndex = 1;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnOK.Location = new Point(0x3a, 0x3a);
            this.btnOK.Margin = new Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x45, 0x18);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnCancel.Location = new Point(0xbb, 0x3a);
            this.btnCancel.Margin = new Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x45, 0x18);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(7, 0x24);
            this.label2.Margin = new Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Baudrate:";
            this.comboBaudrate.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBaudrate.FormattingEnabled = true;
            this.comboBaudrate.Items.AddRange(new object[] { "1200", "2400", "4800", "9600", "14400", "19200", "38400", "56000", "115200", "128000", "256000" });
            this.comboBaudrate.Location = new Point(0x45, 0x21);
            this.comboBaudrate.Margin = new Padding(2);
            this.comboBaudrate.Name = "comboBaudrate";
            this.comboBaudrate.Size = new Size(0xe7, 0x15);
            this.comboBaudrate.TabIndex = 5;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x137, 0x5c);
            base.Controls.Add(this.comboBaudrate);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.comboPort);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Margin = new Padding(2);
            base.Name = "PortWin";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Choose Port";
            base.Load += new EventHandler(this.PortWin_Load);
            base.Shown += new EventHandler(this.PortWin_Shown);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void PortWin_Load(object sender, EventArgs e)
        {
        }

        private void PortWin_Shown(object sender, EventArgs e)
        {
        }
    }
}

