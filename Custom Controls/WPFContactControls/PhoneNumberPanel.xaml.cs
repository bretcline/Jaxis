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
    /// Interaction logic for PhoneNumberPanel.xaml
    /// </summary>
    public partial class PhoneNumberPanel : UserControl
    {
        protected IPhoneNumber m_PhoneNumberData;
        public IPhoneNumber PhoneNumberData
        {
            get
            {
                m_PhoneNumberData.PhoneNumberType = (IPhoneNumberType)cmbType.SelectedValue;
                m_PhoneNumberData.AreaCode = txtAreaCode.Text;
                m_PhoneNumberData.Prefix = txtPrefix.Text;
                m_PhoneNumberData.Suffix = txtSuffix.Text;
                m_PhoneNumberData.Extension = txtExtension.Text;
                return m_PhoneNumberData;
            }
            set
            {
                m_PhoneNumberData = value;
                txtAreaCode.Text = m_PhoneNumberData.AreaCode;
                txtPrefix.Text = m_PhoneNumberData.Prefix;
                txtSuffix.Text = m_PhoneNumberData.Suffix;
                txtExtension.Text = m_PhoneNumberData.Extension;
                cmbType.SelectedValue = m_PhoneNumberData.PhoneNumberType;
            }
        }

        public PhoneNumberPanel()
        {
            InitializeComponent();
        }
    }
}
