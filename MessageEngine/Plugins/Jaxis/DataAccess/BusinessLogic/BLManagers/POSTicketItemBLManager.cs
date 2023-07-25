using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IDataItems;
using Jaxis.Inventory.Data.IUIDataItems;
using System;

namespace Jaxis.Inventory.Data.BusinessLogic.BLManagers
{
// ReSharper disable InconsistentNaming
    public class POSTicketItemBLManager : BLManager<IPOSTicketItem, IBLPOSTicketItem>, IPOSTicketItemBLManager
// ReSharper restore InconsistentNaming
    {
        public IEnumerable<string> GetUniqueDescriptions()
        {
            var descriptions = (from item in GetAll() select item.Description).Distinct();
            return descriptions;
        }
    }

    public class POSTicketItemViewBLManager : BLManager<IPOSTicketItemView, IBLPOSTicketItemView>, IPOSTicketItemViewBLManager
    {
        private POSTicketItemHelper m_Helper = new POSTicketItemHelper();
        
        public IEnumerable<IBLPOSTicketItemView> GetBetween(DateTime _beginDate, DateTime _endDate, bool _unmatched)
        {
            IQueryable<IPOSTicketItemView> rc = null;
            if (_unmatched == true)
            {
                rc = (from t in DataManagerFactory.Get().Manage<IPOSTicketItemView>().GetAll()
                      where t.TicketDate >= _beginDate
                      && t.TicketDate <= _endDate
                      && t.Status == 2
                      select t);
            }
            else
            {
                rc = (from t in DataManagerFactory.Get().Manage<IPOSTicketItemView>().GetAll()
                      where t.TicketDate >= _beginDate
                      && t.TicketDate <= _endDate
                      select t);
            }

            var temp = rc.ToList();
            return temp.Cast<IBLPOSTicketItemView>();
        }

        public IBLRecipeItem GetRecipeByTicketItemAlias(string _description)
        {
            return m_Helper.GetRecipeByTicketItemAlias(_description);
        }
    }

    internal class POSTicketItemHelper
    {
        private BeverageMonitorDB m_Database = null;

        private BeverageMonitorDB GetDB()
        {
            if (null == m_Database)
                m_Database = new BeverageMonitorDB();
            return m_Database;
        }

        // BLRecipeItem and BLIngredientItem are two classes that I will create (like the BLBrandedBottle)
        // BLRecipeItem will have a string and a List<BLIngredient>
        // Here, return a BLRecipeItem.
        // In the app, set the ingredients grid datasource to the BLRecipeItem.IngredientsList
        public IBLRecipeItem GetRecipeByTicketItemAlias(string _description)
        {
            Jaxis.Inventory.Data.BusinessLogic.BLObjects.BLRecipeItem recipe = null;
            var db = GetDB();
            var proc = db.procGetRecipeByTicketItemAlias(_description);
            using (var reader = proc.ExecuteReader())
            {
                if (reader.HasRows == true)
                {
                    recipe = new Jaxis.Inventory.Data.BusinessLogic.BLObjects.BLRecipeItem();

                    while (reader.Read())
                    {
                        var ingredient = new Jaxis.Inventory.Data.BusinessLogic.BLObjects.BLIngredientItem();
                        ingredient.TicketItemAlias = reader.GetString(0);
                        ingredient.RecipeName = reader.GetString(1);
                        ingredient.IngredientID = reader.GetGuid(2);
                        ingredient.Name = reader.GetString(3);
                        ingredient.StandardPourID = reader.GetGuid(4);
                        ingredient.Quality = reader.GetInt32(5);
                        ingredient.PourStandard = reader.GetDouble(6);
                        ingredient.StandardVariance = reader.GetDouble(7);
                        recipe.Ingredients.Add(ingredient);
                    }
                    if (recipe.Ingredients.Count > 0)
                    {
                        recipe.Name = recipe.Ingredients.First().RecipeName;
                        recipe.TicketItemAlias = recipe.Ingredients.First().TicketItemAlias;
                    }
                }
            }

            return recipe;
        }
    }
}
