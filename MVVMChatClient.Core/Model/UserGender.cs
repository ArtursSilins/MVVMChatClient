﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class UserGender
    {
        public static string YourGender { get; set; }
        public static string GetUserGender()
        {
            return YourGender;
        }
    }
}
