namespace LFI.RFID.Editor
{
    partial class DataRowDefEditor
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl( );
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView( );
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn( );
            this.gridColumnDataType = new DevExpress.XtraGrid.Columns.GridColumn( );
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox( );
            this.gridColumnRequired = new DevExpress.XtraGrid.Columns.GridColumn( );
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit( );
            this.gridColumnConstraints = new DevExpress.XtraGrid.Columns.GridColumn( );
            ( (System.ComponentModel.ISupportInitialize)( this.gridControl1 ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.gridView1 ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.repositoryItemComboBox1 ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.repositoryItemCheckEdit1 ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point( 0, 0 );
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange( new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemComboBox1} );
            this.gridControl1.Size = new System.Drawing.Size( 408, 366 );
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange( new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1} );
            this.gridControl1.KeyDown += new System.Windows.Forms.KeyEventHandler( this.gridControl1_KeyDown );
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange( new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName,
            this.gridColumnDataType,
            this.gridColumnRequired,
            this.gridColumnConstraints} );
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.NewItemRowText = "Add Data Element";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsMenu.EnableFooterMenu = false;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "Name";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.OptionsColumn.AllowMove = false;
            this.gridColumnName.OptionsColumn.FixedWidth = true;
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 0;
            this.gridColumnName.Width = 120;
            // 
            // gridColumnDataType
            // 
            this.gridColumnDataType.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnDataType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumnDataType.Caption = "Data Type";
            this.gridColumnDataType.ColumnEdit = this.repositoryItemComboBox1;
            this.gridColumnDataType.FieldName = "DataType";
            this.gridColumnDataType.Name = "gridColumnDataType";
            this.gridColumnDataType.OptionsColumn.AllowMove = false;
            this.gridColumnDataType.OptionsColumn.FixedWidth = true;
            this.gridColumnDataType.Visible = true;
            this.gridColumnDataType.VisibleIndex = 1;
            this.gridColumnDataType.Width = 78;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange( new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)} );
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // gridColumnRequired
            // 
            this.gridColumnRequired.Caption = "Required";
            this.gridColumnRequired.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumnRequired.FieldName = "Required";
            this.gridColumnRequired.Name = "gridColumnRequired";
            this.gridColumnRequired.OptionsColumn.AllowMove = false;
            this.gridColumnRequired.OptionsColumn.FixedWidth = true;
            this.gridColumnRequired.Visible = true;
            this.gridColumnRequired.VisibleIndex = 2;
            this.gridColumnRequired.Width = 60;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // gridColumnConstraints
            // 
            this.gridColumnConstraints.Caption = "Constraints";
            this.gridColumnConstraints.FieldName = "Constraints";
            this.gridColumnConstraints.Name = "gridColumnConstraints";
            this.gridColumnConstraints.OptionsColumn.AllowMove = false;
            this.gridColumnConstraints.Visible = true;
            this.gridColumnConstraints.VisibleIndex = 3;
            this.gridColumnConstraints.Width = 129;
            // 
            // DataRowDefEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.gridControl1 );
            this.Name = "DataRowDefEditor";
            this.Size = new System.Drawing.Size( 408, 366 );
            ( (System.ComponentModel.ISupportInitialize)( this.gridControl1 ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.gridView1 ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.repositoryItemComboBox1 ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.repositoryItemCheckEdit1 ) ).EndInit( );
            this.ResumeLayout( false );

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDataType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnRequired;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnConstraints;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
    }
}
