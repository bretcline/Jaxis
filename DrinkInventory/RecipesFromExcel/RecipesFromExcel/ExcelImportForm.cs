using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using RecipesFromExcel.DataObjects;

namespace RecipesFromExcel
{
    public partial class ExcelImportForm : Form
    {
        public event Func<string, List<SheetName>> RecipeFound;
        public event Func<string, List<SheetName>> IngredientFound;

        public ExcelDataManager Manager { get; set; }

        public ExcelImportForm( )
        {
            InitializeComponent( );
        }

        public void SetBinding( BindingList<string> _messages )
        {
            //BindingList<string> toBind = new BindingList<string>( _messages );
            //lbErrorMessages.DataSource = toBind;
            lbErrorMessages.DataSource = _messages;
        }

        private string ShowOpenFileDialog( )
        {
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Spreadsheets|*.xls;*.xlsx";
            openFileDialog1.ShowDialog( );
            return openFileDialog1.FileName;
        }

        private void btnRecipeFinder_Click( object sender, EventArgs e )
        {
            txtRecipe.Text = ShowOpenFileDialog( );
            if ( RecipeFound != null )
            {
                ProcessRecipeFileName( );
            }
        }

        private void ProcessRecipeFileName( )
        {
            var sheets = RecipeFound( txtRecipe.Text );
            lueRecipeSheets.Properties.DataSource = sheets;
            lueRecipeSheets.Properties.DisplayMember = "Sheet";
            lueRecipeSheets.Properties.Columns.Clear( );
            lueRecipeSheets.Properties.Columns.AddRange( new[ ] { new LookUpColumnInfo( "Sheet", "Sheets" ) } );
            lueRecipeSheets.EditValue = sheets.FirstOrDefault( s => s.Sheet.ToLower( ).Contains( "recipe" ) );

            if ( String.IsNullOrEmpty( txtIngredient.Text ) )
            {
                var ingredientSheet = sheets.FirstOrDefault( s => s.Sheet.ToLower( ).Contains( "ingredient" ) );
                if ( ingredientSheet != null )
                {
                    txtIngredient.Text = txtRecipe.Text;
                    var ingredientSheets = sheets.ToList( );
                    lueIngredientSheets.Properties.DataSource = ingredientSheets;
                    lueIngredientSheets.Properties.DisplayMember = "Sheet";
                    lueIngredientSheets.Properties.Columns.Clear( );
                    lueIngredientSheets.Properties.Columns.AddRange( new[ ] { new LookUpColumnInfo( "Sheet", "Sheets" ) } );
                    lueIngredientSheets.EditValue = ingredientSheets.FirstOrDefault( s => s.Sheet.ToLower( ).Contains( "ingredient" ) );
                }
            }
        }

        private void btnIngredientFinder_Click( object sender, EventArgs e )
        {
            txtIngredient.Text = ShowOpenFileDialog( );
            if ( IngredientFound != null )
            {
                ProcessIngredientFileName( );
            }
        }

        private void ProcessIngredientFileName( )
        {
            var ingredientSheets = IngredientFound( txtIngredient.Text );
            lueIngredientSheets.Properties.DataSource = ingredientSheets;
            lueIngredientSheets.Properties.DisplayMember = "Sheet";
            lueIngredientSheets.Properties.Columns.Clear( );
            lueIngredientSheets.Properties.Columns.AddRange( new[ ] { new LookUpColumnInfo( "Sheet", "Sheets" ) } );
            lueIngredientSheets.EditValue = ingredientSheets.FirstOrDefault( s => s.Sheet.ToLower( ).Contains( "ingredient" ) );
        }

        private void btnGo_Click( object sender, EventArgs e )
        {
            var sheetInfo = new SheetInfo
            {
                RecipeFileName = txtRecipe.Text,
                RecipeSheetName = lueRecipeSheets.EditValue is SheetName ? ( SheetName ) lueRecipeSheets.EditValue : null,
                IngredientFileName = txtIngredient.Text,
                IngredientSheetName = lueIngredientSheets.EditValue is SheetName ? ( SheetName ) lueIngredientSheets.EditValue : null
            };
            var tests = new List<string> { sheetInfo.IngredientFileName, sheetInfo.IngredientSheetName.Sheet, sheetInfo.RecipeFileName, sheetInfo.RecipeSheetName.Sheet };
            if ( tests.Any( t => String.IsNullOrEmpty( t ) ) )
            {
                MessageBox.Show( this, "A selection must be made for each spreadsheet.", "Selection Missinog", MessageBoxButtons.OK );
            }
            else
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Manager.ImportToServer( sheetInfo );
                }
                catch ( Exception _ex )
                {
                    MessageBox.Show( this, _ex.Message + _ex.InnerException, "Error", MessageBoxButtons.OK );
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        void txtRecipe_Leave( object sender, System.EventArgs e )
        {
            ProcessRecipeFileName( );
        }

        void txtIngredient_Leave( object sender, System.EventArgs e )
        {
            ProcessIngredientFileName( );
        }

        private void btnCleanupManufacturers_Click(object sender, EventArgs e)
        {
            using (var context = new RecipeEntities())
            {
                Manager.CleanupManufacturers(context);
                Manager.CleanupAliases();

            }
        }
    }
}
