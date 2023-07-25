namespace ReceiverApp
{
    using PureRF;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TagsFilterWin : Form
    {
        private Button btnInvertSel;
        private Button btnOK;
        private Button btnSelectAll;
        private Button btnSelectNone;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ListView listTags;
        private bool mCheckedEventEnabled;
        private AutoGetTagsWin mParent;
        private RadioButton radioHide;
        private RadioButton radioShow;
        private ColumnHeader TagID;

        public TagsFilterWin(AutoGetTagsWin Parent)
        {
            this.InitializeComponent();
            this.mParent = Parent;
            this.mCheckedEventEnabled = false;
            foreach (uint num in this.mParent.tagsFilter.Keys)
            {
                PureRF.TagID gid = new PureRF.TagID(num);
                ListViewItem item = new ListViewItem(new string[] { gid.ToString() });
                item.Tag = num;
                item.Checked = this.mParent.tagsFilter[num];
                this.listTags.Items.Add(item);
            }
            if (this.mParent.defaultFilterState)
            {
                this.radioShow.Checked = true;
            }
            else
            {
                this.radioHide.Checked = true;
            }
        }

        private void btnInvertSel_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listTags.Items)
            {
                item.Checked = !item.Checked;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listTags.Items)
            {
                item.Checked = true;
            }
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listTags.Items)
            {
                item.Checked = false;
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
            this.btnSelectAll = new Button();
            this.btnSelectNone = new Button();
            this.btnInvertSel = new Button();
            this.listTags = new ListView();
            this.TagID = new ColumnHeader();
            this.btnOK = new Button();
            this.groupBox2 = new GroupBox();
            this.radioHide = new RadioButton();
            this.radioShow = new RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnSelectAll);
            this.groupBox1.Controls.Add(this.btnSelectNone);
            this.groupBox1.Controls.Add(this.btnInvertSel);
            this.groupBox1.Controls.Add(this.listTags);
            this.groupBox1.Location = new Point(10, 11);
            this.groupBox1.Margin = new Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(2, 2, 2, 2);
            this.groupBox1.Size = new Size(0x1b1, 0x116);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detected Tags";
            this.btnSelectAll.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnSelectAll.Location = new Point(5, 0xef);
            this.btnSelectAll.Margin = new Padding(2, 2, 2, 2);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(0x8b, 30);
            this.btnSelectAll.TabIndex = 3;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.btnSelectNone.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnSelectNone.Location = new Point(0x94, 0xef);
            this.btnSelectNone.Margin = new Padding(2, 2, 2, 2);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new Size(0x85, 30);
            this.btnSelectNone.TabIndex = 2;
            this.btnSelectNone.Text = "Select None";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new EventHandler(this.btnSelectNone_Click);
            this.btnInvertSel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnInvertSel.Location = new Point(0x11d, 0xef);
            this.btnInvertSel.Margin = new Padding(2, 2, 2, 2);
            this.btnInvertSel.Name = "btnInvertSel";
            this.btnInvertSel.Size = new Size(140, 0x1d);
            this.btnInvertSel.TabIndex = 1;
            this.btnInvertSel.Text = "Invert Selection";
            this.btnInvertSel.UseVisualStyleBackColor = true;
            this.btnInvertSel.Click += new EventHandler(this.btnInvertSel_Click);
            this.listTags.CheckBoxes = true;
            this.listTags.Columns.AddRange(new ColumnHeader[] { this.TagID });
            this.listTags.Location = new Point(5, 0x12);
            this.listTags.Margin = new Padding(2, 2, 2, 2);
            this.listTags.Name = "listTags";
            this.listTags.Size = new Size(420, 0xd9);
            this.listTags.TabIndex = 0;
            this.listTags.UseCompatibleStateImageBehavior = false;
            this.listTags.View = View.List;
            this.listTags.ItemChecked += new ItemCheckedEventHandler(this.listTags_ItemChecked);
            this.TagID.Text = "Tag ID";
            this.TagID.Width = 200;
            this.btnOK.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnOK.Location = new Point(0x174, 0x16b);
            this.btnOK.Margin = new Padding(2, 2, 2, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x43, 0x1d);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.groupBox2.Controls.Add(this.radioHide);
            this.groupBox2.Controls.Add(this.radioShow);
            this.groupBox2.Location = new Point(11, 0x125);
            this.groupBox2.Margin = new Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new Padding(2, 2, 2, 2);
            this.groupBox2.Size = new Size(0x1ac, 0x42);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Newly discovered tags";
            this.radioHide.AutoSize = true;
            this.radioHide.Location = new Point(5, 0x29);
            this.radioHide.Margin = new Padding(2, 2, 2, 2);
            this.radioHide.Name = "radioHide";
            this.radioHide.Size = new Size(0x60, 0x11);
            this.radioHide.TabIndex = 1;
            this.radioHide.TabStop = true;
            this.radioHide.Text = "Hide by default";
            this.radioHide.UseVisualStyleBackColor = true;
            this.radioHide.CheckedChanged += new EventHandler(this.radioHide_CheckedChanged);
            this.radioShow.AutoSize = true;
            this.radioShow.Location = new Point(5, 0x12);
            this.radioShow.Margin = new Padding(2, 2, 2, 2);
            this.radioShow.Name = "radioShow";
            this.radioShow.Size = new Size(0x65, 0x11);
            this.radioShow.TabIndex = 0;
            this.radioShow.TabStop = true;
            this.radioShow.Text = "Show by default";
            this.radioShow.UseVisualStyleBackColor = true;
            this.radioShow.CheckedChanged += new EventHandler(this.radioShow_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1c6, 0x193);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Margin = new Padding(2, 2, 2, 2);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "TagsFilterWin";
            base.ShowInTaskbar = false;
            this.Text = "PureRF Receiver - Tags Filter";
            base.Shown += new EventHandler(this.TagsFilterWin_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
        }

        private void listTags_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (this.mCheckedEventEnabled)
            {
                this.mParent.tagsFilter[(uint) e.Item.Tag] = e.Item.Checked;
            }
        }

        private void radioHide_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioShow.Checked)
            {
                this.mParent.defaultFilterState = true;
            }
            else
            {
                this.mParent.defaultFilterState = false;
            }
        }

        private void radioShow_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioShow.Checked)
            {
                this.mParent.defaultFilterState = true;
            }
            else
            {
                this.mParent.defaultFilterState = false;
            }
        }

        private void TagsFilterWin_Shown(object sender, EventArgs e)
        {
            this.mCheckedEventEnabled = true;
        }
    }
}

