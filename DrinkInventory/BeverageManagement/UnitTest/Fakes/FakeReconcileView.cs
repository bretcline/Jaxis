using System;
using System.Collections.Generic;
using System.Linq;
using BeverageManagement.Forms.Reconcile;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IUIDataItems;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes
{
    internal class FakeReconcileView : IReconcileView
    {
        private List<ITicketDisplay> m_tickets;
        private List<IUIPosPour> m_pours;

        internal FakeReconcileView(IBLManagerFactory _factory)
        {
            ViewPast = new TimeSpan(100, 0, 0, 0);
            new ReconcilePresenter(this, _factory);
            m_tickets = new List<ITicketDisplay>();
            m_pours = new List<IUIPosPour>();
        }

        public event EventHandler Load;
        public event EventHandler RecipesClick;
        public event EventHandler AliasesClick;
        public event EventHandler RefreshClick;

        public IEnumerable<IUIPosPour> Pours
        {
            get { return m_pours; }
            set { m_pours = value.ToList(); }
        }

        public IRecipesView CreateRecipesView()
        {
            return new FakeRecipesView(FakeManagerFactory.Get());
        }

        public ITicketItemAliasesView CreateAliasesView()
        {
            return new FakeTicketItemAliasesView(FakeManagerFactory.Get());
        }

        public TimeSpan ViewPast { get; set; }

        public IEnumerable<ITicketDisplay> Tickets
        {
            get { return m_tickets; }
            set { m_tickets = value.ToList(); }
        }

        internal void FireLoad()
        {
            if (Load != null)
            {
                Load(this, EventArgs.Empty);
            }
        }

        internal void FireRecipesClick()
        {
            if (RecipesClick != null)
            {
                RecipesClick(this, EventArgs.Empty);
            }
        }

        public void FireAliasesClick()
        {
            if (AliasesClick != null)
            {
                AliasesClick(this, EventArgs.Empty);
            }
        }

        public void FireRefreshClick()
        {
            if (RefreshClick != null)
            {
                RefreshClick(this, EventArgs.Empty);
            }
        }
    }
}
