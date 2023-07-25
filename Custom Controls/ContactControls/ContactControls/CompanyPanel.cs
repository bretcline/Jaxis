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
    public partial class CompanyPanel : UserControl
    {
        #region Fields

        protected ICompany m_CompanyData;

        #endregion Fields

        #region Constructors

        public CompanyPanel()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        public ICompany CompanyData
        {
            get
            {
                m_CompanyData.CompanyName = txtCompanyName.Text;

                ICompanyType CT = cmbRelationshipType.SelectedValue as ICompanyType;
                if( null != CT )
                {
                    m_CompanyData.CompanyTypeID = CT.CompanyTypeID;
                }
                //                m_CompanyData.Addresses
                return m_CompanyData;
            }
            set
            {
                m_CompanyData = value;
                txtCompanyName.Text = m_CompanyData.CompanyName;
                cmbRelationshipType.SelectedValue = m_CompanyData.CompanyTypeID;
            }
        }

        #endregion Properties
    }
}