namespace FileCompare
{
    partial class frmFileCompare
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ofdFiles = new System.Windows.Forms.OpenFileDialog();
            this.txtFileOne = new System.Windows.Forms.TextBox();
            this.btnLoadFileOne = new System.Windows.Forms.Button();
            this.btnLoadFileTwo = new System.Windows.Forms.Button();
            this.txtFileTwo = new System.Windows.Forms.TextBox();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnColumns = new System.Windows.Forms.Button();
            this.clbColumns = new System.Windows.Forms.CheckedListBox();
            this.lstDifferences = new System.Windows.Forms.ListBox();
            this.lblLineDifferences = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ofdFiles
            // 
            this.ofdFiles.FileName = "openFileDialog1";
            // 
            // txtFileOne
            // 
            this.txtFileOne.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileOne.Location = new System.Drawing.Point(13, 13);
            this.txtFileOne.Name = "txtFileOne";
            this.txtFileOne.Size = new System.Drawing.Size(921, 20);
            this.txtFileOne.TabIndex = 0;
            // 
            // btnLoadFileOne
            // 
            this.btnLoadFileOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadFileOne.Location = new System.Drawing.Point(941, 11);
            this.btnLoadFileOne.Name = "btnLoadFileOne";
            this.btnLoadFileOne.Size = new System.Drawing.Size(23, 23);
            this.btnLoadFileOne.TabIndex = 1;
            this.btnLoadFileOne.Text = "...";
            this.btnLoadFileOne.UseVisualStyleBackColor = true;
            this.btnLoadFileOne.Click += new System.EventHandler(this.btnLoadFileOne_Click);
            // 
            // btnLoadFileTwo
            // 
            this.btnLoadFileTwo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadFileTwo.Location = new System.Drawing.Point(941, 37);
            this.btnLoadFileTwo.Name = "btnLoadFileTwo";
            this.btnLoadFileTwo.Size = new System.Drawing.Size(23, 23);
            this.btnLoadFileTwo.TabIndex = 3;
            this.btnLoadFileTwo.Text = "...";
            this.btnLoadFileTwo.UseVisualStyleBackColor = true;
            this.btnLoadFileTwo.Click += new System.EventHandler(this.btnLoadFileTwo_Click);
            // 
            // txtFileTwo
            // 
            this.txtFileTwo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileTwo.Location = new System.Drawing.Point(13, 39);
            this.txtFileTwo.Name = "txtFileTwo";
            this.txtFileTwo.Size = new System.Drawing.Size(921, 20);
            this.txtFileTwo.TabIndex = 2;
            // 
            // btnCompare
            // 
            this.btnCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompare.Location = new System.Drawing.Point(889, 66);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(75, 23);
            this.btnCompare.TabIndex = 4;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // btnColumns
            // 
            this.btnColumns.Location = new System.Drawing.Point(13, 65);
            this.btnColumns.Name = "btnColumns";
            this.btnColumns.Size = new System.Drawing.Size(75, 23);
            this.btnColumns.TabIndex = 5;
            this.btnColumns.Text = "Get Columns";
            this.btnColumns.UseVisualStyleBackColor = true;
            this.btnColumns.Click += new System.EventHandler(this.btnColumns_Click);
            // 
            // clbColumns
            // 
            this.clbColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.clbColumns.CheckOnClick = true;
            this.clbColumns.FormattingEnabled = true;
            this.clbColumns.Location = new System.Drawing.Point(13, 94);
            this.clbColumns.Name = "clbColumns";
            this.clbColumns.Size = new System.Drawing.Size(120, 559);
            this.clbColumns.TabIndex = 6;
            // 
            // lstDifferences
            // 
            this.lstDifferences.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDifferences.FormattingEnabled = true;
            this.lstDifferences.Location = new System.Drawing.Point(140, 94);
            this.lstDifferences.Name = "lstDifferences";
            this.lstDifferences.Size = new System.Drawing.Size(825, 563);
            this.lstDifferences.TabIndex = 7;
            // 
            // lblLineDifferences
            // 
            this.lblLineDifferences.AutoSize = true;
            this.lblLineDifferences.Location = new System.Drawing.Point(140, 75);
            this.lblLineDifferences.Name = "lblLineDifferences";
            this.lblLineDifferences.Size = new System.Drawing.Size(35, 13);
            this.lblLineDifferences.TabIndex = 8;
            this.lblLineDifferences.Text = "label1";
            // 
            // frmFileCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 666);
            this.Controls.Add(this.lblLineDifferences);
            this.Controls.Add(this.lstDifferences);
            this.Controls.Add(this.clbColumns);
            this.Controls.Add(this.btnColumns);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.btnLoadFileTwo);
            this.Controls.Add(this.txtFileTwo);
            this.Controls.Add(this.btnLoadFileOne);
            this.Controls.Add(this.txtFileOne);
            this.Name = "frmFileCompare";
            this.Text = "File Compare";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdFiles;
        private System.Windows.Forms.TextBox txtFileOne;
        private System.Windows.Forms.Button btnLoadFileOne;
        private System.Windows.Forms.Button btnLoadFileTwo;
        private System.Windows.Forms.TextBox txtFileTwo;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button btnColumns;
        private System.Windows.Forms.CheckedListBox clbColumns;
        private System.Windows.Forms.ListBox lstDifferences;
        private System.Windows.Forms.Label lblLineDifferences;
    }
}

