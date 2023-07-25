using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
//using AutoMapper;
using RecipesFromExcel.DataObjects;
using System.ComponentModel;
using System.Windows.Forms;

namespace RecipesFromExcel
{
    // Imports the selected Excel file into SQL Server
    public class ExcelDataManager
    {
        protected static string XLSConnection =
            //              "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";";
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";";

        public ExcelDataManager( )
        {
            Messages = new BindingList<string>( );
        }

        public BindingList<string> Messages { get; private set; }

        /// <summary>
        /// Open the workbook file, using the given path and expect to find therein a DataTable with one row per sheet.  Return the sheet names
        /// so that the caller (a form) can put them into a drop-down listbox.
        /// </summary>
        /// <param name="_fileName"></param>
        /// <returns></returns>
        private List<SheetName> GetSheetNames( string _fileName )
        {
            var rc = new List<SheetName>( );
            if( !string.IsNullOrWhiteSpace(_fileName))
            {
                //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\MyExcel.xls;Extended Properties="Excel 8.0;HDR=Yes;IMEX=1";
                //using (var connection = new OleDbConnection(String.Format(@"Data Source={0};Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=Excel 12.0", _fileName)))
                using (var connection = new OleDbConnection(String.Format(XLSConnection, _fileName)))
                {
                    try
                    {
                        connection.Open();
                        // GetOleDbSchemaTable gives us a DataTable with one row per sheet, where TABLE_NAME is the Sheet name including the $ suffix.
                        var sheetTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        rc = sheetTable.Rows.OfType<DataRow>()
                            .Select(t => t["TABLE_NAME"].ToString())
                            .Where(t => !t.Contains("FilterDatabase"))
                            .Select(s => new SheetName(s))
                            .ToList();
                        connection.Close();
                    }
                    catch( Exception err )
                    {
                        MessageBox.Show(err.Message);
                        //throw;
                    }
                }   
            }
            return rc;
        }

        public List<SheetName> OnRecipeFound( string _fileName )
        {
            return GetSheetNames( _fileName );
        }

        public List<SheetName> OnIngredientFound( string _fileName )
        {
            return GetSheetNames( _fileName );
        }

        /// <summary>
        /// This is the director for all the work of importing and persisting.
        /// </summary>
        /// <param name="_sheetInfo"></param>
        public void ImportToServer( SheetInfo _sheetInfo )
        {
            AddMessage( "Import To Server started..." );
            var ingredients = new List<IngredientImport>( );
            var recipes = new Dictionary<RecipeImport, List<AliasImport>>( );

            //using ( var connection = new OleDbConnection( String.Format( @"Data Source={0};Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=Excel 12.0", 
            //    _sheetInfo.IngredientFileName ) ) )
            using ( var connection = new OleDbConnection( String.Format( XLSConnection, _sheetInfo.IngredientFileName ) ) )
            {
                connection.Open( );

                if ( _sheetInfo.IngredientSheetName != null )
                {
                    ingredients = ExtractIngredients( connection, _sheetInfo.IngredientSheetName.RawName );
                }

                connection.Close( );
            }

            //using ( var connection = new OleDbConnection( String.Format( @"Data Source={0};Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=Excel 12.0",
            //    _sheetInfo.RecipeFileName ) ) )
            using ( var connection = new OleDbConnection( String.Format( XLSConnection, _sheetInfo.RecipeFileName ) ) )
            {
                connection.Open( );
                if ( _sheetInfo.RecipeSheetName != null )
                {
                    recipes = ExtractRecipes( connection, _sheetInfo.RecipeSheetName.RawName );
                }

                connection.Close( );
            }

            using ( var context = new RecipeEntities( ) )
            {
                double conversionMultiplierForOz = ObtainConversionMultiplierForOZ( context );
                if ( conversionMultiplierForOz == 0 )
                {
                    AddMessage( "There is no conversion multiplier for OZ; the program cannot continue." );
                }
                else
                {
                    CleanupManufacturers( context );
                    CreateDrinkSizes( context, conversionMultiplierForOz, recipes.Keys.ToList( ), ingredients );
                    UpdatePOSTicketItems( context );
                    UpdatePours( context );
                    RemoveTicketItemAliases( context );
                    RemoveIngredients( context );
                    RemoveRecipes( context );
                    var tempRecipes = BuildTempRecipeList( context, recipes.Keys.ToList( ) );
                    var ingredientList = BuildTempIngredientList( context, recipes.Keys.ToList( ), tempRecipes, ingredients, conversionMultiplierForOz );
                    UpdateIngredientNumbers( ingredientList );
                    UpdateRecipes( );
                    UpdateIngredients( );
                    UpdateTicketItemAliases( context, recipes.Values.SelectMany( x => x ).ToList( ), tempRecipes );

                    AddMessage( "Writing changes to the database..." );
                    context.SaveChanges( );
                }

                CleanupAliases( );

            }
            AddMessage( "Import To Server complete." );
        }

