using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BeverageManagement.Controls;
using BeverageManagement.Properties;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;

namespace BeverageManagement.Forms.Reconcile
{

// ReSharper disable InconsistentNaming
    public partial class frmRecipes : DevExpress.XtraEditors.XtraForm //, IRecipesView
// ReSharper restore InconsistentNaming
    {
        LayoutControlItem previousItem = null;
        private LayoutControlGroup group = null;
        private static Size MinCtrlSize = new Size(236, 108);
        private static Size MinGroupSize = new Size(241, 113);
        private IBLRecipe m_CurrentRecipe;

        private List<IngredientItem> m_RecipeItems = new List<IngredientItem>();
        
        public frmRecipes()
        {
            InitializeComponent();
        }

        public IEnumerable<IBLRecipe> RecipeList
        {
            set
            {
                var previous = SelectedRecipeId;
                lbcRecipes.Items.Clear();
                foreach (var recipe in value)
                {
                    lbcRecipes.Items.Add(recipe);
                }
                SelectedRecipeId = previous;
            }
        }

        public IEnumerable<IngredientListItem> IngredientList
        {
            get { return null; }
            set
            {
                var ingredients = new BindingList<IIngredientDisplayItem>(value.Cast<IIngredientDisplayItem>().ToList());
            }
        }

        public IEnumerable<IBLUPCItem> UpcList
        {
            set
            {
                var editor = new RepositoryItemLookUpEdit
                {
                    ValueMember = "UPCID",
                    DisplayMember = "Name",
                    NullText = string.Empty,
                    DataSource = value.ToList()
                };

                editor.Columns.Add(new LookUpColumnInfo("Name", "UPC Name"));
                editor.Name = "Name";
                editor.EditValueChanged += GridEditorValueChanged;
            }
        }

        private void GridEditorValueChanged(object _sender, EventArgs _e)
        {
        }

        public string RecipeName
        {
            get { return teRecipeName.Text; }
            set { teRecipeName.Text = value; }
        }

        public Guid SelectedRecipeId
        {
            get
            {
                var selectedItem = lbcRecipes.SelectedItem;
                return selectedItem == null ? Guid.Empty : ((IBLRecipe) selectedItem).ObjectID;
            }
            set
            {
                var selectedItem = lbcRecipes.Items.Cast<IBLRecipe>().FirstOrDefault(_item => _item.ObjectID == value);

                if (selectedItem != null)
                {
                    lbcRecipes.SelectedItem = selectedItem;
                }
                else
                {
                    var oldSelectionMode = lbcRecipes.SelectionMode;
                    lbcRecipes.SelectionMode = SelectionMode.None;
                    lbcRecipes.SelectionMode = oldSelectionMode;
                    lbcRecipes.Refresh();
                }
            }
        }

       public DialogResult AskDelete()
        {
            var message = string.Format("Are you sure you want to delete the recipe named \"{0}\"?", RecipeName);
            return MessageBox.Show(message, Resources.ApplicationTitle, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        public DialogResult AskSave()
        {
            var message = string.Format("Do you want to save changes to the recipe named \"{0}\"?", RecipeName);
            return MessageBox.Show(message, Resources.ApplicationTitle, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            var ingredient = new IngredientItem();
            ingredient.Size = new Size(236, 108);

            lcIngredience.BeginUpdate();
            try
            {
                LayoutControlItem item = null;
                if (0 == lcIngredience.Root.Items.Count)
                {
                    item = lcIngredience.Root.AddItem();
                    lcIngredience.Root.GroupBordersVisible = true;
                }
                else
                {
                    item = new LayoutControlItem();
                    item.Parent = lcIngredience.Root;
                }
                item.SizeConstraintsType = SizeConstraintsType.Custom;
                item.ControlMaxSize = new Size(0, 100);
                item.ControlMinSize = new Size(236, 108);
                item.FillControlToClientArea = false;
                item.ControlMaxSize = new Size(lcIngredience.Width, lcIngredience.Height);
                item.ControlAlignment = ContentAlignment.MiddleCenter;
                item.Control = ingredient;
                item.TextVisible = false;
            }
            finally
            {
                // Unlock and update the layout control.
                lcIngredience.EndUpdate();
            }
        }


        private void LoadRecipe( IBLRecipe _recipe)
        {
            if (null != _recipe)
            {
                RecipeName = _recipe.Description;

                lcIngredience.BeginUpdate();
                lcIngredience.Clear();
                previousItem = null;
                group = new LayoutControlGroup
                {
                    Name = "IngredientInfo",
                    Text = "Ingredient Info",
                    TextVisible = false
                };

                lcIngredience.Root.Add(group);
                try
                {
                    foreach (var ingredient in _recipe.Ingredients)
                    {
                        var item = new IngredientItem(ingredient);
                        AddItem(item);
                    }
                }
                finally
                {
                    lcIngredience.EndUpdate();
                }
            }
        }
        private void AddItem(IngredientItem _item = null)
        {
            LayoutControlItem item = null;
            if (null == _item)
            {
                _item = new IngredientItem();
            }
            m_RecipeItems.Add(_item);

            _item.Size = MinCtrlSize;
            if (null == previousItem)
            {
                previousItem = group.AddItem();
                previousItem.TextVisible = false;
            }
            item = new LayoutControlItem(lcIngredience, _item);
            item.Move(previousItem, InsertType.Top);
            item.SizeConstraintsType = SizeConstraintsType.Custom;
            item.MinSize = MinGroupSize;
            item.ControlMinSize = MinCtrlSize;
            item.TextVisible = false;
            previousItem = item;
        }
        
        private void lbcRecipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( null == m_CurrentRecipe || SelectedRecipeId != m_CurrentRecipe.RecipeID)
            {
                var recipe = lbcRecipes.SelectedItem as IBLRecipe;
                if( null != recipe )
                {
                    teRecipeName.Text = recipe.Description;
                }
                m_CurrentRecipe = BLManagerFactory.Get().ManageRecipes().Get(SelectedRecipeId);
                RecipeName = m_CurrentRecipe == null ? string.Empty : m_CurrentRecipe.Description;

                LoadRecipe(m_CurrentRecipe);
            }
        }

        private void frmRecipes_Load(object sender, EventArgs e)
        {
            RecipeList = BLManagerFactory.Get().ManageRecipes().GetAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var ingredient in m_CurrentRecipe.Ingredients)
                {
                    BLManagerFactory.Get().ManageIngredients().Save(ingredient);
                }
                BLManagerFactory.Get().ManageRecipes().Save(m_CurrentRecipe);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            m_CurrentRecipe = BLManagerFactory.Get().ManageRecipes().Create();
            LoadRecipe(m_CurrentRecipe);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == AskDelete())
            {
                BLManagerFactory.Get().ManageRecipes().Delete(m_CurrentRecipe);
            }
        }
    }
}
