using System;
using System.Linq;
using System.Windows.Forms;
using BeverageManagement.Forms.Reconcile;
using Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;
using NUnit.Framework;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.BeverageManagementTest
{
    [TestFixture]
    public class RecipesPresenterTest
    {
        private FakeRecipesView m_view;
        private IBLManagerFactory m_factory;

        [SetUp]
        public void SetUp()
        {
            m_factory = FakeManagerFactory.Recreate();
            m_view = new FakeRecipesView(m_factory);
            m_view.FireLoad();
        }

        [Test]
        public void InitialElementStates()
        {
            AssertHelper.HasValueIsTrue(m_view.NewEnabled);
            AssertHelper.HasValueIsFalse(m_view.DeleteEnabled);
            AssertHelper.HasValueIsTrue(m_view.RecipePanelEnabled);
            AssertHelper.HasValueIsTrue(m_view.CloseEnabled);
            AssertHelper.HasValueIsFalse(m_view.SaveEnabled);
        }

        [Test]
        public void NewClearsRecipeFields()
        {
            m_view.RecipeName = "some text that should not be here after New";
            m_view.ConcreteIngredientList.Add(new IngredientListItem());

            m_view.FireNewClick();

            Assert.IsEmpty(m_view.RecipeName);
            Assert.IsEmpty(m_view.IngredientList.ToList());
        }

        [Test]
        public void SaveSavesNewRecipe()
        {
            m_view.FireNewClick(); // start editing a new recipe
            m_view.RecipeName = "New Recipe"; // make some change
            m_view.FireSaveClick(); // should cause the recipe to be saved
            var recipe = (from m in m_factory.ManageRecipes().GetAll() where m.Description == "New Recipe" select m).FirstOrDefault();
            Assert.IsNotNull(recipe);
        }

        [Test]
        public void MofifyingRecipeNameCausesSaveToBeEnabled()
        {
            m_view.RecipeName = "Modified name";
            AssertHelper.HasValueIsTrue(m_view.SaveEnabled);
        }
        
        [Test]
        public void SaveListsAndSelectsNewRecipe()
        {
            m_view.FireNewClick(); // start editing a new recipe
            m_view.RecipeName = "New Recipe"; // make some change
            m_view.FireSaveClick(); // should cause the recipe to be saved
            var recipe = (from r in m_view.RecipeList select r).FirstOrDefault();
            Assert.IsNotNull(recipe);
            Assert.AreEqual(recipe.Description, "New Recipe");
            Assert.AreEqual(m_view.SelectedRecipeId, recipe.ObjectID);
        }

        [Test]
        public void RecipeToString()
        {
            var recipe = m_factory.ManageRecipes().Create();
            recipe.Description = "Better be in the ToString so the view list shows the right thing";
            Assert.AreEqual("Better be in the ToString so the view list shows the right thing", recipe.ToString());
        }

        [Test]
        public void InitiallyPopulatesRecipeList()
        {
            RecreateWithTestRecipes();
            var recipeList = m_view.RecipeList.ToList();
            Assert.AreEqual(2, recipeList.Count);
        }
        
        [Test]
        public void ShowsSelectedRecipe()
        {
            RecreateWithTestRecipes();
            var recipeId = GetRecipeIdFromView("Recipe 2");
            Log.Info(string.Format("RecipePresenterTest.ShowsSelectedRecipe selecting id {0}", recipeId));
            m_view.SelectedRecipeId = recipeId;
            Assert.AreEqual("Recipe 2", m_view.RecipeName);
        }
        
        [Test]
        public void ControlStatesAfterSelect()
        {
            RecreateWithTestRecipes();
            var recipeId = GetRecipeIdFromView("Recipe 2");
            Log.Info(string.Format("RecipePresenterTest.ShowsSelectedRecipe selecting id {0}", recipeId));
            m_view.SelectedRecipeId = recipeId;

            AssertHelper.HasValueIsTrue(m_view.NewEnabled);
            AssertHelper.HasValueIsTrue(m_view.DeleteEnabled);
            AssertHelper.HasValueIsTrue(m_view.RecipePanelEnabled);
            AssertHelper.HasValueIsTrue(m_view.CloseEnabled);
            AssertHelper.HasValueIsFalse(m_view.SaveEnabled);
        }

        [Test]
        public void Deletes()
        {
            RecreateWithTestRecipes();
            var recipeId = GetRecipeIdFromView("Recipe 2");
            Log.Info(string.Format("RecipePresenterTest.ShowsSelectedRecipe selecting id {0}", recipeId));
            m_view.SelectedRecipeId = recipeId;
            m_view.DeleteAnswer = DialogResult.Yes;
            m_view.FireDelete();
            var recipe = (from r in m_view.RecipeList where r.ObjectID == recipeId select r).FirstOrDefault();
            Assert.IsNull(recipe);
        }
        
        [Test]
        public void SavesChangesWhenSelectionChanged()
        {
            RecreateWithTestRecipes();
            var recipeId1 = GetRecipeIdFromView("Recipe 1");
            var recipeId2 = GetRecipeIdFromView("Recipe 2");
            
            m_view.SelectedRecipeId = recipeId1;
            m_view.RecipeName = "Changed Name";
            m_view.SaveAnswer = DialogResult.Yes;
            m_view.SelectedRecipeId = recipeId2;

            Assert.IsTrue(m_view.AskedSave, "should ask to save modifications");

            var recipe = m_factory.ManageRecipes().Get(recipeId1);
            Assert.AreEqual("Changed Name", recipe.Description);

            var recipes = m_view.RecipeList.ToList();
            Assert.AreEqual(2, recipes.Count);
            Assert.IsNotNull(recipes.Find(_r => _r.Description == "Changed Name"));

            Assert.AreEqual(recipeId2, m_view.SelectedRecipeId, "should select the one we chose");
        }
        
        [Test]
        public void UpdatesListAfterSave()
        {
            RecreateWithTestRecipes();
            var recipeId1 = GetRecipeIdFromView("Recipe 1");
            m_view.SelectedRecipeId = recipeId1;
            m_view.RecipeName = "New Name";
            m_view.FireSaveClick();
            var recipes = m_view.RecipeList.ToList();
            Assert.AreEqual(2, recipes.Count);
            Assert.IsNotNull(recipes.Find(_r => _r.Description == "New Name"));
            Assert.IsNotNull(recipes.Find(_r => _r.Description == "Recipe 2"));
        }

        [Test]
        public void NewSetsSelectedIdToEmpty()
        {
            RecreateWithTestRecipes();
            var recipeId1 = GetRecipeIdFromView("Recipe 1");
            m_view.SelectedRecipeId = recipeId1;
            m_view.FireNewClick();
            Assert.AreEqual(Guid.Empty, m_view.SelectedRecipeId);
        }

        [Test]
        public void SaveDoesNotCausePrompt()
        {
            m_view.FireNewClick();
            m_view.RecipeName = "Something";
            m_view.FireSaveClick();
            Assert.IsFalse(m_view.AskedDelete);
            Assert.IsFalse(m_view.AskedSave);
        }

        [Test]
        public void SaveAddsNewRecipe()
        {
            m_view.FireNewClick();
            m_view.RecipeName = "Whatever";
            m_view.FireSaveClick();
            
            var recipes = m_factory.ManageRecipes().GetAll().ToList();
            Assert.AreEqual(1, recipes.Count);
            
            m_view.FireNewClick();
            m_view.RecipeName = "Another";
            m_view.FireSaveClick();

            recipes = m_factory.ManageRecipes().GetAll().ToList();
            Assert.AreEqual(2, recipes.Count);
        }

        [Test]
        public void SavesNewWhenSelectionChanged()
        {
            RecreateWithTestRecipes();
            var recipeId1 = GetRecipeIdFromView("Recipe 1");

            m_view.SelectedRecipeId = recipeId1;
            m_view.FireNewClick();
            m_view.SaveAnswer = DialogResult.Yes;
            m_view.RecipeName = "New Name";
            m_view.SelectedRecipeId = recipeId1;
        
            Assert.IsTrue(m_view.AskedSave);

            var recipes = m_factory.ManageRecipes().GetAll().ToList();
            Assert.AreEqual(3, recipes.Count);
            Assert.IsNotNull(recipes.Find(_r => _r.Description == "New Name"));

            Assert.AreEqual(recipeId1, m_view.SelectedRecipeId, "should select the one the user clicked on even though saving the new one");
        }

        [Test]
        public void SortsRecipeList()
        {
            var recipe = m_factory.ManageRecipes().Create();
            recipe.Description = "C";
            m_factory.ManageRecipes().Save(recipe);

            recipe = m_factory.ManageRecipes().Create();
            recipe.Description = "B";
            m_factory.ManageRecipes().Save(recipe);

            recipe = m_factory.ManageRecipes().Create();
            recipe.Description = "A";
            m_factory.ManageRecipes().Save(recipe);

            recipe = m_factory.ManageRecipes().Create();
            recipe.Description = "D";
            m_factory.ManageRecipes().Save(recipe);

            m_view = new FakeRecipesView(m_factory);
            m_view.FireLoad();

            var recipes = m_view.RecipeList.ToList();

            Assert.AreEqual("A", recipes[0].Description);
            Assert.AreEqual("B", recipes[1].Description);
            Assert.AreEqual("C", recipes[2].Description);
            Assert.AreEqual("D", recipes[3].Description);
        }

        [Test]
        public void ChangesSavedWhenNewClicked()
        {
            RecreateWithTestRecipes();
            var recipeId1 = GetRecipeIdFromView("Recipe 1");
            
            m_view.SelectedRecipeId = recipeId1;
            m_view.RecipeName = "something";
            m_view.SaveAnswer = DialogResult.Yes;
            m_view.FireNewClick();
            
            Assert.IsTrue(m_view.AskedSave, "should have asked the user if they wanted to save");

            var recipes = m_factory.ManageRecipes().GetAll().ToList();
            Assert.IsNotNull(recipes.Find(_r => _r.Description == "something"));
        }

        [Test]
        public void ControlStatesAfterNewModifySave()
        {
            m_view.FireNewClick();
            m_view.RecipeName = "doodle";
            m_view.FireSaveClick();

            AssertHelper.HasValueIsTrue(m_view.NewEnabled);
            AssertHelper.HasValueIsTrue(m_view.DeleteEnabled);
            AssertHelper.HasValueIsTrue(m_view.RecipePanelEnabled);
            AssertHelper.HasValueIsTrue(m_view.CloseEnabled);
            AssertHelper.HasValueIsFalse(m_view.SaveEnabled);
        }

        [Test]
        public void NewModSaveModDelDoesNotPromptForSave()
        {
            m_view.FireNewClick();
            m_view.RecipeName = "Mod";
            m_view.FireSaveClick();
            m_view.RecipeName = "Mod again";
            m_view.DeleteAnswer = DialogResult.Yes;
            m_view.SaveAnswer = DialogResult.No;
            m_view.FireDelete();
            Assert.IsTrue(m_view.AskedDelete, "should ask about deleting");
            Assert.IsFalse(m_view.AskedSave, "should NOT ask about saving");
        }

        [Test]
        public void ListsIngredients()
        {
            RecreateWithTestIngredients();
            var recipeId1 = GetRecipeIdFromView("Recipe 1");
            m_view.SelectedRecipeId = recipeId1;
            Assert.AreEqual(2, m_view.ConcreteIngredientList.Count);
        }

        [Test]
        public void SetsUpcList()
        {
            RecreateWithTestIngredients();
            Assert.AreEqual(2, m_view.ConcreteUpcList.Count);
        }

        [Test]
        public void CreatesNewIngredient()
        {
            var id = Guid.NewGuid();
            m_view.CreateNewIngredient(new IngredientListItem { Group = 1, Type = IngredientType.Optional, UPC = id });
            Assert.AreEqual(1, m_view.ConcreteIngredientList.Count);
        }

        [Test]
        public void SavesNewIngredient()
        {
            m_view.FireNewClick();
            m_view.RecipeName = "New Recipe with 1 ingredient";
            var id = Guid.NewGuid();
            m_view.CreateNewIngredient(new IngredientListItem { Group = 1, Type = IngredientType.Optional, UPC = id });
            m_view.FireSaveClick();

            var recipeId = GetRecipeIdFromView("New Recipe with 1 ingredient");
            var recipe = m_factory.ManageRecipes().Get(recipeId);
            Assert.IsNotNull(recipe, "should have found recipe");
            var ingredients = recipe.Ingredients.ToList();
            Assert.AreEqual(1, ingredients.Count, "Should have one ingredient");
            Assert.AreEqual(1, ingredients[0].Number, "Assigned to group 1");
            Assert.AreEqual(IngredientType.Optional, (IngredientType)ingredients[0].Type, "Optional type");
            //Assert.AreEqual(id, ingredients[0].FID, "correct upcid");
        }

        [Test]
        public void UpdatesIngredient()
        {
            RecreateWithTestIngredients();
            var recipeId = GetRecipeIdFromView("Recipe 1");
            m_view.SelectedRecipeId = recipeId;
            var ingredientId = m_view.ConcreteIngredientList[0].IngredientId;
            m_view.ConcreteIngredientList[0].Group = 555;
            m_view.FireSaveClick();

            var recipe = m_factory.ManageRecipes().Get(recipeId);
            var ingredient = (from i in recipe.Ingredients where i.ObjectID == ingredientId select i).FirstOrDefault();
            Assert.AreEqual(555, ingredient.Number);
        }

        [Test]
        public void RemovesIngredient()
        {
            RecreateWithTestIngredients();
            var recipeId = GetRecipeIdFromView("Recipe 1");
            m_view.SelectedRecipeId = recipeId;
            var ingredientId = m_view.ConcreteIngredientList[0].IngredientId;
            m_view.ConcreteIngredientList.RemoveAt(0);
            m_view.FireSaveClick();

            var recipe = m_factory.ManageRecipes().Get(recipeId);
            var ingredient = (from i in recipe.Ingredients where i.ObjectID == ingredientId select i).FirstOrDefault();
            Assert.IsNull(ingredient);
        }

        [Test]
        [Category("TODO")]
        public void SavesOnClose()
        {
            Assert.Fail("Not finished");
        }

        

        private Guid GetRecipeIdFromView(string _name)
        {
            return (from r in m_view.RecipeList where r.Description == _name select r.ObjectID).FirstOrDefault();
        }

        private void RecreateWithTestIngredients()
        {
            m_factory = FakeManagerFactory.Recreate();
            var upc1 = m_factory.ManageUPCs().Create();
            upc1.Name = "UPC 1";
            m_factory.ManageUPCs().Save(upc1);

            IBLUPCItem upc2 = m_factory.ManageUPCs().Create();
            upc2.Name = "UPC 2";
            m_factory.ManageUPCs().Save(upc2);

            var recipe = m_factory.ManageRecipes().Create();
            recipe.Description = "Recipe 1";
            
            var ingredient1 = recipe.NewIngredient();
            ingredient1.Number = 1;
            ingredient1.Type = (int)IngredientType.Required;
            //ingredient1.FID = upc1.UPCID;

            var ingredient2 = recipe.NewIngredient();
            ingredient2.Number = 1;
            ingredient2.Type = (int)IngredientType.Required;
            //ingredient2.FID = upc2.UPCID;

            m_factory.ManageRecipes().Save(recipe);
            
            m_view = new FakeRecipesView(m_factory);
            m_view.FireLoad();
        }

        private void RecreateWithTestRecipes()
        {
            m_factory = FakeManagerFactory.Recreate();
            var recipe = m_factory.ManageRecipes().Create();
            recipe.Description = "Recipe 1";
            m_factory.ManageRecipes().Save(recipe);
            recipe = m_factory.ManageRecipes().Create();
            recipe.Description = "Recipe 2";
            m_factory.ManageRecipes().Save(recipe);

            // recreate view so we can see if it has the values
            m_view = new FakeRecipesView(m_factory);
            m_view.FireLoad(); // .NET normally does this
        }
    }
}
