namespace ReceiverApp
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Data.OleDb;
    using System.Drawing;
    using System.Windows.Forms;

    public class NewSqlDBTableWin : Form
    {
        private Button btnBack;
        private Button btnCancel;
        private Button btnNext;
        private ComboBox comboDBName;
        private ComboBox comboTable;
        private IContainer components;
        public string DB_Name;
        public string DB_Table;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblServerName;
        private OleDbConnection mConn;
        private string mServerName;

        public NewSqlDBTableWin(string ServerName)
        {
            this.InitializeComponent();
            this.mServerName = ServerName;
            this.lblServerName.Text = ServerName;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.comboTable.SelectedItem == null)
            {
                MessageBox.Show("Please select a database and a table in order to proceed", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.DB_Name = this.comboDBName.Text;
                this.DB_Table = this.comboTable.Text;
                base.DialogResult = DialogResult.Yes;
            }
        }

        private void comboDBName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] tablesList = this.GetTablesList(this.comboDBName.Text);
            this.comboTable.Items.Clear();
            foreach (string str in tablesList)
            {
                this.comboTable.Items.Add(str);
            }
            if (tablesList.Length > 0)
            {
                this.comboTable.SelectedIndex = 0;
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

        public string[] GetDBList()
        {
            OleDbDataReader reader = new OleDbCommand("Exec SP_DATABASES", this.mConn).ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                list.Add(reader.GetString(0));
            }
            string[] array = new string[list.Count];
            list.CopyTo(array);
            return array;
        }

        public string[] GetTablesList(string DBName)
        {
            this.mConn.ChangeDatabase(DBName);
            object[] restrictions = new object[4];
            restrictions[3] = "TABLE";
            DataTable oleDbSchemaTable = this.mConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions);
            string[] strArray = new string[oleDbSchemaTable.Rows.Count];
            for (int i = 0; i < strArray.Length; i++)
            {
                strArray[i] = oleDbSchemaTable.Rows[i].ItemArray[2].ToString();
            }
            return strArray;
        }

        private void InitializeComponent()
        {
            this.btnBack = new Button();
            this.groupBox1 = new GroupBox();
            this.label1 = new Label();
            this.btnCancel = new Button();
            this.btnNext = new Button();
            this.lblServerName = new Label();
            this.label2 = new Label();
            this.comboDBName = new ComboBox();
            this.comboTable = new ComboBox();
            this.label3 = new Label();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.btnBack.DialogResult = DialogResult.No;
            this.btnBack.Location = new Point(0x12d, 0xda);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(0x4b, 0x17);
            this.btnBack.TabIndex = 10;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboTable);
            this.groupBox1.Controls.Add(this.comboDBName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblServerName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1bd, 200);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SQL Server Parameters";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 0x16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5f, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Name:";
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(11, 0xda);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnNext.Location = new Point(0x17e, 0xda);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x4b, 0x17);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "Next...";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.lblServerName.AutoSize = true;
            this.lblServerName.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.lblServerName.Location = new Point(0x6d, 0x16);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new Size(0x71, 0x11);
            this.lblServerName.TabIndex = 1;
            this.lblServerName.Text = "SERVERNAME";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(10, 0x2b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x49, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database:";
            this.comboDBName.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboDBName.FormattingEnabled = true;
            this.comboDBName.Location = new Point(90, 0x2b);
            this.comboDBName.Name = "comboDBName";
            this.comboDBName.Size = new Size(0xeb, 0x18);
            this.comboDBName.TabIndex = 3;
            this.comboDBName.SelectedIndexChanged += new EventHandler(this.comboDBName_SelectedIndexChanged);
            this.comboTable.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboTable.FormattingEnabled = true;
            this.comboTable.Location = new Point(90, 0x4a);
            this.comboTable.Name = "comboTable";
            this.comboTable.Size = new Size(0xeb, 0x18);
            this.comboTable.TabIndex = 4;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(13, 0x4a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x30, 0x11);
            this.label3.TabIndex = 5;
            this.label3.Text = "Table:";
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
            base.Name = "NewSqlDBTableWin";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "PureRF Receiver - Database connection";
            base.Shown += new EventHandler(this.NewSqlDBTableWin_Shown);
            base.Load += new EventHandler(this.NewSqlDBTableWin_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void NewSqlDBTableWin_Load(object sender, EventArgs e)
        {
        }

        private void NewSqlDBTableWin_Shown(object sender, EventArgs e)
        {
            this.mConn = new OleDbConnection(string.Format("Provider=SQLOLEDB;Data Source={0};Integrated Security=SSPI;", this.mServerName));
            try
            {
                this.mConn.Open();
            }
            catch (OleDbException exception)
            {
                MessageBox.Show(string.Format("Unable to connect to database {0}:\n{1}", this.mServerName, exception.Message));
                base.DialogResult = DialogResult.No;
                return;
            }
            foreach (string str in this.GetDBList())
            {
                this.comboDBName.Items.Add(str);
            }
        }
    }
}

