using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Jaxis.Inventory.Data.IBLDataItems;

namespace BeverageManagement.Forms.Reconcile
{
    public interface IRecipesView
    {
        event EventHandler Load;
        event EventHandler NewClick;
        event EventHandler DeleteClick;
        event EventHandler SaveClick;
        event EventHandler RecipeModified;
        event EventHandler SelectedRecipeIdChanged;

        IEnumerable<IBLRecipe> RecipeList { set; }
        IEnumerable<IngredientListItem> IngredientList { get; set; }
        IEnumerable<IBLUPCItem> UpcList { set; }
        string RecipeName { get; set; }
        Guid SelectedRecipeId { get; set; }
        
        void ShowDialog();
        void EnableDeleteButton(bool _enabled);
        void EnableRecipePanel(bool _enabled);
        void EnableSaveButton(bool _enabled);
        void EnableNewButton(bool _enabled);
        void EnableCloseButton(bool _enabled);
        DialogResult AskDelete();
        DialogResult AskSave();
    }
}
