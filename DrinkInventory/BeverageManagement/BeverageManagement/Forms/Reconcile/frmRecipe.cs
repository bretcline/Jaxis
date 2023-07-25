using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BeverageManagement.Controls;
using BeverageManagement.Properties;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;

namespace BeverageManagement.Forms.Reconcile
{
    public partial class frmRecipe : DevExpress.XtraEditors.XtraForm
    {
        LayoutControlItem previousItem = null;
        private LayoutControlGroup group = null;
        private static Size MinCtrlSize = new Size(236, 108);
        private static Size MinGroupSize = new Size(241, 113);
        private IBLRecipe m_CurrentRecipe;
        List<IngredientItem> m_Ingredients = new List<IngredientItem>();
        //Dictionary<IngredientItem, LayoutControlItem> m_LayoutControls = new List<IngredientItem, LayoutControlItem>();

        public frmRecipe()
        {
            InitializeComponent();
        }

        private void LoadRecipe(IBLRecipe _recipe)
        {
            if (null != _recipe)
            {
                txtRecipeName.Text = _recipe.Description;

                lcIngredience.BeginUpdate();
                lcIngredience.Clear();
                m_Ingredients.Clear();
                //m_LayoutControls.Clear();
                previousItem = null;
                group = new LayoutControlGroup();
                group.Name = "IngredientInfo";
                group.Text = "Ingredient Info";
                group.TextVisible = false;

                lcIngredience.Root.Add(group);
                try
                {
                    foreach (var ingredient in _recipe.Ingredients.OrderByDescending( i => i.Number ))
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
                _item.Ingredient.Number = m_Ingredients.Count;
            }
            _item.OnDelete -= new IngredientItem.DeleteEvent(_item_OnDelete);
            _item.OnDelete += new IngredientItem.DeleteEvent(_item_OnDelete);

            _item.Size = MinCtrlSize;
            m_Ingredients.Add(_item);
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

        void _item_OnDelete( IngredientItem _control, IBLIngredient _item)
        {
            BLManagerFactory.Get().ManageIngredients().Delete(_item);
            m_CurrentRecipe = BLManagerFactory.Get().ManageRecipes().Get(m_CurrentRecipe.RecipeID);
            this.LoadRecipe( this.m_CurrentRecipe );
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            m_CurrentRecipe.Description = txtRecipeName.Text;
            BLManagerFactory.Get().ManageRecipes().Save(m_CurrentRecipe);
            foreach (var ingredientItem in m_Ingredients)
            {
                var ingredient = ingredientItem.Ingredient;
                ingredient.RecipeID = m_CurrentRecipe.RecipeID;
                BLManagerFactory.Get().ManageIngredients().Save(ingredient);
            }


        }

        private void frmRecipe_Load(object sender, EventArgs e)
        {
            var recipes = BLManagerFactory.Get().ManageRecipes().GetAll().OrderBy( r => r.Description ).ToList();
            lstRecipes.DataSource = recipes;

        }

        private void lstRecipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = lstRecipes.SelectedItem as IBLRecipe;
            if (null != item)
            {
                if (item != m_CurrentRecipe)
                {
                    m_CurrentRecipe = item;
                    LoadRecipe( item );
                }
            }
        }

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRecipe( m_CurrentRecipe );
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            btnSave_Click( sender, e);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            m_CurrentRecipe = BLManagerFactory.Get().ManageRecipes().Create();
            LoadRecipe( m_CurrentRecipe );
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var message = string.Format("Are you sure you want to delete the recipe named \"{0}\"?", m_CurrentRecipe.Description);
            if (DialogResult.Yes == MessageBox.Show(message, Resources.ApplicationTitle, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                BLManagerFactory.Get().ManageRecipes().Delete(m_CurrentRecipe);
            }
        }

        //public DialogResult AskSave()
        //{
        //    var message = string.Format("Do you want to save changes to the recipe named \"{0}\"?", RecipeName);
        //    return MessageBox.Show(message, Resources.ApplicationTitle, MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        //}
    }
}