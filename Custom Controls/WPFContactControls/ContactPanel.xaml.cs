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
    /// Interaction logic for ContactPanel.xaml
    /// </summary>
    public partial class ContactPanel : UserControl
    {
        protected IContact m_ContactData;
        public IContact ContactData
        {
            get
            {
                m_ContactData.FirstName = txtFirstName.Text;
                m_ContactData.MiddleName = txtMiddleName.Text;
                m_ContactData.LastName = txtLastName.Text;
                m_ContactData.CompanyName = txtCompanyName.Text;
                return m_ContactData;
            }
            set
            {
                m_ContactData = value;
                txtFirstName.Text = m_ContactData.FirstName;
                txtMiddleName.Text = m_ContactData.MiddleName;
                txtLastName.Text = m_ContactData.LastName;
                txtCompanyName.Text = m_ContactData.CompanyName;
            }
        }

        public ContactPanel()
        {
            InitializeComponent();
        }
    }
}
