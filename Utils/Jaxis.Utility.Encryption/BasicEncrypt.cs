using System;
using System.Security.Cryptography;
using System.Text;


namespace Jaxis.Utility.Encryption
{
	/// <summary>
	/// Summary description for BaseEncrypt.
	/// </summary>
	public class BaseEncrypt
	{
		public BaseEncrypt()
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
				hashedBytes = md5Hash.ComputeHash( encoder.GetBytes( "PrOfOrMaNcE" ) );

				byte[] Password = encoder.GetBytes( _ClearText );
				byte[] NewPassword = new byte[Password.Length];
				int j = 0;
				for( int i = 0; i < Password.Length; ++i, ++j )
				{
					if( j == hashedBytes.Length )
					{
						j = 0;
					}
					NewPassword[i] = Convert.ToByte(Password[i] ^ hashedBytes[j]);
				}
				rc = Convert.ToBase64String( NewPassword );
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
				MD5CryptoServiceProvider md5Hash  = new MD5CryptoServiceProvider( );
				byte[] hashedBytes; 
				UTF8Encoding encoder  = new UTF8Encoding( );

				byte[] Password = Convert.FromBase64String( _EncryptedText );

				hashedBytes = md5Hash.ComputeHash( encoder.GetBytes( "PrOfOrMaNcE" ) );

				byte[] NewPassword = new byte[Password.Length];
				int j = 0;
				for( int i = 0; i < Password.Length; ++i, ++j )
				{
					if( j == hashedBytes.Length )
					{
						j = 0;
					}
					NewPassword[i] = Convert.ToByte(Password[i] ^ hashedBytes[j]);
				}

				rc = encoder.GetString( NewPassword );

			}
			catch
			{
				throw;
			}
			return rc;
		}
	}
}
