using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipesFromExcel
{
    public class AliasImport : BaseDataObject
    {
        public string RecipeAliasName { get; set; }
        public string RecipeName { get; set; }

        public AliasImport( string _aliasName, string _recipeName )
        {
            RecipeAliasName = _aliasName.Trim( );
            RecipeName = _recipeName.Trim( );
        }
    }
}