        public void CleanupManufacturers( RecipeEntities _context )
        {
            // Only the first Manufacturer with a given name is legitimate.  For all others, find and fix any related records, then delete.
            var mfgs = _context.Manufacturers.GroupBy( m => m.Name ).ToList( );
            foreach( var g in mfgs )
            {
                var duplicates = g.Select( ( m, i ) => new { Sequence = i, Mfg = m } ).ToList( );
                if ( duplicates.Count( ) > 1 )
                {
                    var goodMfg = duplicates.Where( d => d.Sequence == 0 ).Select( d => d.Mfg ).First( );
                    var badMfgs = duplicates.Where( d => d.Sequence > 0 ).Select( d => d.Mfg ).ToList( );
                    foreach ( var m in badMfgs )
                    {
                        // We cannot loop through m.UPCs because the assignment of a new ManufacturerID is considered a change to the collection,
                        // and the second iteration throws an error.
                        var upcs = m.UPCs.ToList( );
                        foreach ( var u in upcs )
                        {
                            u.ManufacturerID = goodMfg.ManufacturerID;
                        }
                        var ingredients = m.Ingredients.ToList( );
                        foreach ( var i in ingredients )
                        {
                            i.ManufacturerID = goodMfg.ManufacturerID;
                        }
                        _context.Manufacturers.DeleteObject( m );
                    }
                }
            }
            _context.SaveChanges( );
        }

        private void RemoveRecipes( RecipeEntities _context )
        {
            AddMessage( "Deleting Recipes..." );
            _context.ExecuteStoreCommand( "DELETE FROM Recipe" );
        }

        private void RemoveTicketItemAliases( RecipeEntities _context )
        {
            AddMessage( "Deleting TicketItemAliases..." );
            _context.ExecuteStoreCommand( "DELETE FROM TicketItemAliases" );
        }

        private void UpdatePours( RecipeEntities _context )
        {
            AddMessage( "Updating Pours to unreconciled..." );
            _context.ExecuteStoreCommand( "UPDATE Pours SET Status = 2, POSTicketItemID = null, IngredientID = null" );
        }

        private void UpdatePOSTicketItems( RecipeEntities _context )
        {
            AddMessage( "Updating POSTicketItems to unreconciled..." );
            _context.ExecuteStoreCommand( "UPDATE POSTicketItems SET Status = 2" );
        }

        private void RemoveIngredients( RecipeEntities _context )
        {
            AddMessage( "Deleting Ingredients..." );
            _context.ExecuteStoreCommand( "DELETE FROM Ingredients" );
        }

        /// <summary>
        /// The pour sizes in the spreadsheets are ounces; the database holds a factor for converting them to ml.
        /// </summary>
        /// <param name="_context"></param>
        /// <returns></returns>
        private double ObtainConversionMultiplierForOZ( RecipeEntities _context ) 
        {
            return _context.SizeTypes
                    .Where( st => st.Abbreviation == "OZ" )
                    .Select( st => st.ConversionMultiplier )
                    .FirstOrDefault( );
        }

