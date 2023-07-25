namespace POSRecon
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( Form1 ) );
            this.grdPOSData = new DevExpress.XtraGrid.GridControl( );
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView( );
            this.grdPourData = new DevExpress.XtraGrid.GridControl( );
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView( );
            this.clbReconciledItems = new DevExpress.XtraEditors.CheckedListBoxControl( );
            this.btnAccept = new DevExpress.XtraEditors.SimpleButton( );
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton( );
            ( (System.ComponentModel.ISupportInitialize)( this.grdPOSData ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.gridView1 ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.grdPourData ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.gridView2 ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.clbReconciledItems ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // grdPOSData
            // 
            this.grdPOSData.Location = new System.Drawing.Point( 12, 12 );
            this.grdPOSData.MainView = this.gridView1;
            this.grdPOSData.Name = "grdPOSData";
            this.grdPOSData.Size = new System.Drawing.Size( 329, 357 );
            this.grdPOSData.TabIndex = 0;
            this.grdPOSData.ViewCollection.AddRange( new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1} );
            // 
            // gridView1
            // 
            this.gridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.Silver;
            this.gridView1.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 255 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ), ( (int)( ( (byte)( 128 ) ) ) ) );
            this.gridView1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 255 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ), ( (int)( ( (byte)( 128 ) ) ) ) );
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.SelectedRow.BackColor = System.Drawing.Color.Yellow;
            this.gridView1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.gridView1.GridControl = this.grdPOSData;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // grdPourData
            // 
            this.grdPourData.Location = new System.Drawing.Point( 393, 12 );
            this.grdPourData.MainView = this.gridView2;
            this.grdPourData.Name = "grdPourData";
            this.grdPourData.Size = new System.Drawing.Size( 329, 357 );
            this.grdPourData.TabIndex = 1;
            this.grdPourData.ViewCollection.AddRange( new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2} );
            // 
            // gridView2
            // 
            this.gridView2.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 255 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ), ( (int)( ( (byte)( 128 ) ) ) ) );
            this.gridView2.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridView2.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 255 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ), ( (int)( ( (byte)( 128 ) ) ) ) );
            this.gridView2.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView2.GridControl = this.grdPourData;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // clbReconciledItems
            // 
            this.clbReconciledItems.Location = new System.Drawing.Point( 57, 375 );
            this.clbReconciledItems.Name = "clbReconciledItems";
            this.clbReconciledItems.Size = new System.Drawing.Size( 665, 166 );
            this.clbReconciledItems.TabIndex = 2;
            // 
            // btnAccept
            // 
            this.btnAccept.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAccept.Image = ( (System.Drawing.Image)( resources.GetObject( "btnAccept.Image" ) ) );
            this.btnAccept.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAccept.Location = new System.Drawing.Point( 348, 78 );
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size( 39, 40 );
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "simpleButton1";
            this.btnAccept.Click += new System.EventHandler( this.btnAccept_Click );
            // 
            // btnRemove
            // 
            this.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRemove.Image = ( (System.Drawing.Image)( resources.GetObject( "btnRemove.Image" ) ) );
            this.btnRemove.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRemove.Location = new System.Drawing.Point( 12, 375 );
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size( 39, 40 );
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "simpleButton1";
            this.btnRemove.Click += new System.EventHandler( this.btnRemove_Click );
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 734, 555 );
            this.Controls.Add( this.btnRemove );
            this.Controls.Add( this.btnAccept );
            this.Controls.Add( this.clbReconciledItems );
            this.Controls.Add( this.grdPourData );
            this.Controls.Add( this.grdPOSData );
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler( this.Form1_Load );
            ( (System.ComponentModel.ISupportInitialize)( this.grdPOSData ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.gridView1 ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.grdPourData ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.gridView2 ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.clbReconciledItems ) ).EndInit( );
            this.ResumeLayout( false );

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdPOSData;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl grdPourData;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.CheckedListBoxControl clbReconciledItems;
        private DevExpress.XtraEditors.SimpleButton btnAccept;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
    }
}

