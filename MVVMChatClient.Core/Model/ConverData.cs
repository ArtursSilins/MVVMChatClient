using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model.EncryptAndDecrypt;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class ConverData
    {
        public static byte[] ToSend(object data)
        {
            string DataToSend = JsonConvert.SerializeObject(data);
            byte[] DataInBytes = Encoding.UTF8.GetBytes(SymmetricEncryption.EncryptDataToBytes(DataToSend)/*DataEncryption.Encrypt(DataToSend)*/);

            return DataInBytes;
        }       
        public static T ToReceiv<T>(string textFromServer)
        {
            T objectFromText = JsonConvert.DeserializeObject<T>(DataEncryption.Decrypt(textFromServer));

            return objectFromText;
        }
        /// <summary>
        /// Send public key to server in first time connecton.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] SendPubKey(object data)
        {
            string DataToSend = JsonConvert.SerializeObject(data);

            byte[] DataInBytes = Encoding.UTF8.GetBytes(DataToSend);

            return DataInBytes;
        }
        public static T ReceivKeys<T>(string textFromServer)
        {
            T objectFromText = JsonConvert.DeserializeObject<T>(textFromServer);

            return objectFromText;
        }
        //public static T ReceivKeys<T>(string textFromServer)
        //{
        //    T objectFromText = JsonConvert.DeserializeObject<T>(AsymmetricEncryption.DecryptWithPrivateKey(textFromServer));

        //    return objectFromText;
        //}
    }
}
