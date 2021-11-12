using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model.EncryptAndDecrypt
{
    public static class MessageTextEncryption
    {
        private static RSAParameters _privateKey;
        private static RSAParameters _publicKey;
        private static string _publicKeyString;

        public static void RSACreate()
        {
            var rsa = new RSACryptoServiceProvider();

            //put key in keyContainer also retrive etc.
            _privateKey = rsa.ExportParameters(true);

            _publicKey = rsa.ExportParameters(false);
            _publicKeyString = rsa.ToXmlString(false);
        }
        public static string Encrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(_publicKey);

            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(data);
            var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false);
           
            return Convert.ToBase64String(encryptedByteArray);
        }
        public static string TestEncrypt(string data, RSAParameters pubKey)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(pubKey);

            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(data);
            var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false);

            return Convert.ToBase64String(encryptedByteArray);
        }

        public static string Decrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(_privateKey);

            byte[] original = Convert.FromBase64String(data);
            byte[] original1 = rsa.Decrypt(original, false);

            return Encoding.UTF8.GetString(original1);
        }
        public static string TestDecrypt(string data )
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(StoreAsymmetricKeys.GetPrivateKeyFromContainer("Test"));

            byte[] original = Convert.FromBase64String(data);
            byte[] original1 = rsa.Decrypt(original, false);

            return Encoding.UTF8.GetString(original1);
        }
    }
}
