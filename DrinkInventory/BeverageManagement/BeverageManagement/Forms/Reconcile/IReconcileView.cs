using System;
using System.Collections.Generic;
using Jaxis.Inventory.Data.IUIDataItems;

namespace BeverageManagement.Forms.Reconcile
{
    public interface IReconcileView
    {
        event EventHandler Load;
        event EventHandler RecipesClick;
        event EventHandler AliasesClick;
        event EventHandler RefreshClick;

        IEnumerable<ITicketDisplay> Tickets { set; }
        IEnumerable<IUIPosPour> Pours { set; }
        IRecipesView CreateRecipesView();
        ITicketItemAliasesView CreateAliasesView();
        TimeSpan ViewPast { get; }
    }
}
