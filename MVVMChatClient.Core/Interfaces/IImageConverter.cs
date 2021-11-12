﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Interfaces
{
    public interface IImageConverter
    {
        byte[] ImageToByte(string path);
        void ByteToImage(byte[] array, string id);
    }
}
