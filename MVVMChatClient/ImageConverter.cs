using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient
{
    public class ImageConverter : Core.Interfaces.IImageConverter
    {
        public byte[] ImageToByte(string path)
        {

            System.Drawing.Image img = System.Drawing.Image.FromFile(path);
            byte[] arr;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = ms.ToArray();
            }
            return arr;
        }
        /// <summary>
        /// Convert bytes to image file and set Id as a file name.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="id"></param>
        public void ByteToImage(byte[] array, string id)
        {
            using (Image image = Image.FromStream(new MemoryStream(array)))
            {
                try
                {
                    CreateFolder();

                    image.Save(/*@"C:\Users\X\Downloads\ChatData\ChatImage"*/Path.GetTempPath() + "ChatImage" +
                        id + ".jpg", ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {

                    
                }
                
            }
        }
        private void CreateFolder()
        {
            System.IO.Directory.CreateDirectory(@"C:\Users\X\Downloads\ChatData");
        }
    }
}
