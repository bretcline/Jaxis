using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipesFromExcel
{
    public class SheetInfo
    {
        public string RecipeFileName { get; set; }
        public SheetName RecipeSheetName { get; set; }
        public string IngredientFileName { get; set; }
        public SheetName IngredientSheetName { get; set; }
    }
}
