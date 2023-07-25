namespace EngineConfigUtil
{
    partial class AddDeviceForm
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
            this.lbDevices = new DevExpress.XtraEditors.ListBoxControl();
            this.sbGetDevices = new DevExpress.XtraEditors.SimpleButton();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbOK = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControlLocalDevices = new DevExpress.XtraGrid.GridControl();
            this.gridViewLocalDevices = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.lbDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLocalDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLocalDevices)).BeginInit();
            this.SuspendLayout();
            // 
            // lbDevices
            // 
            this.lbDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDevices.Location = new System.Drawing.Point(-117, -28);
            this.lbDevices.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbDevices.Name = "lbDevices";
            this.lbDevices.Size = new System.Drawing.Size(9, 0);
            this.lbDevices.TabIndex = 5;
            // 
            // sbGetDevices
            // 
            this.sbGetDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sbGetDevices.Location = new System.Drawing.Point(-121, 367);
            this.sbGetDevices.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sbGetDevices.Name = "sbGetDevices";
            this.sbGetDevices.Size = new System.Drawing.Size(9, 8);
            this.sbGetDevices.TabIndex = 4;
            this.sbGetDevices.Text = "Refresh Devices";
            // 
            // sbCancel
            // 
            this.sbCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbCancel.Location = new System.Drawing.Point(741, 226);
            this.sbCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(64, 19);
            this.sbCancel.TabIndex = 7;
            this.sbCancel.Text = "Cancel";
            // 
            // sbOK
            // 
            this.sbOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sbOK.Location = new System.Drawing.Point(663, 225);
            this.sbOK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sbOK.Name = "sbOK";
            this.sbOK.Size = new System.Drawing.Size(64, 19);
            this.sbOK.TabIndex = 8;
            this.sbOK.Text = "Ok";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 10);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 13);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "Devices/Filters";
            // 
            // gridControlLocalDevices
            // 
            this.gridControlLocalDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlLocalDevices.Location = new System.Drawing.Point(10, 28);
            this.gridControlLocalDevices.MainView = this.gridViewLocalDevices;
            this.gridControlLocalDevices.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridControlLocalDevices.Name = "gridControlLocalDevices";
            this.gridControlLocalDevices.Size = new System.Drawing.Size(795, 184);
            this.gridControlLocalDevices.TabIndex = 10;
            this.gridControlLocalDevices.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLocalDevices});
            // 
            // gridViewLocalDevices
            // 
            this.gridViewLocalDevices.GridControl = this.gridControlLocalDevices;
            this.gridViewLocalDevices.Name = "gridViewLocalDevices";
            this.gridViewLocalDevices.OptionsBehavior.Editable = false;
            // 
            // AddDeviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 253);
            this.Controls.Add(this.gridControlLocalDevices);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.sbOK);
            this.Controls.Add(this.sbCancel);
            this.Controls.Add(this.lbDevices);
            this.Controls.Add(this.sbGetDevices);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AddDeviceForm";
            this.Text = "Add Device";
            ((System.ComponentModel.ISupportInitialize)(this.lbDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLocalDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLocalDevices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl lbDevices;
        private DevExpress.XtraEditors.SimpleButton sbGetDevices;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraEditors.SimpleButton sbOK;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraGrid.GridControl gridControlLocalDevices;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewLocalDevices;
    }
}