using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipesFromExcel
{
    public class RecipeImport : BaseDataObject
    {
        public string OrganizationName { get; set; }
        public string RecipeName { get; set; }
        public string Description { get; set; }
        public string IngredientName { get; set; }
        public decimal DrinkSize { get; set; }

        // For this to work, the elements in the array must be in the right order.
        // StandardPour may not be available.
        public RecipeImport( object[ ] _values )
        {
            OrganizationName = string.Empty;
            RecipeName = getString( _values[ 0 ] ).Trim( ).ToUpperInvariant();
            Description = getString(_values[1]).Trim().ToUpperInvariant();
            IngredientName = getString(_values[2]).Trim().ToUpperInvariant();
            DrinkSize = _values.Count( ) == 4 ? getDecimal( _values[ 3 ] ) : 0.0M;
        }
    }
}
