using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model.EncryptAndDecrypt
{
    public static class KeySplitter
    {
        /// <summary>
        /// Retuns smoller parts of key for RSA private key decryption (max decryption size 117 byte[])
        /// </summary>
        /// <param name="key">Key to split.</param>
        /// <param name="partCount">In how much patrs key will be split.</param>
        /// <param name="partToReturn">Which patr of split to return</param>
        /// <returns></returns>
        public static string ReturnPart(string key, int partCount, int partToReturn)
        {
            string keyPart = "";

            int partSize = 0;
            int startIndex = 0;
            int count = 0;

            if (key.Length % partCount != 0 && partCount == partToReturn)
            {
                partSize = (key.Length / partCount) + 1;

                count = key.Length - partSize;

                keyPart = key.Remove(0, count);
            }
            else if (partToReturn == 1 && key.Length % partCount != 0)
            {
                partSize = key.Length / partCount;

                startIndex = partSize;

                count = partSize * (partCount - partToReturn);

                keyPart = key.Remove(startIndex, count + 1);
            }
            else if (partToReturn == 1)
            {
                partSize = key.Length / partCount;

                startIndex = partSize;

                count = partSize * (partCount - partToReturn);

                keyPart = key.Remove(startIndex, count);
            }
            else if (partCount == partToReturn)
            {
                partSize = key.Length / partCount;

                count = key.Length - partSize;

                keyPart = key.Remove(0, count);
            }
            else if (key.Length % partCount != 0)
            {
                partSize = key.Length / partCount;

                startIndex = key.Length - ((partCount - partToReturn) * partSize);

                count = (partCount - partToReturn) * partSize;

                string holder = key.Remove(startIndex - 1, count + 1);

                count = (partToReturn - 1) * partSize;

                keyPart = holder.Remove(0, count);
            }
            else
            {
                partSize = key.Length / partCount;

                startIndex = key.Length - ((partCount - partToReturn) * partSize);

                count = (partCount - partToReturn) * partSize;

                string holder = key.Remove(startIndex, count);

                count = (partToReturn - 1) * partSize;

                keyPart = holder.Remove(0, count);
            }

            return keyPart;
        }
    }
}
