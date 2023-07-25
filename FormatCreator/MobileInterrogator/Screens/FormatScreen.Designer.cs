namespace MobileInterrogator
{
    partial class FormatScreen
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
            this.pickListPanel = new System.Windows.Forms.Panel(); 
            this.btnCreateTag = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.SuspendLayout();
            // 
            // pickListPanel
            // 
            this.pickListPanel.Location = new System.Drawing.Point(2, 2);
            this.pickListPanel.Size = new System.Drawing.Size(236, 216);
            this.pickListPanel.Name = "pickListPanel";
            // 
            // btnCreateTag
            // 
            this.btnCreateTag.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold); 
            this.btnCreateTag.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCreateTag.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btnCreateTag.ForeColorDown = System.Drawing.Color.Black;
            this.btnCreateTag.Location = new System.Drawing.Point(4, 220);
            this.btnCreateTag.Size = new System.Drawing.Size(232, 40);
            this.btnCreateTag.Margin = 0;
            this.btnCreateTag.ItemShiftOnClick = true;
            this.btnCreateTag.Name = "btnCreateTag";
            this.btnCreateTag.Text = "Use Format";
            this.btnCreateTag.ShowIcon = false;
            this.btnCreateTag.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btnCreateTag.TextAlignIsRelative = true;
            this.btnCreateTag.UseTransparency = true;
            this.btnCreateTag.Click += new System.EventHandler(this.OnCreateTagClick);
            // 
            // FormatScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(380, 392);
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Controls.Add(this.pickListPanel);
            this.Controls.Add(this.btnCreateTag);
            this.Name = "HomeScreen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pickListPanel;
        private LFI.Mobile.Controls.Button.SkinnedButton btnCreateTag;

    }
}