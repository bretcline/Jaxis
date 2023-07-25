using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BeverageManagement.Forms;
using DevExpress.XtraEditors;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;
using Jaxis.Utility.Encryption;

namespace BeverageManagement
{
    public partial class frmLogin : frmScannerCommands
    {
        private IBLManagerFactory m_Factory = BLManagerFactory.Get( );

        public string UserName { get; set; }
        private bool KeyCapture { get; set; }

        public frmLogin( )
        {
            InitializeComponent( );
            m_ScannerCommands.CustomProcessor = AutoLogin;
            KeyCapture = false;
        }

        private void btnLogin_Click( object sender, EventArgs e )
        {
            Log.Debug("Log-in");

            //Log.Wrap( "Log-in", LogType.Debug, false, () =>
            {
                IBLUserSession userSession;
                string userName;
                try
                {
                    if (m_Factory.ManageUsers().Login(txtUserID.Text, txtPassword.Text, out userSession, out userName))
                    {
                        m_Factory.UserSession = userSession;
                        UserName = userName;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show(this, global::BeverageManagement.Properties.Resources.InvalidLogin, "",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(this, err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);                  
                }

                //return true;
            }//);
        }

        private bool PlusEntered = false;
        private bool DollarEntered = false;
        private string UserID = string.Empty;
        protected Stack<char> m_Stack = new Stack<char>();

        private bool AutoLogin(KeyPressEventArgs _keyChar)
        {
            bool rc = false;
            if (_keyChar.KeyChar == '$')
            {
                if (true == KeyCapture)
                {
                    var user = m_Factory.ManageUsers().GetAll().Where(u => u.UserName.Equals( UserID, StringComparison.CurrentCultureIgnoreCase ) ).FirstOrDefault();
                    if( null != user )
                    {
                        txtUserID.Text = user.UserName;
                        txtPassword.Text = Encryption.Decrypt(EncryptionType.BaseEncrypt, user.Password);
                    }
                }
                KeyCapture = !KeyCapture;
                rc = true;
            }
            else if( true == KeyCapture )
            {
                UserID += _keyChar.KeyChar;
                rc = true;
            }
            return rc;
        }
    }
}