        /// <summary>
        /// If the spreadsheets have a category/standard drink size that does not have a match in StandardPours, create an entry in StandardPours.
        /// In order to be able to use them further on in the process, go ahead and persist them here.
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_multiplier"></param>
        /// <param name="_recipes"></param>
        /// <param name="_ingredients"></param>
        private void CreateDrinkSizes( RecipeEntities _context, double _multiplier, List<RecipeImport> _recipes, List<IngredientImport> _ingredients ) 
        {
            AddMessage( "Creating new drink sizes, if necessary..." );
            var existingStandardPours = _context.StandardPours.ToList( );
            var categories = _context.Categories.Where( c => c.ParentID == null )
                .Select( c => new { c.CategoryID, Name = c.Name.ToLower( ) } )
                .ToList( );
            var potentialNewStandardPours = ( from ri in _recipes
                                     join i in _ingredients on ri.IngredientName.ToLower( ) equals i.IngredientName.ToLower( )
                                     join c in categories on i.Category.ToLower( ) equals c.Name.ToLower( )
                                     where ri.DrinkSize > 0
                                     select new
                                     {
                                         c.CategoryID,
                                         Name = string.Format( "{0} {1}", i.Category, ri.DrinkSize.ToString( ) ),
                                         StandardPour = ri.DrinkSize,
                                         PourStandard = Convert.ToDouble( ri.DrinkSize ) / _multiplier
                                     }
                                    ).Distinct( )
                                    .ToList( );

            if ( potentialNewStandardPours.Any( ) )
            {
                // It is conceivable that potentialNewStandardPours includes some entries which, although having different names, are already
                // represented in the database (by CategoryId and PourStandard).  In such cases, skip them.
                var hits = from nsp in potentialNewStandardPours
                           join esp in existingStandardPours on new { CategoryId = nsp.CategoryID, nsp.PourStandard }
                            equals new { CategoryId = ( Guid ) esp.CategoryID.Value, esp.PourStandard }
                           select nsp;
                var newStandardPours = potentialNewStandardPours.Except( hits )
                    .ToList( );


                if ( newStandardPours.Any( ) )
                {
                    newStandardPours.ForEach( nsp =>
                        {
                            var sp = _context.StandardPours.CreateObject( );
                            sp.StandardPourID = Guid.NewGuid( );
                            sp.Name = nsp.Name;
                            sp.PourStandard = nsp.PourStandard;
                            sp.StandardVariance = 0.15;
                            sp.CategoryID = nsp.CategoryID;
                            sp.SystemStandard = true;
                            _context.StandardPours.AddObject( sp );
                        } );
                    _context.SaveChanges( );
                }
            }
        }

        private List<Recipe> BuildTempRecipeList( RecipeEntities _context, List<RecipeImport> _recipes ) 
        {
            AddMessage( "Building new recipes..." );
            var rc = new List<Recipe>( );
            var newRecipes = _recipes.Select( r => r.RecipeName.Trim( ) ).Distinct( ).ToList( );
            
            if ( newRecipes.Any( ) )
            {
                newRecipes.ForEach( nr =>
                    {
                        var r = _context.Recipes.CreateObject( );
                        r.RecipeID = Guid.NewGuid( );
                        r.Description = nr;
                        _context.Recipes.AddObject( r );
                        rc.Add( r );
                    } );
            }
            return rc;
        }

