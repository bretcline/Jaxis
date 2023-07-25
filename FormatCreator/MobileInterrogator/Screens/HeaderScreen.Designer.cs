namespace MobileInterrogator
{
    partial class HeaderScreen
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
            this.btnEdit = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.SuspendLayout();
            // 
            // pickListPanel
            // 
            this.pickListPanel.Location = new System.Drawing.Point(2, 2);
            this.pickListPanel.Size = new System.Drawing.Size(236, 208);
            this.pickListPanel.Name = "pickListPanel";
            // 
            // btnEdit
            // 
            this.btnEdit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnEdit.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btnEdit.ForeColorDown = System.Drawing.Color.Black;
            this.btnEdit.ShowIcon = true;
            this.btnEdit.IconSize = new System.Drawing.Size(36, 36);
            this.btnEdit.Icon = IconResources.Edit;
            this.btnEdit.IconDisabled = IconResources.Edit_Disabled;
            this.btnEdit.IconAlign = LFI.Mobile.Controls.Align.Center;
            this.btnEdit.Location = new System.Drawing.Point(184, 212);
            this.btnEdit.Size = new System.Drawing.Size(48, 48);
            this.btnEdit.Margin = 0;
            this.btnEdit.ItemShiftOnClick = true;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Text = "";            
            this.btnEdit.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btnEdit.TextAlignIsRelative = true;
            this.btnEdit.UseTransparency = true;
            this.btnEdit.Click += new System.EventHandler(this.OnEditValueClick);
            // 
            // FormatScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(380, 392);
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Controls.Add(this.pickListPanel);
            this.Controls.Add(this.btnEdit);
            this.Name = "HomeScreen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pickListPanel;
        private LFI.Mobile.Controls.Button.SkinnedButton btnEdit;

    }
}