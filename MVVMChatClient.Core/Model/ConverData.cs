﻿using MVVMChatClient.Core.Interfaces;
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
            //messageEncript

            string DataToSend = JsonConvert.SerializeObject(data);
            byte[] DataInBytes = Encoding.UTF8.GetBytes(DataEncryption.Encrypt(DataToSend));

            return DataInBytes;
        }       
        public static T ToReceiv<T>(string textFromServer)
        {
            T objectFromText = JsonConvert.DeserializeObject<T>(DataEncryption.Decrypt(textFromServer));

            //messageDecrypt

            return objectFromText;
        }
    }
}