        /// <summary>
        /// BuildTempIngredientList is responsible for resolving CategoryId, ManufacturerId, and StandardPourId.
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_recipes"></param>
        /// <param name="_tempRecipes"></param>
        /// <param name="_ingredients"></param>
        /// <param name="_multiplier"></param>
        /// <returns></returns>
        private List<Ingredient> BuildTempIngredientList( RecipeEntities _context, 
            List<RecipeImport> _recipes, 
            List<Recipe> _tempRecipes, 
            List<IngredientImport> _ingredients,
            double _multiplier ) 
        {
            AddMessage( "Building new ingredients..." );
            var rc = new List<Ingredient>( );

            var categories = _context.Categories.Select( c => new { c.CategoryID, c.Name, c.ParentID } ).ToList( );

            // This looks like an awful query, but it really is quick.  We have to left-join the categories because we might not get a hit on subcat, 
            // and we have to left-join on manufacturers similarly.  We pull in the categories once (above), because we join to it twice and, 
            // since the query cannot be all resolved in the database, it would load the full table for categories twice.
            var newIngredients = ( from i in _ingredients
                      join r in _recipes on i.IngredientName.ToLower( ) equals r.IngredientName.ToLower( )
                      join c in categories on i.SubCategory.ToLower( ) equals c.Name.ToLower( ) into cats
                      from c in cats.DefaultIfEmpty( )
                      join root in categories.Where( c => c.ParentID == null ) on i.Category.ToLower( ) equals root.Name.ToLower( )
                      join m in _context.Manufacturers on i.ManufacturerName.ToLower( ) equals m.Name.ToLower( ) into mfgs
                      from m in mfgs.DefaultIfEmpty( )
                      join tr in _tempRecipes on r.RecipeName.ToLower( ) equals tr.Description.ToLower( )
                      select new { 
                          i.IngredientName,
                          tr.Description,
                          tr.RecipeID, 
                          i.Quality, 
                          StandardPour = r.DrinkSize, 
                          ManufacturerId = m == null ? Guid.Empty : m.ManufacturerID, 
                          CategoryId = c == null ? Guid.Empty : c.CategoryID,
                          RootCategoryId = root.CategoryID } )
                    .GroupBy( i => new { i.RecipeID, i.IngredientName } )
                    .Select( g => g.FirstOrDefault( i => i.ManufacturerId != Guid.Empty ) ?? g.First( ) )
                    .ToList( );

            var sp = _context.StandardPours.ToList( );
            newIngredients.ForEach( i =>
                {
                    // This process could be improved if the SystemStandard flag in StandardPours was only true for the standard for each root category...
                    var bases = new List<string>{ "Beer", "Liquor", "Wine" };
                    // Match up the StandardPours.  When you can't match up, settle for the standard pour for the base category.
                    var query = sp.Where( s => s.CategoryID == i.RootCategoryId );
                    var stdPour = i.StandardPour == 0 ? query.Where( s => bases.Contains( s.Name ) ).FirstOrDefault( ) :
                        query.Where( s => s.PourStandard == Convert.ToDouble( i.StandardPour ) / _multiplier ).FirstOrDefault( );
                    var spID = stdPour == null ? Guid.Empty : stdPour.StandardPourID;

                    var ingredient = _context.Ingredients.CreateObject( );
                    ingredient.IngredientID = Guid.NewGuid( );
                    ingredient.RecipeID = i.RecipeID;
                    ingredient.Number = 0;
                    ingredient.Type = 1;
                    ingredient.UPCID = null;
                    ingredient.Quality = i.Quality.ToLower( ) == "premium" ? 3 : i.Quality.ToLower( ) == "super premium" ? 4 : 1;
                    ingredient.StandardPourID = spID;
                    ingredient.ManufacturerID = i.ManufacturerId == Guid.Empty ? ( Guid? ) null : i.ManufacturerId;
                    ingredient.CategoryID = ( i.CategoryId == Guid.Empty || i.ManufacturerId != Guid.Empty ) ? ( Guid? ) null : i.CategoryId;
                    _context.Ingredients.AddObject( ingredient );
                    rc.Add( ingredient );
                } );
            return rc;
        }

        /// <summary>
        /// Assign incremental numbers to ingredients; there may be more than one for a recipe.
        /// </summary>
        /// <param name="_ingredients"></param>
        private void UpdateIngredientNumbers( List<Ingredient> _ingredients ) 
        {
            var groupedIngredients = _ingredients
                .GroupBy( item => item.RecipeID )
                .ToList( );

            // GroupBy returns an IGrouping with a property called Key that represents the item grouped by.  There is not a property
            // for the grouped items, but if you reference the IGrouping, that's what you get.  Thus, the reference to "i" below is
            // a reference to the IGrouping, but i.Select iterates the set in the group.
            // "l" then, is the anonymous object { ingredient, index } which we can use to set item.Number
            groupedIngredients.ForEach( i =>
                {
                    i.Select( ( ingredient, index ) => new { ingredient, index } )
                        .ToList( )
                        .ForEach( l => l.ingredient.Number = l.index + 1 );
                } );
        } 

