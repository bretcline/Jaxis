using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IUIDataItems;

namespace BeverageManagement.Forms.Reconcile
{
// ReSharper disable InconsistentNaming
    public partial class frmReconcile : DevExpress.XtraEditors.XtraForm //, IReconcileView
// ReSharper restore InconsistentNaming
    {
        private TimeSpan m_viewPast;

        private frmTicketItemAliases m_TicketItemAlias = new frmTicketItemAliases();
        private frmRecipe m_Recipe = new frmRecipe();

        public frmReconcile()
        {
            InitializeComponent();
//            new ReconcilePresenter(this);
        }

        public IEnumerable<ITicketDisplay> Tickets
        {
            set
            {
                grdTickets.DataSource = value.ToList();
            }
        }

        public IEnumerable<IUIPosPour> Pours
        {
            set
            {
                grdPours.DataSource = value.ToList();
            }
        }

        //public IRecipesView CreateRecipesView()
        //{
        //    return new frmRecipes();
        //}

        //public ITicketItemAliasesView CreateAliasesView()
        //{
        //    return new frmTicketItemAliases();
        //}

        public TimeSpan ViewPast
        {
            get
            {
                return m_viewPast;
            }
        }

        private void BbtnRecipesItemClick(object _sender, ItemClickEventArgs _e)
        {
            if (m_Recipe.IsDisposed || null == m_Recipe)
            {
                m_Recipe = new frmRecipe();
            }
            m_Recipe.Show();
        }

        private void BbtnAliasesItemClick(object _sender, ItemClickEventArgs _e)
        {
            if( m_TicketItemAlias.IsDisposed || null == m_TicketItemAlias )
            {
                m_TicketItemAlias = new frmTicketItemAliases();
            }
            m_TicketItemAlias.Show();
        }

        private void BbtnRefreshItemClick(object _sender, ItemClickEventArgs _e)
        {
            var tickets = BLManagerFactory.Get().ManagePOSTickets().GetAll();
            this.Tickets = from t in tickets select new TicketDisplay(t);
            var beginDate = DateTime.Now - m_viewPast;
            this.Pours = BLManagerFactory.Get().ManagePosPours().GetAfter(beginDate);
        }

// ReSharper disable MemberCanBeMadeStatic.Local
        private void GvTicketsRowStyle(object _sender, RowStyleEventArgs _e)
// ReSharper restore MemberCanBeMadeStatic.Local
        {
            var view = _sender as GridView;
            if (_e.RowHandle < 0 || view == null) return;
            var row = view.GetRow(_e.RowHandle);
            if (row is IUIPosDisplay)
            {
                var posDisplay = (IUIPosDisplay)row;
                _e.Appearance.BackColor = ColorFor(posDisplay.Status);
                _e.Appearance.BackColor2 = Color.White;
            }
        }

        private static Color ColorFor(PosStatus _status)
        {
            switch (_status)
            {
                case PosStatus.Alert:
                    return Color.FromArgb(255, 192, 192);
                case PosStatus.Pending:
                    return Color.FromArgb(255, 255, 192);
                case PosStatus.Complete:
                    return Color.FromArgb(192, 255, 192);
                case PosStatus.Void:
                    return Color.FromArgb(192, 192, 192);
            }

            return Color.White;
        }

// ReSharper disable MemberCanBeMadeStatic.Local
        private void GvTicketsMasterRowExpanded(object _sender, CustomMasterRowEventArgs _e)
// ReSharper restore MemberCanBeMadeStatic.Local
        {
            var view = (GridView)((GridView)_sender).GetDetailView(_e.RowHandle, _e.RelationIndex);
            if (view != null)
            {
                view.RowStyle += GvTicketsRowStyle;
            }
        }

// ReSharper disable MemberCanBeMadeStatic.Local
        private void GvTicketsMasterRowCollapsed(object _sender, CustomMasterRowEventArgs _e)
// ReSharper restore MemberCanBeMadeStatic.Local
        {
            var view = (GridView)((GridView)_sender).GetDetailView(_e.RowHandle, _e.RelationIndex);
            if (view != null)
            {
                view.RowStyle -= GvTicketsRowStyle;
            }
        }

        private void BarEditItem1EditValueChanged(object _sender, EventArgs _e)
        {
            var editItem = (BarEditItem) _sender;
            if (editItem.EditValue == null) return;
            m_viewPast = ((ViewPastChoice)editItem.EditValue).TimeSpan;
        }

        private void FrmReconcileLoad(object _sender, EventArgs _e)
        {
            repositoryItemComboBox1.Items.Clear();
            repositoryItemComboBox1.Items.Add(new ViewPastChoice { Display = "Hour", TimeSpan = new TimeSpan(1, 0, 0) });
            repositoryItemComboBox1.Items.Add(new ViewPastChoice { Display = "2 Hours", TimeSpan = new TimeSpan(2, 0, 0) });
            repositoryItemComboBox1.Items.Add(new ViewPastChoice { Display = "4 Hours", TimeSpan = new TimeSpan(4, 0, 0) });
            repositoryItemComboBox1.Items.Add(new ViewPastChoice { Display = "8 Hours", TimeSpan = new TimeSpan(8, 0, 0) });
            repositoryItemComboBox1.Items.Add(new ViewPastChoice { Display = "24 Hours", TimeSpan = new TimeSpan(24, 0, 0) });
            repositoryItemComboBox1.Items.Add(new ViewPastChoice { Display = "72 Hours", TimeSpan = new TimeSpan(72, 0, 0) });
        }
    }
}
