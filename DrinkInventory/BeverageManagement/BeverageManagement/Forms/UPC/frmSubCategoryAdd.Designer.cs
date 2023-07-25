namespace BeverageManagement.Forms
{
    partial class frmSubCategoryAdd
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
            if ( disposing && ( components != null ) )
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkSetNozzle = new DevExpress.XtraEditors.CheckEdit();
            this.cmbNozzleType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.spinNozzleWidth = new DevExpress.XtraEditors.SpinEdit();
            this.spinNozzleLength = new DevExpress.XtraEditors.SpinEdit();
            this.lueSubcategories = new DevExpress.XtraEditors.LookUpEdit();
            this.lueCategories = new DevExpress.XtraEditors.LookUpEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciDescription = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciOK = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lciCategories = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciSubcategories = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciSpinNozzle = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSetNozzle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNozzleType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNozzleWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNozzleLength.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSubcategories.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCategories.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCategories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSubcategories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSpinNozzle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkSetNozzle);
            this.layoutControl1.Controls.Add(this.cmbNozzleType);
            this.layoutControl1.Controls.Add(this.spinNozzleWidth);
            this.layoutControl1.Controls.Add(this.spinNozzleLength);
            this.layoutControl1.Controls.Add(this.lueSubcategories);
            this.layoutControl1.Controls.Add(this.lueCategories);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.txtDescription);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(455, 174);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkSetNozzle
            // 
            this.chkSetNozzle.Location = new System.Drawing.Point(12, 84);
            this.chkSetNozzle.Name = "chkSetNozzle";
            this.chkSetNozzle.Properties.Caption = "Set Nozzle Size";
            this.chkSetNozzle.Size = new System.Drawing.Size(431, 19);
            this.chkSetNozzle.StyleController = this.layoutControl1;
            this.chkSetNozzle.TabIndex = 21;
            this.chkSetNozzle.CheckedChanged += new System.EventHandler(this.chkSetNozzle_CheckedChanged);
            // 
            // cmbNozzleType
            // 
            this.cmbNozzleType.Enabled = false;
            this.cmbNozzleType.Location = new System.Drawing.Point(383, 107);
            this.cmbNozzleType.Name = "cmbNozzleType";
            this.cmbNozzleType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNozzleType.Size = new System.Drawing.Size(60, 20);
            this.cmbNozzleType.StyleController = this.layoutControl1;
            this.cmbNozzleType.TabIndex = 20;
            // 
            // spinNozzleWidth
            // 
            this.spinNozzleWidth.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinNozzleWidth.Enabled = false;
            this.spinNozzleWidth.EnterMoveNextControl = true;
            this.spinNozzleWidth.Location = new System.Drawing.Point(241, 107);
            this.spinNozzleWidth.Name = "spinNozzleWidth";
            this.spinNozzleWidth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinNozzleWidth.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.spinNozzleWidth.Properties.MaxValue = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.spinNozzleWidth.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinNozzleWidth.Size = new System.Drawing.Size(66, 20);
            this.spinNozzleWidth.StyleController = this.layoutControl1;
            this.spinNozzleWidth.TabIndex = 19;
            // 
            // spinNozzleLength
            // 
            this.spinNozzleLength.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinNozzleLength.Enabled = false;
            this.spinNozzleLength.EnterMoveNextControl = true;
            this.spinNozzleLength.Location = new System.Drawing.Point(84, 107);
            this.spinNozzleLength.Name = "spinNozzleLength";
            this.spinNozzleLength.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinNozzleLength.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.spinNozzleLength.Properties.MaxValue = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.spinNozzleLength.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinNozzleLength.Size = new System.Drawing.Size(81, 20);
            this.spinNozzleLength.StyleController = this.layoutControl1;
            this.spinNozzleLength.TabIndex = 18;
            // 
            // lueSubcategories
            // 
            this.lueSubcategories.EditValue = "";
            this.lueSubcategories.EnterMoveNextControl = true;
            this.lueSubcategories.Location = new System.Drawing.Point(84, 36);
            this.lueSubcategories.Name = "lueSubcategories";
            this.lueSubcategories.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueSubcategories.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.lueSubcategories.Properties.NullText = "";
            this.lueSubcategories.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueSubcategories.Size = new System.Drawing.Size(359, 20);
            this.lueSubcategories.StyleController = this.layoutControl1;
            this.lueSubcategories.TabIndex = 14;
            // 
            // lueCategories
            // 
            this.lueCategories.EnterMoveNextControl = true;
            this.lueCategories.Location = new System.Drawing.Point(84, 12);
            this.lueCategories.Name = "lueCategories";
            this.lueCategories.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueCategories.Properties.NullText = "";
            this.lueCategories.Size = new System.Drawing.Size(359, 20);
            this.lueCategories.StyleController = this.layoutControl1;
            this.lueCategories.TabIndex = 13;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(343, 131);
            this.btnCancel.MaximumSize = new System.Drawing.Size(100, 22);
            this.btnCancel.MinimumSize = new System.Drawing.Size(100, 22);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(239, 131);
            this.btnOK.MaximumSize = new System.Drawing.Size(100, 22);
            this.btnOK.MinimumSize = new System.Drawing.Size(100, 22);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.EnterMoveNextControl = true;
            this.txtDescription.Location = new System.Drawing.Point(84, 60);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(359, 20);
            this.txtDescription.StyleController = this.layoutControl1;
            this.txtDescription.TabIndex = 5;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciDescription,
            this.lciCancel,
            this.lciOK,
            this.emptySpaceItem1,
            this.lciCategories,
            this.lciSubcategories,
            this.lciSpinNozzle,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(455, 174);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciDescription
            // 
            this.lciDescription.Control = this.txtDescription;
            this.lciDescription.CustomizationFormText = global::BeverageManagement.Properties.Resources.Description;
            this.lciDescription.Location = new System.Drawing.Point(0, 48);
            this.lciDescription.Name = "lciDescription";
            this.lciDescription.Size = new System.Drawing.Size(435, 24);
            this.lciDescription.Text = global::BeverageManagement.Properties.Resources.Description;
            this.lciDescription.TextSize = new System.Drawing.Size(68, 13);
            // 
            // lciCancel
            // 
            this.lciCancel.Control = this.btnCancel;
            this.lciCancel.CustomizationFormText = "lciCancel";
            this.lciCancel.Location = new System.Drawing.Point(331, 119);
            this.lciCancel.Name = "lciCancel";
            this.lciCancel.Size = new System.Drawing.Size(104, 35);
            this.lciCancel.Text = "lciCancel";
            this.lciCancel.TextSize = new System.Drawing.Size(0, 0);
            this.lciCancel.TextToControlDistance = 0;
            this.lciCancel.TextVisible = false;
            // 
            // lciOK
            // 
            this.lciOK.Control = this.btnOK;
            this.lciOK.CustomizationFormText = "lciOK";
            this.lciOK.Location = new System.Drawing.Point(227, 119);
            this.lciOK.Name = "lciOK";
            this.lciOK.Size = new System.Drawing.Size(104, 35);
            this.lciOK.Text = "lciOK";
            this.lciOK.TextSize = new System.Drawing.Size(0, 0);
            this.lciOK.TextToControlDistance = 0;
            this.lciOK.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 119);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(227, 35);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lciCategories
            // 
            this.lciCategories.Control = this.lueCategories;
            this.lciCategories.CustomizationFormText = global::BeverageManagement.Properties.Resources.Categories;
            this.lciCategories.Location = new System.Drawing.Point(0, 0);
            this.lciCategories.Name = "lciCategories";
            this.lciCategories.Size = new System.Drawing.Size(435, 24);
            this.lciCategories.Text = global::BeverageManagement.Properties.Resources.Categories;
            this.lciCategories.TextSize = new System.Drawing.Size(68, 13);
            // 
            // lciSubcategories
            // 
            this.lciSubcategories.Control = this.lueSubcategories;
            this.lciSubcategories.CustomizationFormText = global::BeverageManagement.Properties.Resources.Subcategories;
            this.lciSubcategories.Location = new System.Drawing.Point(0, 24);
            this.lciSubcategories.Name = "lciSubcategories";
            this.lciSubcategories.Size = new System.Drawing.Size(435, 24);
            this.lciSubcategories.Text = global::BeverageManagement.Properties.Resources.Subcategories;
            this.lciSubcategories.TextSize = new System.Drawing.Size(68, 13);
            // 
            // lciSpinNozzle
            // 
            this.lciSpinNozzle.Control = this.spinNozzleLength;
            this.lciSpinNozzle.CustomizationFormText = global::BeverageManagement.Properties.Resources.NozzleDiameter;
            this.lciSpinNozzle.Location = new System.Drawing.Point(0, 95);
            this.lciSpinNozzle.Name = "lciSpinNozzle";
            this.lciSpinNozzle.Size = new System.Drawing.Size(157, 24);
            this.lciSpinNozzle.Text = "Nozzle Length";
            this.lciSpinNozzle.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.spinNozzleWidth;
            this.layoutControlItem1.CustomizationFormText = "Nozzle Width";
            this.layoutControlItem1.Location = new System.Drawing.Point(157, 95);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(142, 24);
            this.layoutControlItem1.Text = "Nozzle Width";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbNozzleType;
            this.layoutControlItem2.CustomizationFormText = "Type";
            this.layoutControlItem2.Location = new System.Drawing.Point(299, 95);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(136, 24);
            this.layoutControlItem2.Text = "Shape";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkSetNozzle;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(435, 23);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmSubCategoryAdd
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(455, 174);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmSubCategoryAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Sub Category";
            this.Load += new System.EventHandler(this.frmSubcategoryAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkSetNozzle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNozzleType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNozzleWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNozzleLength.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSubcategories.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCategories.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCategories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSubcategories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSpinNozzle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraLayout.LayoutControlItem lciDescription;
        private DevExpress.XtraLayout.LayoutControlItem lciCancel;
        private DevExpress.XtraLayout.LayoutControlItem lciOK;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.LookUpEdit lueSubcategories;
        private DevExpress.XtraEditors.LookUpEdit lueCategories;
        private DevExpress.XtraLayout.LayoutControlItem lciCategories;
        private DevExpress.XtraLayout.LayoutControlItem lciSubcategories;
        private DevExpress.XtraEditors.SpinEdit spinNozzleLength;
        private DevExpress.XtraLayout.LayoutControlItem lciSpinNozzle;
        private DevExpress.XtraEditors.ComboBoxEdit cmbNozzleType;
        private DevExpress.XtraEditors.SpinEdit spinNozzleWidth;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CheckEdit chkSetNozzle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}