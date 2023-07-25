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
    public partial class ReconciliationWidget : DevExpress.XtraEditors.XtraUserControl, IActivityControl
    {
        public ReconciliationWidget()
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

        public string DisplayName { get { return "POS Reconciliation"; } }
        public Guid DisplayID { get { return new Guid("7343CA4A-B164-417C-A8EA-7F2833B05C58"); } }
        public object ControlTag { get; set; }

        #endregion
    }
}
