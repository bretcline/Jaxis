namespace LogViewer
{
    partial class MainWindow
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuItemClear = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuItemImportLogs = new System.Windows.Forms.ToolStripMenuItem();
            this.m_gridControlLog = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewLog = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridControlLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewLog)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(982, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuItemClear,
            this.m_menuItemImportLogs});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // m_menuItemClear
            // 
            this.m_menuItemClear.Name = "m_menuItemClear";
            this.m_menuItemClear.Size = new System.Drawing.Size(147, 22);
            this.m_menuItemClear.Text = "Clear";
            this.m_menuItemClear.Click += new System.EventHandler(this.MenuItemClearClick);
            // 
            // m_menuItemImportLogs
            // 
            this.m_menuItemImportLogs.Name = "m_menuItemImportLogs";
            this.m_menuItemImportLogs.Size = new System.Drawing.Size(147, 22);
            this.m_menuItemImportLogs.Text = "Import Logs...";
            this.m_menuItemImportLogs.Click += new System.EventHandler(this.MenuItemImportLogsClick);
            // 
            // m_gridControlLog
            // 
            this.m_gridControlLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridControlLog.Location = new System.Drawing.Point(0, 24);
            this.m_gridControlLog.MainView = this.m_gridViewLog;
            this.m_gridControlLog.Name = "m_gridControlLog";
            this.m_gridControlLog.Size = new System.Drawing.Size(982, 571);
            this.m_gridControlLog.TabIndex = 1;
            this.m_gridControlLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewLog});
            // 
            // m_gridViewLog
            // 
            this.m_gridViewLog.GridControl = this.m_gridControlLog;
            this.m_gridViewLog.Name = "m_gridViewLog";
            this.m_gridViewLog.OptionsView.ShowAutoFilterRow = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 595);
            this.Controls.Add(this.m_gridControlLog);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Jaxis Log Viewer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridControlLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_menuItemImportLogs;
        private DevExpress.XtraGrid.GridControl m_gridControlLog;
        private DevExpress.XtraGrid.Views.Grid.GridView m_gridViewLog;
        private System.Windows.Forms.ToolStripMenuItem m_menuItemClear;
    }
}

