
using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class MessageList
    {
        public static ObservableCollection<IMessageContent> Items { get; set; } = new ObservableCollection<IMessageContent>();

    }
}
