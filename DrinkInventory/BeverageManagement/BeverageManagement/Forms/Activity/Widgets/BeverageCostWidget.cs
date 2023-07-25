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
    public partial class BeverageCostWidget : DevExpress.XtraEditors.XtraUserControl, IActivityControl
    {
        public BeverageCostWidget()
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

        public string DisplayName { get { return "Beverage Cost %"; } }
        public Guid DisplayID { get { return new Guid("67FA6D52-1AF1-47E6-8216-62F3B5C9C3B4"); } }
        public object ControlTag { get; set; }

        #endregion
    }
}
