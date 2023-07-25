using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BeverageManagement.Forms.Reconcile;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes
{
    internal class FakeRecipesView : IRecipesView
    {
        private string m_recipeName;
        private Guid m_selectedRecipeId;
        private IEnumerable<IBLRecipe> m_recipeList;
        private List<IngredientListItem> m_ingredientList;
        private List<IBLUPCItem> m_concreteUpcList;

        internal FakeRecipesView(IBLManagerFactory _factory)
        {
            m_recipeList = new List<IBLRecipe>();
            m_ingredientList = new List<IngredientListItem>();
            m_recipeName = string.Empty;
            new RecipesPresenter(this, _factory);
        }

        public event EventHandler Load;
        public event EventHandler NewClick;
        public event EventHandler DeleteClick;
        public event EventHandler SaveClick;
        public event EventHandler RecipeModified;
        public event EventHandler SelectedRecipeIdChanged;

        public Guid SelectedRecipeId
        {
            get
            {
                return m_selectedRecipeId;
            }
            set
            {
                if (m_selectedRecipeId != value)
                {
                    m_selectedRecipeId = value;

                    if (SelectedRecipeIdChanged != null)
                    {
                        SelectedRecipeIdChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public void ShowDialog()
        {
            Shown = true;
        }

        public void EnableDeleteButton(bool _enabled)
        {
            DeleteEnabled = _enabled;
        }

        public void EnableRecipePanel(bool _enabled)
        {
            RecipePanelEnabled = _enabled;
        }

        public void EnableSaveButton(bool _enabled)
        {
            SaveEnabled = _enabled;
        }

        public void EnableNewButton(bool _enabled)
        {
            NewEnabled = _enabled;
        }

        public void EnableCloseButton(bool _enabled)
        {
            CloseEnabled = _enabled;
        }

        public DialogResult AskDelete()
        {
            AskedDelete = true;
            return DeleteAnswer;
        }

        public DialogResult AskSave()
        {
            AskedSave = true;
            return SaveAnswer;
        }


        public bool AskedDelete { get; set; }
        public DialogResult DeleteAnswer { get; set; }
        public bool AskedSave { get; set; }
        public DialogResult SaveAnswer { get; set; }

        internal static bool? Shown { get; set; }
        internal bool? DeleteEnabled { get; set; }
        internal bool? RecipePanelEnabled { get; set; }
        internal bool? SaveEnabled { get; set; }
        internal bool? NewEnabled { get; set; }
        internal bool? CloseEnabled { get; set; }

        public IEnumerable<IngredientListItem> IngredientList
        {
            get
            {
                return m_ingredientList;
            }
            set
            {
                m_ingredientList = new List<IngredientListItem>(value);
            }
        }

        public List<IngredientListItem> ConcreteIngredientList
        {
            get { return m_ingredientList; }
        }

        public IEnumerable<IBLUPCItem> UpcList
        {
            set { m_concreteUpcList = value.ToList(); }
        }

        public string RecipeName
        {
            get
            {
                return m_recipeName;
            }
            set
            {
                m_recipeName = value;
                if (RecipeModified != null)
                {
                    RecipeModified(this, EventArgs.Empty);
                }
            }
        }
        
        public IEnumerable<IBLRecipe> RecipeList
        {
            get
            {
                return m_recipeList;
            } 
            set
            {
                m_recipeList = value;
            }
        }

        public List<IBLUPCItem> ConcreteUpcList
        {
            get {
                return m_concreteUpcList;
            }
            set {
                m_concreteUpcList = value;
            }
        }

        internal void FireLoad()
        {
            if (Load != null)
            {
                Load(this, EventArgs.Empty);
            }
        }

        internal void FireNewClick()
        {
            if (NewClick != null)
            {
                NewClick(this, EventArgs.Empty);
            }
        }

        internal void FireSaveClick()
        {
            if (SaveClick != null)
            {
                SaveClick(this, EventArgs.Empty);
            }
        }

        public void FireDelete()
        {
            if (DeleteClick != null)
            {
                DeleteClick(this, EventArgs.Empty);
            }
        }

        public void CreateNewIngredient(IngredientListItem _ingredient)
        {
            m_ingredientList.Add(_ingredient);
        }
    }
}
