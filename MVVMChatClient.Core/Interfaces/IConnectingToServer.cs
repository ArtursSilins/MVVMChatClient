using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Interfaces
{
    public interface IChatting
    {
        void Receiving(IWindowsViewModel windowsViewModel, ILoginViewModel loginViewModel, IMessageContent messageContent,
            ITcpEndPoint tcpEndPoint, IJsonContainer container);
        
    }
}
