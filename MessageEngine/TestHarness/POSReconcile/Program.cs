using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.BeverageManagement;
using Jaxis.Interfaces;

namespace POSReconcile
{
    class Program
    {
        static void Main(string[] args)
        {
            var reconcile = new Reconcile();

            reconcile.ConsolidatedReconcile( 15 );

            //reconcile.ReconcileNow( IngredientContainerTypes.Manufacturer, false, false );
        }
    }
}
