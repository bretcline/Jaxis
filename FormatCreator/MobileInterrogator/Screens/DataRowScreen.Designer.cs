namespace MobileInterrogator
{
    partial class DataRowScreen
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
            this.btnPrev = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btnNext = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btnNew = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btnEdit = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.SuspendLayout();
            // 
            // pickListPanel
            // 
            this.pickListPanel.Location = new System.Drawing.Point(2, 2);
            this.pickListPanel.Size = new System.Drawing.Size(236, 208);
            this.pickListPanel.Name = "pickListPanel";
            // 
            // btnPrev
            // 
            this.btnPrev.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPrev.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btnPrev.ForeColorDown = System.Drawing.Color.Black;
            this.btnPrev.ShowIcon = true;
            this.btnPrev.IconSize = new System.Drawing.Size(36, 36);
            this.btnPrev.Icon = IconResources.Move_Previous;
            this.btnPrev.IconDisabled = IconResources.Move_Previous_Disabled;
            this.btnPrev.IconAlign = LFI.Mobile.Controls.Align.Center;
            this.btnPrev.Location = new System.Drawing.Point(8, 212);
            this.btnPrev.Size = new System.Drawing.Size(48, 48);
            this.btnPrev.Margin = 0;
            this.btnPrev.ItemShiftOnClick = true;
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Text = "";            
            this.btnPrev.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btnPrev.TextAlignIsRelative = true;
            this.btnPrev.UseTransparency = true;
            this.btnPrev.Click += new System.EventHandler(this.OnPreviousRowClick);
            this.btnPrev.Enabled = false;
            // 
            // btnNext
            // 
            this.btnNext.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNext.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btnNext.ForeColorDown = System.Drawing.Color.Black;
            this.btnNext.ShowIcon = true; 
            this.btnNext.IconSize = new System.Drawing.Size(36, 36);
            this.btnNext.Icon = IconResources.Move_Next;
            this.btnNext.IconDisabled = IconResources.Move_Next_Disabled;
            this.btnNext.IconAlign = LFI.Mobile.Controls.Align.Center;
            this.btnNext.Location = new System.Drawing.Point(60, 212);
            this.btnNext.Size = new System.Drawing.Size(48, 48);
            this.btnNext.Margin = 0;
            this.btnNext.ItemShiftOnClick = true;
            this.btnNext.Name = "btnNext";
            this.btnNext.Text = "";            
            this.btnNext.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btnNext.TextAlignIsRelative = true;
            this.btnNext.UseTransparency = true;
            this.btnNext.Click += new System.EventHandler(this.OnNextRowClick);
            this.btnNext.Enabled = false;
            // 
            // btnNew
            // 
            this.btnNew.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNew.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btnNew.ForeColorDown = System.Drawing.Color.Black;
            this.btnNew.ShowIcon = true;
            this.btnNew.IconSize = new System.Drawing.Size(36, 36);
            this.btnNew.Icon = IconResources.Add;
            this.btnNew.IconDisabled = IconResources.Add_Disabled;
            this.btnNew.IconAlign = LFI.Mobile.Controls.Align.Center;
            this.btnNew.Location = new System.Drawing.Point(112, 212);
            this.btnNew.Size = new System.Drawing.Size(48, 48);
            this.btnNew.Margin = 0;
            this.btnNew.ItemShiftOnClick = true;
            this.btnNew.Name = "btnNew";
            this.btnNew.Text = "";
            this.btnNew.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btnNew.TextAlignIsRelative = true;
            this.btnNew.UseTransparency = true;
            this.btnNew.Click += new System.EventHandler(this.OnNewRowClick);
            // 
            // btnEdit
            // 
            this.btnEdit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnEdit.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btnEdit.ForeColorDown = System.Drawing.Color.Black;
            this.btnEdit.IconSize = new System.Drawing.Size(36, 36);
            this.btnEdit.ShowIcon = true; 
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
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnEdit);
            this.Name = "HomeScreen";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pickListPanel;
        private LFI.Mobile.Controls.Button.SkinnedButton btnEdit;
        private LFI.Mobile.Controls.Button.SkinnedButton btnPrev;
        private LFI.Mobile.Controls.Button.SkinnedButton btnNext;
        private LFI.Mobile.Controls.Button.SkinnedButton btnNew;
    }
}