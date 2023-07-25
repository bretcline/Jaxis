using System;
using System.Collections.Generic;
using Jaxis.Utility.Encryption;
using System.Linq;

namespace Jaxis.Inventory.Data
{
    public class UserDataManager : DataManager<IUser, User>, IUserDataManager
    {
        #region IDataManager<IUser> Members


        public IQueryable<IUser> GetAll( )
        {
            return User.All();
        }

        public IUser Get( Guid ID )
        {
            return User.GetByID(ID);
        }

        #endregion

        #region IUserManager Members

        //public bool Login( string _UserID, string _Password, out IUserSession _SessionID )
        //{
        //    bool rc = false;
        //    _SessionID = null;
        //    try
        //    {
        //        string Password = Encryption.Encrypt(EncryptionType.BaseEncrypt, _Password);

        //        IUser user = DataManagerFactory.Get().ManageUsers().GetAll().Where(
        //            U => U.UserName.Equals(_UserID) && U.Password.Equals(Password)).FirstOrDefault();

        //        if( null != user )
        //        {
        //            _SessionID = DataManagerFactory.Get( ).ManageUserSessions( ).Create( );
        //            _SessionID.UserID = user.UserID;
        //            _SessionID.Save( );
        //            rc = true;
        //        }
        //    }
        //    catch
        //    {
                
        //        throw;
        //    }
        //    return rc;
        //}

        #endregion
    }
}