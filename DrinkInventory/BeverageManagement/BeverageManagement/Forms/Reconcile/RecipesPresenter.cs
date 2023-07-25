using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;

namespace BeverageManagement.Forms.Reconcile
{
    //public class RecipesPresenter
    //{
    //    private readonly IRecipeBLManager m_recipeManager;
    //    private readonly IUPCItemBLManager m_upcManager;
    //    private readonly IRecipesView m_view;
    //    private bool m_recipeModified;
    //    private Guid m_currentRecipeId;

    //    public RecipesPresenter(IRecipesView _view, IBLManagerFactory _factory = null)
    //    {
    //        m_view = _view;

    //        // Allowing the factory to be passed in allows for injecting fake
    //        // managers into the tests.  Otherwise, we create the "real" managers.
    //        if (_factory == null) _factory = BLManagerFactory.Get();
    //        m_recipeManager = _factory.ManageRecipes();
    //        m_upcManager = _factory.ManageUPCs();

    //        SubscribeViewEvents();
    //        RecipeModified = false;
    //    }

    //    private void SubscribeViewEvents()
    //    {
    //        m_view.Load += Load;
    //        m_view.NewClick += NewClick;
    //        m_view.DeleteClick += DeleteClick;
    //        m_view.SaveClick += SaveClick;
    //        m_view.RecipeModified += RecipeModifiedHandler;
    //        m_view.SelectedRecipeIdChanged += SelectedRecipeIdChanged;
    //    }

    //    private void UnsubscribeViewEvents()
    //    {
    //        m_view.Load -= Load;
    //        m_view.NewClick -= NewClick;
    //        m_view.DeleteClick -= DeleteClick;
    //        m_view.SaveClick -= SaveClick;
    //        m_view.RecipeModified -= RecipeModifiedHandler;
    //        m_view.SelectedRecipeIdChanged -= SelectedRecipeIdChanged;
    //    }

    //    private void DeleteClick(object _sender, EventArgs _e)
    //    {
    //        if (m_view.SelectedRecipeId == Guid.Empty) return;
    //        var recipe = m_recipeManager.Get(m_view.SelectedRecipeId);
    //        if (recipe == null) return;
    //        var result = m_view.AskDelete();
    //        if (result != DialogResult.Yes) return;
    //        m_recipeModified = false;
    //        m_recipeManager.Delete(recipe);
    //        m_currentRecipeId = Guid.Empty;
    //        UpdateRecipeList();
    //    }

    //    private void SelectedRecipeIdChanged(object _sender, EventArgs _e)
    //    {
    //        if (m_view.SelectedRecipeId == m_currentRecipeId) return;
    //        CheckSave();
    //        m_currentRecipeId = m_view.SelectedRecipeId;
    //        IgnoreViewEvents(() =>
    //        {
    //            var recipe = m_recipeManager.Get(m_view.SelectedRecipeId);
    //            m_view.RecipeName = recipe == null ? string.Empty : recipe.Description;
    //            m_view.IngredientList = recipe == null ? new List<IngredientListItem>() :
    //                (from i in recipe.Ingredients select new IngredientListItem(i)).ToList();
    //        });

    //        RecipeModified = false;
    //    }

    //    private void IgnoreViewEvents(Action _action)
    //    {
    //        try
    //        {
    //            UnsubscribeViewEvents();
    //            _action();
    //        }
    //        finally
    //        {
    //            SubscribeViewEvents();
    //        }
    //    }

    //    private void CheckSave()
    //    {
    //        if (!m_recipeModified) return;
    //        var answer = m_view.AskSave();
    //        if (answer != DialogResult.Yes) return;
    //        SaveCurrent();
    //    }

    //    private void Load(object _sender, EventArgs _e)
    //    {
    //        var upcs = from u in m_upcManager.GetAll() orderby u.Name select u;
    //        m_view.UpcList = upcs;
    //        UpdateRecipeList();
    //    }

    //    private void UpdateRecipeList()
    //    {
    //        m_view.RecipeList = from r in m_recipeManager.GetAll() orderby r.Description select r;
    //    }

    //    private bool RecipeModified
    //    {
    //        get
    //        {
    //            return m_recipeModified;
    //        }
    //        set
    //        {
    //            m_recipeModified = value;
    //            EnableControls();
    //        }
    //    }

    //    private void RecipeModifiedHandler(object _sender, EventArgs _e)
    //    {
    //        RecipeModified = true;
    //    }

    //    private void SaveClick(object _sender, EventArgs _e)
    //    {
    //        var recipeId = SaveCurrent();
    //        IgnoreViewEvents(() =>
    //        {
    //            m_view.SelectedRecipeId = recipeId;
    //        });

    //        EnableControls();
    //    }

    //    private Guid SaveCurrent()
    //    {
    //        var recipe = m_currentRecipeId == Guid.Empty ? m_recipeManager.Create() : m_recipeManager.Get(m_currentRecipeId);
    //        recipe.Description = m_view.RecipeName;
    //        UpdateIngredients(recipe);
    //        m_recipeManager.Save(recipe);
    //        IgnoreViewEvents(UpdateRecipeList);
    //        RecipeModified = false;
    //        return recipe.ObjectID;
    //    }

    //    private void UpdateIngredients(IBLRecipe _recipe)
    //    {
    //        var viewIngredients = new List<IngredientListItem>(m_view.IngredientList);
    //        var deleteList = new List<IBLIngredient>();

    //        // 1: Go through each ingredient on the recipe data object and either update them
    //        // from the corresponding view item or add them for deletion
    //        foreach (var ingredient in _recipe.Ingredients)
    //        {
    //            var viewIngredient = (from vi in viewIngredients 
    //                where vi.IngredientId == ingredient.ObjectID select vi).FirstOrDefault();
                
    //            if (viewIngredient != null)
    //            {
    //                viewIngredient.UpdateIngredient(ingredient);
    //                viewIngredients.Remove(viewIngredient);
    //            }
    //            else
    //            {
    //                deleteList.Add(ingredient);
    //            }
    //        }

    //        // 2: Delete all that were not present in the view
    //        foreach (var ingredient in deleteList)
    //        {
    //            _recipe.RemoveIngredient(ingredient);
    //        }

    //        // 3: Create new ones for every new ingredient in the view list
    //        foreach (var viewIngredient in viewIngredients)
    //        {
    //            var ingredient = _recipe.NewIngredient();
    //            viewIngredient.UpdateIngredient(ingredient);
    //        }
    //    }

    //    private void NewClick(object _sender, EventArgs _e)
    //    {
    //        CheckSave();
            
    //        IgnoreViewEvents(() =>
    //        {
    //            m_view.RecipeName = string.Empty;
    //            m_view.IngredientList = new List<IngredientListItem>();
    //            m_view.SelectedRecipeId = Guid.Empty;
    //        });
            
    //        m_currentRecipeId = Guid.Empty;
    //        RecipeModified = false;
    //    }

    //    private void EnableControls()
    //    {
    //        m_view.EnableNewButton(true);
    //        m_view.EnableDeleteButton(m_view.SelectedRecipeId != Guid.Empty);
    //        m_view.EnableRecipePanel(true);
    //        m_view.EnableCloseButton(true);
    //        m_view.EnableSaveButton(RecipeModified);
    //    }
    //}
}
