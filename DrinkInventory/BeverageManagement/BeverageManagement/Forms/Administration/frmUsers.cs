using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BeverageManagement.Properties;
using DevExpress.XtraEditors;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;
using Jaxis.Utility.Encryption;

namespace BeverageManagement.Forms.Administration
{
    public partial class frmUsers : DevExpress.XtraEditors.XtraForm
    {
        private bool m_NewUser = false;
        private IBLUser m_User = null;

        public frmUsers( )
        {
            InitializeComponent( );
        }

        private void frmUsers_Load( object sender, EventArgs e )
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            lstUsers.Items.Clear();
            var users = new BindingList<IBLUser>( BLManagerFactory.Get( ).ManageUsers( ).GetAll( ).ToList() );
            lstUsers.DataSource = users;

            lstUsers.DisplayMember = "ProperName";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                bool rc = true;
                m_User.UserName = txtUserName.Text;
                m_User.ProperName = txtProperName.Text;

                rc = VerifyPassword( );
                if( true == rc )
                {
                    m_User.Password = Encryption.Encrypt(EncryptionType.BaseEncrypt, txtPassword.Text); ;
                    BLManagerFactory.Get().ManageUsers().Save(m_User);

                    LoadUsers();
                }
                m_NewUser = false;
                return rc;
            });
        }

        private bool VerifyPassword( )
        {
            return Log.MsgWrap(false, () =>
            {
                bool rc = true;

                if( true == m_NewUser &&
                    !string.IsNullOrWhiteSpace( txtPassword.Text ) && 
                    !string.IsNullOrWhiteSpace( txtPasswordVerify.Text ) )
                {
                    if( txtPassword.Text != txtPasswordVerify.Text )
                    {
                        rc = false;
                        MessageBox.Show(Resources.PasswordsDontMatch, "", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    }
                }
                else if(  !string.IsNullOrWhiteSpace( txtPasswordOriginal.Text ) && 
                     !string.IsNullOrWhiteSpace( txtPassword.Text ) && 
                     !string.IsNullOrWhiteSpace( txtPasswordVerify.Text ) )
                {
                    string Password = Encryption.Encrypt(EncryptionType.BaseEncrypt, txtPasswordOriginal.Text);

                    if( Password != m_User.Password ||
                        txtPassword.Text != txtPasswordVerify.Text )
                    {
                        rc = false;
                        MessageBox.Show(Resources.PasswordsDontMatch, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return rc;
            });
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                m_NewUser = true;
                SetUser( BLManagerFactory.Get().ManageUsers().Create() );
                return true;
            });
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (m_User.UserID != new Guid("83AA6585-D541-46B0-9CD0-6B91C47C6BED") && m_User.UserID != Guid.Empty)
            {
                var sessionManager = BLManagerFactory.Get().ManageUserSessions();
                var userSessions = sessionManager.GetAll().Where(U => U.UserID == m_User.UserID);
                foreach (var blUserSession in userSessions)
                {
                    sessionManager.Delete(blUserSession);
                }

                BLManagerFactory.Get().ManageUsers().Delete(m_User);
                LoadUsers();
                lstUsers.SelectedIndex = 0;

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetUser(lstUsers.SelectedItem as IBLUser);
        }

        private void SetUser(IBLUser _blUser)
        {
            Log.MsgWrap(false, () =>
            {
                if (null != _blUser)
                {
                    m_User = _blUser;
                    txtUserName.Text = m_User.UserName;
                    txtProperName.Text = m_User.ProperName;
                }
                return true;
            });
        }
    }
}