using System;
using System.Linq;
using Jaxis.Inventory.Data;

namespace BeverageManagement.Forms.Reconcile
{
    //public class ReconcilePresenter
    //{
    //    private readonly IReconcileView m_view;
    //    private readonly IBLManagerFactory m_factory;

    //    public ReconcilePresenter(IReconcileView _view, IBLManagerFactory _factory = null)
    //    {
    //        m_factory = _factory ?? BLManagerFactory.Get();
    //        m_view = _view;
    //        SubscribeViewEvents();
    //    }

    //    private void SubscribeViewEvents()
    //    {
    //        m_view.Load += ViewLoad;
    //        m_view.RecipesClick += ViewRecipesClick;
    //        m_view.AliasesClick += AliasesClick;
    //        m_view.RefreshClick += RefreshClick;
    //    }

        //private void RefreshClick(object _sender, EventArgs _e)
        //{
        //    RefreshView();
        //}

        //private void AliasesClick(object _sender, EventArgs _e)
        //{
        //    var aliasesForm = m_view.CreateAliasesView();
        //    aliasesForm.ShowDialog();
        //}

        //private void ViewRecipesClick(object _sender, EventArgs _e)
        //{
        //    var recipesForm = m_view.CreateRecipesView();
        //    recipesForm.ShowDialog();
        //}

        //private void ViewLoad(object _sender, EventArgs _e)
        //{
        //    RefreshView();
        //}

        //private void RefreshView()
        //{
        //    var tickets = m_factory.ManagePOSTickets().GetAll();
        //    m_view.Tickets = from t in tickets select new TicketDisplay(t);
        //    var beginDate = Services.Clock.Now - m_view.ViewPast;
        //    m_view.Pours = m_factory.ManagePosPours().GetAfter(beginDate);
        //}
    //}
}
