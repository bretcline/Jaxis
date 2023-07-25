namespace PourBucketCollector
{
    partial class frmTestOutputDispaly
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
            this.txtTestDisplay = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtTestDisplay
            // 
            this.txtTestDisplay.Location = new System.Drawing.Point(13, 13);
            this.txtTestDisplay.Multiline = true;
            this.txtTestDisplay.Name = "txtTestDisplay";
            this.txtTestDisplay.Size = new System.Drawing.Size(352, 338);
            this.txtTestDisplay.TabIndex = 0;
            // 
            // frmTestOutputDispaly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 363);
            this.Controls.Add(this.txtTestDisplay);
            this.Name = "frmTestOutputDispaly";
            this.Text = "TestOutputDisplay";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTestOutputDispaly_FormClosed);
            this.Load += new System.EventHandler(this.frmTestOutputDispaly_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTestDisplay;

    }
}

