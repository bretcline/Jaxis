using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Jaxis.Inventory.Data.IBLDataItems;

namespace BeverageManagement.Forms.Reconcile
{
    public interface ITicketItemAliasesView
    {
        event EventHandler Load;
        event FormClosingEventHandler Closing;
        event EventHandler DeleteClick;
        event EventHandler AssignClick;
        event EventHandler AliasModified;

        DialogResult DialogResult { get; set; }
        IEnumerable<IBLRecipe> RecipeList { set; }
        IEnumerable<TicketItemAliasDisplay> Aliases { get; set; }
        IEnumerable<string> DeletedAliases { get; set; }
        IEnumerable<string> UnknownTicketItems { set; }
        TicketItemAliasDisplay SelectedAlias { get; set; }
        string SelectedUnknownItem { get; set; }

        void ShowDialog();
        void AddAlias(string _description, decimal _price);
        void DeleteSelectedAlias();
        void ShowError(string _message);
    }
}
