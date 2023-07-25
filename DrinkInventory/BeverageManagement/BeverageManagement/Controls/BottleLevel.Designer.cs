namespace BeverageManagement.Controls
{
    partial class BottleLevel
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
            this.lblFillLevel = new DevExpress.XtraEditors.LabelControl();
            this.spinNozzleLength = new DevExpress.XtraEditors.SpinEdit();
            this.picBottleEmpty = new System.Windows.Forms.PictureBox();
            this.picBottle = new System.Windows.Forms.PictureBox();
            this.spinNozzleWidth = new DevExpress.XtraEditors.SpinEdit();
            this.cmbNozzleShape = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNozzleLength.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBottleEmpty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBottle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNozzleWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNozzleShape.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFillLevel
            // 
            this.lblFillLevel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblFillLevel.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFillLevel.Location = new System.Drawing.Point(15, 120);
            this.lblFillLevel.Name = "lblFillLevel";
            this.lblFillLevel.Size = new System.Drawing.Size(0, 23);
            this.lblFillLevel.TabIndex = 14;
            // 
            // spinNozzleLength
            // 
            this.spinNozzleLength.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinNozzleLength.Enabled = false;
            this.spinNozzleLength.Location = new System.Drawing.Point(16, 30);
            this.spinNozzleLength.Name = "spinNozzleLength";
            this.spinNozzleLength.Properties.AllowFocused = false;
            this.spinNozzleLength.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spinNozzleLength.Properties.Appearance.Options.UseFont = true;
            this.spinNozzleLength.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinNozzleLength.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.spinNozzleLength.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.spinNozzleLength.Size = new System.Drawing.Size(68, 20);
            this.spinNozzleLength.TabIndex = 15;
            this.spinNozzleLength.Visible = false;
            this.spinNozzleLength.EditValueChanged += new System.EventHandler(this.SpinNozzleDiameterEditValueChanged);
            // 
            // picBottleEmpty
            // 
            this.picBottleEmpty.BackgroundImage = global::BeverageManagement.Properties.Resources.bottle_empty;
            this.picBottleEmpty.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picBottleEmpty.InitialImage = null;
            this.picBottleEmpty.Location = new System.Drawing.Point(3, 3);
            this.picBottleEmpty.Name = "picBottleEmpty";
            this.picBottleEmpty.Size = new System.Drawing.Size(96, 336);
            this.picBottleEmpty.TabIndex = 13;
            this.picBottleEmpty.TabStop = false;
            this.picBottleEmpty.Click += new System.EventHandler(this.SetBottleFluid_Click);
            // 
            // picBottle
            // 
            this.picBottle.BackgroundImage = global::BeverageManagement.Properties.Resources.bottle_full;
            this.picBottle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picBottle.InitialImage = null;
            this.picBottle.Location = new System.Drawing.Point(3, 3);
            this.picBottle.Name = "picBottle";
            this.picBottle.Size = new System.Drawing.Size(96, 336);
            this.picBottle.TabIndex = 12;
            this.picBottle.TabStop = false;
            this.picBottle.Click += new System.EventHandler(this.SetBottleFluid_Click);
            // 
            // spinNozzleWidth
            // 
            this.spinNozzleWidth.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinNozzleWidth.Enabled = false;
            this.spinNozzleWidth.Location = new System.Drawing.Point(16, 52);
            this.spinNozzleWidth.Name = "spinNozzleWidth";
            this.spinNozzleWidth.Properties.AllowFocused = false;
            this.spinNozzleWidth.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spinNozzleWidth.Properties.Appearance.Options.UseFont = true;
            this.spinNozzleWidth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinNozzleWidth.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.spinNozzleWidth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.spinNozzleWidth.Size = new System.Drawing.Size(68, 20);
            this.spinNozzleWidth.TabIndex = 16;
            this.spinNozzleWidth.Visible = false;
            this.spinNozzleWidth.EditValueChanged += new System.EventHandler(this.SpinNozzleDiameterEditValueChanged);
            // 
            // cmbNozzleShape
            // 
            this.cmbNozzleShape.Enabled = false;
            this.cmbNozzleShape.Location = new System.Drawing.Point(6, 8);
            this.cmbNozzleShape.Name = "cmbNozzleShape";
            this.cmbNozzleShape.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNozzleShape.Size = new System.Drawing.Size(90, 20);
            this.cmbNozzleShape.TabIndex = 17;
            this.cmbNozzleShape.Visible = false;
            this.cmbNozzleShape.EditValueChanged += new System.EventHandler(this.SpinNozzleDiameterEditValueChanged);
            // 
            // BottleLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbNozzleShape);
            this.Controls.Add(this.spinNozzleWidth);
            this.Controls.Add(this.spinNozzleLength);
            this.Controls.Add(this.lblFillLevel);
            this.Controls.Add(this.picBottleEmpty);
            this.Controls.Add(this.picBottle);
            this.Name = "BottleLevel";
            this.Size = new System.Drawing.Size(103, 342);
            this.Load += new System.EventHandler(this.BottleLevel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spinNozzleLength.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBottleEmpty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBottle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNozzleWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNozzleShape.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblFillLevel;
        private System.Windows.Forms.PictureBox picBottleEmpty;
        private System.Windows.Forms.PictureBox picBottle;
        private DevExpress.XtraEditors.SpinEdit spinNozzleLength;
        private DevExpress.XtraEditors.SpinEdit spinNozzleWidth;
        private DevExpress.XtraEditors.ComboBoxEdit cmbNozzleShape;
    }
}
