using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using SecureRisk.Data.Interfaces;

namespace ContactControls
{
    public partial class PhoneNumberPanel : UserControl
    {
        #region Fields

        protected IPhoneNumber m_PhoneNumberData;

        #endregion Fields

        #region Constructors

        public PhoneNumberPanel()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        public IPhoneNumber PhoneNumberData
        {
            get
            {
                if( null != m_PhoneNumberData )
                {
                    IPhoneNumberType PNT = cmbType.SelectedItem as IPhoneNumberType;
                    if( null != PNT )
                    {
                        m_PhoneNumberData.PhoneNumberTypeID = PNT.ElementID;
                    }
                    if( 10 == txtPhoneNumber.Text.Length )
                    {
                        m_PhoneNumberData.AreaCode = txtPhoneNumber.Text.Substring( 0, 3 );
                        m_PhoneNumberData.Prefix = txtPhoneNumber.Text.Substring( 3, 3 );
                        m_PhoneNumberData.Suffix = txtPhoneNumber.Text.Substring( 6, 4 );
                    }
                    m_PhoneNumberData.Extension = mtxtExtension.Text;
                }
                return m_PhoneNumberData;
            }
            set
            {
                m_PhoneNumberData = value;
                if( null != m_PhoneNumberData )
                {
                    txtPhoneNumber.Text = String.Format( "{0}-{1}-{2}", m_PhoneNumberData.AreaCode, m_PhoneNumberData.Prefix, m_PhoneNumberData.Suffix );
                    mtxtExtension.Text = m_PhoneNumberData.Extension;
                    cmbType.SelectedItem = m_PhoneNumberData.PhoneNumberTypeID;
                }
            }
        }

        public List<IPhoneNumberType> PhoneNumberTypes
        {
            set
            {
                if( null != value )
                {
                    cmbType.Properties.Items.AddRange( value );
                }
            }
        }

        #endregion Properties
    }
}