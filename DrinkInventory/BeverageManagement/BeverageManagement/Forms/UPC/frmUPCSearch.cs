using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;

namespace BeverageManagement.Forms.UPC
{
    public partial class frmUPCSearch : DevExpress.XtraEditors.XtraForm
    {
        public IBLUPCItem UPCItem { get; set; }

        public frmUPCSearch()
        {
            InitializeComponent();
        }

        private void frmUPCSearch_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                UPCUI.LoadUPCGrid(gvUPCSearch);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void gvUPCSearch_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var data = gvUPCSearch.GetFocusedDataRow();
            var other = data;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UPCItem = gvUPCSearch.GetFocusedRow() as IBLUPCItem;

        }

        private void frmUPCSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            var items = gvUPCSearch.DataSource as BindingList<IUIUPCItem>;
            if (null != items)
            {
                items.Clear();
            }

        }
    }
}