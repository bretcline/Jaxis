using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BevClasses;

namespace BevCartUI
{
    //public delegate void notifyParentCallbackType(int x, int y);

    public partial class InventoryList : UserControl
    {
        public event EventHandler deleteBottleFromInventry;

        //public notifyParentCallbackType notifyParent = null;
        private int nInventoryCount = 0;
        private List<InventoryItem> _InventoryList = new List<InventoryItem>();
        public InventoryList()
        {
            InitializeComponent();
            
        }

      /*  protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                return cp;
            }
        }*/
        public void InsertRow(Bottle this_bottle)
        {

            InventoryItem new_item = new InventoryItem();
            if (this_bottle.Beverage != null)
            {
                new_item.Image = new Bitmap(this_bottle.Beverage.PicFile);
                new_item.Description = this_bottle.Beverage.Label;
                new_item.Quantity = this_bottle.QuantityLeft.ToString();
                new_item.Tag = this_bottle;

                _InventoryList.Add(new_item);
                addRow(new_item);
            }
        }

        private void addRow(InventoryItem new_item)
        {

            System.Windows.Forms.PictureBox new_pictureBox = new System.Windows.Forms.PictureBox();
            System.Windows.Forms.Label new_label = new System.Windows.Forms.Label();
            System.Windows.Forms.Label new_label2 = new System.Windows.Forms.Label();
            Button new_button = new Button();
            // 
            // pictureBox19
            // 
            new_pictureBox.BackColor = System.Drawing.Color.White;
            new_pictureBox.BackgroundImage = new_item.Image;
            new_pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            new_pictureBox.Location = new System.Drawing.Point(3, 723);
            new_pictureBox.Name = new_item.Description;
            new_pictureBox.Size = new System.Drawing.Size(34, 34);
            new_pictureBox.TabIndex = 0;
            new_pictureBox.TabStop = false;
            // 
            // label22
            // 
            new_label.AutoSize = true;
            new_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            new_label.Location = new System.Drawing.Point(43, 720);
            new_label.Name = new_item.Description;
            new_label.Size = new System.Drawing.Size(136, 26);
            new_label.TabIndex = 1;
            new_label.Text = new_item.Description;
            new_label.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tableLayoutPanel1_MouseMove);
            new_label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tableLayoutPanel1_MouseDown);
            new_label.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tableLayoutPanel1_MouseUp);
            // 
            // label38
            // 
            new_label2.AutoSize = true;
            new_label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            new_label2.Location = new System.Drawing.Point(243, 720);
            new_label2.Name = new_item.Quantity;
            new_label2.Size = new System.Drawing.Size(72, 26);
            new_label2.TabIndex = 1;
            new_label2.Text = new_item.Quantity;

            new_button.Height = 40;
            new_button.Width = 80;
            new_button.Text = "Remove";
            new_button.Location = new Point(100, 0);
            new_button.Click += new System.EventHandler(this.delete_row);
            new_button.Tag = new_item;

            ++nInventoryCount;
            ++this.tableLayoutPanel1.RowCount;

            this.tableLayoutPanel1.Controls.Add(new_pictureBox, 0, nInventoryCount);
            this.tableLayoutPanel1.Controls.Add(new_label, 1, nInventoryCount);
            this.tableLayoutPanel1.Controls.Add(new_label2, 2, nInventoryCount);
            this.tableLayoutPanel1.Controls.Add(new_button, 3, nInventoryCount);
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Height += 60;
            this.Height += 60; 
        }

        private void delete_row(object sender, EventArgs e)
        {
            
            //MessageBox.Show((sender as Button).Tag.ToString());
            InventoryItem target_item = (InventoryItem)(sender as Button).Tag;
            if (deleteBottleFromInventry != null)
                deleteBottleFromInventry(target_item.Tag, new EventArgs());
            _InventoryList.Remove(target_item);
            rebuildInventoryListView();

        }

        public void Clear()
        {
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.Controls.Clear();
            _InventoryList.Clear();
            nInventoryCount = 0;
            tableLayoutPanel1.ResumeLayout();
        }

        private void rebuildInventoryListView()
        {
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.Controls.Clear();
            nInventoryCount = 0;
            foreach (InventoryItem target_item in _InventoryList)
            {
                addRow(target_item);
            }
            tableLayoutPanel1.ResumeLayout();
        }

        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void tableLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void tableLayoutPanel1_MouseUp(object sender, MouseEventArgs e)
        {

        }

    }
}
