﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Interfaces
{
    public interface ITcpEndPoint
    {
        IPEndPoint TcpEndPoint { get; set; }
    }
}
