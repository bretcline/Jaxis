using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;
using JaxisExtensions;
using System.Xml;

namespace BeverageManagement.Forms
{
    public partial class frmUPC : XtraForm
    {
        private IList<IBLCategory> m_Categories = null;
        private IList<IBLCategory> m_Subcategories = null;
        private IList<IBLManufacturerView> m_Manufacturers = null;
        private IList<IBLSizeType> m_SizeTypes = null;
        //private IBLManagerFactory m_Factory = BLManagerFactory.Get( );
        //private ICategoryBLManager m_CategoriesMan;
        //private ISizeTypeBLManager m_SizeTypeMan;

        private IBLUPCItem m_Item = null;

        public frmUPC( )
        {
            InitializeComponent( );
            lueCategories.EditValueChanged += new System.EventHandler( lueCategories_EditValueChanged );
            lueSubcategories.EditValueChanged += new EventHandler( lueSubcategories_EditValueChanged );
            lueManufacturer.ProcessNewValue += new ProcessNewValueEventHandler( lueManufacturer_ProcessNewValue );
            spinNozzleLength.EditValueChanging += new ChangingEventHandler( CommonUI.NozzleDiameterValidation );
        }

        public IBLUPCItem Item
        {
            get
            {
                return m_Item;
            }
            set
            {
                m_Item = value;
            }
        }
        
        private void frmUPC_Load( object sender, EventArgs e )
        {
            Log.MsgWrap( false, () =>  
            {	        
                var categoriesMan = BLManagerFactory.Get( ).ManageCategories( );
                var sizeTypeMan = BLManagerFactory.Get( ).ManageSizeTypes( );

                List<IBLCategory> categories = categoriesMan.GetAll().ToList();

                var quality = BLManagerFactory.Get().ManageQuality().GetAll().OrderBy( q => q.QualityLevel ).ToList();
                m_Categories = categories.Where( C => C.ParentID == null ).OrderBy( c => c.Name).ToList( );
                m_Subcategories = categories.Where(C => C.ParentID != null).OrderBy(c => c.Name).ToList();
                m_Manufacturers = BLManagerFactory.Get( ).ManageManufacturerViews().GetAll().OrderBy(c => c.Name).ToList();
                m_SizeTypes = sizeTypeMan.GetAll( ).ToList( );

                ( new List<LookUpEdit> { lueCategories, lueSubcategories } ).SetupNamedObjectEdits( "Name", "CategoryID" );
                ( new List<LookUpEdit> { lueSizeType } ).SetupNamedObjectEdits( "Name", "SizeTypeID" );
                ( new List<LookUpEdit> { lueManufacturer } ).SetupNamedObjectEdits( "Name", "ManufacturerID" );
                ( new List<LookUpEdit> { lueQuality } ).SetupNamedObjectEdits( "Name", "QualityLevel" );

                lueQuality.Properties.DataSource = quality;

                lueCategories.Properties.DataSource = m_Categories;

                lueSubcategories.Enabled = false;
                SetEnabled( false );

                lueSizeType.Properties.DataSource = m_SizeTypes;

                CommonUI.LoadNozzleTypes(cmbNozzleType);

                lueCategories.EditValue = Item.RootCategoryID;
                lueSubcategories.EditValue = Item.CategoryID;

                lueSizeType.EditValue = Item.SizeTypeID;
                var size = BLManagerFactory.ConvertFromMLFromSizeType(Item.Size, Item.SizeType);
                txtSize.Text = Math.Round(size, 1).ToString();
                txtUPC.Text = Item.ItemNumber;
                txtCustomID.Text = Item.CustomID;
                txtDescription.Text = Item.Name;
                lueManufacturer.Text = Item.Manufacturer;
                lueQuality.EditValue = Item.Quality;
                txtPourModifier.Text = Item.PourModifier.ToString();
                txtParAmount.Text = Item.MinimumParLevel.ToString();
                chkHalfPour.Checked = Item.AllowHalfPour;

                if (Item.ChildUPCID.HasValue)
                {
                    chkCase.Checked = true;
                    var upc = BLManagerFactory.Get().ManageUPCs().Get(Item.ChildUPCID.Value);
                    //var upc = BLManagerFactory.Get().ManageUPCs().GetUPCByUPCNumber(txtBottleUPC.Text);
                    if (null != upc && Item.BottleCount != null)
                    {
                        txtCaseQuantity.Text = Item.BottleCount.Value.ToString();
                        txtBottleUPC.Text = upc.ItemNumber;
                        btnUPC.Text = upc.Name;
                    }
                }
                if ( null != Item.Nozzle )
                {
                    chkSetNozzle.Checked = true;
                    spinNozzleLength.Value = Convert.ToDecimal(((IUPCItem)Item).Nozzle.Length);
                    spinNozzleWidth.Value = Convert.ToDecimal(((IUPCItem)Item).Nozzle.Width);
                    cmbNozzleType.SelectedItem = (NozzleShapes)((IUPCItem)Item).Nozzle.Shape;
                }
                else
                {
                    spinNozzleLength.Value = Convert.ToDecimal(BLManagerFactory.Get( ).GetDefaultNozzle().Length);
                    spinNozzleWidth.Value = Convert.ToDecimal( BLManagerFactory.Get( ).GetDefaultNozzle().Width );
                    cmbNozzleType.SelectedItem = (NozzleShapes) BLManagerFactory.Get( ).GetDefaultNozzle().Shape;
                }

                if( Item.UnitPrice.HasValue )
                {
                    chkSetPrice.Checked = true;
                    txtBottleCost.Text = Item.UnitPrice.Value.ToString( "F2" );
                }
                return true;
            } ); 
        }

