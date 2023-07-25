namespace MobileInterrogator
{
    partial class HomeScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeScreen));
            this.btnUploadData = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btnCreateTag = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btnScanTag = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.SuspendLayout();
            // 
            // btnUploadData
            // 
            this.btnUploadData.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold); 
            this.btnUploadData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUploadData.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btnUploadData.ForeColorDown = System.Drawing.Color.Black;
            this.btnUploadData.Location = new System.Drawing.Point(4, 128);
            this.btnUploadData.Size = new System.Drawing.Size(232, 40);
            this.btnUploadData.Margin = 0;
            this.btnUploadData.ItemShiftOnClick = true;
            this.btnUploadData.Name = "btnUploadData";
            this.btnUploadData.Text = "Upload Data";
            this.btnUploadData.ShowIcon = false;
            this.btnUploadData.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btnUploadData.TextAlignIsRelative = true;
            this.btnUploadData.UseTransparency = true;
            this.btnUploadData.Click += new System.EventHandler(this.OnUploadDataClick);
            this.btnUploadData.Enabled = false;
            // 
            // btnCreateTag
            // 
            this.btnCreateTag.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold); 
            this.btnCreateTag.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCreateTag.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btnCreateTag.ForeColorDown = System.Drawing.Color.Black;
            this.btnCreateTag.Location = new System.Drawing.Point(4, 72);
            this.btnCreateTag.Size = new System.Drawing.Size(232, 40);
            this.btnCreateTag.Margin = 0;
            this.btnCreateTag.ItemShiftOnClick = true;
            this.btnCreateTag.Name = "btnCreateTag";
            this.btnCreateTag.Text = "Create Tag";
            this.btnCreateTag.ShowIcon = false;
            this.btnCreateTag.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btnCreateTag.TextAlignIsRelative = true;
            this.btnCreateTag.UseTransparency = true;
            this.btnCreateTag.Click += new System.EventHandler(this.OnCreateTagClick);
            // 
            // btnScanTag
            // 
            this.btnScanTag.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold); 
            this.btnScanTag.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnScanTag.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btnScanTag.ForeColorDown = System.Drawing.Color.Black;
            this.btnScanTag.Location = new System.Drawing.Point(4, 16);
            this.btnScanTag.Size = new System.Drawing.Size(232, 40);
            this.btnScanTag.Margin = 0;
            this.btnScanTag.ItemShiftOnClick = true;
            this.btnScanTag.Name = "btnScanTag";
            this.btnScanTag.Text = "Scan Tag";
            this.btnScanTag.ShowIcon = false;
            this.btnScanTag.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btnScanTag.TextAlignIsRelative = true;
            this.btnScanTag.UseTransparency = true;
            this.btnScanTag.Click += new System.EventHandler(this.OnScanTagClick);
            // 
            // HomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(380, 392);
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Controls.Add(this.btnUploadData);
            this.Controls.Add(this.btnCreateTag);
            this.Controls.Add(this.btnScanTag);
            this.Name = "HomeScreen";
            this.ResumeLayout(false);

        }

        #endregion

        private LFI.Mobile.Controls.Button.SkinnedButton btnScanTag;
        private LFI.Mobile.Controls.Button.SkinnedButton btnCreateTag;
        private LFI.Mobile.Controls.Button.SkinnedButton btnUploadData;

    }
}