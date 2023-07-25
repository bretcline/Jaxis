namespace ReceiverApp
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NewSqlDBServerWin : Form
    {
        private Button btnBack;
        private Button btnCancel;
        private Button btnNext;
        private IContainer components;
        public string DB_Name;
        public string DB_ServerName;
        public string DB_Table;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtServerName;

        public NewSqlDBServerWin()
        {
            this.InitializeComponent();
            this.txtServerName.Text = Environment.MachineName + @"\SQLEXPRESS";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NewSqlDBTableWin win = new NewSqlDBTableWin(this.txtServerName.Text);
            switch (win.ShowDialog())
            {
                case DialogResult.Cancel:
                    base.DialogResult = DialogResult.Cancel;
                    return;

                case DialogResult.No:
                    return;
            }
            this.DB_ServerName = this.txtServerName.Text;
            this.DB_Name = win.DB_Name;
            this.DB_Table = win.DB_Table;
            base.DialogResult = DialogResult.Yes;
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
            this.btnCancel = new Button();
            this.btnNext = new Button();
            this.btnBack = new Button();
            this.label1 = new Label();
            this.txtServerName = new TextBox();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtServerName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1bd, 200);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SQL Server Parameters";
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(12, 0xdb);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnNext.Location = new Point(0x17f, 0xdb);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x4b, 0x17);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Next...";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnBack.DialogResult = DialogResult.No;
            this.btnBack.Location = new Point(0x12e, 0xdb);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(0x4b, 0x17);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 0x16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5f, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Name:";
            this.txtServerName.CharacterCasing = CharacterCasing.Upper;
            this.txtServerName.Location = new Point(0x6d, 0x16);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new Size(330, 0x16);
            this.txtServerName.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(8f, 16f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(470, 0xfe);
            base.Controls.Add(this.btnBack);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnNext);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "NewSqlDBServerWin";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "PureRF Receiver - Database connection";
            base.Load += new EventHandler(this.NewSqlDBServerWin_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void NewSqlDBServerWin_Load(object sender, EventArgs e)
        {
        }
    }
}

