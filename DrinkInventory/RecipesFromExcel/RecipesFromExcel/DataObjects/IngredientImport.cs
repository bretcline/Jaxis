using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipesFromExcel
{
    public class IngredientImport : BaseDataObject
    {
        public string IngredientName { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string ManufacturerName { get; set; }
        public string Quality { get; set; }

        // For this to work, the elements in the array must be in the right order.
        public IngredientImport( object[ ] _values )
        {
            IngredientName = getString( _values[ 0 ] ).Trim( );
            Category = getString( _values[ 1 ] ).Trim( );
            SubCategory = getString( _values[ 2 ] ).Trim( );
            ManufacturerName = getString( _values[ 3 ] ).Trim( );
            Quality = getString( _values[ 5 ] ).Trim( );
        }
    }
}
