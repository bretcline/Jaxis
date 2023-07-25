namespace ReceiverApp
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    public class NewConnWizardWin : Form
    {
        private Button btnCancel;
        private Button btnNext;
        private IContainer components;
        private SaveFileDialog dlgNewDB;
        private OpenFileDialog dlgOpenDB;
        private OpenFileDialog dlgOpenSQLConn;
        private SaveFileDialog dlgSaveSQLConn;
        private GroupBox groupBox1;
        private TagsOleDB mTagsDB;
        private RadioButton radioExistingAccessDB;
        private RadioButton radioExistingSQL;
        private RadioButton radioNewAccessDB;
        private RadioButton radioNewSQL;

        public NewConnWizardWin(TagsOleDB tagsDB)
        {
            this.InitializeComponent();
            this.mTagsDB = tagsDB;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.radioNewAccessDB.Checked)
            {
                if (this.NewAccessDB())
                {
                    base.DialogResult = DialogResult.OK;
                }
            }
            else if (this.radioExistingAccessDB.Checked)
            {
                if (this.ExistingAccessDB())
                {
                    base.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                if (this.radioNewSQL.Checked)
                {
                    this.StartNewSQLWizard();
                }
                if (this.radioExistingSQL.Checked && this.OpenSQLConn())
                {
                    base.DialogResult = DialogResult.OK;
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

        public bool ExistingAccessDB()
        {
            if (this.dlgOpenDB.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            return this.OpenAccessDB(this.dlgOpenDB.FileName);
        }

        private void InitializeComponent()
        {
            this.btnNext = new Button();
            this.btnCancel = new Button();
            this.groupBox1 = new GroupBox();
            this.radioExistingSQL = new RadioButton();
            this.radioNewSQL = new RadioButton();
            this.radioExistingAccessDB = new RadioButton();
            this.radioNewAccessDB = new RadioButton();
            this.dlgOpenDB = new OpenFileDialog();
            this.dlgNewDB = new SaveFileDialog();
            this.dlgSaveSQLConn = new SaveFileDialog();
            this.dlgOpenSQLConn = new OpenFileDialog();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.btnNext.Location = new Point(0x11f, 0xb2);
            this.btnNext.Margin = new Padding(2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x38, 0x18);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "Next...";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(9, 0xb2);
            this.btnCancel.Margin = new Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.groupBox1.Controls.Add(this.radioExistingSQL);
            this.groupBox1.Controls.Add(this.radioNewSQL);
            this.groupBox1.Controls.Add(this.radioExistingAccessDB);
            this.groupBox1.Controls.Add(this.radioNewAccessDB);
            this.groupBox1.Location = new Point(10, 11);
            this.groupBox1.Margin = new Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(2);
            this.groupBox1.Size = new Size(0x14e, 0xa2);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection Type";
            this.radioExistingSQL.AutoSize = true;
            this.radioExistingSQL.Location = new Point(5, 0x56);
            this.radioExistingSQL.Margin = new Padding(2);
            this.radioExistingSQL.Name = "radioExistingSQL";
            this.radioExistingSQL.Size = new Size(0x8d, 0x11);
            this.radioExistingSQL.TabIndex = 3;
            this.radioExistingSQL.TabStop = true;
            this.radioExistingSQL.Text = "Existing SQL connection";
            this.radioExistingSQL.UseVisualStyleBackColor = true;
            this.radioNewSQL.AutoSize = true;
            this.radioNewSQL.Location = new Point(5, 0x3f);
            this.radioNewSQL.Margin = new Padding(2);
            this.radioNewSQL.Name = "radioNewSQL";
            this.radioNewSQL.Size = new Size(0x7f, 0x11);
            this.radioNewSQL.TabIndex = 2;
            this.radioNewSQL.TabStop = true;
            this.radioNewSQL.Text = "New SQL connection";
            this.radioNewSQL.UseVisualStyleBackColor = true;
            this.radioExistingAccessDB.AutoSize = true;
            this.radioExistingAccessDB.Location = new Point(5, 0x29);
            this.radioExistingAccessDB.Margin = new Padding(2);
            this.radioExistingAccessDB.Name = "radioExistingAccessDB";
            this.radioExistingAccessDB.Size = new Size(0x99, 0x11);
            this.radioExistingAccessDB.TabIndex = 1;
            this.radioExistingAccessDB.TabStop = true;
            this.radioExistingAccessDB.Text = "Existing ACCESS database";
            this.radioExistingAccessDB.UseVisualStyleBackColor = true;
            this.radioNewAccessDB.AutoSize = true;
            this.radioNewAccessDB.Checked = true;
            this.radioNewAccessDB.Location = new Point(5, 0x12);
            this.radioNewAccessDB.Margin = new Padding(2);
            this.radioNewAccessDB.Name = "radioNewAccessDB";
            this.radioNewAccessDB.Size = new Size(0x8b, 0x11);
            this.radioNewAccessDB.TabIndex = 0;
            this.radioNewAccessDB.TabStop = true;
            this.radioNewAccessDB.Text = "New ACCESS database";
            this.radioNewAccessDB.UseVisualStyleBackColor = true;
            this.dlgOpenDB.DefaultExt = "mdb";
            this.dlgOpenDB.Filter = "Access Database files (*.mdb)|*.mdb";
            this.dlgOpenDB.Title = "Open existing database...";
            this.dlgNewDB.DefaultExt = "mdb";
            this.dlgNewDB.Filter = "Access Database files (*.mdb)|*.mdb";
            this.dlgNewDB.Title = "Create new database...";
            this.dlgSaveSQLConn.DefaultExt = "xml";
            this.dlgSaveSQLConn.Filter = "SQL Connections (*.xml)|*.xml";
            this.dlgSaveSQLConn.Title = "Save connection";
            this.dlgOpenSQLConn.DefaultExt = "xml";
            this.dlgOpenSQLConn.Filter = "SQL Connections (*.xml)|*.xml";
            this.dlgOpenSQLConn.Title = "Open SQL connection";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x160, 0xd5);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnNext);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Margin = new Padding(2);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "NewConnWizardWin";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "PureRF Receiver - Database connection";
            base.Load += new EventHandler(this.NewConnWizardWin_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        public bool NewAccessDB()
        {
            if (this.dlgNewDB.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "PureRF.mdb");
            if (!File.Exists(path))
            {
                MessageBox.Show("Missing DB template file: " + path, "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            try
            {
                File.Copy(path, this.dlgNewDB.FileName, true);
            }
            catch
            {
                MessageBox.Show("Unable to copy template file into " + this.dlgNewDB.FileName, "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            return this.OpenAccessDB(this.dlgNewDB.FileName);
        }

        private void NewConnWizardWin_Load(object sender, EventArgs e)
        {
            this.dlgNewDB.InitialDirectory = Application.ExecutablePath;
            this.dlgOpenDB.InitialDirectory = Application.ExecutablePath;
            this.dlgOpenSQLConn.InitialDirectory = Application.ExecutablePath;
            this.dlgSaveSQLConn.InitialDirectory = Application.ExecutablePath;
        }

        public bool OpenAccessDB(string Filename)
        {
            this.mTagsDB.Close();
            if (!this.mTagsDB.OpenAccess(Filename))
            {
                MessageBox.Show("Unable to open database: " + Filename, "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            return true;
        }

        public bool OpenSQLConn()
        {
            SQLConn conn = new SQLConn();
            this.mTagsDB.Close();
            if (this.dlgOpenSQLConn.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SQLConn));
                TextReader textReader = new StreamReader(this.dlgOpenSQLConn.FileName);
                conn = (SQLConn) serializer.Deserialize(textReader);
            }
            catch
            {
                MessageBox.Show("Unable to open/read connection file", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            if (!this.mTagsDB.OpenSQL(conn.ServerName, conn.DBName, conn.Table))
            {
                MessageBox.Show("Unable to connect to SQL server", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            return true;
        }

        public bool SaveSQLConn(string ServerName, string DBName, string Table)
        {
            SQLConn o = new SQLConn();
            o.ServerName = ServerName;
            o.DBName = DBName;
            o.Table = Table;
            if (this.dlgSaveSQLConn.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SQLConn));
                    TextWriter textWriter = new StreamWriter(this.dlgSaveSQLConn.FileName);
                    serializer.Serialize(textWriter, o);
                    textWriter.Close();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        private void StartNewSQLWizard()
        {
            NewSqlDBServerWin win = new NewSqlDBServerWin();
            switch (win.ShowDialog())
            {
                case DialogResult.Cancel:
                    base.DialogResult = DialogResult.Cancel;
                    break;

                case DialogResult.Yes:
                    if (!this.mTagsDB.OpenSQL(win.DB_ServerName, win.DB_Name, win.DB_Table))
                    {
                        MessageBox.Show("Connection to SQL server failed", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        if (MessageBox.Show("Would you like to save the newly created connection?", "PureRF Receiver", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.SaveSQLConn(win.DB_ServerName, win.DB_Name, win.DB_Table);
                        }
                        base.DialogResult = DialogResult.OK;
                    }
                    break;
            }
        }

        public class SQLConn
        {
            public string DBName;
            public string ServerName;
            public string Table;
        }
    }
}

