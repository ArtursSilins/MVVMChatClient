using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class MessageEncryption
    {
        private const string Hash = "ChatApp";

        public static string Encrypt(string message)
        {
            string encryptedMessage;

            byte[] data = Encoding.UTF8.GetBytes(message);

            using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] key = md5.ComputeHash(Encoding.UTF8.GetBytes(Hash));

                using(TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider()
                { Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDES.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);

                    return encryptedMessage = Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }
        public static string Decrypt(string message)
        {
            string decryptedMessage;

            byte[] data = Convert.FromBase64String(message);

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] key = md5.ComputeHash(Encoding.UTF8.GetBytes(Hash));

                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider()
                { Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDES.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);

                    return decryptedMessage = Encoding.UTF8.GetString(results);
                }
            }
        }
    }
}
