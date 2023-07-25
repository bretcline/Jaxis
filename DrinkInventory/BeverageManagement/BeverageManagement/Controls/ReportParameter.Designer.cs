namespace BeverageManagement.Controls
{
    partial class ReportParameter
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
            this.cmbCompare = new DevExpress.XtraEditors.ComboBoxEdit( );
            this.cmbWhereCompare = new DevExpress.XtraEditors.ComboBoxEdit( );
            this.lblName = new DevExpress.XtraEditors.LabelControl( );
            this.txtValue = new DevExpress.XtraEditors.TextEdit( );
            this.chkUse = new DevExpress.XtraEditors.CheckEdit( );
            ( (System.ComponentModel.ISupportInitialize)( this.cmbCompare.Properties ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.cmbWhereCompare.Properties ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.txtValue.Properties ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.chkUse.Properties ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // cmbCompare
            // 
            this.cmbCompare.Location = new System.Drawing.Point( 145, 3 );
            this.cmbCompare.Name = "cmbCompare";
            this.cmbCompare.Properties.Buttons.AddRange( new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)} );
            this.cmbCompare.Properties.Items.AddRange( new object[] {
            "=",
            "!=",
            "<",
            "<=",
            ">",
            ">=",
            "LIKE"} );
            this.cmbCompare.Size = new System.Drawing.Size( 60, 20 );
            this.cmbCompare.TabIndex = 0;
            // 
            // cmbWhereCompare
            // 
            this.cmbWhereCompare.Location = new System.Drawing.Point( 0, 3 );
            this.cmbWhereCompare.Name = "cmbWhereCompare";
            this.cmbWhereCompare.Properties.Buttons.AddRange( new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)} );
            this.cmbWhereCompare.Properties.Items.AddRange( new object[] {
            "AND",
            "OR"} );
            this.cmbWhereCompare.Size = new System.Drawing.Size( 43, 20 );
            this.cmbWhereCompare.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point( 52, 6 );
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size( 63, 13 );
            this.lblName.TabIndex = 2;
            this.lblName.Text = "labelControl1";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point( 211, 3 );
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size( 105, 20 );
            this.txtValue.TabIndex = 3;
            // 
            // chkUse
            // 
            this.chkUse.Location = new System.Drawing.Point( 322, 3 );
            this.chkUse.Name = "chkUse";
            this.chkUse.Properties.Caption = "";
            this.chkUse.Size = new System.Drawing.Size( 20, 19 );
            this.chkUse.TabIndex = 4;
            this.chkUse.CheckedChanged += new System.EventHandler( this.chkUse_CheckedChanged );
            // 
            // ReportParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.chkUse );
            this.Controls.Add( this.txtValue );
            this.Controls.Add( this.lblName );
            this.Controls.Add( this.cmbWhereCompare );
            this.Controls.Add( this.cmbCompare );
            this.Name = "ReportParameter";
            this.Size = new System.Drawing.Size( 344, 26 );
            ( (System.ComponentModel.ISupportInitialize)( this.cmbCompare.Properties ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.cmbWhereCompare.Properties ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.txtValue.Properties ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.chkUse.Properties ) ).EndInit( );
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cmbCompare;
        private DevExpress.XtraEditors.ComboBoxEdit cmbWhereCompare;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.TextEdit txtValue;
        private DevExpress.XtraEditors.CheckEdit chkUse;
    }
}
