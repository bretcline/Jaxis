using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using SecureRisk.Data.Interfaces;

namespace ContactControls
{
    public partial class AddressPanel : UserControl
    {
        #region Fields

        protected IAddress m_AddressData;

        #endregion Fields

        #region Constructors

        public AddressPanel()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        public IAddress AddressData
        {
            get
            {
                if( null != m_AddressData )
                {
                    IAddressType AT = cmbAddressType.SelectedItem as IAddressType;
                    if( null != AT )
                    {
                        m_AddressData.AddressTypeID = AT.AddressTypeID;
                    }
                    m_AddressData.Street = txtStreet.Text;
                    m_AddressData.City = txtCity.Text;
                    m_AddressData.State = txtState.Text;
                    m_AddressData.Zip = txtZip.Text;
                    if( null != cmbCountry.SelectedItem )
                    {
                        m_AddressData.Country = cmbCountry.SelectedText;
                    }
                }
                return m_AddressData;
            }
            set
            {
                m_AddressData = value;
                if( null != m_AddressData )
                {
                    txtStreet.Text = m_AddressData.Street;
                    txtCity.Text = m_AddressData.City;
                    txtState.Text = m_AddressData.State;
                    txtZip.Text = m_AddressData.Zip;
                    cmbCountry.SelectedItem = m_AddressData.Country;
                    cmbAddressType.SelectedItem = m_AddressData.AddressTypeID;
                }
            }
        }

        public List<IAddressType> AddressTypes
        {
            set
            {
                if( null != value )
                {
            //                    cmbAddressType.DataBindings.Add( "EditValue", value, "AddressTypeName" );
                    cmbAddressType.Properties.Items.AddRange( value );
                }
            }
        }

        #endregion Properties
    }
}