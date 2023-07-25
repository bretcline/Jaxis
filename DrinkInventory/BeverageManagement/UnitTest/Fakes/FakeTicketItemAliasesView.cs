using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BeverageManagement.Forms.Reconcile;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes
{
    class FakeTicketItemAliasesView : ITicketItemAliasesView
    {
        private IList<TicketItemAliasDisplay> m_aliases;

        public FakeTicketItemAliasesView(IBLManagerFactory _factory)
        {
            new TicketItemAliasesPresenter(this, _factory);
        }

        internal void FireLoad()
        {
            Fire(Load, EventArgs.Empty);
        }

        private void Fire(EventHandler _handler, EventArgs _args)
        {
            if (_handler != null)
            {
                _handler(this, _args);
            }
        }

        public event FormClosingEventHandler Closing;
        public event EventHandler DeleteClick;
        public event EventHandler AssignClick;
        public event EventHandler AliasModified;
        public IEnumerable<IBLRecipe> RecipeList { get; set; }

        public IEnumerable<TicketItemAliasDisplay> Aliases
        {
            get
            {
                return m_aliases;
            }
            set 
            {
                m_aliases = value.ToList();
                //Log.Info(string.Format("FakeTicketItemAliasesView: Aliases set, count = {0}", m_aliases.Count));
            }
        }

        public IEnumerable<string> UnknownTicketItems { get; set; }

        public TicketItemAliasDisplay SelectedAlias { get; set; }

        public void ShowDialog()
        {
            Shown = true;
        }

        public void AddAlias(string _description)
        {
            var alias = new TicketItemAliasDisplay {DescriptionOnTicket = _description};
            m_aliases.Add(alias);
        }

        public void DeleteSelectedAlias()
        {
            var selectedAlias = SelectedAlias;
            if (selectedAlias != null)
                m_aliases.Remove(selectedAlias);
        }

        public void ShowError(string _message)
        {
            ShownError = _message;
        }

        protected string ShownError { get; set; }

        internal static bool? Shown { get; set; }

        public IList<TicketItemAliasDisplay> ConcreteAliases
        {
            get { return m_aliases; }
        }

        public event EventHandler Load;

        public void Close(DialogResult _result)
        {
            DialogResult = _result;
            var close = true;
            
            if (Closing != null)
            {
                var args = new FormClosingEventArgs(CloseReason.UserClosing, false);
                Closing(this, args);
                close = !args.Cancel;
            }

            Closed = close;
        }

        public DialogResult DialogResult { get; set; }
        public bool Closed { get; set; }

        public string SelectedUnknownItem { get; set; }

        internal void FireDeleteClick()
        {
            Fire(DeleteClick, EventArgs.Empty);
        }

        public void FireAssignClick()
        {
            Fire(AssignClick, EventArgs.Empty);
        }
    }
}
