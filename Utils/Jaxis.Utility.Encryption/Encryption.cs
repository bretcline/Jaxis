using System;
using System.Security.Cryptography;
using System.Text;

namespace Jaxis.Utility.Encryption
{
	public enum EncryptionType 
	{
		Hash			= 1,
		BaseEncrypt		= 2
	}
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Encryption
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			//
			// TODO: Add code to start application here
			//

			UTF8Encoding encoder  = new UTF8Encoding( );

			string test = "This is a test a test a test a test a test a test";
			string result = null;
			string Decrypt = string.Empty;
			Encryption tmp = new Encryption( );
			result = Encryption.Encrypt( EncryptionType.BaseEncrypt, test );
			Decrypt = Encryption.Decrypt( EncryptionType.BaseEncrypt, result );
		
			Console.WriteLine( test );
			Console.WriteLine( result );
			Console.WriteLine( Decrypt );


		}

		public Encryption( )
		{

		}

		static public string Encrypt( EncryptionType _Type, string _ClearText )
		{
			string rc = string.Empty;
			try
			{
				rc = Process( _Type, true, _ClearText );
			}
			catch
			{
				throw;
			}
			return rc;
		}

		static public string Decrypt( EncryptionType _Type, string _EncryptedText )
		{
			string rc = string.Empty;
			try
			{
				rc = Process( _Type, false, _EncryptedText );
			}
			catch
			{
				throw;
			}
			return rc;
		}


		static protected string Process( EncryptionType _Type, bool _Encrypt, string _Text )
		{
			string rc = string.Empty;
			try
			{
				switch( _Type )
				{
					case EncryptionType.BaseEncrypt:
					{
						BaseEncrypt Worker = new BaseEncrypt( );
						rc = Worker.Process( _Encrypt, _Text );
						break;
					}
					case EncryptionType.Hash:
					{
						BaseHash Worker = new BaseHash( );
						rc = Worker.Process( _Encrypt, _Text );
						break;
					}
					default:
					{
						break;
					}
				}
			}
			catch
			{
				throw;
			}
			return rc;
		}
	}
}
