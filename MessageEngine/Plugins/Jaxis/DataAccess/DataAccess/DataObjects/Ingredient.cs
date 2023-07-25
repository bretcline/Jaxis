using System;
using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

// ReSharper disable CheckNamespace
namespace Jaxis.Inventory.Data
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// A class which represents the mobPours table in the BevMetMobile Database.
    /// </summary>
    public partial class Ingredient : IBLIngredient
    {
        public IEnumerable<IIngredient> GetAll()
        {
            return All();
        }

        public IBLRecipe Recipe
        {
            get { return this.Recipe as IBLRecipe; }
            set { throw new NotImplementedException(); }
        }

        public IBLUPCItem UPC
        {
            get { return this.UPC as IBLUPCItem; }
            set { throw new NotImplementedException(); }
        }
    }
}