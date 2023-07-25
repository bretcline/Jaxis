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
    public partial class TransactionList : UserControl
    {
        public int Count = 0;
        private List<TransactionItem> _TransactionList = new List<TransactionItem>();
        public TransactionList()
        {
            InitializeComponent();
            
        }

        public void InsertRow(Bitmap new_image, string new_beverage, string new_duration, string new_amount)
        {
            TransactionItem new_item = new TransactionItem();
            new_item.Image = new_image;
            new_item.Description = new_beverage;
            new_item.Amount = new_amount;
            new_item.Duration = new_duration;

            if (this.Count == 9)
            {
                _TransactionList.RemoveAt(0);
                --this.Count;
            }
            _TransactionList.Add(new_item);
            addRow(new_item);
            rebuildTransactionListView();
        }

        delegate void addRowDelegate(TransactionItem new_item);

        private void addRow(TransactionItem new_item)
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
                // 
                // label38
                // 
                new_label2.AutoSize = true;
                new_label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                new_label2.Location = new System.Drawing.Point(243, 720);
                new_label2.Name = new_item.Duration;
                new_label2.Size = new System.Drawing.Size(72, 26);
                new_label2.TabIndex = 1;
                new_label2.Text = new_item.Duration;

                new_label3.AutoSize = true;
                new_label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                new_label3.Location = new System.Drawing.Point(243, 720);
                new_label3.Name = new_item.Amount;
                new_label3.Size = new System.Drawing.Size(72, 26);
                new_label3.TabIndex = 1;
                new_label3.Text = new_item.Amount;

                ++this.Count;
                ++this.tableLayoutPanel1.RowCount;

                this.tableLayoutPanel1.Controls.Add(new_pictureBox, 0, this.Count);
                this.tableLayoutPanel1.Controls.Add(new_label, 1, this.Count);
                this.tableLayoutPanel1.Controls.Add(new_label2, 2, this.Count);
                this.tableLayoutPanel1.Controls.Add(new_label3, 3, this.Count);
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
                this.tableLayoutPanel1.Height += 60;
                this.Height += 60;
            }
        }

        private void delete_row(object sender, EventArgs e)
        {
            
            //MessageBox.Show((sender as Button).Tag.ToString());
            TransactionItem target_item = (TransactionItem)(sender as Button).Tag;

            _TransactionList.Remove(target_item);
            rebuildTransactionListView();

        }

        public delegate void rebuildTransactionListViewDelegate();

        private void rebuildTransactionListView()
        {
            if (tableLayoutPanel1.InvokeRequired)
            {
                tableLayoutPanel1.Invoke(new rebuildTransactionListViewDelegate(rebuildTransactionListView));
            }
            else
            {
                tableLayoutPanel1.SuspendLayout();
                tableLayoutPanel1.Controls.Clear();
                this.Count = 0;
                for (int x = _TransactionList.Count; x > 0; --x)
                {
                    addRow(_TransactionList.ElementAt(x - 1));
                }
                //foreach (TransactionItem target_item in _TransactionList)
                //{
                //    addRow(target_item);
                //}
                tableLayoutPanel1.ResumeLayout();
            }
        }
    }
}
