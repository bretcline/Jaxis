namespace BeverageManagement.Forms.Reconcile
{
    partial class frmRecipes
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
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbcRecipes = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.teRecipeName = new DevExpress.XtraEditors.TextEdit();
            this.pRecipe = new System.Windows.Forms.Panel();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemoveIngredient = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddIngredient = new DevExpress.XtraEditors.SimpleButton();
            this.lcIngredience = new DevExpress.XtraLayout.LayoutControl();
            this.lcgIngredients = new DevExpress.XtraLayout.LayoutControlGroup();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbcRecipes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teRecipeName.Properties)).BeginInit();
            this.pRecipe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcIngredience)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgIngredients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitterControl1
            // 
            this.splitterControl1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterControl1.Location = new System.Drawing.Point(179, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 431);
            this.splitterControl1.TabIndex = 10;
            this.splitterControl1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbcRecipes);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(179, 431);
            this.panel1.TabIndex = 13;
            // 
            // lbcRecipes
            // 
            this.lbcRecipes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbcRecipes.Location = new System.Drawing.Point(5, 32);
            this.lbcRecipes.Name = "lbcRecipes";
            this.lbcRecipes.Size = new System.Drawing.Size(173, 358);
            this.lbcRecipes.TabIndex = 1;
            this.lbcRecipes.SelectedIndexChanged += new System.EventHandler(this.lbcRecipes_SelectedIndexChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Location = new System.Drawing.Point(0, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(179, 23);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Recipe Box";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(66, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Recipe Name:";
            // 
            // teRecipeName
            // 
            this.teRecipeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teRecipeName.Location = new System.Drawing.Point(78, 17);
            this.teRecipeName.Name = "teRecipeName";
            this.teRecipeName.Size = new System.Drawing.Size(374, 20);
            this.teRecipeName.TabIndex = 1;
            // 
            // pRecipe
            // 
            this.pRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pRecipe.Controls.Add(this.btnSave);
            this.pRecipe.Controls.Add(this.btnRemoveIngredient);
            this.pRecipe.Controls.Add(this.btnAddIngredient);
            this.pRecipe.Controls.Add(this.lcIngredience);
            this.pRecipe.Controls.Add(this.labelControl1);
            this.pRecipe.Controls.Add(this.teRecipeName);
            this.pRecipe.Location = new System.Drawing.Point(0, 0);
            this.pRecipe.Name = "pRecipe";
            this.pRecipe.Size = new System.Drawing.Size(464, 397);
            this.pRecipe.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(200, 43);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRemoveIngredient
            // 
            this.btnRemoveIngredient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveIngredient.Location = new System.Drawing.Point(352, 43);
            this.btnRemoveIngredient.Name = "btnRemoveIngredient";
            this.btnRemoveIngredient.Size = new System.Drawing.Size(100, 23);
            this.btnRemoveIngredient.TabIndex = 4;
            this.btnRemoveIngredient.Text = "Remove Ingredient";
            // 
            // btnAddIngredient
            // 
            this.btnAddIngredient.Location = new System.Drawing.Point(78, 43);
            this.btnAddIngredient.Name = "btnAddIngredient";
            this.btnAddIngredient.Size = new System.Drawing.Size(100, 23);
            this.btnAddIngredient.TabIndex = 3;
            this.btnAddIngredient.Text = "Add Ingredient";
            this.btnAddIngredient.Click += new System.EventHandler(this.btnAddIngredient_Click);
            // 
            // lcIngredience
            // 
            this.lcIngredience.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lcIngredience.Location = new System.Drawing.Point(6, 69);
            this.lcIngredience.Name = "lcIngredience";
            this.lcIngredience.Root = this.lcgIngredients;
            this.lcIngredience.Size = new System.Drawing.Size(449, 324);
            this.lcIngredience.TabIndex = 2;
            this.lcIngredience.Text = "layoutControl1";
            // 
            // lcgIngredients
            // 
            this.lcgIngredients.CustomizationFormText = "layoutControlGroup1";
            this.lcgIngredients.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgIngredients.GroupBordersVisible = false;
            this.lcgIngredients.Location = new System.Drawing.Point(0, 0);
            this.lcgIngredients.Name = "lcgIngredients";
            this.lcgIngredients.Size = new System.Drawing.Size(449, 324);
            this.lcgIngredients.Text = "lcgIngredients";
            this.lcgIngredients.TextVisible = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnDelete);
            this.panelControl1.Controls.Add(this.btnNew);
            this.panelControl1.Controls.Add(this.pRecipe);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(184, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(464, 431);
            this.panelControl1.TabIndex = 14;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(384, 403);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(169, 403);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(7, 403);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "New";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // frmRecipes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 431);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(536, 321);
            this.Name = "frmRecipes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Drink Recipes";
            this.Load += new System.EventHandler(this.frmRecipes_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbcRecipes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teRecipeName.Properties)).EndInit();
            this.pRecipe.ResumeLayout(false);
            this.pRecipe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcIngredience)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgIngredients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ListBoxControl lbcRecipes;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit teRecipeName;
        private System.Windows.Forms.Panel pRecipe;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControl lcIngredience;
        private DevExpress.XtraLayout.LayoutControlGroup lcgIngredients;
        private DevExpress.XtraEditors.SimpleButton btnRemoveIngredient;
        private DevExpress.XtraEditors.SimpleButton btnAddIngredient;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraEditors.SimpleButton btnSave;
    }
}