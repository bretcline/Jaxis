using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BeverageManagement.Forms;
using DevExpress.XtraEditors;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;

namespace BeverageManagement.Controls
{
    public partial class StandardPourItem : DevExpress.XtraEditors.XtraUserControl
    {

        Func<double, string> convert = null;

        protected IBLStandardPour m_StandardPour = null;
        public IBLStandardPour PourItem
        {
            get
            {
                if (null == m_StandardPour)
                {
                    m_StandardPour = BLManagerFactory.Get().ManageStandardPours().Create();
                }
                m_StandardPour.StandardVariance = double.Parse(txtVariance.Text)/100;
                m_StandardPour.Name = txtName.Text;
                m_StandardPour.PourStandard = BLManagerFactory.Get().ConvertPourFromUnits(double.Parse(txtPour.Text));
                m_StandardPour.CategoryID = (Guid?)lueCategories.EditValue;
                return m_StandardPour;
            }
            set
            {
                if (null == m_StandardPour)
                {
                    m_StandardPour = value;
                }
                Name = value.Name;
                Pour = value.PourStandard;
                Variance = value.StandardVariance;
                if (value.CategoryID.HasValue)
                {
                    lueCategories.EditValue = value.CategoryID.Value;
                }
                txtName.Enabled = !m_StandardPour.SystemStandard;
            }
        }

        public StandardPourItem()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                convert = (double _value) => BLManagerFactory.Get().ConvertPourToUnits(_value).ToString("F2");

                (new List<LookUpEdit> { lueCategories }).SetupNamedObjectEdits("Name", "CategoryID");
                lueCategories.Properties.DataSource = BLManagerFactory.Get().ManageCategories().GetRootCategories().ToList( );
                lueCategories.ItemIndex = 0;
            }
        }

        public StandardPourItem(IBLStandardPour _blStandardPour) : this()
        {
            PourItem = _blStandardPour;
        }


        public string Name
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        public double Pour
        {
            get { return double.Parse(txtPour.Text); }
            set { txtPour.Text = convert(value); }
        }

        public double Variance
        {
            get { return double.Parse(txtVariance.Text)/100; }
            set { txtVariance.Text = (Math.Round(value, 2) * 100 ).ToString(); }
        }
    }
}
