namespace BeverageManagement.Forms
{
    partial class frmSummary<T>
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
            this.components = new System.ComponentModel.Container();
            this.grdSummary = new DevExpress.XtraGrid.GridControl();
            this.gvSummary = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tmrAutoClose = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // grdSummary
            // 
            this.grdSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSummary.Location = new System.Drawing.Point(0, 0);
            this.grdSummary.MainView = this.gvSummary;
            this.grdSummary.Name = "grdSummary";
            this.grdSummary.Size = new System.Drawing.Size(714, 502);
            this.grdSummary.TabIndex = 0;
            this.grdSummary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSummary});
            // 
            // gvSummary
            // 
            this.gvSummary.GridControl = this.grdSummary;
            this.gvSummary.Name = "gvSummary";
            this.gvSummary.OptionsView.ShowFooter = true;
            // 
            // tmrAutoClose
            // 
            this.tmrAutoClose.Tick += new System.EventHandler(this.tmrAutoClose_Tick);
            // 
            // frmSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 502);
            this.Controls.Add(this.grdSummary);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Summary";
            this.Deactivate += new System.EventHandler(this.frmSummary_Deactivate);
            this.Load += new System.EventHandler(this.frmSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSummary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdSummary;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSummary;
        private System.Windows.Forms.Timer tmrAutoClose;
    }
}