using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BeverageActivity.Forms.Activity.Widgets
{
    public partial class TransactionSummaryWidget : DevExpress.XtraEditors.XtraUserControl, IActivityControl
    {
        public TransactionSummaryWidget()
        {
            InitializeComponent();
        }

        #region Implementation of IActivityReceiver

        public List<Type> MessageType { get; protected set; }


        public bool AddActivityItem(object _item)
        {
            return true;
        }

        #endregion

        #region Implementation of IActivityControl

        public string DisplayName { get { return "Transaction Summary"; } }
        public Guid DisplayID { get { return new Guid("9B9D215D-46CA-40F8-AE39-4DAF9CE77857"); } }
        public object ControlTag { get; set; }

        #endregion
    }
}
