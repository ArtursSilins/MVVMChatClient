using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model.EncryptAndDecrypt
{
    public class AsymmetricEncryption
    {
        /// <summary>
        /// Create the RSA key pair, store them in container with given name.
        /// </summary>
        /// <param name="containerName"></param>
        public static void CreateKey(string containerName)
        {
            StoreAsymmetricKeys.GenKey_SaveInContainer(containerName);
        }
        /// <summary>
        /// Get the public key into a string representation
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public static string PubKeyString(string containerName)
        {
            //we need some buffer
            var sw = new System.IO.StringWriter();
            //we need a serializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //serialize the key into the stream
            xs.Serialize(sw, StoreAsymmetricKeys.GetPublicKeyFromContainer(containerName));
            //get the string from the stream
            string pubKeyString = sw.ToString();

            return pubKeyString;
        }
        
        public static RSAParameters StringToPubKey(string pubKeyString)
        {
            //get a stream from the string
            var sr = new System.IO.StringReader(pubKeyString);
            //we need a deserializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //get the object back from the stream
            return (RSAParameters)xs.Deserialize(sr);
        }
        public static byte[] EncryptForPrivateKey(RSAParameters pubKey, object data)
        {
            var csp = new RSACryptoServiceProvider();
            csp.ImportParameters(pubKey);

            string DataToSend = JsonConvert.SerializeObject(data);

            byte[] DataInBytes = Convert.FromBase64String(DataToSend);

            var bytesCypherText = csp.Encrypt(DataInBytes, false);

            return bytesCypherText;

        }
        /// <summary>
        /// Decrypt message from server with private key.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string DecryptWithPrivateKey(string text)
        {
            using (var csp = new RSACryptoServiceProvider())
            {
                csp.ImportParameters(StoreAsymmetricKeys.GetPrivateKeyFromContainer("Client"));
                try
                {
                    byte[] bytesCypherText = Convert.FromBase64String(text);

                    byte[] bytesPlainTextData = csp.Decrypt(bytesCypherText, false);

                    string plainTextData = Encoding.UTF8.GetString(bytesPlainTextData);

                    return plainTextData;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        ////conversion for the private key is no black magic either ... omitted

        ////we have a public key ... let's get a new csp and load that key
        //csp = new RSACryptoServiceProvider();
        //csp.ImportParameters(pubKey);

        ////we need some data to encrypt
        //var plainTextData = "foobar";

        ////for encryption, always handle bytes...
        //var bytesPlainTextData = System.Text.Encoding.Unicode.GetBytes(plainTextData);

        ////apply pkcs#1.5 padding and encrypt our data 
        //var bytesCypherText = csp.Encrypt(bytesPlainTextData, false);

        ////we might want a string representation of our cypher text... base64 will do
        //var cypherText = Convert.ToBase64String(bytesCypherText);


        ///*
        // * some transmission / storage / retrieval
        // * 
        // * and we want to decrypt our cypherText
        // */

        ////first, get our bytes back from the base64 string ...
        //bytesCypherText = Convert.FromBase64String(cypherText);

        ////we want to decrypt, therefore we need a csp and load our private key
        //csp = new RSACryptoServiceProvider();
        //csp.ImportParameters(privKey);

        ////decrypt and strip pkcs#1.5 padding
        //bytesPlainTextData = csp.Decrypt(bytesCypherText, false);

        ////get our original plainText back...
        //plainTextData = System.Text.Encoding.Unicode.GetString(bytesPlainTextData);
    }
}