        private void SetEnabled( bool enabled )
        {
            Log.MsgWrap(false, () =>
            {
                lueSizeType.Enabled = enabled;
                lueManufacturer.Enabled = enabled;
                txtUPC.Enabled = enabled;
                txtDescription.Enabled = enabled;
                txtSize.Enabled = enabled;

                return true;
            });
        }

        private void lueCategories_EditValueChanged( object sender, EventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (null != lueCategories.EditValue)
                {
                    lueSubcategories.Enabled = true;
                    Guid categoryId = (Guid)lueCategories.EditValue;

                    lueSubcategories.Properties.DataSource = null;

                    lueSubcategories.Properties.DataSource = m_Subcategories.Where(c => c.ParentID == categoryId).ToList();

                    lueSubcategories.EditValue = Item.CategoryID;

                    var category = lueCategories.GetSelectedDataRow() as IBLCategory;
                    if (null != category)
                    {
                        SetDefaultsByCategory( category );
                    }
                }
                else
                {
                    lueSubcategories.Enabled = false;
                    SetEnabled(false);
                }

                return true;
            });
        }

        private void SetDefaultsByCategory( IBLCategory _category )
        {
            Log.MsgWrap(false, () =>
            {
                string size = "1";
                string sizeType = "Liter";

                switch (_category.Name)
                {
                    case "Wine":
                    {
                        size = "750";
                        sizeType = "Milliliter";
                        Item.Nozzle = BLManagerFactory.Get().GetDefaultNozzle(DefaultNozzleType.Wine);
                        break;
                    }
                    case "Beer":
                    {
                        size = "12";
                        sizeType = "Ounce";
                        break;
                    }
                    default:
                    {
                        size = "1";
                        sizeType = "Liter";
                        break;
                    }
                }

                txtSize.Text = size;
                lueSizeType.Text = sizeType;

                lueSizeType.ClosePopup();
                return true;
            });
        }

        private void lueSubcategories_EditValueChanged( object sender, EventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (null != lueSubcategories.EditValue)
                {
                    SetEnabled(true);
                    Guid categoryId = (Guid)lueSubcategories.EditValue;

                    var Man = m_Manufacturers.Where(M => M.CategoryID == categoryId);
                    lueManufacturer.Properties.DataSource = Man.ToList();

                    lueManufacturer.EditValue = Item.Manufacturer;
                }
                else
                {
                    SetEnabled(false);
                }

                return true;
            });
        }

        private void lueManufacturer_ProcessNewValue( object sender, ProcessNewValueEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                String name = ((string)e.DisplayValue).Trim();

                if (!String.IsNullOrEmpty(name))
                {
                    var item = BLManagerFactory.Get().ManageManufacturerViews().Create();
                    item.Name = name;
                    item.CategoryID = (Guid)lueSubcategories.EditValue;
                    item.RootCategoryID = (Guid)lueCategories.EditValue;
                    ((List<IBLManufacturerView>)lueManufacturer.Properties.DataSource).Add(item);

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
                        Item = BLManagerFactory.Get().ManageUPCs().Create();
                    }

                    IBLUPCItem item = Item;
                    item.SizeTypeID = (Guid)lueSizeType.EditValue;
                    double size = Convert.ToDouble(txtSize.Text);
                    item.Size = Convert.ToInt32( BLManagerFactory.ConvertToMLFromSizeType(size, item.SizeTypeID) );
                    item.Name = txtDescription.Text;
                    item.CategoryID = (Guid)lueSubcategories.EditValue;
                    item.RootCategoryID = (Guid)lueCategories.EditValue;
                    item.ItemNumber = txtUPC.Text;
                    item.CustomID = txtCustomID.Text;
                    if (string.IsNullOrWhiteSpace(lueQuality.Text))
                    {
                        item.Quality = 1;
                    }
                    else
                    {
                        item.Quality = (int) lueQuality.EditValue;
                    }
                    ///TODO: Update Manufacturer table.
                    //item.Manufacturer = lueManufacturer.Text;
                    Guid manId = (Guid)lueManufacturer.EditValue;

                    var manMan = BLManagerFactory.Get().ManageManufacturers();
                    var manufacturer = manMan.Get(manId);
                    if (null == manufacturer)
                    {
                        manufacturer = manMan.Create();
                        manufacturer.ManufacturerID = manId;
                        manufacturer.Name = lueManufacturer.Text;

                        manMan.Save(manufacturer);
                    }
                    item.ManufacturerID = manufacturer.ManufacturerID;
                    item.MinimumParLevel = Convert.ToInt32(txtParAmount.Text);
                    item.AllowHalfPour = chkHalfPour.Checked;

                    if (chkCase.Checked)
                    {
                        var upc = BLManagerFactory.Get().ManageUPCs().GetUPCByUPCNumber(txtBottleUPC.Text);
                        item.ChildUPCID = upc.UPCID;
                        item.BottleCount = Convert.ToInt32(txtCaseQuantity.Text);
                    }
                    if (chkSetNozzle.Checked)
                    {
                        IBLStandardNozzle nozzle = ((IBLUPCItem) item).Nozzle;
                        if (null == nozzle)
                        {
                            nozzle = BLManagerFactory.Get().ManageStandardNozzles().Create();
                            item.Nozzle = nozzle;
                        }
                        nozzle.Length = Convert.ToDouble(spinNozzleLength.Value);
                        nozzle.Width = Convert.ToDouble(spinNozzleWidth.Value);
                        nozzle.Shape = (int)cmbNozzleType.SelectedItem;
                        BLManagerFactory.Get().ManageStandardNozzles().Save(nozzle);
                    }
                    if (chkSetPrice.Checked)
                    {
                        item.UnitPrice = Convert.ToDecimal(txtBottleCost.Text);
                        //IBLStandardPrice price = item.Price;
                        //if (null == price)
                        //{
                        //    price = BLManagerFactory.Get().ManageStandardPrices().Create();
                        //}
                        //price.SinglePrice = Convert.ToDecimal( ( string.IsNullOrWhiteSpace(txtSingle.Text) ) ? "0" : txtSingle.Text );
                        //price.DoublePrice = Convert.ToDecimal(txtDouble.Text);
                        //BLManagerFactory.Get().ManageStandardPrices().Save(price);
                        //item.Price = price;
                    }
                    if (chkPourModifier.Checked)
                    {
                        item.PourModifier = Convert.ToDouble(txtPourModifier.Text);
                    }
                    BLManagerFactory.Get().ManageUPCs().Save(item);

                    DialogResult = DialogResult.OK;
                }

                return true;
            });
        }

        private bool IsValid( )
        {
            return Log.MsgWrap(false, () =>
            {
                string defaultString = global::BeverageManagement.Properties.Resources.ValueIsMissing;

                StringBuilder errorText = new StringBuilder();
                if (lueCategories.IsLUENullOrEmpty())
                {
                    errorText.AppendFormat(defaultString, global::BeverageManagement.Properties.Resources.Category, Environment.NewLine);
                }
                if (lueSubcategories.IsLUENullOrEmpty())
                {
                    errorText.AppendFormat(defaultString, global::BeverageManagement.Properties.Resources.Subcategory, Environment.NewLine);
                }
                if (String.IsNullOrEmpty(txtUPC.Text))
                {
                    errorText.AppendFormat(defaultString, global::BeverageManagement.Properties.Resources.UPCLabel, Environment.NewLine);
                }
                if (String.IsNullOrEmpty(txtDescription.Text))
                {
                    errorText.AppendFormat(defaultString, global::BeverageManagement.Properties.Resources.Description, Environment.NewLine);
                }
                if (lueManufacturer.IsLUENullOrEmpty())
                {
                    errorText.AppendFormat(defaultString, global::BeverageManagement.Properties.Resources.Manufacturer, Environment.NewLine);
                }
                int result;
                if (String.IsNullOrEmpty(txtSize.Text) || !(int.TryParse(txtSize.Text, out result)) || result <= 0)
                {
                    errorText.AppendFormat(defaultString, global::BeverageManagement.Properties.Resources.Size, Environment.NewLine);
                }
                if (lueSizeType.IsLUENullOrEmpty())
                {
                    errorText.AppendFormat(defaultString, global::BeverageManagement.Properties.Resources.SizeType, Environment.NewLine);
                }
                if (errorText.ToString().Length > 0)
                {
                    MessageBox.Show(this, errorText.ToString(), global::BeverageManagement.Properties.Resources.Errors);
                }
                return !(errorText.ToString().Length > 0);

                return true;
            });
        }

        private void chkSetNozzle_CheckedChanged( object sender, EventArgs e )
        {
            spinNozzleWidth.Enabled = spinNozzleLength.Enabled = cmbNozzleType.Enabled = chkSetNozzle.Checked;
        }

        private void chkSetPrice_CheckedChanged( object sender, EventArgs e )
        {
            txtBottleCost.Enabled = txtSingle.Enabled = txtDouble.Enabled = chkSetPrice.Checked;
        }

        private void chkPourModifier_CheckedChanged( object sender, EventArgs e )
        {
            txtPourModifier.Enabled = chkPourModifier.Checked;
        }

        private void chkCase_CheckedChanged(object sender, EventArgs e)
        {
            txtCaseQuantity.Enabled = txtBottleUPC.Enabled = chkCase.Checked;
            layoutControlItem14.Visibility = ( true == chkCase.Checked ) ? LayoutVisibility.Always : LayoutVisibility.Never;
        }

        private void txtBottleUPC_EditValueChanged(object sender, EventArgs e)
        {
        }


        private void ModifyUPC(IBLUPCItem _item)
        {
            Log.MsgWrap(false, () =>
            {
                if (null == _item)
                {
                    _item = BLManagerFactory.Get().ManageUPCs().Create();
                }
                using (frmUPC upc = new frmUPC() { Item = _item })
                {
                    if (System.Windows.Forms.DialogResult.OK == upc.ShowDialog())
                    {
                        var item = upc.Item;
                        this.m_Item.ChildUPCID = item.UPCID;
                    }
                }
                return true;
            });
        }

        private void btnUPC_Click(object sender, EventArgs e)
        {
            var upc = BLManagerFactory.Get().ManageUPCs().GetUPCByUPCNumber(txtBottleUPC.Text);
            ModifyUPC( upc );
        }

        private void txtBottleUPC_Leave(object sender, EventArgs e)
        {
            var upc = BLManagerFactory.Get().ManageUPCs().GetUPCByUPCNumber(txtBottleUPC.Text);
            ModifyUPC(upc);
        }
    }
}