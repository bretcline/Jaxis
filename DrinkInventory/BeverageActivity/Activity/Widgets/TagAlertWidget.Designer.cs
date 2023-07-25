namespace BeverageActivity.Forms.Activity.Widgets
{
    partial class TagAlertWidget
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.grdActivityLog = new DevExpress.XtraGrid.GridControl();
            this.gvActivityLog = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdActivityLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvActivityLog)).BeginInit();
            this.SuspendLayout();
            // 
            // grdActivityLog
            // 
            this.grdActivityLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdActivityLog.Location = new System.Drawing.Point(0, 0);
            this.grdActivityLog.MainView = this.gvActivityLog;
            this.grdActivityLog.Name = "grdActivityLog";
            this.grdActivityLog.Size = new System.Drawing.Size(582, 336);
            this.grdActivityLog.TabIndex = 0;
            this.grdActivityLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvActivityLog});
            this.grdActivityLog.Load += new System.EventHandler(this.grdActivityLog_Load);
            // 
            // gvActivityLog
            // 
            this.gvActivityLog.GridControl = this.grdActivityLog;
            this.gvActivityLog.Name = "gvActivityLog";
            this.gvActivityLog.OptionsBehavior.Editable = false;
            this.gvActivityLog.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gvActivityLog.OptionsBehavior.ReadOnly = true;
            this.gvActivityLog.OptionsNavigation.AutoFocusNewRow = true;
            this.gvActivityLog.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvActivityLog.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvActivityLog.OptionsSelection.UseIndicatorForSelection = false;
            this.gvActivityLog.OptionsView.ShowGroupPanel = false;
            this.gvActivityLog.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvActivityLog_RowStyle);
            // 
            // TagAlertWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdActivityLog);
            this.Name = "TagAlertWidget";
            this.Size = new System.Drawing.Size(582, 336);
            ((System.ComponentModel.ISupportInitialize)(this.grdActivityLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvActivityLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdActivityLog;
        private DevExpress.XtraGrid.Views.Grid.GridView gvActivityLog;
    }
}
