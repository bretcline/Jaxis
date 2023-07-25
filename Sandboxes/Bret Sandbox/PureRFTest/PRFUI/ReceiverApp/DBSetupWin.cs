namespace ReceiverApp
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DBSetupWin : Form
    {
        private Button btnDisconnect;
        private Button btnNewConn;
        private Button btnOK;
        private IContainer components;
        private SaveFileDialog dlgNewDB;
        private OpenFileDialog dlgOpenDB;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label lblCurDB;
        private Label lblRecCount;
        private TagsOleDB mTagsDB;

        public DBSetupWin(TagsOleDB curDB)
        {
            this.InitializeComponent();
            this.mTagsDB = curDB;
            if (this.mTagsDB.IsOpened)
            {
                this.lblCurDB.Text = this.mTagsDB.DBName;
                this.lblRecCount.Text = this.mTagsDB.NumRecords.ToString();
                this.btnDisconnect.Enabled = true;
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            this.mTagsDB.Close();
            this.btnDisconnect.Enabled = false;
            this.lblCurDB.Text = "None";
            this.lblRecCount.Text = "0";
        }

        private void btnNewConn_Click(object sender, EventArgs e)
        {
            new NewConnWizardWin(this.mTagsDB).ShowDialog();
            if (this.mTagsDB.IsOpened)
            {
                this.lblCurDB.Text = this.mTagsDB.DBName;
                this.lblRecCount.Text = this.mTagsDB.NumRecords.ToString();
                this.btnDisconnect.Enabled = true;
            }
            else
            {
                this.lblCurDB.Text = "None";
                this.lblRecCount.Text = "0";
                this.btnDisconnect.Enabled = false;
            }
        }

        private void DBSetupWin_Load(object sender, EventArgs e)
        {
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
            this.btnOK = new Button();
            this.groupBox1 = new GroupBox();
            this.btnNewConn = new Button();
            this.lblRecCount = new Label();
            this.label2 = new Label();
            this.lblCurDB = new Label();
            this.label1 = new Label();
            this.dlgNewDB = new SaveFileDialog();
            this.dlgOpenDB = new OpenFileDialog();
            this.btnDisconnect = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x1bb, 0x74);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Close";
            this.btnOK.UseVisualStyleBackColor = true;
            this.groupBox1.Controls.Add(this.btnNewConn);
            this.groupBox1.Controls.Add(this.btnDisconnect);
            this.groupBox1.Controls.Add(this.lblRecCount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblCurDB);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1f9, 0x61);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database Settings";
            this.btnNewConn.Location = new Point(0xc7, 0x44);
            this.btnNewConn.Name = "btnNewConn";
            this.btnNewConn.Size = new Size(0x93, 0x17);
            this.btnNewConn.TabIndex = 6;
            this.btnNewConn.Text = "New connection...";
            this.btnNewConn.UseVisualStyleBackColor = true;
            this.btnNewConn.Click += new EventHandler(this.btnNewConn_Click);
            this.lblRecCount.AutoSize = true;
            this.lblRecCount.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.lblRecCount.Location = new Point(0xde, 0x2b);
            this.lblRecCount.Name = "lblRecCount";
            this.lblRecCount.Size = new Size(0x11, 0x11);
            this.lblRecCount.TabIndex = 5;
            this.lblRecCount.Text = "0";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(7, 0x2b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xd0, 0x11);
            this.label2.TabIndex = 4;
            this.label2.Text = "Number of records in database:";
            this.lblCurDB.AutoSize = true;
            this.lblCurDB.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.lblCurDB.Location = new Point(0x88, 0x16);
            this.lblCurDB.Name = "lblCurDB";
            this.lblCurDB.Size = new Size(0x2e, 0x11);
            this.lblCurDB.TabIndex = 1;
            this.lblCurDB.Text = "None";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 0x16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x7a, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current database:";
            this.dlgNewDB.DefaultExt = "mdb";
            this.dlgNewDB.Filter = "Access Database files (*.mdb)|*.mdb";
            this.dlgNewDB.Title = "Create new database...";
            this.dlgOpenDB.DefaultExt = "mdb";
            this.dlgOpenDB.Filter = "Access Database files (*.mdb)|*.mdb";
            this.dlgOpenDB.Title = "Open existing database...";
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new Point(0x160, 0x44);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new Size(0x93, 0x17);
            this.btnDisconnect.TabIndex = 6;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new EventHandler(this.btnDisconnect_Click);
            base.AutoScaleDimensions = new SizeF(8f, 16f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(530, 150);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DBSetupWin";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "PureRF Receiver - DB Setup";
            base.Load += new EventHandler(this.DBSetupWin_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }
    }
}

