using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RecipesFromExcel.DataObjects;

namespace RecipesFromExcel
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main( )
        {
            Application.EnableVisualStyles( );
            Application.SetCompatibleTextRenderingDefault( false );
            var manager = new ExcelDataManager( );

            using (var context = new RecipeEntities())
            {
                manager.CleanupManufacturers(context);
                manager.CleanupAliases();
            }

            var form = new ExcelImportForm( );
            form.Manager = manager;
            form.RecipeFound += manager.OnRecipeFound;
            form.IngredientFound += manager.OnIngredientFound;
            form.SetBinding( manager.Messages );
            form.ShowDialog( );
        }
    }
}
