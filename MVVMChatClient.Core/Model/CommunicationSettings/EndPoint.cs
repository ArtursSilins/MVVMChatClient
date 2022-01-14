using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public class EndPoint : ITcpEndPoint
    {
        const int port = 8080;

        public IPEndPoint TcpEndPoint { get; set; } = new IPEndPoint(IPAddress.Loopback/*IPAddress.Parse(ip)*/, port);
    }
}
