using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;
using Jaxis.Utility.Encryption;

namespace Jaxis.Inventory.Data
{
    public class UserBLManager : BLManager<IUser, IBLUser>, IUserBLManager
    {
        #region IUserBLManager Members

        public bool Login( string _UserID, string _Password, out IBLUserSession _SessionID, out string UserName )
        {
            var factory = BLManagerFactory.Get( );
            bool rc = false;
            _SessionID = null;
            UserName = null;
            try
            {
                var password = Encryption.Encrypt(EncryptionType.BaseEncrypt, _Password);

                var objects = DataManagerFactory.Get( ).Manage<IUser>( ).GetAll( ).Where(
                    U => U.UserName.Equals(_UserID) && U.Password.Equals(password) && U.Active == true);

                IUser user = objects.FirstOrDefault();
                if( null != user )
                {
                    _SessionID = factory.ManageUserSessions( ).Create( );
                    _SessionID.UserID = user.UserID;
                    factory.ManageUserSessions( ).Save( _SessionID );
                    UserName = user.ProperName;
                    rc = true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                throw;
            }
            return rc;
        }

        public void LogOut( IBLUserSession _session )
        {
            try
            {
                if (null != _session)
                {
                    _session.EndTime = DateTime.Now;
                    BLManagerFactory.Get().ManageUserSessions().Save(_session);
                }
            }
            catch
            {
            }
        }

        #endregion
    }
}