        /// <summary>
        /// So far, all the necessary changes have been made in the EF Recipes ObjectSet
        /// </summary>
        private void UpdateRecipes( ) { }

        /// <summary>
        /// So far, all the necessary changes have been made in the EF Ingredients ObjectSet
        /// </summary>
        private void UpdateIngredients( ) { }

        /// <summary>
        /// Build all the TicketItemAliases.
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_aliases"></param>
        /// <param name="_recipes"></param>
        private void UpdateTicketItemAliases( RecipeEntities _context, List<AliasImport> _aliases, List<Recipe> _recipes )
        {
            AddMessage( "Adding aliases..." );

            _aliases.GroupBy( a => a.RecipeAliasName.ToLower( ).Trim( ) )
                .Where( i => i.Count( ) > 1 ).ToList( ).ForEach( i =>
                    {
                        AddMessage( string.Format( "Duplicate Alias, {0}; only the first will be stored.", i.First( ).RecipeAliasName ) );
                        i.Skip( 1 ).Select( ( item, index ) => new { item, index } )
                            .ToList( ).ForEach( g => g.item.RecipeAliasName = "skip" );
                    } );

            _aliases.ForEach( i =>
            {
                if ( i.RecipeAliasName != "skip" )
                {
                    var recipeAliasImport = _context.TicketItemAliases.CreateObject( );
                    recipeAliasImport.TicketItemAliasID = Guid.NewGuid( );
                    recipeAliasImport.RecipeID = _recipes.FirstOrDefault( r => r.Description == i.RecipeName ).RecipeID;
                    recipeAliasImport.Description = i.RecipeAliasName;
                    recipeAliasImport.Price = 0.0M;
                    _context.TicketItemAliases.AddObject( recipeAliasImport );
                }
            } );
        }

        /// <summary>
        /// Extract the information from the Ingredients spreadsheet into a list.
        /// </summary>
        /// <param name="_connection"></param>
        /// <param name="_ingredientSheetName"></param>
        /// <returns></returns>
        private List<IngredientImport> ExtractIngredients( OleDbConnection _connection, string _ingredientSheetName )
        {
            AddMessage( "Importing Ingredients..." );
            var ingredients = new List<IngredientImport>( );
            var ingredientCommand = new OleDbCommand( String.Format( "select * from [{0}]", _ingredientSheetName ), _connection );
            using ( var ingredientReader = ingredientCommand.ExecuteReader( ) )
            {
                // GetSchemaTable gives us a DataTable with one row per spreadsheet column, where "ColumnName" is the "header", if there is one.  
                // Also accessible are DataType, Size, NumericPrecision, and NumericScale, to mention a few.
                var ingredientSchema = ingredientReader.GetSchemaTable( );
                var values = new object[ ingredientSchema.Rows.Count ];
                while ( ingredientReader.Read( ) )
                {
                    ingredientReader.GetValues( values );
                    var ingredient = new IngredientImport( values );
                    // The spreadsheet can have empty rows.
                    if ( !String.IsNullOrEmpty( ingredient.IngredientName ) )
                    {
                        ingredients.Add( ingredient );
                    }
                }
            }
            return ingredients;
        }

        /// <summary>
        /// Extract the information from the Recipe spreadsheet.  This includes Aliases, which go to a separate but related table.
        /// </summary>
        /// <param name="_connection"></param>
        /// <param name="_recipeSheetName"></param>
        /// <returns></returns>
        private Dictionary<RecipeImport, List<AliasImport>> ExtractRecipes( OleDbConnection _connection, string _recipeSheetName )
        {
            AddMessage( "Importing Recipes..." );
            var recipes = new Dictionary<RecipeImport, List<AliasImport>>( );
            var recipeCommand = new OleDbCommand( String.Format( "select * from [{0}]", _recipeSheetName ), _connection );
            using ( var recipeReader = recipeCommand.ExecuteReader( ) )
            {
                var recipeSchema = recipeReader.GetSchemaTable( );
                var values = new object[ recipeSchema.Rows.Count ];
                var aliasColumns = FindAliasColumns( recipeSchema );
                var nonAliasColumns = FindNonAliasColumns( recipeSchema );
                while ( recipeReader.Read( ) )
                {
                    recipeReader.GetValues( values );
                    var recipe = new RecipeImport( GetNonAliasValues( values, nonAliasColumns ) );
                    var aliasStrings = GetAliasStrings( values, aliasColumns );
                    if ( !String.IsNullOrEmpty( recipe.RecipeName ) )
                    {
                        recipes.Add( recipe, aliasStrings.Select( s => new AliasImport( s, recipe.RecipeName ) ).ToList( ) );
                    }
                }
            }
            return recipes;
        }

