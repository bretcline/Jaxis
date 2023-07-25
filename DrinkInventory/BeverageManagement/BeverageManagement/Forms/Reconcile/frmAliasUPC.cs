using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;

namespace BeverageManagement.Forms.Reconcile
{
    public partial class frmAliasUPC : DevExpress.XtraEditors.XtraForm
    {
        protected BindingList<IUITicketItemAlias> m_Aliases;
        protected BindingList<IUIUPCItemShort> m_Upcs;
        private IBLTicketItemAlias m_CurrentItem;

        private Dictionary<string, IBLTicketItemAlias> m_Updates = new Dictionary<string, IBLTicketItemAlias>();

        public frmAliasUPC()
        {
            InitializeComponent();
            var items = BLManagerFactory.Get().ManageTicketItemAliases().GetAll();

            m_Aliases = new BindingList<IUITicketItemAlias>(
                        BLManagerFactory.Get().ManageTicketItemAliases().GetAll().Cast<IUITicketItemAlias>().ToList());

            m_Upcs = new BindingList<IUIUPCItemShort>(
                    BLManagerFactory.Get().ManageUPCs().GetAll().Cast<IUIUPCItemShort>().ToList());
        }

        private void frmAliasUPC_Load(object sender, EventArgs e)
        {
            try
            {
                grdAliases.DataSource = m_Aliases;

                grdUPCList.DataSource = m_Upcs;
            }
            catch
            {
                
            }
        }

        private void gvAliases_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            m_CurrentItem = gvAliases.GetFocusedRow() as IBLTicketItemAlias;
            if (null != m_CurrentItem)
            {
                lblUPCName.Text = string.Empty;
                lblAlias.Text = m_CurrentItem.Description;
                if ( m_CurrentItem.PosUPC.HasValue)
                {
                    var upc = BLManagerFactory.Get().ManageUPCs().Get(m_CurrentItem.PosUPC.Value);
                    if( null != upc)
                    {
                        lblUPCName.Text = upc.Name;
                    }
                }
            }

        }

        private void gvUPCList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Log.Wrap("frmAliasUPC::gvUPCList_FocusedRowChanged", false, () =>
            {
                var item = gvUPCList.GetFocusedRow() as IBLUPCItem;
                if (null != item && item.UPCID != Guid.Empty && null != m_CurrentItem)
                {
                    lblUPCName.Text = item.Name;
                    m_CurrentItem.PosUPC = item.UPCID;

                    m_Updates[m_CurrentItem.Description] = m_CurrentItem;
                }
                return true;
            });
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Log.Wrap("frmAliasUPC::btnOK_Click", false, () =>
            {
                foreach (var blTicketItemAliase in m_Updates.Values)
                {
                    BLManagerFactory.Get().ManageTicketItemAliases().Save(blTicketItemAliase);
                }
                return true;
            });
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Log.Wrap("frmAliasUPC::btnClear_Click", false, () =>
            {
                m_CurrentItem.PosUPC = null;
                m_Updates[m_CurrentItem.Description] = m_CurrentItem;
                return true;
            });
        }
    }
}