namespace BevCart
{
    partial class TestForm
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
            this.gridPours = new DevExpress.XtraGrid.GridControl( );
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView( );
            this.tePourData = new DevExpress.XtraEditors.TextEdit( );
            this.sbStart = new DevExpress.XtraEditors.SimpleButton( );
            this.sbStop = new DevExpress.XtraEditors.SimpleButton( );
            ( (System.ComponentModel.ISupportInitialize)( this.gridPours ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.gridView1 ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.tePourData.Properties ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // gridPours
            // 
            this.gridPours.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.gridPours.Location = new System.Drawing.Point( 13, 96 );
            this.gridPours.MainView = this.gridView1;
            this.gridPours.Name = "gridPours";
            this.gridPours.Size = new System.Drawing.Size( 548, 282 );
            this.gridPours.TabIndex = 0;
            this.gridPours.ViewCollection.AddRange( new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1} );
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridPours;
            this.gridView1.Name = "gridView1";
            // 
            // tePourData
            // 
            this.tePourData.Location = new System.Drawing.Point( 12, 53 );
            this.tePourData.Name = "tePourData";
            this.tePourData.Size = new System.Drawing.Size( 268, 20 );
            this.tePourData.TabIndex = 1;
            // 
            // sbStart
            // 
            this.sbStart.Location = new System.Drawing.Point( 12, 12 );
            this.sbStart.Name = "sbStart";
            this.sbStart.Size = new System.Drawing.Size( 75, 23 );
            this.sbStart.TabIndex = 2;
            this.sbStart.Text = "Start";
            this.sbStart.Click += new System.EventHandler( this.sbStart_Click );
            // 
            // sbStop
            // 
            this.sbStop.Location = new System.Drawing.Point( 93, 12 );
            this.sbStop.Name = "sbStop";
            this.sbStop.Size = new System.Drawing.Size( 75, 23 );
            this.sbStop.TabIndex = 3;
            this.sbStop.Text = "Stop";
            this.sbStop.Click += new System.EventHandler( this.sbStop_Click );
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 573, 390 );
            this.Controls.Add( this.sbStop );
            this.Controls.Add( this.sbStart );
            this.Controls.Add( this.tePourData );
            this.Controls.Add( this.gridPours );
            this.Name = "TestForm";
            this.Text = "Form1";
            ( (System.ComponentModel.ISupportInitialize)( this.gridPours ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.gridView1 ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.tePourData.Properties ) ).EndInit( );
            this.ResumeLayout( false );

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridPours;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.TextEdit tePourData;
        private DevExpress.XtraEditors.SimpleButton sbStart;
        private DevExpress.XtraEditors.SimpleButton sbStop;
    }
}

