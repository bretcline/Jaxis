using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BevCartUI
{
    public partial class BeverageList : UserControl
    {
        public event EventHandler BeverageSelected;

        public int Count = 0;
        public string SelectedBeverage { get; set; } 
        private List<BeverageItem> _BeverageList = new List<BeverageItem>();
        public BeverageList()
        {
            InitializeComponent();
            
        }

        public void InsertRow(Bitmap new_image, string new_beverage, Guid beverage_id)
        {
            BeverageItem new_item = new BeverageItem();
            new_item.Image = new_image;
            new_item.Description = new_beverage;
            new_item.BeverageID = beverage_id;

            _BeverageList.Add(new_item);
            addRow(new_item);
            //rebuildBeverageListView();
        }

        delegate void addRowDelegate(BeverageItem new_item);

        private void addRow(BeverageItem new_item)
        {
            if (this.tableLayoutPanel1.InvokeRequired)
            {
                this.tableLayoutPanel1.Invoke(new addRowDelegate(addRow), new_item);
            }
            else
            {

                System.Windows.Forms.PictureBox new_pictureBox = new System.Windows.Forms.PictureBox();
                System.Windows.Forms.Label new_label = new System.Windows.Forms.Label();
                System.Windows.Forms.Label new_label2 = new System.Windows.Forms.Label();
                System.Windows.Forms.Label new_label3 = new System.Windows.Forms.Label();
                Button new_button = new Button();

                new_pictureBox.BackColor = System.Drawing.Color.White;
                new_pictureBox.BackgroundImage = new_item.Image;
                new_pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                //new_pictureBox.Location = new System.Drawing.Point(3, 723);
                new_pictureBox.Name = new_item.Description;
                new_pictureBox.Size = new System.Drawing.Size(100, 100);
                new_pictureBox.TabIndex = 0;
                new_pictureBox.TabStop = false;
                new_pictureBox.Tag = new_item.BeverageID.ToString();
                new_pictureBox.DoubleClick += new EventHandler(new_pictureBox_DoubleClick);

                new_label.AutoSize = true;
                new_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //new_label.Location = new System.Drawing.Point(43, 720);
                new_label.Name = new_item.Description;
                new_label.Size = new System.Drawing.Size(136, 26);
                new_label.TabIndex = 1;
                new_label.Text = new_item.Description;

                ++this.Count;
                ++this.tableLayoutPanel1.RowCount;

                this.tableLayoutPanel1.Controls.Add(new_pictureBox, 0, this.Count);
                this.tableLayoutPanel1.Controls.Add(new_label, 1, this.Count);
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
                this.tableLayoutPanel1.Height += 100;
                this.Height += 100;
            }
        }

        void new_pictureBox_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show(((sender as PictureBox).Tag as string));
            SelectedBeverage = ((sender as PictureBox).Tag as string);
            BeverageSelected(this, new EventArgs());
        }

        private void delete_row(object sender, EventArgs e)
        {
            
            BeverageItem target_item = (BeverageItem)(sender as Button).Tag;

            _BeverageList.Remove(target_item);
            rebuildBeverageListView();

        }

        public void Clear()
        {
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.Controls.Clear();
            this.Count = 0;
            tableLayoutPanel1.ResumeLayout();
        }

        public delegate void rebuildBeverageListViewDelegate();

        private void rebuildBeverageListView()
        {
            if (tableLayoutPanel1.InvokeRequired)
            {
                tableLayoutPanel1.Invoke(new rebuildBeverageListViewDelegate(rebuildBeverageListView));
            }
            else
            {
                tableLayoutPanel1.SuspendLayout();
                tableLayoutPanel1.Controls.Clear();
                foreach( BeverageItem this_item in _BeverageList)
                {
                    addRow(this_item);
                }
                tableLayoutPanel1.ResumeLayout();
            }
        }

    }
}
