using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Jaxis.Inventory.Data.IBLDataItems;

namespace BeverageManagement.Forms
{
    public partial class frmSummary<T> : DevExpress.XtraEditors.XtraForm
    {
        public IList<T> DataItems { get; set; }
        public int m_AutoClose { get; set; }
        private int m_Count = 0;

        public frmSummary( int _autoClose = 0 )
        {
            InitializeComponent();
            m_AutoClose = _autoClose;
            tmrAutoClose.Enabled = false;
        }

        private void frmSummary_Deactivate(object sender, EventArgs e)
        {
            tmrAutoClose.Enabled = false;
            this.Close();
        }

        private void frmSummary_Load(object sender, EventArgs e)
        {
            if (null != DataItems && 0 < DataItems.Count)
            {
                grdSummary.DataSource = DataItems;
                if (null != gvSummary.Columns.ColumnByFieldName("BottleCount"))
                {
                    gvSummary.Columns["UPCName"].Group();
                    gvSummary.Columns["BottleCount"].Summary.Add(SummaryItemType.Sum);
                    gvSummary.GroupSummary.Add(SummaryItemType.Sum, "BottleCount");
                }
                else if (null != gvSummary.Columns.ColumnByFieldName("TagNumber"))
                {
                    gvSummary.Columns["UPCName"].Group();
                    gvSummary.Columns["TagNumber"].Summary.Add(SummaryItemType.Count);
                    gvSummary.GroupSummary.Add(SummaryItemType.Count, "TagNumber");
                }
                grdSummary.RefreshDataSource();
            }
            if (0 < m_AutoClose)
            {
                tmrAutoClose.Enabled = true;
            }
        }

        private void tmrAutoClose_Tick(object sender, EventArgs e)
        {
            m_Count++;
            if (m_Count > 10 * m_AutoClose)
            {
                this.Close();
            }
        }
    }
}