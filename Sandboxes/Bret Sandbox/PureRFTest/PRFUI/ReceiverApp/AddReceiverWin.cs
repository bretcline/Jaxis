namespace ReceiverApp
{
    using PureRF;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AddReceiverWin : Form
    {
        private Button btnAdd;
        private Button btnCancel;
        private CheckBox checkNameAutoGen;
        public ComboBox comboLoop;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private ReceiversManager mReceiversManager;
        public NumericUpDown numUnitID;
        public TextBox txtName;

        public AddReceiverWin(ReceiversManager ReceiversManager)
        {
            this.InitializeComponent();
            this.mReceiversManager = ReceiversManager;
        }

        private void AddReceiverWin_Load(object sender, EventArgs e)
        {
        }

        private void AddReceiverWin_Shown(object sender, EventArgs e)
        {
            foreach (ReceiversManager.Loop loop in this.mReceiversManager.LoopsList)
            {
                this.comboLoop.Items.Add(loop.Name);
            }
        }

        private void AutoGenName()
        {
            if (this.checkNameAutoGen.Checked)
            {
                if ((this.numUnitID.Value == 0M) || (this.comboLoop.SelectedIndex == -1))
                {
                    this.txtName.Text = "Invalid Settings";
                }
                else
                {
                    this.txtName.Text = string.Format("{0}-{1}", this.comboLoop.SelectedItem.ToString(), this.numUnitID.Value.ToString());
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (((this.numUnitID.Value == 0M) || (this.comboLoop.SelectedIndex == -1)) || (this.txtName.Text == ""))
            {
                MessageBox.Show("Please fill all fields with valid values.", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (!this.mReceiversManager.IsReceiverNameAvailable(this.txtName.Text))
            {
                MessageBox.Show("Receiver name already taken!", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                string loopName = this.comboLoop.SelectedItem.ToString();
                byte unitID = (byte) this.numUnitID.Value;
                ReceiversManager.RetVal val = this.mReceiversManager.AddReceiver(this.txtName.Text, loopName, unitID);
                if (val != ReceiversManager.RetVal.SUCCESS)
                {
                    MessageBox.Show("Unable to add receiver: " + val.ToString(), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    base.DialogResult = DialogResult.OK;
                }
            }
        }

        private void checkNameAutoGen_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkNameAutoGen.Checked)
            {
                this.txtName.Enabled = false;
                this.AutoGenName();
            }
            else
            {
                this.txtName.Enabled = true;
            }
        }

        private void comboLoop_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AutoGenName();
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
            this.btnAdd = new Button();
            this.btnCancel = new Button();
            this.groupBox1 = new GroupBox();
            this.checkNameAutoGen = new CheckBox();
            this.txtName = new TextBox();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.comboLoop = new ComboBox();
            this.label3 = new Label();
            this.numUnitID = new NumericUpDown();
            this.label2 = new Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.numUnitID.BeginInit();
            base.SuspendLayout();
            this.btnAdd.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnAdd.Location = new Point(0x58, 0xa8);
            this.btnAdd.Margin = new Padding(2, 2, 2, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x38, 0x1b);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnCancel.Location = new Point(0x94, 0xa8);
            this.btnCancel.Margin = new Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x1b);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.groupBox1.Controls.Add(this.checkNameAutoGen);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(10, 11);
            this.groupBox1.Margin = new Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(2, 2, 2, 2);
            this.groupBox1.Size = new Size(0xc2, 0x43);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Receiver Name";
            this.checkNameAutoGen.AutoSize = true;
            this.checkNameAutoGen.Checked = true;
            this.checkNameAutoGen.CheckState = CheckState.Checked;
            this.checkNameAutoGen.Location = new Point(8, 0x29);
            this.checkNameAutoGen.Margin = new Padding(2, 2, 2, 2);
            this.checkNameAutoGen.Name = "checkNameAutoGen";
            this.checkNameAutoGen.Size = new Size(0x7a, 0x11);
            this.checkNameAutoGen.TabIndex = 2;
            this.checkNameAutoGen.Text = "Auto-generate name";
            this.checkNameAutoGen.UseVisualStyleBackColor = true;
            this.checkNameAutoGen.CheckedChanged += new EventHandler(this.checkNameAutoGen_CheckedChanged);
            this.txtName.Enabled = false;
            this.txtName.Location = new Point(0x2f, 0x12);
            this.txtName.Margin = new Padding(2, 2, 2, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0x8e, 20);
            this.txtName.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(5, 0x12);
            this.label1.Margin = new Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            this.groupBox2.Controls.Add(this.comboLoop);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numUnitID);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new Point(10, 0x53);
            this.groupBox2.Margin = new Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new Padding(2, 2, 2, 2);
            this.groupBox2.Size = new Size(0xc2, 0x51);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Receiver Details";
            this.comboLoop.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboLoop.FormattingEnabled = true;
            this.comboLoop.Location = new Point(0x7f, 40);
            this.comboLoop.Margin = new Padding(2, 2, 2, 2);
            this.comboLoop.Name = "comboLoop";
            this.comboLoop.Size = new Size(0x3e, 0x15);
            this.comboLoop.TabIndex = 3;
            this.comboLoop.SelectedIndexChanged += new EventHandler(this.comboLoop_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(4, 40);
            this.label3.Margin = new Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x6b, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Coomunication Loop:";
            this.numUnitID.Location = new Point(0x7f, 0x10);
            this.numUnitID.Margin = new Padding(2, 2, 2, 2);
            int[] bits = new int[4];
            bits[0] = 0xff;
            this.numUnitID.Maximum = new decimal(bits);
            this.numUnitID.Name = "numUnitID";
            this.numUnitID.Size = new Size(0x3e, 20);
            this.numUnitID.TabIndex = 1;
            this.numUnitID.ValueChanged += new EventHandler(this.numUnitID_ValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(5, 0x12);
            this.label2.Margin = new Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x2b, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Unit ID:";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0xd5, 0xc9);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnAdd);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Margin = new Padding(2, 2, 2, 2);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "AddReceiverWin";
            base.ShowInTaskbar = false;
            this.Text = "PureRF Receiver - Add Receiver";
            base.Load += new EventHandler(this.AddReceiverWin_Load);
            base.Shown += new EventHandler(this.AddReceiverWin_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.numUnitID.EndInit();
            base.ResumeLayout(false);
        }

        private void numUnitID_ValueChanged(object sender, EventArgs e)
        {
            this.AutoGenName();
        }
    }
}

