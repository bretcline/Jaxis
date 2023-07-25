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
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;

namespace BeverageManagement.Controls
{
    public partial class IngredientItem : DevExpress.XtraEditors.XtraUserControl
    {
        private IBLIngredient m_Ingredient;

        public delegate void DeleteEvent( IngredientItem _control, IBLIngredient _item );

        public event DeleteEvent OnDelete;

        public IngredientItem()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                lueType.Properties.DataSource = Enum.GetNames(typeof(IngredientContainerTypes));
                lueRequirement.Properties.DataSource = Enum.GetNames(typeof(IngredientRequirementType));

                var quality = BLManagerFactory.Get().ManageQuality().GetAll().OrderBy(q => q.QualityLevel).ToList();
                var sizes = BLManagerFactory.Get().ManageStandardPours().GetAll().ToList();

                (new List<LookUpEdit> { luePourSize }).SetupNamedObjectEdits("Name", "StandardPourID");
                (new List<LookUpEdit> { lueQuality }).SetupNamedObjectEdits("Name", "QualityLevel");

                lueQuality.Properties.DataSource = quality;
                luePourSize.Properties.DataSource = sizes;

                lueQuality.EditValue = quality[0].QualityLevel;
                luePourSize.EditValue = sizes[0].StandardPourID;
            }
        }

        public IngredientItem( IBLIngredient _ingredient ) : this()
        {
            Ingredient = _ingredient;
        }

        public IBLIngredient Ingredient
        {
            get
            {
                if (null == m_Ingredient)
                {
                    m_Ingredient = BLManagerFactory.Get().ManageIngredients().Create();
                }
                m_Ingredient.Quality = (int)lueQuality.EditValue;
                m_Ingredient.StandardPourID = (Guid)luePourSize.EditValue;
                if (null == lueRequirement.EditValue)
                {
                    m_Ingredient.Type = (int)IngredientRequirementType.Required;
                }
                else
                {
                    m_Ingredient.Type = (int) Enum.Parse(typeof(IngredientRequirementType), lueRequirement.EditValue.ToString() );
                }

                if (string.IsNullOrWhiteSpace(txtGroup.Text))
                {
                    m_Ingredient.Number = 0;
                }
                else
                {
                    m_Ingredient.Number = int.Parse(txtGroup.Text);
                }

                var type = IngredientContainerTypes.UPC;
                Enum.TryParse(lueType.Text, true, out type);
                switch (type)
                {
                    case IngredientContainerTypes.UPC:
                    {
                        m_Ingredient.UPCID = (Guid)(lueItem.EditValue ?? Guid.Empty);
                        m_Ingredient.CategoryID = m_Ingredient.ManufacturerID = (Guid?)null;
                        break;
                    }
                    case IngredientContainerTypes.Category:
                    {
                        m_Ingredient.CategoryID = (Guid) (lueItem.EditValue ?? Guid.Empty);
                        m_Ingredient.UPCID = m_Ingredient.ManufacturerID = (Guid?)null;
                        break;
                    }
                    case IngredientContainerTypes.Manufacturer:
                    {
                        m_Ingredient.ManufacturerID = (Guid)(lueItem.EditValue ?? Guid.Empty);
                        m_Ingredient.UPCID = m_Ingredient.CategoryID = (Guid?)null;
                        break;
                    }
                }
                return m_Ingredient;
            }
            set
            {
                if( null == m_Ingredient )
                {
                    m_Ingredient = value;
                }
                lueQuality.EditValue = m_Ingredient.Quality ;
                luePourSize.EditValue = m_Ingredient.StandardPourID;
                lueRequirement.EditValue = ((IngredientRequirementType) m_Ingredient.Type).ToString();
                txtGroup.Text = m_Ingredient.Number.ToString();

                if( m_Ingredient.UPCID.HasValue )
                {
                    lueType.Text = IngredientContainerTypes.UPC.ToString();
                    lueType.EditValue = IngredientContainerTypes.UPC.ToString();
                    lueItem.EditValue = m_Ingredient.UPCID;
                }
                else if( m_Ingredient.CategoryID.HasValue )
                {
                    lueType.Text = IngredientContainerTypes.Category.ToString();
                    lueType.EditValue = IngredientContainerTypes.Category.ToString();
                    lueItem.EditValue = m_Ingredient.CategoryID;
                }
                else if( m_Ingredient.ManufacturerID.HasValue )
                {
                    lueType.Text = IngredientContainerTypes.Manufacturer.ToString();
                    lueType.EditValue = IngredientContainerTypes.Manufacturer.ToString();
                    lueItem.EditValue = m_Ingredient.ManufacturerID;
                }
            }
        }

        private void IngredientItem_Load(object sender, EventArgs e)
        {
        }

        private void cmbType_EditValueChanged(object sender, EventArgs e)
        {
            var type = IngredientContainerTypes.UPC;
            Enum.TryParse(lueType.EditValue.ToString(), true, out type);
            switch ( type )
            {
                case IngredientContainerTypes.UPC:
                {
                    var items = BLManagerFactory.Get().ManageUPCs().GetAll().ToList();
                    (new List<LookUpEdit> { lueItem }).SetupNamedObjectEdits("Name", "UPCID");
                    lueItem.Properties.DataSource = items;
                    break;
                }
                case IngredientContainerTypes.Category:
                {
                    var items = BLManagerFactory.Get().ManageCategories().GetAll().Where(C => C.ParentID != null).OrderBy(c => c.Name).ToList();
                    (new List<LookUpEdit> { lueItem }).SetupNamedObjectEdits("Name", "CategoryID");
                    lueItem.Properties.DataSource = items;
                    break;
                }
                case IngredientContainerTypes.Manufacturer:
                {
                    var items = BLManagerFactory.Get().ManageManufacturerViews().GetAll().OrderBy(c => c.Name).ToList();
                    (new List<LookUpEdit> { lueItem }).SetupNamedObjectEdits("Name", "ManufacturerID");
                    lueItem.Properties.DataSource = items;
                    break;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (null != OnDelete)
            {
                OnDelete( this, m_Ingredient );
            }
        }
    }
}
