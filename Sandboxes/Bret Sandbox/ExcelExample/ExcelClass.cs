using System;
using Microsoft.Office.Interop.Excel;

namespace ExcelExample
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    class ExcelClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main( string[] args )
        {
            Application excelApp = new ApplicationClass( );  // Creates a new Excel Application
            excelApp.Visible = true;  // Makes Excel visible to the user.

            // The following line adds a new workbook
            Workbook newWorkbook = excelApp.Workbooks.Add( XlWBATemplate.xlWBATWorksheet );

            // The following code opens an existing workbook
            string workbookPath = @"C:\Source\BevMetrics\trunk\Documents\UPC codes W-Pics 9-3\Bev Met UPC List-E-M.edit.xls";  // Add your own path here
            Workbook excelWorkbook = excelApp.Workbooks.Open( workbookPath, 0,
                false, 5, "", "", false, XlPlatform.xlWindows, "", true,
                false, 0, true, false, false );

            // The following gets the Worksheets collection
            Sheets excelSheets = excelWorkbook.Worksheets;

            // The following gets Sheet1 for editing
            string currentSheet = "Sheet1";
            Worksheet excelWorksheet = (Worksheet)excelSheets.get_Item( currentSheet );

            // The following gets cell A1 for editing
            Range excelCell = (Range)excelWorksheet.get_Range( "A1", "A1" );

            // The following sets cell A1's value to "Hi There"
            excelCell.Value2 = "Hi There";
        }
    }
}