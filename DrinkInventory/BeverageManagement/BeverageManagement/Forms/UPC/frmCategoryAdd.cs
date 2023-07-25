using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;
using JaxisExtensions;

namespace BeverageManagement.Forms
{
    public partial class frmCategoryAdd : XtraForm
    {
        private bool m_IsNew;
        private IList<IBLCategory> m_Categories = null;
        private IList<IBLCategory> m_Subcategories = null;
        private IBLManagerFactory m_Factory = BLManagerFactory.Get( );
        private ICategoryBLManager m_CategoriesMan;

        private IBLCategory m_Item = null;

        public frmUPCManagement.NodeInfo CurrentNodeInfo { get; set; }

        public frmCategoryAdd( )
        {
            InitializeComponent( );
            lueCategories.EditValueChanged += new System.EventHandler( lueCategories_EditValueChanged );
            lueCategories.ProcessNewValue += new ProcessNewValueEventHandler( lueCategories_ProcessNewValue );
        }

        public IBLCategory Item
        {
            get
            {
                return m_Item;
            }
            private set
            {
                m_Item = value;
            }
        }

        private void frmSubcategoryAdd_Load( object sender, EventArgs e )
        {
            try
            {
                m_CategoriesMan = m_Factory.ManageCategories( );

                m_Categories = m_CategoriesMan.GetRootCategories( ).ToList( );
                m_Subcategories = m_CategoriesMan.GetSubCategories( ).ToList( );

                ( new List<LookUpEdit> { lueCategories } ).SetupNamedObjectEdits( "Name", "CategoryID" );
                lueCategories.Properties.DataSource = m_Categories.AsQueryable( ).ToList( );

                SetEnabled( false );

                if ( null != CurrentNodeInfo )
                {
                    IBLCategory currentCategory;
                    if ( ( int ) CategoryLevel.Category < CurrentNodeInfo.NodeObjects.Count
                        && null != ( currentCategory = ( IBLCategory ) CurrentNodeInfo.NodeObjects[ ( int ) CategoryLevel.Category ] ) )
                    {
                        lueCategories.EditValue = currentCategory.CategoryID;
                    }
                }
            }
            catch ( Exception err )
            {
                MessageBox.Show( err.Message );
            }
        }

        private void SetEnabled( bool enabled )
        {
            txtDescription.Enabled = enabled;
        }

        private void lueCategories_EditValueChanged( object sender, EventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (null != lueCategories.EditValue)
                {
                    IBLCategory selection = lueCategories.GetSelectedDataRow() as IBLCategory;
                    m_IsNew = selection.Description == null;
                    SetEnabled(m_IsNew);
                    txtDescription.Text = selection.Description ?? String.Empty;
                }
                else
                {
                    SetEnabled(false);
                }

                return true;
            });
        }

        private void lueCategories_ProcessNewValue( object sender, ProcessNewValueEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                String name = ((string)e.DisplayValue).Trim();
                if (!String.IsNullOrEmpty(name))
                {
                    Item = m_CategoriesMan.Create();
                    m_IsNew = true;
                    Item.Name = name;
                    ((List<IBLCategory>)lueCategories.Properties.DataSource).Add(Item);
                    e.Handled = true;
                }

                return true;
            });
        }

        private void btnOK_Click( object sender, EventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (IsValid())
                {
                    if (null == Item)
                    {
                        Item = m_CategoriesMan.Create();
                    }

                    IBLCategory item = Item;

                    Item.ParentID = null;
                    item.Description = txtDescription.Text;
                    item.CategoryID = (Guid)lueCategories.EditValue;
                    item.Name = lueCategories.Text;

                    if (chkSetNozzle.Checked)
                    {
                        IBLStandardNozzle nozzle = item.Nozzle;
                        if (null == nozzle)
                        {
                            nozzle = BLManagerFactory.Get().ManageStandardNozzles().Create();
                        }
                        nozzle.Length = Convert.ToDouble(spinNozzleLength.Value);
                        nozzle.Width = Convert.ToDouble(spinNozzleWidth.Value);
                        nozzle.Shape = (int)cmbNozzleType.SelectedItem;

                        item.Nozzle = nozzle;

                        BLManagerFactory.Get().ManageStandardNozzles().Save(nozzle);
                    }

                    m_CategoriesMan.Save(item);

                    DialogResult = DialogResult.OK;
                }

                return true;
            });
        }

        private bool IsValid( )
        {
            return Log.MsgWrap(false, () =>
            {
                string defaultString = "{0} is missing...{1}";

                StringBuilder errorText = new StringBuilder();
                if (lueCategories.IsLUENullOrEmpty())
                {
                    errorText.AppendFormat(defaultString, global::BeverageManagement.Properties.Resources.Categories, Environment.NewLine);
                }
                if (String.IsNullOrEmpty(txtDescription.Text))
                {
                    errorText.AppendFormat(defaultString, global::BeverageManagement.Properties.Resources.Description, Environment.NewLine);
                }
                if (errorText.ToString().Length > 0)
                {
                    MessageBox.Show(this, errorText.ToString(), "Errors");
                }
                return !(errorText.ToString().Length > 0);

                return true;
            });
        }

        private void chkSetNozzle_CheckedChanged( object sender, EventArgs e )
        {
            spinNozzleWidth.Enabled = spinNozzleLength.Enabled = cmbNozzleType.Enabled = chkSetNozzle.Checked;
        }
    }
}