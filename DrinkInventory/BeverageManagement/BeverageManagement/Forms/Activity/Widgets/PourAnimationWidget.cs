using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Jaxis.Controls.GlassFill;
using Jaxis.Inventory.Data;
using Jaxis.Util.Log4Net;

namespace BeverageManagement.Forms.Activity.Widgets
{
    public partial class PourAnimationWidget : DevExpress.XtraEditors.XtraUserControl, IActivityControl
    {
        private double m_BeerPour;
        private double m_WinePour;
        private double m_LiquorPour;


        public PourAnimationWidget()
        {
            InitializeComponent();
            MessageType = new List<Type> {typeof (DataPour)};
        }

        #region IActivityControl Members

        public List<Type> MessageType { get; protected set; }


        public bool AddActivityItem(object _item)
        {
            bool rc = true;

            if (_item is DataPour)
            {
                Log.Debug("Widget", string.Format("CurrentPoursWidget::AddActivityItem {0}", _item.ToString()));

                var item = (DataPour)_item;

                var upc = BLManagerFactory.Get().ManageUPCs().Get(item.UPCID);
                var root = BLManagerFactory.Get().ManageCategories().Get(upc.RootCategoryID);
                var standard = BLManagerFactory.Get().ManageStandardPours().Get(root.Name);

                var value = (int)((item.Volume/standard)*100);

                ctrlGlassFill.FillLevel = value;
                switch (root.Name)
                {
                    case "Liquor":
                    {
                        ctrlGlassFill.GlassType = GlassTypes.Shot;
                        break;
                    }
                    case "Beer":
                    {
                        ctrlGlassFill.GlassType = GlassTypes.Beer;
                        break;
                    }
                    case "Wine":
                    {
                        ctrlGlassFill.GlassType = GlassTypes.Wine;
                        break;
                    }
                }
                ctrlGlassFill.Fill();
            }
            return rc;
        }

        public string DisplayName { get { return "Pour Animation"; } }
        public Guid DisplayID { get { return new Guid("F58DB6EB-DB29-43FC-8D58-175D3EEF9886"); } }
        public object ControlTag { get; set; }

        #endregion

    }
}
