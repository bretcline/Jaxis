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
    public partial class CostWidget : DevExpress.XtraEditors.XtraUserControl, IActivityControl
    {
        public CostWidget()
        {
            InitializeComponent();
        }

        #region Implementation of IActivityReceiver

        public bool AddActivityItem(object _item)
        {
            return true;
        }

        #endregion

        #region Implementation of IActivityControl

        public List<Type> MessageType { get; protected set; }


        public string DisplayName { get { return "Cost"; } }
        public Guid DisplayID { get { return new Guid("3B01D8F6-2A2D-4D8E-9243-D54C608E69A5"); } }
        public object ControlTag { get; set; }

        #endregion

    }
}
