using RecipesFromExcel.Properties;
namespace RecipesFromExcel
{
    partial class ExcelImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExcelImportForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCleanupManufacturers = new DevExpress.XtraEditors.SimpleButton();
            this.lbErrorMessages = new System.Windows.Forms.ListBox();
            this.lueIngredientSheets = new DevExpress.XtraEditors.LookUpEdit();
            this.lueRecipeSheets = new DevExpress.XtraEditors.LookUpEdit();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnRecipeFinder = new System.Windows.Forms.Button();
            this.txtIngredient = new DevExpress.XtraEditors.TextEdit();
            this.btnIngredientsFinder = new System.Windows.Forms.Button();
            this.txtRecipe = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcRecipePath = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciIngredientsPath = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciRecipeFinder = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciIngredientsFinder = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciGo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciRecipeSheets = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciIngredientSheets = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lciErrorMessages = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueIngredientSheets.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueRecipeSheets.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIngredient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecipe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRecipePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIngredientsPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRecipeFinder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIngredientsFinder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciGo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRecipeSheets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIngredientSheets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciErrorMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCleanupManufacturers);
            this.layoutControl1.Controls.Add(this.lbErrorMessages);
            this.layoutControl1.Controls.Add(this.lueIngredientSheets);
            this.layoutControl1.Controls.Add(this.lueRecipeSheets);
            this.layoutControl1.Controls.Add(this.btnGo);
            this.layoutControl1.Controls.Add(this.btnRecipeFinder);
            this.layoutControl1.Controls.Add(this.txtIngredient);
            this.layoutControl1.Controls.Add(this.btnIngredientsFinder);
            this.layoutControl1.Controls.Add(this.txtRecipe);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2324, 139, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(541, 408);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCleanupManufacturers
            // 
            this.btnCleanupManufacturers.Location = new System.Drawing.Point(12, 374);
            this.btnCleanupManufacturers.Name = "btnCleanupManufacturers";
            this.btnCleanupManufacturers.Size = new System.Drawing.Size(227, 22);
            this.btnCleanupManufacturers.StyleController = this.layoutControl1;
            this.btnCleanupManufacturers.TabIndex = 12;
            this.btnCleanupManufacturers.Text = "Cleanup Manufacturers";
            this.btnCleanupManufacturers.Click += new System.EventHandler(this.btnCleanupManufacturers_Click);
            // 
            // lbErrorMessages
            // 
            this.lbErrorMessages.FormattingEnabled = true;
            this.lbErrorMessages.Location = new System.Drawing.Point(12, 115);
            this.lbErrorMessages.Name = "lbErrorMessages";
            this.lbErrorMessages.Size = new System.Drawing.Size(517, 251);
            this.lbErrorMessages.TabIndex = 11;
            // 
            // lueIngredientSheets
            // 
            this.lueIngredientSheets.Location = new System.Drawing.Point(370, 67);
            this.lueIngredientSheets.Name = "lueIngredientSheets";
            this.lueIngredientSheets.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueIngredientSheets.Properties.NullText = "";
            this.lueIngredientSheets.Size = new System.Drawing.Size(159, 20);
            this.lueIngredientSheets.StyleController = this.layoutControl1;
            this.lueIngredientSheets.TabIndex = 10;
            // 
            // lueRecipeSheets
            // 
            this.lueRecipeSheets.Location = new System.Drawing.Point(370, 12);
            this.lueRecipeSheets.Name = "lueRecipeSheets";
            this.lueRecipeSheets.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueRecipeSheets.Properties.NullText = "";
            this.lueRecipeSheets.Size = new System.Drawing.Size(159, 20);
            this.lueRecipeSheets.StyleController = this.layoutControl1;
            this.lueRecipeSheets.TabIndex = 9;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(474, 374);
            this.btnGo.MaximumSize = new System.Drawing.Size(55, 20);
            this.btnGo.MinimumSize = new System.Drawing.Size(55, 20);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(55, 20);
            this.btnGo.TabIndex = 8;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnRecipeFinder
            // 
            this.btnRecipeFinder.Location = new System.Drawing.Point(340, 12);
            this.btnRecipeFinder.MaximumSize = new System.Drawing.Size(26, 20);
            this.btnRecipeFinder.MinimumSize = new System.Drawing.Size(26, 20);
            this.btnRecipeFinder.Name = "btnRecipeFinder";
            this.btnRecipeFinder.Size = new System.Drawing.Size(26, 20);
            this.btnRecipeFinder.TabIndex = 6;
            this.btnRecipeFinder.Text = "...";
            this.btnRecipeFinder.UseVisualStyleBackColor = true;
            this.btnRecipeFinder.Click += new System.EventHandler(this.btnRecipeFinder_Click);
            // 
            // txtIngredient
            // 
            this.txtIngredient.Location = new System.Drawing.Point(134, 67);
            this.txtIngredient.Name = "txtIngredient";
            this.txtIngredient.Size = new System.Drawing.Size(202, 20);
            this.txtIngredient.StyleController = this.layoutControl1;
            this.txtIngredient.TabIndex = 5;
            this.txtIngredient.Leave += new System.EventHandler(this.txtIngredient_Leave);
            // 
            // btnIngredientsFinder
            // 
            this.btnIngredientsFinder.Location = new System.Drawing.Point(340, 67);
            this.btnIngredientsFinder.MaximumSize = new System.Drawing.Size(26, 20);
            this.btnIngredientsFinder.MinimumSize = new System.Drawing.Size(26, 20);
            this.btnIngredientsFinder.Name = "btnIngredientsFinder";
            this.btnIngredientsFinder.Size = new System.Drawing.Size(26, 20);
            this.btnIngredientsFinder.TabIndex = 7;
            this.btnIngredientsFinder.Text = "...";
            this.btnIngredientsFinder.UseVisualStyleBackColor = true;
            this.btnIngredientsFinder.Click += new System.EventHandler(this.btnIngredientFinder_Click);
            // 
            // txtRecipe
            // 
            this.txtRecipe.Location = new System.Drawing.Point(134, 12);
            this.txtRecipe.Name = "txtRecipe";
            this.txtRecipe.Size = new System.Drawing.Size(202, 20);
            this.txtRecipe.StyleController = this.layoutControl1;
            this.txtRecipe.TabIndex = 4;
            this.txtRecipe.Leave += new System.EventHandler(this.txtRecipe_Leave);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcRecipePath,
            this.lciIngredientsPath,
            this.lciRecipeFinder,
            this.lciIngredientsFinder,
            this.lciGo,
            this.lciRecipeSheets,
            this.lciIngredientSheets,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.lciErrorMessages,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(541, 408);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcRecipePath
            // 
            this.lcRecipePath.Control = this.txtRecipe;
            this.lcRecipePath.CustomizationFormText = "Recipe Spreadsheet";
            this.lcRecipePath.Location = new System.Drawing.Point(0, 0);
            this.lcRecipePath.Name = "lcRecipePath";
            this.lcRecipePath.Size = new System.Drawing.Size(328, 24);
            this.lcRecipePath.Text = "Recipe Spreadsheet";
            this.lcRecipePath.TextSize = new System.Drawing.Size(119, 13);
            // 
            // lciIngredientsPath
            // 
            this.lciIngredientsPath.Control = this.txtIngredient;
            this.lciIngredientsPath.CustomizationFormText = "Ingredients Spreadsheet";
            this.lciIngredientsPath.Location = new System.Drawing.Point(0, 55);
            this.lciIngredientsPath.Name = "lciIngredientsPath";
            this.lciIngredientsPath.Size = new System.Drawing.Size(328, 24);
            this.lciIngredientsPath.Text = "Ingredients Spreadsheet";
            this.lciIngredientsPath.TextSize = new System.Drawing.Size(119, 13);
            // 
            // lciRecipeFinder
            // 
            this.lciRecipeFinder.Control = this.btnRecipeFinder;
            this.lciRecipeFinder.CustomizationFormText = "layoutControlItem1";
            this.lciRecipeFinder.Location = new System.Drawing.Point(328, 0);
            this.lciRecipeFinder.Name = "lciRecipeFinder";
            this.lciRecipeFinder.Size = new System.Drawing.Size(30, 24);
            this.lciRecipeFinder.Text = "lciRecipeFinder";
            this.lciRecipeFinder.TextSize = new System.Drawing.Size(0, 0);
            this.lciRecipeFinder.TextToControlDistance = 0;
            this.lciRecipeFinder.TextVisible = false;
            // 
            // lciIngredientsFinder
            // 
            this.lciIngredientsFinder.Control = this.btnIngredientsFinder;
            this.lciIngredientsFinder.CustomizationFormText = "layoutControlItem2";
            this.lciIngredientsFinder.Location = new System.Drawing.Point(328, 55);
            this.lciIngredientsFinder.Name = "lciIngredientsFinder";
            this.lciIngredientsFinder.Size = new System.Drawing.Size(30, 24);
            this.lciIngredientsFinder.Text = "lciIngredientsFinder";
            this.lciIngredientsFinder.TextSize = new System.Drawing.Size(0, 0);
            this.lciIngredientsFinder.TextToControlDistance = 0;
            this.lciIngredientsFinder.TextVisible = false;
            // 
            // lciGo
            // 
            this.lciGo.Control = this.btnGo;
            this.lciGo.CustomizationFormText = "layoutControlItem3";
            this.lciGo.Location = new System.Drawing.Point(462, 362);
            this.lciGo.Name = "lciGo";
            this.lciGo.Size = new System.Drawing.Size(59, 26);
            this.lciGo.Text = "lciGo";
            this.lciGo.TextSize = new System.Drawing.Size(0, 0);
            this.lciGo.TextToControlDistance = 0;
            this.lciGo.TextVisible = false;
            // 
            // lciRecipeSheets
            // 
            this.lciRecipeSheets.Control = this.lueRecipeSheets;
            this.lciRecipeSheets.CustomizationFormText = "lciRecipeSheets";
            this.lciRecipeSheets.Location = new System.Drawing.Point(358, 0);
            this.lciRecipeSheets.Name = "lciRecipeSheets";
            this.lciRecipeSheets.Size = new System.Drawing.Size(163, 24);
            this.lciRecipeSheets.Text = "lciRecipeSheets";
            this.lciRecipeSheets.TextSize = new System.Drawing.Size(0, 0);
            this.lciRecipeSheets.TextToControlDistance = 0;
            this.lciRecipeSheets.TextVisible = false;
            // 
            // lciIngredientSheets
            // 
            this.lciIngredientSheets.Control = this.lueIngredientSheets;
            this.lciIngredientSheets.CustomizationFormText = "lciIngredientSheets";
            this.lciIngredientSheets.Location = new System.Drawing.Point(358, 55);
            this.lciIngredientSheets.Name = "lciIngredientSheets";
            this.lciIngredientSheets.Size = new System.Drawing.Size(163, 24);
            this.lciIngredientSheets.Text = "lciIngredientSheets";
            this.lciIngredientSheets.TextSize = new System.Drawing.Size(0, 0);
            this.lciIngredientSheets.TextToControlDistance = 0;
            this.lciIngredientSheets.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 79);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(521, 24);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(521, 31);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(231, 362);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(231, 26);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lciErrorMessages
            // 
            this.lciErrorMessages.Control = this.lbErrorMessages;
            this.lciErrorMessages.CustomizationFormText = "lciErrorMessages";
            this.lciErrorMessages.Location = new System.Drawing.Point(0, 103);
            this.lciErrorMessages.Name = "lciErrorMessages";
            this.lciErrorMessages.Size = new System.Drawing.Size(521, 259);
            this.lciErrorMessages.Text = "lciErrorMessages";
            this.lciErrorMessages.TextSize = new System.Drawing.Size(0, 0);
            this.lciErrorMessages.TextToControlDistance = 0;
            this.lciErrorMessages.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnCleanupManufacturers;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 362);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(231, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // ExcelImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 408);
            this.Controls.Add(this.layoutControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExcelImportForm";
            this.Text = "Recipe Import";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lueIngredientSheets.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueRecipeSheets.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIngredient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecipe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRecipePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIngredientsPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRecipeFinder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIngredientsFinder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciGo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRecipeSheets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIngredientSheets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciErrorMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit txtRecipe;
        private DevExpress.XtraLayout.LayoutControlItem lcRecipePath;
        private DevExpress.XtraEditors.TextEdit txtIngredient;
        private DevExpress.XtraLayout.LayoutControlItem lciIngredientsPath;
        private DevExpress.XtraEditors.LookUpEdit lueRecipeSheets;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnRecipeFinder;
        private System.Windows.Forms.Button btnIngredientsFinder;
        private DevExpress.XtraLayout.LayoutControlItem lciRecipeFinder;
        private DevExpress.XtraLayout.LayoutControlItem lciIngredientsFinder;
        private DevExpress.XtraLayout.LayoutControlItem lciGo;
        private DevExpress.XtraLayout.LayoutControlItem lciRecipeSheets;
        private DevExpress.XtraEditors.LookUpEdit lueIngredientSheets;
        private DevExpress.XtraLayout.LayoutControlItem lciIngredientSheets;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private System.Windows.Forms.ListBox lbErrorMessages;
        private DevExpress.XtraLayout.LayoutControlItem lciErrorMessages;
        private DevExpress.XtraEditors.SimpleButton btnCleanupManufacturers;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}

