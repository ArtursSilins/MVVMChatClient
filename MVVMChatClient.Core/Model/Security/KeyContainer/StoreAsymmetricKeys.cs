using MVVMChatClient.Core.Model.EncryptAndDecrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class StoreAsymmetricKeys
    {
        /// <summary>
        /// Create the RSA key pair, store them in container with given name.
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public static void GenKey_SaveInContainer(string containerName)
        {
            // Create the CspParameters object and set the key container
            // name used to store the RSA key pair.
            var parameters = new CspParameters
            {
                KeyContainerName = containerName
            };

            // Create a new instance of RSACryptoServiceProvider that accesses
            // the key container MyKeyContainerName.
            var rsa = new RSACryptoServiceProvider(parameters);
        }
        /// <summary>
        /// Get Privat Key from container.
        /// </summary>
        /// <param name="containerName"></param>
        public static RSAParameters GetPrivateKeyFromContainer(string containerName)
        {
            // Create the CspParameters object and set the key container
            // name used to store the RSA key pair.
            var parameters = new CspParameters
            {
                KeyContainerName = containerName
            };

            // Create a new instance of RSACryptoServiceProvider that accesses
            // the key container MyKeyContainerName.
            var rsa = new RSACryptoServiceProvider(parameters);

            var privKey = rsa.ExportParameters(true);

            return privKey;
        }
        /// <summary>
        /// Get Public Key from container.
        /// </summary>
        /// <param name="containerName"></param>
        public static RSAParameters GetPublicKeyFromContainer(string containerName)
        {
            // Create the CspParameters object and set the key container
            // name used to store the RSA key pair.
            var parameters = new CspParameters
            {
                KeyContainerName = containerName
            };

            // Create a new instance of RSACryptoServiceProvider that accesses
            // the key container MyKeyContainerName.
            var rsa = new RSACryptoServiceProvider(parameters);

            var pubKey = rsa.ExportParameters(false);

            return pubKey;
        }
        public static RSAParameters TestGetPrivKeyFromContainer(string containerName)
        {
            // Create the CspParameters object and set the key container
            // name used to store the RSA key pair.
            var parameters = new CspParameters
            {
                KeyContainerName = containerName
            };

            // Create a new instance of RSACryptoServiceProvider that accesses
            // the key container MyKeyContainerName.
            var rsa = new RSACryptoServiceProvider(parameters);

            var pubKey = rsa.ExportParameters(true);

            return pubKey;
        }
        public static void DeleteKeyFromContainer(string containerName)
        {
            // Create the CspParameters object and set the key container
            // name used to store the RSA key pair.
            var parameters = new CspParameters
            {
                KeyContainerName = containerName
            };

            // Create a new instance of RSACryptoServiceProvider that accesses
            // the key container.
            var rsa = new RSACryptoServiceProvider(parameters)
            {
                // Delete the key entry in the container.
                PersistKeyInCsp = false
            };

            // Call Clear to release resources and delete the key from the container.
            rsa.Clear();

        }
    }
}
