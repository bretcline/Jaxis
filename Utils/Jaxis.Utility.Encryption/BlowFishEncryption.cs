using System;
using System.Text;
using BlowfishNET;
using System.Security.Cryptography;

namespace Jaxis.Utility.Encryption
{
    public class BlowFishEncryption
    {
        private static string m_PW = "JaxisBevMet6765564232";

        public static byte[] Encrypt(byte[] _Buffer, out byte[] _iv)
        {
            _iv = new byte[BlowfishCFB.BLOCK_SIZE];
            (new RNGCryptoServiceProvider()).GetBytes(_iv);

            byte[] key = Encoding.UTF8.GetBytes(m_PW);

            BlowfishCFB bff = new BlowfishCFB(key, 0, key.Length);
            bff.IV = _iv;

            byte[] cipherText = new byte[_Buffer.Length];

            bff.Encrypt(_Buffer, 0, cipherText, 0, _Buffer.Length);

            bff.Invalidate();

            return cipherText;
        }

        public static byte[] Decrypt(byte[] _Buffer, byte[] _iv)
        {
            byte[] key = Encoding.UTF8.GetBytes(m_PW);

            BlowfishCFB bff = new BlowfishCFB(key, 0, key.Length);
            bff.SetIV(_iv, 0);

            byte[] decryptedText = new byte[_Buffer.Length];
            bff.Decrypt(_Buffer, 0, decryptedText, 0, _Buffer.Length);

            bff.Invalidate();
            return decryptedText;
        }

        public static string Encrypt( string _ClearString )
        {
            string rc = string.Empty;

            byte[] data = Convert.FromBase64String(_ClearString);

            return rc;
        }

        public static string Decrypt( string _EncryptedString )
        {
            string rc = string.Empty;


            return rc;
        }

    }
}
