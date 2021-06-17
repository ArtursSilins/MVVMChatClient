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
        void Receiving(IWindowsViewModel windowsViewModel, ISignInViewModel loginViewModel, IMessageContent messageContent,
            ITcpEndPoint tcpEndPoint, IJsonContainer container);
        void Receiving2(IWindowsViewModel windowsViewModel, IMessageContent messageContent, IJsonContainer container);
    }
}
