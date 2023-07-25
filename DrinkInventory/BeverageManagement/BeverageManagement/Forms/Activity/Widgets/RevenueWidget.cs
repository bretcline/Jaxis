using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BeverageManagement.Forms.Activity.Widgets
{
    public partial class RevenueWidget : DevExpress.XtraEditors.XtraUserControl, IActivityControl
    {
        public RevenueWidget()
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

        public string DisplayName { get { return "Revenue"; } }
        public Guid DisplayID { get { return new Guid("BA2E4F23-55FC-4A32-96C4-107BC296EA53"); } }
        public object ControlTag { get; set; }

        #endregion

    }
}
