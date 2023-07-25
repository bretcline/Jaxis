using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SecureRisk.Data.Interfaces;

namespace ContactControls
{
    public partial class ContactPanel : UserControl
    {
        #region Fields

        protected IContact m_ContactData;

        #endregion Fields

        #region Constructors

        public ContactPanel()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        public IContact ContactData
        {
            get
            {
                m_ContactData.FirstName = txtFirstName.Text;
                m_ContactData.MiddleName = txtMiddleName.Text;
                m_ContactData.LastName = txtLastName.Text;
                m_ContactData.Company.CompanyName = txtCompanyName.Text;
                return m_ContactData;
            }
            set
            {
                m_ContactData = value;
                txtFirstName.Text = m_ContactData.FirstName;
                txtMiddleName.Text = m_ContactData.MiddleName;
                txtLastName.Text = m_ContactData.LastName;
                txtCompanyName.Text = m_ContactData.Company.CompanyName;
            }
        }

        #endregion Properties
    }
}