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
        
        public void ByteToImage(byte[] array, int id)
        {
            using (Image image = Image.FromStream(new MemoryStream(array)))
            {
                try
                {
                    image.Save(@"C:\Users\X\Downloads\ChatImage" + id + ".jpg", ImageFormat.Jpeg);  // Or Png
                }
                catch (Exception ex)
                {

                    
                }
                
            }
        }
    }
}
