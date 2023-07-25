using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ContactDataInterfaces;

namespace WPFContactControls
{
    /// <summary>
    /// Interaction logic for AddressPanel.xaml
    /// </summary>
    public partial class AddressPanel : UserControl
    {
        protected IAddress m_AddressData;
        public IAddress AddressData
        {
            get
            {
                m_AddressData.Street = txtStreet.Text;
                m_AddressData.City = txtCity.Text;
                m_AddressData.State = txtState.Text;
                m_AddressData.Zip = txtZip.Text;
                m_AddressData.Country = cmbCountry.SelectedValue.ToString();
                return m_AddressData;
            }
            set
            {
                m_AddressData = value;
                txtStreet.Text = m_AddressData.Street;
                txtCity.Text = m_AddressData.City;
                txtState.Text = m_AddressData.State;
                txtZip.Text = m_AddressData.Zip;
                cmbCountry.SelectedValue = m_AddressData.Country;
            }
        }

        public AddressPanel()
        {
            InitializeComponent();
        }
    }
}
