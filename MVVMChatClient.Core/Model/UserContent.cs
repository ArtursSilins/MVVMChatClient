﻿using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public class UserContent : IUserContent
    {
        public string UserName { get; set; }
        public string UserPicture { get; set; }
        public int PersonId { get; set; }
        public int FontSize { get => Size(UserName.Length); }      

        public int Size(int length)
        {
            int size;

            //if (length > 6)
            //    return size = 15;
            //else
                return size = 25;
        }
    }
}
