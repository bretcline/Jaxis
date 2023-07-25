namespace MobileInterrogator
{
    partial class MainForm
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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.btnBackspace = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.SuspendLayout();
            // 
            // contentPanel
            // 
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(240, 268);
            // 
            // btnBackspace 
            // 
            this.btnBackspace.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBackspace.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btnBackspace.ForeColorDown = System.Drawing.Color.Black;
            this.btnBackspace.ShowIcon = true;
            this.btnBackspace.IconSize = new System.Drawing.Size(24, 24);
            this.btnBackspace.Icon = IconResources.Backspace;
            this.btnBackspace.IconDisabled = IconResources.Backspace;
            this.btnBackspace.IconAlign = LFI.Mobile.Controls.Align.Center;
            this.btnBackspace.Location = new System.Drawing.Point(4, 40);
            this.btnBackspace.Size = new System.Drawing.Size(48, 48);
            this.btnBackspace.ItemShiftOnClick = true;
            this.btnBackspace.Name = "btnBackspace";
            this.btnBackspace.Text = "";
            this.btnBackspace.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btnBackspace.UseTransparency = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.btnBackspace);
            this.KeyPreview = true;
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Mobile Interrogator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.MainMenu mainMenu;

        private LFI.Mobile.Controls.Button.SkinnedButton btnBackspace;
    }
}