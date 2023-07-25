using System;
using System.Security.Cryptography;
using System.Text;


namespace Jaxis.Utility.Encryption
{
	/// <summary>
	/// Summary description for BaseHash.
	/// </summary>
	public class BaseHash
	{
		public BaseHash()
		{
		}

		
		public string Process( bool _Encrypt, string _Text )
		{
			string rc = string.Empty;
			try
			{
				if( true == _Encrypt )
				{
					rc = Encrypt( _Text );
				}
				else
				{
					rc = Decrypt( _Text );
				}
			}
			catch
			{
				throw;
			}
			return rc;
		}
		
		public string Encrypt( string _ClearText )
		{
			string rc = null;
			try
			{
				MD5CryptoServiceProvider md5Hash  = new MD5CryptoServiceProvider( );
				byte[] hashedBytes; 
				UTF8Encoding encoder  = new UTF8Encoding( );
				hashedBytes = md5Hash.ComputeHash( encoder.GetBytes( _ClearText ) );
				rc = Convert.ToBase64String( hashedBytes );
			}
			catch
			{
				throw;
			}
			return rc;
		}
		
		public string Decrypt( string _EncryptedText )
		{
			string rc = string.Empty;
			try
			{

			}
			catch
			{
				throw;
			}
			return rc;
		}
	}
}
