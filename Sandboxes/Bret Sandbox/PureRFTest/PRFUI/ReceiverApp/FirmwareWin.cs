namespace ReceiverApp
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class FirmwareWin : Form
    {
        private Button btnBrowse;
        private Button btnCancel;
        private Button btnContinue;
        private IContainer components;
        private OpenFileDialog dlgFirmwareFile;
        private GroupBox groupBox1;
        private GroupBox groupBox6;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label lblFilename;
        public byte[] mFirmware;
        public NumericUpDown numPageDelay;
        public NumericUpDown numResendCnt;
        public NumericUpDown numResendDelay;

        public FirmwareWin()
        {
            this.InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (this.dlgFirmwareFile.ShowDialog() == DialogResult.OK)
            {
                this.lblFilename.Text = this.dlgFirmwareFile.FileName;
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (this.lblFilename.Text == "None")
            {
                MessageBox.Show("Please select a firmware file", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    FileStream stream = File.OpenRead(this.lblFilename.Text);
                    this.mFirmware = ReadAll(stream, 0xffff);
                    stream.Close();
                    base.DialogResult = DialogResult.OK;
                }
                catch
                {
                    MessageBox.Show("Unable to open firmware file!", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

        private void FirmwareWin_Load(object sender, EventArgs e)
        {
            this.dlgFirmwareFile.InitialDirectory = Application.ExecutablePath;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.numResendCnt = new NumericUpDown();
            this.label2 = new Label();
            this.label3 = new Label();
            this.numResendDelay = new NumericUpDown();
            this.numPageDelay = new NumericUpDown();
            this.groupBox6 = new GroupBox();
            this.btnContinue = new Button();
            this.btnCancel = new Button();
            this.groupBox1 = new GroupBox();
            this.btnBrowse = new Button();
            this.lblFilename = new Label();
            this.label4 = new Label();
            this.dlgFirmwareFile = new OpenFileDialog();
            this.numResendCnt.BeginInit();
            this.numResendDelay.BeginInit();
            this.numPageDelay.BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(5, 0x12);
            this.label1.Margin = new Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Resend count:";
            this.numResendCnt.Location = new Point(0xa3, 0x12);
            this.numResendCnt.Margin = new Padding(2);
            this.numResendCnt.Name = "numResendCnt";
            this.numResendCnt.Size = new Size(80, 20);
            this.numResendCnt.TabIndex = 1;
            int[] bits = new int[4];
            bits[0] = 1;
            this.numResendCnt.Value = new decimal(bits);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(5, 0x25);
            this.label2.Margin = new Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4b, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Resend delay:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(4, 0x38);
            this.label3.Margin = new Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x58, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Page write delay:";
            this.numResendDelay.Location = new Point(0xa3, 0x25);
            this.numResendDelay.Margin = new Padding(2);
            int[] numArray2 = new int[4];
            numArray2[0] = 0xffff;
            this.numResendDelay.Maximum = new decimal(numArray2);
            this.numResendDelay.Name = "numResendDelay";
            this.numResendDelay.Size = new Size(80, 20);
            this.numResendDelay.TabIndex = 3;
            int[] numArray3 = new int[4];
            numArray3[0] = 60;
            this.numResendDelay.Value = new decimal(numArray3);
            this.numPageDelay.Location = new Point(0xa3, 0x38);
            this.numPageDelay.Margin = new Padding(2);
            int[] numArray4 = new int[4];
            numArray4[0] = 0xffff;
            this.numPageDelay.Maximum = new decimal(numArray4);
            this.numPageDelay.Name = "numPageDelay";
            this.numPageDelay.Size = new Size(80, 20);
            this.numPageDelay.TabIndex = 15;
            int[] numArray5 = new int[4];
            numArray5[0] = 500;
            this.numPageDelay.Value = new decimal(numArray5);
            this.groupBox6.Controls.Add(this.numPageDelay);
            this.groupBox6.Controls.Add(this.numResendDelay);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.numResendCnt);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Location = new Point(9, 10);
            this.groupBox6.Margin = new Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new Padding(2);
            this.groupBox6.Size = new Size(0xf8, 0x53);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Firmware upgrade parameters";
            this.btnContinue.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnContinue.Location = new Point(10, 0xa6);
            this.btnContinue.Margin = new Padding(2);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new Size(0x51, 0x18);
            this.btnContinue.TabIndex = 15;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new EventHandler(this.btnContinue_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnCancel.Location = new Point(0xac, 0xa6);
            this.btnCancel.Margin = new Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x54, 0x18);
            this.btnCancel.TabIndex = 0x10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.lblFilename);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new Point(9, 0x62);
            this.groupBox1.Margin = new Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(2);
            this.groupBox1.Size = new Size(0xf8, 0x3f);
            this.groupBox1.TabIndex = 0x11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Firmware file";
            this.btnBrowse.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnBrowse.Location = new Point(0xa3, 0x1f);
            this.btnBrowse.Margin = new Padding(2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new Size(80, 0x18);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);
            this.lblFilename.AutoSize = true;
            this.lblFilename.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.lblFilename.Location = new Point(0x3e, 0x12);
            this.lblFilename.Margin = new Padding(2, 0, 2, 0);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new Size(0x25, 13);
            this.lblFilename.TabIndex = 1;
            this.lblFilename.Text = "None";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(5, 0x12);
            this.label4.Margin = new Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x34, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Filename:";
            this.dlgFirmwareFile.DefaultExt = "bin";
            this.dlgFirmwareFile.Filter = "Firmware files|*.bin";
            this.dlgFirmwareFile.Title = "Choose firmware file";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x109, 0xc3);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnContinue);
            base.Controls.Add(this.groupBox6);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Margin = new Padding(2);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FirmwareWin";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Upload Firmware Settings";
            base.Load += new EventHandler(this.FirmwareWin_Load);
            this.numResendCnt.EndInit();
            this.numResendDelay.EndInit();
            this.numPageDelay.EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        public static byte[] ReadAll(Stream stream, int initialLength)
        {
            int num2;
            if (initialLength < 1)
            {
                initialLength = 0x8000;
            }
            byte[] buffer = new byte[initialLength];
            int offset = 0;
            while ((num2 = stream.Read(buffer, offset, buffer.Length - offset)) > 0)
            {
                offset += num2;
                if (offset == buffer.Length)
                {
                    int num3 = stream.ReadByte();
                    if (num3 == -1)
                    {
                        return buffer;
                    }
                    byte[] buffer2 = new byte[buffer.Length * 2];
                    Array.Copy(buffer, buffer2, buffer.Length);
                    buffer2[offset] = (byte) num3;
                    buffer = buffer2;
                    offset++;
                }
            }
            byte[] destinationArray = new byte[offset];
            Array.Copy(buffer, destinationArray, offset);
            return destinationArray;
        }
    }
}

