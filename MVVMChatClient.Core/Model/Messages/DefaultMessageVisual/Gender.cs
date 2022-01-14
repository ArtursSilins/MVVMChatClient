using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class Gender
    {
        public const string Male = "/View/Images/Male.jpg";
        public const string Female = "/View/Images/Female.jpg";
        /// <summary>
        /// If return 1 = Male if 2 = Female. 
        /// </summary>
        /// <param name="male"></param>
        /// <returns></returns>
        public static int Check(bool male)
        {
            return male ? 1 : 2;
        }
    }
}
