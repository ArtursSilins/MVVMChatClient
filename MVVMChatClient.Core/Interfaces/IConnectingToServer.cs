using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Interfaces
{
    public interface IChatting
    {
        void Receiving(IWindowsViewModel windowsViewModel,
            IMessageContent messageContent,
            IJsonContainer container,
            IJsonMessageContainer jsonMessageContainer);
    }
}
