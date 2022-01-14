using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class TcpSocket 
    {
        public static Socket tcpSocket { get; set; }
    }
}
