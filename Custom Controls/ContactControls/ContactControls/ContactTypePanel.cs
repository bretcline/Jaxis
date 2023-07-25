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
    public partial class ContactTypePanel : UserControl
    {
        #region Fields

        protected IContactType m_ContactTypeData;

        #endregion Fields

        #region Constructors

        public ContactTypePanel()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        public IContactType ContactTypeData
        {
            get
            {
                m_ContactTypeData.Name = txtName.Text;
                m_ContactTypeData.Description = txtDescription.Text;
                return m_ContactTypeData;
            }
            set
            {
                m_ContactTypeData = value;
                txtName.Text = m_ContactTypeData.Name;
                txtDescription.Text = m_ContactTypeData.Description;
            }
        }

        #endregion Properties
    }
}