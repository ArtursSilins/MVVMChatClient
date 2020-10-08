using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class ConvertImage
    {
        public static IImageConverter imageConverter { get; set; }

        public static byte[] ToByte(string path)
        {
            return imageConverter.ImageToByte(path);
        }
        public static void ByteToImage(byte[] array, int id)
        {
            imageConverter.ByteToImage(array, id);
        }
    }
}
