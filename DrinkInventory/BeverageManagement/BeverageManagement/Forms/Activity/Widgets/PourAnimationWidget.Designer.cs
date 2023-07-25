namespace BeverageManagement.Forms.Activity.Widgets
{
    partial class PourAnimationWidget
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlGlassFill = new Jaxis.Controls.GlassFill.FillGlass();
            this.SuspendLayout();
            // 
            // ctrlGlassFill
            // 
            this.ctrlGlassFill.BackColor = System.Drawing.Color.White;
            this.ctrlGlassFill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ctrlGlassFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlGlassFill.FillLevel = 100;
            this.ctrlGlassFill.GlassType = Jaxis.Controls.GlassFill.GlassTypes.Beer;
            this.ctrlGlassFill.Location = new System.Drawing.Point(0, 0);
            this.ctrlGlassFill.Name = "ctrlGlassFill";
            this.ctrlGlassFill.Size = new System.Drawing.Size(367, 331);
            this.ctrlGlassFill.TabIndex = 0;
            // 
            // PourAnimationWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrlGlassFill);
            this.Name = "PourAnimationWidget";
            this.Size = new System.Drawing.Size(367, 331);
            this.ResumeLayout(false);

        }

        #endregion

        private Jaxis.Controls.GlassFill.FillGlass ctrlGlassFill;
    }
}
