using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Jaxis.Inventory.Data.IBLDataItems;

namespace BeverageManagement.Reports
{
    class ReportFomat
    {
        static public void Format(GridControl _grid, PrintingSystem _printingSystem, string _reportName, string _headerData = "")
        {
            var gridView = _grid.MainView as GridView;
            if (gridView != null)
            {
                var pl = new DevExpress.XtraPrinting.PrintableComponentLink();
                _printingSystem.Links.Add(pl);
                pl.Component = _grid;
                pl.Landscape = true;

                string headerColumn = _reportName;
                string companyColumn = "Beverage Metrics";
                string dateColumn = string.Format("Date: {0:g}", DateTime.Now);

                // Create a PageHeaderFooter object and initializing it with
                // the link's PageHeaderFooter.
                var phf = pl.PageHeaderFooter as PageHeaderFooter;

                // Clear the PageHeaderFooter's contents.
                if (phf != null)
                {
                    phf.Header.Content.Clear();
                    phf.Header.Font = new Font(phf.Header.Font.Name, 14);
                    if (!string.IsNullOrWhiteSpace(_headerData))
                    {
                        headerColumn += System.Environment.NewLine + _headerData;
                    }
                    // Add custom information to the link's header.
                    phf.Header.Content.AddRange(new string[] { null, headerColumn, null });
                    phf.Footer.Content.AddRange(new string[] { companyColumn, null, dateColumn });
                }

                // Show the document's preview.
                pl.CreateDocument();
                pl.ShowPreview();
            }
        }

        static public void Format(GridControl _grid, PrintingSystem _printingSystem, IBLReport _report, string _headerData = "")
        {
            Format( _grid, _printingSystem, (null != _report)?_report.Name:string.Empty, _headerData );
        }
    }
}
