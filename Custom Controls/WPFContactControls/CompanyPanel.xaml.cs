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
    /// Interaction logic for CompanyPanel.xaml
    /// </summary>
    public partial class CompanyPanel : UserControl
    {
        protected ICompany m_CompanyData;
        public ICompany CompanyData
        {
            get
            {
                m_CompanyData.CompanyName = txtCompanyName.Text;
                m_CompanyData.RelationshipType = (IRelationshipType)cmbRelationshipType.SelectedValue;
//                m_CompanyData.Addresses 
                return m_CompanyData;
            }
            set
            {
                m_CompanyData = value;
                txtCompanyName.Text = m_CompanyData.CompanyName;
                cmbRelationshipType.SelectedValue = m_CompanyData.RelationshipType;
            }
        }

        public CompanyPanel()
        {
            InitializeComponent();
        }
    }
}