        private void AddMessage( string _message )
        {
            Messages.Add( _message );
            Application.DoEvents( );
        }

        // Return a list of column ordinals for columns whose headers start with "alias".
        private List<int> FindAliasColumns ( DataTable _table )
        {
            return _table.Rows.OfType<DataRow>( )
                .Where( dr => dr["ColumnName"].ToString( ).ToLower( ).StartsWith( "alias" ) )
                .Select( dr => Convert.ToInt32( dr["ColumnOrdinal"] ) )
                .ToList( );
        }

        // Return a list of distinct, non-null string values from the columns identified by FindAliasColumns.
        private List<string> GetAliasStrings( object[ ] _values, List<int> _aliasColumns )
        {
            return _aliasColumns
                .Where( a => _values[ a ] != DBNull.Value )
                .Select( a => _values[ a ].ToString( ).Trim( ) )
                .Distinct( )
                .ToList( );
        }

        private List<int> FindNonAliasColumns( DataTable _table )
        {
            var acceptableNames = new List<string> { "Recipe", "Description", "Ingredient", "Standard Drink Size" };
            return _table.Rows.OfType<DataRow>( )
                        .Where( dr => acceptableNames.Contains( dr[ "ColumnName" ].ToString( ) ) )
                        .Select( dr => Convert.ToInt32( dr[ "ColumnOrdinal" ] ) )
                        .ToList( );
        }

        // Return an object array of all the values from FindNonAliasColumns.
        private object[ ] GetNonAliasValues( object[ ] _values, List<int> _nonAliasColumns )
        {
            return _values
                .Select( ( v, i ) => new { i, v } ) // <- include the index as part of the data
                .Where( a => _nonAliasColumns.Contains( a.i ) )   // <- skip the items with matching indexes
                .OrderBy( a => a.i )
                .Select( a => a.v ) // <- select the remaining values into a new array
                .ToArray( );
        }

        //// Return an object array of all the values EXCEPT the ones from the FindAliasColumns columns.
        //private object[ ] GetNonAliasValues( object[ ] _values, List<int> _aliasColumns )
        //{
        //    return _values
        //        .Select( ( v, i ) => new { i, v } ) // <- include the index as part of the data
        //        .Where( a => !_aliasColumns.Contains( a.i ) )   // <- skip the items with matching indexes
        //        .OrderBy( a => a.i )    
        //        .Select( a => a.v ) // <- select the remaining values into a new array
        //        .ToArray( );
        //}

        public void CleanupAliases( )
        {
            AddMessage( "Cleaning up Aliases..." );
            using ( var _data = new RecipeEntities( ) )
            {
                {
                    var count = _data.TicketItemAliases.Count( );

                    var aliases = ( from m in _data.TicketItemAliases
                                    select m.Description ).Distinct( );

                    //Console.WriteLine(string.Format("{1} Ticket Item Aliases Count: {0}", aliases.Count(), count));
                    foreach ( var alias in aliases )
                    {
                        var copies = from m in _data.TicketItemAliases
                                     where m.Description == alias
                                     select m;

                        var first = copies.First( );

                        foreach ( var copy in copies )
                        {
                            if ( copy.TicketItemAliasID != first.TicketItemAliasID )
                            {
                                _data.DeleteObject( copy );
                            }
                        }

                        //Console.WriteLine(String.Format("Alias = {0}", alias));
                    }
                    _data.SaveChanges( );
                }
            }
        }
    }
}
